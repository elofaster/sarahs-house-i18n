using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SarahsHouseI18n;

/// <summary>
/// EN→target dictionary for the active pack (exact / template / reverse).
/// </summary>
internal static class TranslationDict
{
    private static Dictionary<string, string> _dict = new(8192);
    private static Dictionary<string, string> _reverse = new(8192);
    private static readonly List<TemplateEntry> _templates = new();
    private static string _loadedCode = "";

    // Cross-language reverse index (any pack's translated string → English source),
    // built from ALL installed i18n packs. Lets us re-translate text that is currently
    // in some other language (e.g. Russian) into the active target (e.g. German/Ukrainian).
    private static Dictionary<string, string> _globalReverse;
    private static bool _globalBuilt;

    private static readonly Regex DirectiveRe = new(@"<\?[^>]*>", RegexOptions.Compiled);
    private static readonly Regex VarRe = new(@"@\{([A-Za-z0-9_]+)\}", RegexOptions.Compiled);
    private static readonly Regex DoubledVarRe = new(@"@\{\{([A-Za-z0-9_]+)\}\}", RegexOptions.Compiled);

    public static int Count => _dict?.Count ?? 0;
    public static int TemplateCount => _templates.Count;
    public static string LoadedCode => _loadedCode;

    public static void Load()
    {
        var code = LanguageManager.IsReady ? LanguageManager.CurrentCode : "ru";
        ReloadForLanguage(code);
    }

    public static void ReloadForLanguage(string code)
    {
        code = LanguageManager.NormalizeCode(code);
        var keptReverse = _reverse;
        _dict = new Dictionary<string, string>(8192);
        _templates.Clear();
        _loadedCode = code;

        if (string.Equals(code, LanguageManager.English, StringComparison.OrdinalIgnoreCase))
        {
            _reverse = keptReverse ?? new Dictionary<string, string>(8192);
            if (_reverse.Count == 0)
            {
                try
                {
                    var ruPath = LanguageManager.ResolveDictPath(LanguageManager.Russian);
                    if (ruPath != null && File.Exists(ruPath))
                    {
                        var tmp = new Dictionary<string, string>(8192);
                        ParseStringMap(File.ReadAllText(ruPath), tmp);
                        foreach (var kv in tmp)
                        {
                            if (string.IsNullOrEmpty(kv.Key) || string.IsNullOrEmpty(kv.Value)) continue;
                            if (!_reverse.ContainsKey(kv.Value))
                                _reverse[kv.Value] = kv.Key;
                        }
                    }
                }
                catch (Exception e)
                {
                    Plugin.Log.LogWarning($"English reverse seed failed: {e.Message}");
                }
            }
            Plugin.Log.LogInfo($"TranslationDict: English passthrough (reverse={_reverse.Count})");
            return;
        }

        _reverse = new Dictionary<string, string>(8192);
        var path = LanguageManager.ResolveDictPath(code);
        if (path == null || !File.Exists(path))
        {
            Plugin.Log.LogWarning($"TranslationDict: map not found for '{code}' path={path}");
            return;
        }

        try
        {
            ParseStringMap(File.ReadAllText(path), _dict);
        }
        catch (Exception e)
        {
            Plugin.Log.LogError($"Failed to parse language map '{code}': {e}");
            return;
        }

        var extras = new List<KeyValuePair<string, string>>();
        foreach (var kv in _dict)
        {
            var norm = Normalize(kv.Key);
            if (norm != kv.Key && !_dict.ContainsKey(norm))
                extras.Add(new KeyValuePair<string, string>(norm, kv.Value));
        }
        foreach (var kv in extras) _dict[kv.Key] = kv.Value;

        foreach (var kv in _dict)
        {
            var en = kv.Key;
            var tr = kv.Value;
            if (string.IsNullOrEmpty(en) || string.IsNullOrEmpty(tr)) continue;
            if (!_reverse.ContainsKey(tr))
                _reverse[tr] = en;
            var trNorm = Normalize(tr);
            if (trNorm != tr && !_reverse.ContainsKey(trNorm))
                _reverse[trNorm] = en;
        }

        BuildTemplates();
        Plugin.Log.LogInfo(
            $"Loaded lang='{code}' pairs={_dict.Count} (+{extras.Count} normalized, reverse={_reverse.Count}, templates={_templates.Count}) from {path}");
    }

