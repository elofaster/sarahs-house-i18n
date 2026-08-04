using System;
using System.Collections.Generic;
using System.IO;

namespace SarahsHouseI18n;

/// <summary>
/// Cheap, exact key counts for the i18n packs, used as the "N строк" detail on the
/// language tiles. Counting is streamed (top-level commas outside strings) so a pack
/// is never fully materialised into memory, and only one pack is measured per pump
/// so opening the picker never stutters.
/// </summary>
internal static class LangStats
{
    private static readonly Dictionary<string, int> _counts = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Queue<string> _pending = new();
    private static readonly HashSet<string> _queued = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>English names shown under the native name.</summary>
    private static readonly Dictionary<string, string> _english = new(StringComparer.OrdinalIgnoreCase)
    {
        ["en"] = "original", ["ru"] = "Russian", ["uk"] = "Ukrainian", ["de"] = "German",
        ["es"] = "Spanish", ["fr"] = "French", ["pt"] = "Portuguese", ["tr"] = "Turkish",
        ["zh"] = "Chinese", ["ja"] = "Japanese", ["ko"] = "Korean", ["pl"] = "Polish",
        ["it"] = "Italian", ["cs"] = "Czech", ["vi"] = "Vietnamese",
    };

    public static string EnglishName(string code)
    {
        if (string.IsNullOrEmpty(code)) return "";
        return _english.TryGetValue(code, out var n) ? n : code.ToUpperInvariant();
    }

    // ---------------------------------------------------------------- attribution

    /// <summary>How a pack was produced — shown on the tile instead of a country code.</summary>
    public enum Source { None, Ai, Human }

    private static HashSet<string> _human;

    /// <summary>
    /// Codes listed in <c>i18n/human.txt</c> (one per line) count as human-translated;
    /// everything else is machine translation. Keeping it in a file means a pack can be
    /// promoted after a human pass without rebuilding the plugin.
    /// </summary>
    private static HashSet<string> Human
    {
        get
        {
            if (_human != null) return _human;
            _human = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                var dir = Path.GetDirectoryName(typeof(LangStats).Assembly.Location) ?? ".";
                var path = Path.Combine(dir, "i18n", "human.txt");
                if (File.Exists(path))
                {
                    foreach (var raw in File.ReadAllLines(path))
                    {
                        var line = (raw ?? "").Trim();
                        if (line.Length == 0 || line.StartsWith("#")) continue;
                        _human.Add(line.ToLowerInvariant());
                    }
                    Plugin.Log.LogInfo($"Human-translated packs: {_human.Count}");
                }
            }
            catch (Exception e)
            {
                Plugin.Log.LogWarning($"human.txt: {e.Message}");
            }
            return _human;
        }
    }

    public static Source SourceFor(string code)
    {
        if (string.IsNullOrEmpty(code)) return Source.None;
        if (string.Equals(code, "en", StringComparison.OrdinalIgnoreCase)) return Source.None; // the original
        return Human.Contains(code) ? Source.Human : Source.Ai;
    }

    /// <summary>-1 = not measured yet, 0 = no pack.</summary>
    public static int Count(string code)
    {
        if (string.IsNullOrEmpty(code)) return 0;
        if (string.Equals(code, "en", StringComparison.OrdinalIgnoreCase)) return -2; // original
        if (_counts.TryGetValue(code, out var n)) return n;

        if (_queued.Add(code)) _pending.Enqueue(code);
        return -1;
    }

    /// <summary>Measure at most one queued pack. Call once per frame while the UI is open.</summary>
    public static void Pump()
    {
        if (_pending.Count == 0) return;
        var code = _pending.Dequeue();
        _queued.Remove(code);
        _counts[code] = Measure(code);
    }

    private static int Measure(string code)
    {
        try
        {
            var path = LanguageManager.ResolveDictPath(code);
            if (path == null || !File.Exists(path)) return 0;

            int pairs = 0, depth = 0;
            bool inStr = false, esc = false;

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1 << 16);
            using var sr = new StreamReader(fs);
            var buf = new char[1 << 16];
            int read;
            bool sawAny = false;

            while ((read = sr.Read(buf, 0, buf.Length)) > 0)
            {
                for (int i = 0; i < read; i++)
                {
                    char c = buf[i];
                    if (inStr)
                    {
                        if (esc) esc = false;
                        else if (c == '\\') esc = true;
                        else if (c == '"') inStr = false;
                        continue;
                    }

                    switch (c)
                    {
                        case '"': inStr = true; sawAny = true; break;
                        case '{':
                        case '[': depth++; break;
                        case '}':
                        case ']': depth--; break;
                        case ',': if (depth == 1) pairs++; break;
                    }
                }
            }

            int total = sawAny ? pairs + 1 : 0;
            Plugin.Log.LogInfo($"Language pack '{code}': {total} entries");
            return total;
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"LangStats '{code}': {e.Message}");
            return 0;
        }
    }

    public static void Invalidate()
    {
        _human = null;
        _counts.Clear();
        _pending.Clear();
        _queued.Clear();
    }
}
