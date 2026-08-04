using System;
using System.Collections.Generic;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace SarahsHouseI18n;

/// <summary>
/// Translate text on setters/OnEnable without recursive Awake hooks.
/// </summary>
[HarmonyPatch]
internal static class TextHooks
{
    private static string _skipPatternsRaw;
    private static List<string> _skipPatterns = new();
    [ThreadStatic] private static int _depth;

    public static void PatchDynamic(Harmony harmony)
    {
        TryDynamicPatch(harmony, typeof(TMP_Text), "SetText",
            new[] { typeof(string), typeof(bool) },
            prefixName: nameof(Prefix_SetText));

        // OnEnable postfix only on concrete text types (no Awake).
        TryDynamicPatch(harmony, typeof(TMP_Text), "OnEnable",
            null, postfixName: nameof(Postfix_TmpOnEnable));
        TryDynamicPatch(harmony, typeof(UnityEngine.UI.Text), "OnEnable",
            null, postfixName: nameof(Postfix_UiOnEnable));
    }

    private static void TryDynamicPatch(
        Harmony harmony,
        Type type,
        string method,
        Type[] sig,
        string prefixName = null,
        string postfixName = null)
    {
        try
        {
            var m = sig != null
                ? AccessTools.Method(type, method, sig)
                : AccessTools.Method(type, method);
            if (m == null)
            {
                Plugin.Log.LogWarning($"{type.Name}.{method} not found — skipping hook");
                return;
            }
            // Skip base UIBehaviour methods.
            if (m.DeclaringType != null &&
                m.DeclaringType.Name == "UIBehaviour")
            {
                Plugin.Log.LogWarning(
                    $"Refusing hook {type.Name}.{method} — resolved to UIBehaviour (would SO)");
                return;
            }
            HarmonyMethod prefix = null, postfix = null;
            if (prefixName != null)
                prefix = new HarmonyMethod(AccessTools.Method(typeof(TextHooks), prefixName));
            if (postfixName != null)
                postfix = new HarmonyMethod(AccessTools.Method(typeof(TextHooks), postfixName));
            harmony.Patch(m, prefix: prefix, postfix: postfix);
            Plugin.Log.LogInfo(
                $"Hooked {type.Name}.{method} (decl={m.DeclaringType?.Name})" +
                (prefix != null ? " [prefix]" : "") +
                (postfix != null ? " [postfix]" : ""));
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"Hook {type.Name}.{method} failed: {e.Message}");
        }
    }

    public static void Prefix_SetText(TMP_Text __instance, ref string sourceText, bool syncTextInputBox)
    {
        if (_depth > 0) return;
        TryTranslateRef(ref sourceText, __instance, "SetText");
    }

    [HarmonyPatch(typeof(TMP_Text), nameof(TMP_Text.text), MethodType.Setter)]
    [HarmonyPrefix]
    public static void Prefix_TmpTextSetter(TMP_Text __instance, ref string value)
    {
        if (_depth > 0) return;
        TryTranslateRef(ref value, __instance, "TMP.text");
    }

    [HarmonyPatch(typeof(UnityEngine.UI.Text), nameof(UnityEngine.UI.Text.text), MethodType.Setter)]
    [HarmonyPrefix]
    public static void Prefix_UiTextSetter(UnityEngine.UI.Text __instance, ref string value)
    {
        if (_depth > 0) return;
        TryTranslateRef(ref value, __instance, "UI.text");
    }

    public static void Postfix_TmpOnEnable(TMP_Text __instance)
    {
        if (_depth > 0) return;
        TranslateInPlace(__instance, "TMP.OnEnable");
    }

    public static void Postfix_UiOnEnable(UnityEngine.UI.Text __instance)
    {
        if (_depth > 0) return;
        TranslateInPlace(__instance, "UI.OnEnable");
    }

    private static void TryTranslateRef(ref string text, UnityEngine.Object instance, string source)
    {
        if (string.IsNullOrEmpty(text)) return;
        if (MatchesSkipPattern(text)) return;

        try
        {
            // Non-English target: text may be the English source OR a leftover
            // translation from a previous language (the game boots in Russian).
            // Try to (re)translate into the active target; only fit-font when there
            // is no NEW translation to apply.
            if (!LanguageManager.IsEnglish)
            {
                if (TranslationDict.TryLookup(text, out var mapped)
                    && !string.IsNullOrEmpty(mapped)
                    && !string.Equals(mapped, text, StringComparison.Ordinal))
                {
                    if (Plugin.Config.VerboseLogging.Value)
                        Plugin.Log.LogInfo($"[{source}] '{SafeName(instance)}' {Truncate(text, 60)} → {Truncate(mapped, 60)}");
                    if (instance is TMP_Text tmpPrep)
                        TextLayout.PrepareBeforeSet(tmpPrep, text, mapped);
                    else if (instance is UnityEngine.UI.Text uiPrep)
                        TextLayout.PrepareBeforeSet(uiPrep, text, mapped);
                    text = mapped;
                    return;
                }
                if (HasCyrillic(text) || !HasLatinLetter(text))
                    FitForeignFor(instance, text); // already foreign — just fit the font
                else
                    MissingLogger.Note(text);      // Latin text with no translation
                return;
            }

            // English active: revert any non-English overlay back to English.
            if (HasLatinLetter(text) && !HasCyrillic(text)) return;
            if (TranslationDict.TryLookup(text, out var en)
                && !string.IsNullOrEmpty(en)
                && !string.Equals(en, text, StringComparison.Ordinal))
            {
                if (Plugin.Config.VerboseLogging.Value)
                    Plugin.Log.LogInfo($"[{source}] '{SafeName(instance)}' {Truncate(text, 60)} → {Truncate(en, 60)}");
                if (instance is TMP_Text tmpPrep2)
                    TextLayout.PrepareBeforeSet(tmpPrep2, text, en);
                else if (instance is UnityEngine.UI.Text uiPrep2)
                    TextLayout.PrepareBeforeSet(uiPrep2, text, en);
                text = en;
            }
        }
        catch (Exception e)
        {
            Plugin.Log.LogError($"TryTranslateRef({source}) failed: {e.Message}");
        }
    }

    private static void TranslateInPlace(UnityEngine.Object instance, string source)
    {
        if (instance == null || _depth > 0) return;
        _depth++;
        try
        {
            string text;
            switch (instance)
            {
                case TMP_Text tmp: text = tmp.text; break;
                case UnityEngine.UI.Text ui: text = ui.text; break;
                default: return;
            }
            if (string.IsNullOrEmpty(text)) return;
            if (MatchesSkipPattern(text)) return;

            if (!LanguageManager.IsEnglish)
            {
                if (TranslationDict.TryLookup(text, out var mapped)
                    && !string.IsNullOrEmpty(mapped)
                    && !string.Equals(mapped, text, StringComparison.Ordinal))
                {
                    if (Plugin.Config.VerboseLogging.Value)
                        Plugin.Log.LogInfo($"[{source}] '{SafeName(instance)}' {Truncate(text, 60)} → {Truncate(mapped, 60)}");
                    switch (instance)
                    {
                        case TMP_Text tmp2: TextLayout.PrepareBeforeSet(tmp2, text, mapped); tmp2.text = mapped; break;
                        case UnityEngine.UI.Text ui2: TextLayout.PrepareBeforeSet(ui2, text, mapped); ui2.text = mapped; break;
                    }
                    return;
                }
                if (HasCyrillic(text) || !HasLatinLetter(text)) FitForeignFor(instance, text);
                else MissingLogger.Note(text);
                return;
            }

            // English active: revert any non-English overlay back to English.
            if (HasLatinLetter(text) && !HasCyrillic(text)) return;
            if (TranslationDict.TryLookup(text, out var en)
                && !string.IsNullOrEmpty(en)
                && !string.Equals(en, text, StringComparison.Ordinal))
            {
                if (Plugin.Config.VerboseLogging.Value)
                    Plugin.Log.LogInfo($"[{source}] '{SafeName(instance)}' {Truncate(text, 60)} → {Truncate(en, 60)}");
                switch (instance)
                {
                    case TMP_Text tmp3: TextLayout.PrepareBeforeSet(tmp3, text, en); tmp3.text = en; break;
                    case UnityEngine.UI.Text ui3: TextLayout.PrepareBeforeSet(ui3, text, en); ui3.text = en; break;
                }
            }
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"TranslateInPlace({source}) failed: {e.Message}");
        }
        finally
        {
            _depth--;
        }
    }

    private static void FitForeignFor(UnityEngine.Object instance, string text)
    {
        try
        {
            if (instance is TMP_Text tmp) TextLayout.FitForeign(tmp, text);
            else if (instance is UnityEngine.UI.Text ui) TextLayout.FitForeign(ui, text);
        }
        catch { /* ignore */ }
    }

    internal static bool MatchesSkipPattern(string text)
    {
        var raw = Plugin.Config.SkipTextPatterns?.Value;
        if (string.IsNullOrEmpty(raw)) return false;
        if (!ReferenceEquals(raw, _skipPatternsRaw))
        {
            _skipPatternsRaw = raw;
            _skipPatterns = new List<string>();
            var parts = raw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                var p = parts[i].Trim();
                if (p.Length == 0) continue;
                p = p.Replace("\\n", "\n").Replace("\\r", "\r");
                _skipPatterns.Add(p);
            }
        }
        if (_skipPatterns.Count == 0) return false;
        var trimmed = text.Trim();
        for (int i = 0; i < _skipPatterns.Count; i++)
            if (string.Equals(trimmed, _skipPatterns[i], StringComparison.OrdinalIgnoreCase))
                return true;
        return false;
    }

    private static bool HasLatinLetter(string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')) return true;
        }
        return false;
    }

    private static bool HasCyrillic(string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if (c >= 0x0400 && c <= 0x04FF) return true;
        }
        return false;
    }

    private static string SafeName(UnityEngine.Object o)
    {
        try { return o?.name ?? "?"; } catch { return "?"; }
    }

    private static string Truncate(string s, int n)
    {
        if (s == null) return "";
        s = s.Replace("\n", "\\n").Replace("\r", "");
        return s.Length <= n ? s : s.Substring(0, n) + "…";
    }
}