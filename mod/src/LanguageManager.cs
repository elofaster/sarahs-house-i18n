using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SarahsHouseI18n;

/// <summary>
/// Active language pack loader and switcher (i18n/{code}.json).
/// </summary>
internal static class LanguageManager
{
    public const string English = "en";
    public const string Russian = "ru";

    public static event Action<string, string> LanguageChanged;

    private static readonly string[] BuiltinOrder = { English, Russian, "de", "uk", "es", "fr", "pt", "tr", "zh", "ja", "ko", "pl" };

    private static readonly Dictionary<string, string> DisplayNames = new(StringComparer.OrdinalIgnoreCase)
    {
        [English] = "English",
        [Russian] = "Русский",
        ["de"] = "Deutsch",
        ["uk"] = "Українська",
        ["es"] = "Español",
        ["fr"] = "Français",
        ["pt"] = "Português",
        ["tr"] = "Türkçe",
        ["zh"] = "中文",
        ["ja"] = "日本語",
        ["ko"] = "한국어",
        ["pl"] = "Polski",
        ["it"] = "Italiano",
        ["cs"] = "Čeština",
    };

    private static string _code = Russian;
    private static bool _ready;
    private static readonly Dictionary<string, Dictionary<string, string>> _cache = new(StringComparer.OrdinalIgnoreCase);

    public static string CurrentCode => _code;
    public static bool IsReady => _ready;
    public static bool IsEnglish => string.Equals(_code, English, StringComparison.OrdinalIgnoreCase);
    public static bool RuntimeOverlayEnabled => !IsEnglish;

    public static string DisplayName(string code)
    {
        if (string.IsNullOrEmpty(code)) return "?";
        return DisplayNames.TryGetValue(code, out var n) ? n : code.ToUpperInvariant();
    }

    public static void Initialize()
    {
        var cfg = Plugin.Config.LanguageCode?.Value;
        _code = NormalizeCode(string.IsNullOrWhiteSpace(cfg) ? English : cfg);
        EnsurePackDir();
        TranslationDict.ReloadForLanguage(_code);
        // Keep Unity Localization on the ENGLISH source so the game emits its
        // original (fully-populated) English strings; the plugin overlays the
        // active-language translation on top. Switching Unity to a target locale
        // whose string tables are incomplete makes the game render
        // "No translation found for '<key>'" placeholders (uk/de had empty tables).
        LocaleSwitcher.SetDesiredLocale(English);
        _ready = true;
        Plugin.Log.LogInfo($"LanguageManager ready: '{_code}' ({DisplayName(_code)}), dict={TranslationDict.Count}");
    }

    public static IReadOnlyList<string> GetAvailableLanguages()
    {
        var list = new List<string>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        void add(string c)
        {
            c = NormalizeCode(c);
            if (seen.Add(c)) list.Add(c);
        }

        add(English);
        // Prefer discovered i18n/*.json
        try
        {
            var dir = I18nDir();
            if (Directory.Exists(dir))
            {
                foreach (var f in Directory.GetFiles(dir, "*.json"))
                {
                    var name = Path.GetFileNameWithoutExtension(f);
                    if (!string.IsNullOrEmpty(name)) add(name);
                }
            }
        }
        catch { /* ignore */ }

        // Keep built-in language slots visible
        foreach (var c in BuiltinOrder) add(c);

        return list;
    }

    public static bool SetLanguage(string code, bool refreshUi = true)
    {
        code = NormalizeCode(code);
        if (string.Equals(code, _code, StringComparison.OrdinalIgnoreCase) && _ready)
        {
            // Keep Unity Localization on the ENGLISH source so the game emits its
        // original (fully-populated) English strings; the plugin overlays the
        // active-language translation on top. Switching Unity to a target locale
        // whose string tables are incomplete makes the game render
        // "No translation found for '<key>'" placeholders (uk/de had empty tables).
        LocaleSwitcher.SetDesiredLocale(English);
            return false;
        }

        var prev = _code;
        _code = code;
        try { Plugin.Config.LanguageCode.Value = _code; } catch { /* ignore */ }

        TranslationDict.ReloadForLanguage(_code);
        // Keep Unity Localization on the ENGLISH source so the game emits its
        // original (fully-populated) English strings; the plugin overlays the
        // active-language translation on top. Switching Unity to a target locale
        // whose string tables are incomplete makes the game render
        // "No translation found for '<key>'" placeholders (uk/de had empty tables).
        LocaleSwitcher.SetDesiredLocale(English);
        LocaleSwitcher.TryApplyDesired(force: true);

        if (refreshUi)
        {
            try { TextScanner.InvalidateAll(); } catch { /* ignore */ }
            // Attach + pre-bake the CJK/JP donor BEFORE re-applying text, so the first switch
            // to a dynamic-donor language (ja/ko) doesn't render tofu until a second switch.
            try { FontManager.PrepareForActiveLanguage(); } catch { /* ignore */ }
            Bootstrap.RequestBurst(12);
            try { TextScanner.ScanScene(); } catch { /* ignore */ }
            // Clear the fallback glyph cache + mark text dirty so already-rendered strings
            // re-resolve against the new language's fallback (Cyrillic/CJK) immediately.
            try { FontManager.RefreshAllText(); } catch { /* ignore */ }
        }

        Plugin.Log.LogInfo($"Language changed: '{prev}' → '{_code}' (dict={TranslationDict.Count})");
        try { LanguageChanged?.Invoke(prev, _code); } catch { /* ignore */ }
        return true;
    }