    public static void ParseStringMapPublic(string json, Dictionary<string, string> into)
        => ParseStringMap(json, into);

    /// <summary>
    /// Map <paramref name="value"/> into the active language. Handles the normal
    /// EN→target case AND the cross-language case where the on-screen text is
    /// currently in another language (e.g. Russian while switching to German):
    /// it is mapped back to English via the global reverse index, then forwarded
    /// into the active target. When English is active, maps any overlay back to EN.
    /// </summary>
    /// <summary>
    /// Display lookup: like <see cref="TryLookupRaw"/> but strips inline
    /// &lt;?...&gt; directives from the result. Those are dialogue-system
    /// processing instructions (e.g. &lt;?emotion=13&gt;) that must NOT appear in
    /// the final on-screen text; only the TextAnimator path keeps them.
    /// </summary>
    public static bool TryLookup(string value, out string ru)
    {
        if (TryLookupRaw(value, out ru))
        {
            ru = StripDirectives(ru);
            return true;
        }
        return false;
    }

    /// <summary>Lookup that preserves inline directives (used by the TextAnimator hook).</summary>
    public static bool TryLookupRaw(string value, out string ru)
    {
        ru = null;
        if (string.IsNullOrEmpty(value)) return false;
        if (LanguageManager.IsEnglish)
            return TryReverseLookup(value, out ru);

        // 1) Direct EN→target (value is the English source).
        if (TryForwardActive(value, out ru)) return true;

        // 2) Cross-language: value is a translation from another pack → back to EN → target.
        EnsureGlobalReverse();
        if (_globalReverse != null && _globalReverse.Count > 0
            && TryGlobalToEnglish(value, out var en)
            && !string.Equals(en, value, StringComparison.Ordinal)
            && TryForwardActive(en, out ru))
            return true;

        ru = null;
        return false;
    }

    /// <summary>Remove inline &lt;?...&gt; directives (kept in dict values, stripped for display).</summary>
    public static string StripDirectives(string s)
        => (!string.IsNullOrEmpty(s) && s.IndexOf("<?", StringComparison.Ordinal) >= 0)
            ? DirectiveRe.Replace(s, "")
            : s;

    /// <summary>Exact/normalized/trimmed/template EN→active-target lookup.</summary>
    private static bool TryForwardActive(string value, out string ru)
    {
        ru = null;
        if (_dict == null || _dict.Count == 0 || string.IsNullOrEmpty(value)) return false;
        if (_dict.TryGetValue(value, out ru)) return true;

        var norm = Normalize(value);
        if (!ReferenceEquals(norm, value) && _dict.TryGetValue(norm, out ru)) return true;

        var trimmed = value.Trim();
        if (trimmed.Length != value.Length && _dict.TryGetValue(trimmed, out ru)) return true;
        var normTrim = Normalize(trimmed);
        if (normTrim != trimmed && _dict.TryGetValue(normTrim, out ru)) return true;

        // Trailing whitespace ("Inventory\n") — strip, lookup, re-attach.
        int ti = value.Length;
        while (ti > 0 && IsTrailingWs(value[ti - 1])) ti--;
        if (ti > 0 && ti != value.Length)
        {
            var head = value.Substring(0, ti);
            var tail = value.Substring(ti);
            if (_dict.TryGetValue(head, out ru)) { ru += tail; return true; }
            var headNorm = Normalize(head);
            if (headNorm != head && _dict.TryGetValue(headNorm, out ru)) { ru += tail; return true; }
        }

        // Template fallback after the game substituted @{vars}.
        if (TryMatchTemplate(value, out ru)) return true;
        if (!ReferenceEquals(norm, value) && TryMatchTemplate(norm, out ru)) return true;
        return false;
    }

    public static bool TryReverseLookup(string value, out string en)
    {
        en = null;
        if (string.IsNullOrEmpty(value)) return false;
        if (_reverse != null && _reverse.Count > 0)
        {
            if (_reverse.TryGetValue(value, out en)) return true;
            var norm = Normalize(value);
            if (!ReferenceEquals(norm, value) && _reverse.TryGetValue(norm, out en)) return true;
            var trimmed = value.Trim();
            if (trimmed.Length != value.Length && _reverse.TryGetValue(trimmed, out en)) return true;
        }
        // Fall back to the cross-language index so any overlay reverts to English.
        EnsureGlobalReverse();
        if (_globalReverse != null && _globalReverse.Count > 0 && TryGlobalToEnglish(value, out en))
            return true;
        en = null;
        return false;
    }

