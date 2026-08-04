using System;
using HarmonyLib;

namespace SarahsHouseI18n;

/// <summary>
/// Translate text that flows through Febucci TextAnimator (animated / typewriter
/// / shake-effect text). TextAnimator receives the raw source string (with tags
/// like &lt;shake&gt;) and drives TMP itself, bypassing the plain TMP setter hook,
/// so those lines stayed English even though they are in the dictionary. We hook
/// the TextAnimator entry points and translate the incoming string first; tags
/// are preserved because dictionary keys keep them (e.g. "…&lt;shake&gt;what the fuck!?").
/// </summary>
internal static class TextAnimatorHooks
{
    public static void PatchDynamic(Harmony harmony)
    {
        TryPatch(harmony, typeof(Febucci.UI.TextAnimator), "SetText", new[] { typeof(string), typeof(bool) });
        TryPatch(harmony, typeof(Febucci.UI.TextAnimator), "AppendText", new[] { typeof(string), typeof(bool) });
        TryPatch(harmony, typeof(Febucci.UI.Core.TAnimPlayerBase), "ShowText", new[] { typeof(string) });
        // Fallback: a single-arg SetText overload, if this version exposes one.
        TryPatch(harmony, typeof(Febucci.UI.TextAnimator), "SetText", new[] { typeof(string) });
    }

    private static void TryPatch(Harmony harmony, Type type, string method, Type[] sig)
    {
        try
        {
            if (type == null) return;
            var m = AccessTools.Method(type, method, sig);
            if (m == null)
            {
                Plugin.Log.LogWarning($"TAHook: {type.Name}.{method}({sig.Length} args) not found — skipped");
                return;
            }
            var prefix = new HarmonyMethod(AccessTools.Method(typeof(TextAnimatorHooks), nameof(Prefix_TAText)));
            harmony.Patch(m, prefix: prefix);
            Plugin.Log.LogInfo($"TAHook: patched {type.Name}.{method}({sig.Length} args)");
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"TAHook patch {type?.Name}.{method}: {e.Message}");
        }
    }

    // Matched by parameter name "text" on all three target methods.
    public static void Prefix_TAText(ref string text)
    {
        try
        {
            if (string.IsNullOrEmpty(text)) return;
            if (LanguageManager.IsEnglish) return;      // only when a non-English pack is active
            if (TextHooks.MatchesSkipPattern(text)) return;

            // Translate first — the incoming string may be English source OR leftover
            // text from a previous language (game boots in Russian → switch to de/uk).
            // Use the RAW lookup so inline directives (<?emotion=..>) are preserved —
            // TextAnimator parses and strips them itself.
            if (TranslationDict.TryLookupRaw(text, out var tr)
                && !string.IsNullOrEmpty(tr)
                && !string.Equals(tr, text, StringComparison.Ordinal))
            {
                if (Plugin.Config.VerboseLogging.Value)
                    Plugin.Log.LogInfo($"[TextAnimator] {Trunc(text)} -> {Trunc(tr)}");
                text = tr;
            }
            else if (HasLatinLetter(text) && !HasCyrillic(text))
            {
                MissingLogger.Note(text);               // Latin text with no translation
            }
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"TextAnimator prefix: {e.Message}");
        }
    }

    private static bool HasCyrillic(string s)
    {
        for (int i = 0; i < s.Length; i++) { char c = s[i]; if (c >= 0x0400 && c <= 0x04FF) return true; }
        return false;
    }

    private static bool HasLatinLetter(string s)
    {
        for (int i = 0; i < s.Length; i++) { char c = s[i]; if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')) return true; }
        return false;
    }

    private static string Trunc(string s)
    {
        if (s == null) return "";
        s = s.Replace("\n", "\\n").Replace("\r", "");
        return s.Length <= 60 ? s : s.Substring(0, 60) + "…";
    }
}