    public static string CycleNextLanguage()
    {
        var all = GetAvailableLanguages();
        // Cycle only languages with real entries
        var usable = new List<string>();
        foreach (var c in all)
        {
            if (IsLanguageUsable(c))
                usable.Add(c);
        }
        if (usable.Count == 0) usable.Add(English);

        int idx = 0;
        for (int i = 0; i < usable.Count; i++)
        {
            if (string.Equals(usable[i], _code, StringComparison.OrdinalIgnoreCase))
            { idx = i; break; }
        }
        var next = usable[(idx + 1) % usable.Count];
        SetLanguage(next, refreshUi: true);
        return next;
    }

    public static bool HasDictFile(string code)
    {
        code = NormalizeCode(code);
        if (string.Equals(code, English, StringComparison.OrdinalIgnoreCase)) return true;
        return File.Exists(Path.Combine(I18nDir(), code + ".json"));
    }

    /// <summary>True if the pack has translations and can be selected.</summary>
    public static bool IsLanguageUsable(string code)
    {
        code = NormalizeCode(code);
        if (string.Equals(code, English, StringComparison.OrdinalIgnoreCase)) return true;
        if (!HasDictFile(code)) return false;
        try
        {
            var map = LoadMapCached(code);
            return map != null && map.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static string ResolveDictPath(string code)
    {
        code = NormalizeCode(code);
        if (string.Equals(code, English, StringComparison.OrdinalIgnoreCase))
            return null; // passthrough

        return Path.Combine(I18nDir(), code + ".json"); // may not exist yet
    }

    public static Dictionary<string, string> LoadMapCached(string code)
    {
        code = NormalizeCode(code);
        if (_cache.TryGetValue(code, out var cached)) return cached;

        var map = new Dictionary<string, string>(8192);
        if (string.Equals(code, English, StringComparison.OrdinalIgnoreCase))
        {
            _cache[code] = map;
            return map;
        }

        var path = ResolveDictPath(code);
        if (path != null && File.Exists(path))
        {
            try
            {
                TranslationDict.ParseStringMapPublic(File.ReadAllText(path), map);
            }
            catch (Exception e)
            {
                Plugin.Log.LogError($"Failed loading language map '{code}' from {path}: {e.Message}");
            }
        }
        else
        {
            Plugin.Log.LogWarning($"Language map missing for '{code}' (path={path}). Runtime overlay disabled for missing keys.");
        }
        _cache[code] = map;
        return map;
    }

    public static void InvalidateCache()
    {
        _cache.Clear();
        TranslationDict.InvalidateGlobalReverse();
        FontManager.InvalidateInjectChars();
    }

    /// <summary>
    /// Make sure the pack folder exists and carries a current README. Nothing is seeded:
    /// packs ship with the mod, and empty stub files would show up as selectable
    /// languages with no translation behind them.
    /// </summary>
    private static void EnsurePackDir()
    {
        try
        {
            Directory.CreateDirectory(I18nDir());

            var readme = Path.Combine(I18nDir(), "README.txt");
            if (!File.Exists(readme))
            {
                File.WriteAllText(readme,
                    "Language packs — Sarah's House i18n\n" +
                    "-----------------------------------\n" +
                    "Each {code}.json is a flat EN->TARGET map in UTF-8:\n" +
                    "  {\n" +
                    "    \"New Game\": \"Nouvelle partie\",\n" +
                    "    \"Tuesday\": \"Mardi\"\n" +
                    "  }\n" +
                    "Keys are the game's original English strings.\n" +
                    "English needs no file — it is the untranslated original.\n" +
                    "human.txt lists codes that got a human pass (one per line);\n" +
                    "every other pack is labelled AI in the picker.\n" +
                    "Drop a new {code}.json here, then pick it with F10 in game.\n");
            }
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"EnsurePackDir: {e.Message}");
        }
    }

    public static string NormalizeCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) return English;
        code = code.Trim().ToLowerInvariant().Replace('_', '-');
        // ru-RU → ru
        var i = code.IndexOf('-');
        if (i > 0) code = code.Substring(0, i);
        if (code == "russian") return Russian;
        if (code == "english") return English;
        return code;
    }

    private static string PluginDir()
    {
        try { return Path.GetDirectoryName(typeof(LanguageManager).Assembly.Location) ?? "."; }
        catch { return "."; }
    }

    private static string I18nDir() => Path.Combine(PluginDir(), "i18n");
}