using System;
using System.Collections.Generic;
using System.IO;

namespace SarahsHouseI18n;

/// <summary>
/// Optional missing-string logger (Diagnostics.LogMissing).
/// </summary>
internal static class MissingLogger
{
    private static readonly HashSet<string> _seen = new();
    private static int _dirtyCount;
    private static float _nextFlush;

    // Pure keyboard-hint labels; not worth translator attention.
    private static readonly HashSet<string> KeyboardKeys = new(StringComparer.OrdinalIgnoreCase)
    {
        "Alt", "Ctrl", "Control", "Shift", "Esc", "Escape", "Tab", "Space",
        "Enter", "Return", "Del", "Delete", "Home", "End", "PgUp", "PgDn",
        "PageUp", "PageDown", "CapsLock", "Backspace", "Insert",
        "LMB", "RMB", "MMB",
    };

    public static void Note(string s)
    {
        if (!Plugin.Config.LogMissingTranslations.Value) return;
        if (string.IsNullOrEmpty(s)) return;
        if (s.Length < 3 || s.Length > 300) return;

        var t = s.Trim();
        if (t.Length < 3) return;
        if (KeyboardKeys.Contains(t)) return;
        if (LooksLikeUrl(t)) return;
        if (LooksLikeVersion(t)) return;
        if (t.StartsWith("(FPS", StringComparison.OrdinalIgnoreCase)) return;
        if (LooksLikeKeyboardMash(t)) return;

        // Keep strings that look English (latin-heavy).
        int latin = 0, cyr = 0, runMax = 0, run = 0;
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
            { latin++; run++; if (run > runMax) runMax = run; }
            else { run = 0; if (c >= 0x0400 && c <= 0x04FF) cyr++; }
        }
        if (runMax < 3) return;
        if (cyr > 0 && latin <= cyr) return;

        if (_seen.Add(s)) _dirtyCount++;
    }


    private static bool LooksLikeUrl(string s)
        => s.IndexOf("://", StringComparison.Ordinal) >= 0
           || s.StartsWith("www.", StringComparison.OrdinalIgnoreCase)
           || s.StartsWith("http", StringComparison.OrdinalIgnoreCase);

    private static bool LooksLikeVersion(string s)
    {
        // v0.11.2, 1.2.3, v2.8.8 (Early Access)
        int i = (s[0] == 'v' || s[0] == 'V') ? 1 : 0;
        int digits = 0, dots = 0;
        for (; i < s.Length; i++)
        {
            char c = s[i];
            if (c >= '0' && c <= '9') { digits++; continue; }
            if (c == '.') { dots++; continue; }
            break;
        }
        return digits >= 2 && dots >= 1 && (i >= s.Length || s[i] == ' ' || s[i] == '(');
    }

    private static bool LooksLikeKeyboardMash(string s)
    {
        // "asdasdasd asd", "DESCDESCDESC": long, letters from a tiny alphabet.
        if (s.Length <= 12) return false;
        int letters = 0;
        var distinct = new HashSet<char>();
        for (int i = 0; i < s.Length; i++)
        {
            char c = char.ToLowerInvariant(s[i]);
            if (c >= 'a' && c <= 'z') { letters++; distinct.Add(c); }
        }
        return letters >= 10 && distinct.Count <= 4;
    }

    public static void TryFlush()
    {
        if (!Plugin.Config.LogMissingTranslations.Value) return;
        if (_dirtyCount == 0) return;
        if (UnityEngine.Time.unscaledTime < _nextFlush) return;
        _nextFlush = UnityEngine.Time.unscaledTime + 10f;
        try
        {
            var path = Path.Combine(BepInEx.Paths.BepInExRootPath, "missing_translations.txt");
            var lines = new List<string>(_seen);
            lines.Sort();
            File.WriteAllLines(path, lines);
            _dirtyCount = 0;
        }
        catch (Exception e) { Plugin.Log.LogWarning($"MissingLogger flush: {e.Message}"); }
    }
}