    /// <summary>Build translated→English index across every installed pack (once).</summary>
    private static void EnsureGlobalReverse()
    {
        if (_globalBuilt) return;
        _globalBuilt = true;
        _globalReverse = new Dictionary<string, string>(32768);
        try
        {
            foreach (var code in LanguageManager.GetAvailableLanguages())
            {
                if (string.Equals(code, LanguageManager.English, StringComparison.OrdinalIgnoreCase))
                    continue;
                Dictionary<string, string> map;
                try { map = LanguageManager.LoadMapCached(code); }
                catch { continue; }
                if (map == null) continue;
                foreach (var kv in map)
                {
                    if (string.IsNullOrEmpty(kv.Key) || string.IsNullOrEmpty(kv.Value)) continue;
                    if (!_globalReverse.ContainsKey(kv.Value)) _globalReverse[kv.Value] = kv.Key;
                    var vn = Normalize(kv.Value);
                    if (!ReferenceEquals(vn, kv.Value) && !_globalReverse.ContainsKey(vn))
                        _globalReverse[vn] = kv.Key;
                }
            }
            Plugin.Log.LogInfo($"Global reverse index built: {_globalReverse.Count} entries");
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"EnsureGlobalReverse failed: {e.Message}");
        }
    }

    /// <summary>Drop the cross-language index (rebuilt lazily after packs change).</summary>
    public static void InvalidateGlobalReverse()
    {
        _globalBuilt = false;
        _globalReverse = null;
    }

    private static bool TryGlobalToEnglish(string value, out string en)
    {
        en = null;
        if (_globalReverse == null || _globalReverse.Count == 0 || string.IsNullOrEmpty(value)) return false;
        if (_globalReverse.TryGetValue(value, out en)) return true;
        var norm = Normalize(value);
        if (!ReferenceEquals(norm, value) && _globalReverse.TryGetValue(norm, out en)) return true;
        var trimmed = value.Trim();
        if (trimmed.Length != value.Length && _globalReverse.TryGetValue(trimmed, out en)) return true;
        return false;
    }

    public static string Normalize(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        if (s.IndexOf("<noparse></noparse>", StringComparison.Ordinal) >= 0)
            s = s.Replace("<noparse></noparse>", "");
        if (s.IndexOf("<?", StringComparison.Ordinal) >= 0)
            s = DirectiveRe.Replace(s, "");
        // Invisible characters (ZWSP etc.) break dict matching, e.g. "John\u200B" from the game UI.
        if (HasInvisibleChar(s))
            s = StripInvisibleChars(s);
        return s;
    }

    private static bool IsInvisibleChar(char c)
    {
        return c == '\u200B'   // zero width space
            || c == '\u200C'   // zero width non-joiner
            || c == '\u200D'   // zero width joiner
            || c == '\u2060'   // word joiner
            || c == '\uFEFF';  // BOM / zero width no-break space
    }

    private static bool HasInvisibleChar(string s)
    {
        for (int i = 0; i < s.Length; i++)
            if (IsInvisibleChar(s[i])) return true;
        return false;
    }

    private static string StripInvisibleChars(string s)
    {
        var sb = new StringBuilder(s.Length);
        for (int i = 0; i < s.Length; i++)
            if (!IsInvisibleChar(s[i])) sb.Append(s[i]);
        return sb.ToString();
    }

    private static bool IsTrailingWs(char c) => c == ' ' || c == '\t' || c == '\n' || c == '\r';

    // ---------- templates ----------

    private sealed class TemplateEntry
    {
        public Regex Pattern;
        public string Format;
        public int KeyLength;
    }

    private static void BuildTemplates()
    {
        _templates.Clear();
        if (_dict == null) return;
        foreach (var kv in _dict)
        {
            if (kv.Key.IndexOf("@{", StringComparison.Ordinal) < 0) continue;
            var key = kv.Key;
            var names = new List<string>();
            foreach (Match m in VarRe.Matches(key)) names.Add(m.Groups[1].Value);

            // Build template regex; keep name captures short.
            var sb = new StringBuilder("^");
            int last = 0;
            foreach (Match m in VarRe.Matches(key))
            {
                if (m.Index > last)
                    sb.Append(Regex.Escape(key.Substring(last, m.Index - last)));
                var varName = m.Groups[1].Value;
                // Short captures for player/name/npc.
                if (IsShortTokenVar(varName))
                    sb.Append("([^\\s!?,:;]+)");
                else
                    sb.Append("(.*?)");
                last = m.Index + m.Length;
            }
            if (last < key.Length) sb.Append(Regex.Escape(key.Substring(last)));
            sb.Append('$');

            // Build string.Format pattern from @{name} tokens.
            string fmt = kv.Value ?? "";
            var idx = new Dictionary<string, int>();
            for (int i = 0; i < names.Count; i++) if (!idx.ContainsKey(names[i])) idx[names[i]] = i;
            fmt = fmt.Replace("{", "{{").Replace("}", "}}");
            fmt = DoubledVarRe.Replace(fmt, m =>
            {
                if (idx.TryGetValue(m.Groups[1].Value, out var j)) return "{" + j + "}";
                return m.Value;
            });

            Regex re;
            try { re = new Regex(sb.ToString(), RegexOptions.Compiled); }
            catch { continue; }
            _templates.Add(new TemplateEntry
            {
                Pattern = re,
                Format = fmt,
                KeyLength = key.Length,
            });
        }

        // Longest template keys first.
        _templates.Sort((a, b) => b.KeyLength.CompareTo(a.KeyLength));
    }

    private static bool IsShortTokenVar(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        switch (name.ToLowerInvariant())
        {
            case "player":
            case "name":
            case "npc":
            case "char":
            case "character":
            case "girl":
            case "boy":
            case "user":
                return true;
            default:
                return name.Length <= 8;
        }
    }

    private static bool TryMatchTemplate(string value, out string ru)
    {
        ru = null;
        // Templates are pre-sorted longest-first.
        for (int i = 0; i < _templates.Count; i++)
        {
            var t = _templates[i];
            Match m;
            try { m = t.Pattern.Match(value); } catch { continue; }
            if (!m.Success) continue;
            var args = new object[m.Groups.Count - 1];
            for (int g = 1; g < m.Groups.Count; g++) args[g - 1] = m.Groups[g].Value;
            try { ru = string.Format(t.Format, args); return true; }
            catch { ru = null; }
        }
        return false;
    }

    // ---------- minimal JSON object parser (string→string only) ----------

    private static void ParseStringMap(string s, Dictionary<string, string> into)
    {
        int i = 0;
        while (i < s.Length && s[i] != '{') i++;
        if (i >= s.Length) return;
        i++;
        while (i < s.Length)
        {
            while (i < s.Length && (char.IsWhiteSpace(s[i]) || s[i] == ',')) i++;
            if (i >= s.Length || s[i] == '}') break;
            if (s[i] != '"') { i++; continue; }
            string key = ReadJsonString(s, ref i);
            while (i < s.Length && (char.IsWhiteSpace(s[i]) || s[i] == ':')) i++;
            if (i >= s.Length || s[i] != '"') continue;
            string val = ReadJsonString(s, ref i);
            into[key] = val;
        }
    }

    private static string ReadJsonString(string s, ref int i)
    {
        // Assumes s[i] == '"'
        i++;
        var sb = new StringBuilder();
        while (i < s.Length)
        {
            char c = s[i++];
            if (c == '"') return sb.ToString();
            if (c == '\\' && i < s.Length)
            {
                char e = s[i++];
                switch (e)
                {
                    case '"': sb.Append('"'); break;
                    case '\\': sb.Append('\\'); break;
                    case '/': sb.Append('/'); break;
                    case 'n': sb.Append('\n'); break;
                    case 'r': sb.Append('\r'); break;
                    case 't': sb.Append('\t'); break;
                    case 'b': sb.Append('\b'); break;
                    case 'f': sb.Append('\f'); break;
                    case 'u':
                        if (i + 4 <= s.Length)
                        {
                            int cp = Convert.ToInt32(s.Substring(i, 4), 16);
                            sb.Append((char)cp);
                            i += 4;
                        }
                        break;
                    default: sb.Append(e); break;
                }
            }
            else sb.Append(c);
        }
        return sb.ToString();
    }
}