using System;
using System.Collections.Generic;
using Il2CppInterop.Runtime;
using TMPro;
using UnityEngine;

namespace SarahsHouseI18n;

/// <summary>
/// Periodic scan of loaded TMP/UI text for baked strings hooks miss.
/// </summary>
internal static class TextScanner
{
    // Already-translated component ids.
    private static readonly HashSet<int> _processedTmp = new();
    private static readonly HashSet<int> _processedUi = new();

    public static void InvalidateAll()
    {
        _processedTmp.Clear();
        _processedUi.Clear();
    }

    /// <summary>Force every active TMP text to rebuild its mesh, so glyphs that became available
    /// after the first render (e.g. Cyrillic injected a frame after the launch disclaimer showed)
    /// replace tofu without needing a manual language switch. Only regenerates meshes; does not
    /// re-translate, so it is safe to call after each font-injection pass.</summary>
    public static void ForceRefreshAll()
    {
        try
        {
            var all = Resources.FindObjectsOfTypeAll(Il2CppType.Of<TMP_Text>());
            for (int i = 0; i < all.Length; i++)
            {
                var t = all[i].TryCast<TMP_Text>();
                if (t == null) continue;
                try
                {
                    if (!t.isActiveAndEnabled) continue;
                    if (string.IsNullOrEmpty(t.text)) continue;
                    t.ForceMeshUpdate(false, false);
                }
                catch { /* ignore */ }
            }
        }
        catch (Exception e) { Plugin.Log.LogWarning($"ForceRefreshAll: {e.Message}"); }
    }

    public static void ScanScene()
    {
        // English uses reverse map (may be non-empty even if forward Count==0 after reload order).
        if (!LanguageManager.IsEnglish && TranslationDict.Count == 0) return;
        // Bound processed-id caches (transient TMP/UI objects accumulate over a session).
        if (_processedTmp.Count > 20000) _processedTmp.Clear();
        if (_processedUi.Count > 20000) _processedUi.Clear();
        try { ScanTmp(); }
        catch (Exception e) { Plugin.Log.LogError($"ScanTmp failed: {e}"); }
        try { ScanUi(); }
        catch (Exception e) { Plugin.Log.LogError($"ScanUi failed: {e}"); }
    }

    private static void ScanTmp()
    {
        var all = Resources.FindObjectsOfTypeAll(Il2CppType.Of<TMP_Text>());
        for (int i = 0; i < all.Length; i++)
        {
            var t = all[i].TryCast<TMP_Text>();
            if (t == null) continue;
            int id = t.GetInstanceID();

            // Give the component a font that can actually draw this language before any
            // text is written into it.
            FontManager.ApplyFontSwap(t);

            string s;
            try { s = t.text; } catch { continue; }
            if (string.IsNullOrEmpty(s)) { continue; }
            if (TextHooks.MatchesSkipPattern(s)) { _processedTmp.Add(id); continue; }

            if (!LanguageManager.IsEnglish)
            {
                // Translate first — handles English source AND leftover text from a
                // previous language (game boots in Russian → switch to de/uk).
                if (TranslationDict.TryLookup(s, out var mapped)
                    && !string.IsNullOrEmpty(mapped)
                    && !string.Equals(mapped, s, StringComparison.Ordinal))
                {
                    try
                    {
                        TextLayout.PrepareBeforeSet(t, s, mapped);
                        t.text = mapped;
                        _processedTmp.Add(id);
                    }
                    catch (Exception e) { Plugin.Log.LogWarning($"scan TMP set failed on '{SafeName(t)}': {e.Message}"); }
                    if (Plugin.Config.VerboseLogging.Value)
                        Plugin.Log.LogInfo($"[scan] '{SafeName(t)}' {Truncate(s, 60)} → {Truncate(mapped, 60)}");
                    continue;
                }
                // No new translation: fit already-foreign text; note untranslated Latin.
                if (HasCyrillic(s) || !HasLatinLetter(s))
                {
                    try { TextLayout.FitForeign(t, s); } catch { }
                    _processedTmp.Add(id);
                }
                else MissingLogger.Note(s);
                continue;
            }

            // EN active: revert non-English overlays back to English.
            if (HasLatinLetter(s) && !HasCyrillic(s)) { _processedTmp.Add(id); continue; }
            if (TranslationDict.TryLookup(s, out var enTxt)
                && !string.IsNullOrEmpty(enTxt)
                && !string.Equals(enTxt, s, StringComparison.Ordinal))
            {
                try
                {
                    TextLayout.PrepareBeforeSet(t, s, enTxt);
                    t.text = enTxt;
                    _processedTmp.Add(id);
                }
                catch (Exception e) { Plugin.Log.LogWarning($"scan TMP set failed on '{SafeName(t)}': {e.Message}"); }
                if (Plugin.Config.VerboseLogging.Value)
                    Plugin.Log.LogInfo($"[scan] '{SafeName(t)}' {Truncate(s, 60)} → {Truncate(enTxt, 60)}");
            }
        }
    }

    private static void ScanUi()
    {
        var all = Resources.FindObjectsOfTypeAll(Il2CppType.Of<UnityEngine.UI.Text>());
        for (int i = 0; i < all.Length; i++)
        {
            var t = all[i].TryCast<UnityEngine.UI.Text>();
            if (t == null) continue;
            int id = t.GetInstanceID();

            string s;
            try { s = t.text; } catch { continue; }
            if (string.IsNullOrEmpty(s)) continue;
            if (TextHooks.MatchesSkipPattern(s)) { _processedUi.Add(id); continue; }

            if (!LanguageManager.IsEnglish)
            {
                if (TranslationDict.TryLookup(s, out var mapped)
                    && !string.IsNullOrEmpty(mapped)
                    && !string.Equals(mapped, s, StringComparison.Ordinal))
                {
                    try
                    {
                        TextLayout.PrepareBeforeSet(t, s, mapped);
                        t.text = mapped;
                        _processedUi.Add(id);
                    }
                    catch (Exception e) { Plugin.Log.LogWarning($"scan UI set failed on '{SafeName(t)}': {e.Message}"); }
                    if (Plugin.Config.VerboseLogging.Value)
                        Plugin.Log.LogInfo($"[scan-ui] '{SafeName(t)}' {Truncate(s, 60)} → {Truncate(mapped, 60)}");
                    continue;
                }
                if (HasCyrillic(s) || !HasLatinLetter(s))
                {
                    try { TextLayout.FitForeign(t, s); } catch { }
                    _processedUi.Add(id);
                }
                else MissingLogger.Note(s);
                continue;
            }

            // EN active: revert non-English overlays back to English.
            if (HasLatinLetter(s) && !HasCyrillic(s)) { _processedUi.Add(id); continue; }
            if (TranslationDict.TryLookup(s, out var enTxt)
                && !string.IsNullOrEmpty(enTxt)
                && !string.Equals(enTxt, s, StringComparison.Ordinal))
            {
                try
                {
                    TextLayout.PrepareBeforeSet(t, s, enTxt);
                    t.text = enTxt;
                    _processedUi.Add(id);
                }
                catch (Exception e) { Plugin.Log.LogWarning($"scan UI set failed on '{SafeName(t)}': {e.Message}"); }
                if (Plugin.Config.VerboseLogging.Value)
                    Plugin.Log.LogInfo($"[scan-ui] '{SafeName(t)}' {Truncate(s, 60)} → {Truncate(enTxt, 60)}");
            }
        }
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