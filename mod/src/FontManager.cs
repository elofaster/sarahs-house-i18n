using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using BepInEx;
using HarmonyLib;
using Il2CppInterop.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace SarahsHouseI18n;

/// <summary>
/// Inject Cyrillic into the game's existing TMP fonts (same size/metrics).
/// </summary>
internal static class FontManager
{
    private static readonly HashSet<int> _processed = new();
    private static readonly HashSet<int> _loggedFace = new();
    private static bool _loggedDirs;
    private static bool _ready;
    private static int _okCount;
    private static int _failCount;
    // Cyrillic-capable TMP asset used as last-resort fallback.
    private static TMP_FontAsset _cyrillicDonor;

    private const string CyrillicSample =
        "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя" +
        "ІіЇїЄєҐґÉé—–…«»№";

    /// <summary>Drop cached glyph sets (rebuilt lazily after packs change).</summary>
    public static void InvalidateInjectChars()
    {
        _cjkChars = null;
        _cjkResetDone = false;
        _cjkDonor = null;
        _cjkDonorTried = false;
        _jpDonor = null;
        _jpDonorTried = false;
    }

    /// <summary>CJK / kana / hangul ranges — rendered via the CJK fallback font, not glyph injection.</summary>
    private static bool IsCjk(char c)
    {
        return (c >= 0x2E80 && c <= 0x2FDF)   // CJK radicals / Kangxi
            || (c >= 0x3040 && c <= 0x30FF)   // Hiragana + Katakana
            || (c >= 0x3100 && c <= 0x312F)   // Bopomofo
            || (c >= 0x3130 && c <= 0x318F)   // Hangul compatibility jamo
            || (c >= 0x31F0 && c <= 0x31FF)   // Katakana phonetic ext
            || (c >= 0x3400 && c <= 0x4DBF)   // CJK ext A
            || (c >= 0x4E00 && c <= 0x9FFF)   // CJK unified ideographs
            || (c >= 0xA960 && c <= 0xA97F)   // Hangul jamo ext A
            || (c >= 0xAC00 && c <= 0xD7AF)   // Hangul syllables + jamo ext B
            || (c >= 0xF900 && c <= 0xFAFF);  // CJK compatibility ideographs
    }

    // ---- CJK fallback font (Noto Sans CJK: covers zh + ja + ko) ----
    private const string CjkFontFile = "NotoSansCJKsc-Regular.otf";
    // Native Japanese donor: OS-installed "Noto Sans JP" (bundled at fonts/NotoSansJP-Regular.ttf).
    // Used only while 'ja' is active so kanji render in Japanese glyph forms instead of the
    // pan-Unicode Arial Unicode MS bundle. Falls back to the bundle if unavailable.
    private const string JpOsFontName = "Noto Sans JP";
    private static string _cjkChars;
    private static bool _cjkResetDone;
    private static TMP_FontAsset _cjkDonor;
    private static bool _cjkDonorTried;
    private static TMP_FontAsset _jpDonor;
    private static bool _jpDonorTried;

    /// <summary>Distinct CJK characters used across all installed packs (empty until a zh/ja/ko pack exists).</summary>
    private static string CjkChars()
    {
        if (_cjkChars != null) return _cjkChars;
        var set = new HashSet<char>();
        try
        {
            foreach (var code in LanguageManager.GetAvailableLanguages())
            {
                if (string.Equals(code, LanguageManager.English, StringComparison.OrdinalIgnoreCase)) continue;
                Dictionary<string, string> map;
                try { map = LanguageManager.LoadMapCached(code); } catch { continue; }
                if (map == null) continue;
                foreach (var kv in map)
                {
                    var v = kv.Value;
                    if (string.IsNullOrEmpty(v)) continue;
                    for (int i = 0; i < v.Length; i++)
                        if (IsCjk(v[i])) set.Add(v[i]);
                }
            }
        }
        catch (Exception e) { Plugin.Log.LogWarning($"CjkChars build: {e.Message}"); }
        var sb = new StringBuilder(set.Count);
        foreach (var c in set) sb.Append(c);
        _cjkChars = sb.ToString();
        if (_cjkChars.Length > 0)
            Plugin.Log.LogInfo($"CJK glyphs available to inject: {_cjkChars.Length}");
        return _cjkChars;
    }

    private static bool IsCjkActiveLanguage()
    {
        var c = LanguageManager.CurrentCode;
        return string.Equals(c, "zh", StringComparison.OrdinalIgnoreCase)
            || string.Equals(c, "ja", StringComparison.OrdinalIgnoreCase)
            || string.Equals(c, "ko", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>Distinct CJK characters used by a single language pack's values (for pre-baking a donor atlas).</summary>
    private static string CjkCharsForLanguage(string code)
    {
        var set = new HashSet<char>();
        try
        {
            var map = LanguageManager.LoadMapCached(code);
            if (map != null)
                foreach (var kv in map)
                {
                    var v = kv.Value;
                    if (string.IsNullOrEmpty(v)) continue;
                    for (int i = 0; i < v.Length; i++)
                        if (IsCjk(v[i])) set.Add(v[i]);
                }
        }
        catch (Exception e) { Plugin.Log.LogWarning($"CjkCharsForLanguage({code}): {e.Message}"); }
        var sb = new StringBuilder(set.Count);
        foreach (var c in set) sb.Append(c);
        return sb.ToString();
    }

    /// <summary>When a CJK language becomes active, reprocess fonts once so CJK glyphs get injected.</summary>
    private static void MaybeResetForCjk()
    {
        if (_cjkResetDone) return;
        if (!IsCjkActiveLanguage()) return;
        if (CjkChars().Length == 0) return;
        _cjkResetDone = true;
        _processed.Clear();
        Plugin.Log.LogInfo("CJK language active — reprocessing fonts to inject CJK glyphs");
    }

    // Prebuilt CJK TMP font asset packaged as an AssetBundle for this Unity version (Arial Unicode MS SDF).
    private const string CjkBundleFile = "arialuni_sdf_u2021";

    /// <summary>Build (once) a dynamic TMP font asset from the OS-installed "Noto Sans JP"
    /// so Japanese renders with native JP glyph forms. Returns null if the OS font is
    /// missing or asset creation fails (caller then falls back to the Arial Unicode bundle).</summary>
    private static TMP_FontAsset EnsureJpDonor()
    {
        if (_jpDonor != null) return _jpDonor;
        if (_jpDonorTried) return null;
        _jpDonorTried = true;
        try
        {
            Font src = null;
            try { src = Font.CreateDynamicFontFromOSFont(JpOsFontName, 48); }
            catch (Exception e) { Plugin.Log.LogWarning($"JP donor: CreateDynamicFontFromOSFont('{JpOsFontName}') threw: {e.Message}"); }
            if (src == null) { Plugin.Log.LogWarning($"JP donor: OS font '{JpOsFontName}' not available — using Arial Unicode bundle"); return null; }

            var fa = TMP_FontAsset.CreateFontAsset(
                src, 48, 5, GlyphRenderMode.SDFAA, 2048, 2048,
                AtlasPopulationMode.Dynamic, true);
            if (fa == null) { Plugin.Log.LogWarning("JP donor: CreateFontAsset returned null"); return null; }
            fa.name = "JpDonor_NotoSansJP";
            UnityEngine.Object.DontDestroyOnLoad(fa);
            try { if (fa.atlasPopulationMode != AtlasPopulationMode.Dynamic) fa.atlasPopulationMode = AtlasPopulationMode.Dynamic; } catch { /* ignore */ }
            // Pre-bake the Japanese glyph set so the atlas is populated up front. A dynamic
            // donor otherwise rasterizes lazily, and the first switch to ja shows tofu until a
            // re-switch (the prebuilt Arial Unicode bundle used by zh is already populated).
            try
            {
                var jc = CjkCharsForLanguage("ja");
                if (jc.Length > 0)
                {
                    fa.TryAddCharacters(jc, true);
                    Plugin.Log.LogInfo($"JP donor pre-baked {jc.Length} Japanese glyphs");
                }
            }
            catch (Exception e) { Plugin.Log.LogWarning($"JP donor pre-bake: {e.Message}"); }
            _jpDonor = fa;
            Plugin.Log.LogInfo($"JP donor created from OS font '{JpOsFontName}' (native Japanese glyph forms)");
            return fa;
        }
        catch (Exception e) { Plugin.Log.LogWarning($"EnsureJpDonor: {e.Message}"); return null; }
    }

    /// <summary>Load (once) a prebuilt CJK TMP font asset from the bundled AssetBundle and use it as the CJK fallback.</summary>
    private static TMP_FontAsset EnsureCjkDonor()
    {
        // Prefer a native Japanese donor while 'ja' is active (kanji in JP forms).
        if (string.Equals(LanguageManager.CurrentCode, "ja", StringComparison.OrdinalIgnoreCase))
        {
            var jp = EnsureJpDonor();
            if (jp != null) return jp;
            // else fall through to the shared Arial Unicode MS bundle
        }
        return EnsureArialUniDonor();
    }

    /// <summary>Load (once) the prebuilt pan-Unicode Arial Unicode MS SDF asset from the bundled
    /// AssetBundle. It covers Latin + Cyrillic + full CJK, so it serves as the universal fallback
    /// for EVERY non-English language. Returns null if the bundle is missing / fails to load.</summary>
    private static TMP_FontAsset EnsureArialUniDonor()
    {
        if (_cjkDonor != null) return _cjkDonor;
        if (_cjkDonorTried) return null;
        _cjkDonorTried = true;
        try
        {
            var dir = PluginDirectory();
            if (dir == null) return null;
            // Ships in fonts/; the pre-3.0 layout kept it in the plugin root.
            var path = Path.Combine(dir, "fonts", CjkBundleFile);
            if (!File.Exists(path)) path = Path.Combine(dir, CjkBundleFile);
            if (!File.Exists(path)) { Plugin.Log.LogWarning($"CJK bundle missing: {path}"); return null; }

            AssetBundle bundle;
            try { bundle = AssetBundle.LoadFromFile(path); }
            catch (Exception e) { Plugin.Log.LogWarning($"CJK bundle LoadFromFile: {e.Message}"); return null; }
            if (bundle == null) { Plugin.Log.LogWarning("CJK bundle: LoadFromFile returned null"); return null; }

            TMP_FontAsset fa = null;
            try
            {
                var assets = bundle.LoadAllAssets(Il2CppType.Of<TMP_FontAsset>());
                if (assets != null)
                    for (int i = 0; i < assets.Length; i++)
                    {
                        var cand = assets[i] == null ? null : assets[i].TryCast<TMP_FontAsset>();
                        if (cand != null) { fa = cand; break; }
                    }
            }
            catch (Exception e) { Plugin.Log.LogWarning($"CJK bundle LoadAllAssets: {e.Message}"); }

            if (fa == null) { Plugin.Log.LogWarning($"CJK bundle '{CjkBundleFile}': no TMP_FontAsset inside"); return null; }
            UnityEngine.Object.DontDestroyOnLoad(fa);
            try { if (fa.atlasPopulationMode != AtlasPopulationMode.Dynamic) fa.atlasPopulationMode = AtlasPopulationMode.Dynamic; } catch { /* ignore */ }
            int cc = 0; try { cc = fa.characterTable.Count; } catch { /* ignore */ }
            _cjkDonor = fa;
            Plugin.Log.LogInfo($"Universal donor loaded from bundle '{CjkBundleFile}' ('{fa.name}', chars={cc})");
            return fa;
        }
        catch (Exception e) { Plugin.Log.LogWarning($"EnsureArialUniDonor: {e.Message}"); return null; }
    }

    // ---- Global TMP fallback (the robust, canonical fix) --------------------------------------
    // Instead of guessing a per-game-font Cyrillic donor, register a complete pan-Unicode font in
    // TMP's GLOBAL fallback list (TMP_Settings.fallbackFontAssets). Every TMP text then resolves
    // any missing Cyrillic/CJK glyph from it — no ordering, no empty-donor, no per-font guessing.
    private static bool _globalRuAdded;
    private static bool _globalJpAdded;
    private static float _lastRefresh;

    private static bool AddToGlobalFallback(TMP_FontAsset fa, bool front)
    {
        if (fa == null) return false;
        try
        {
            // Getter returns the live backing list; mutate it in place (the property is read-only).
            var list = TMP_Settings.fallbackFontAssets;
            if (list == null)
            {
                Plugin.Log.LogWarning("Global fallback unavailable (TMP_Settings.fallbackFontAssets is null); per-font donor still applies.");
                return false;
            }
            for (int i = 0; i < list.Count; i++)
                if (list[i] != null && list[i].GetInstanceID() == fa.GetInstanceID()) return false;
            if (front) list.Insert(0, fa); else list.Add(fa);
            return true;
        }
        catch (Exception e) { Plugin.Log.LogWarning($"AddToGlobalFallback('{fa.name}'): {e.Message}"); return false; }
    }

    /// <summary>Register the pan-Unicode Arial Unicode donor (and, for Japanese, the native JP
    /// donor ahead of it) in TMP's global fallback list so ALL text — menu, dialogue, the launch
    /// disclaimer — resolves Cyrillic/CJK glyphs. Refreshes rendered text the first time it changes
    /// the list. Cheap no-op once established for the active language.</summary>
    public static void EnsureGlobalFallback()
    {
        try
        {
            if (LanguageManager.IsEnglish) return; // English uses the game's own glyphs
            bool changed = false;

            var arial = EnsureArialUniDonor();     // Latin + Cyrillic + CJK
            if (arial != null && !_globalRuAdded)
            {
                if (AddToGlobalFallback(arial, front: false)) changed = true;
                _globalRuAdded = true;
            }

            // Japanese: put the native NotoSansJP donor AHEAD of Arial Unicode so kanji use JP forms.
            if (string.Equals(LanguageManager.CurrentCode, "ja", StringComparison.OrdinalIgnoreCase) && !_globalJpAdded)
            {
                var jp = EnsureJpDonor();
                if (jp != null)
                {
                    if (AddToGlobalFallback(jp, front: true)) changed = true;
                    _globalJpAdded = true;
                }
            }

            if (changed)
            {
                Plugin.Log.LogInfo("Global TMP fallback updated — refreshing rendered text.");
                RefreshAllText();
            }
        }
        catch (Exception e) { Plugin.Log.LogWarning($"EnsureGlobalFallback: {e.Message}"); }
    }

    /// <summary>Force already-rendered TMP text to re-resolve missing glyphs against the current
    /// fallback fonts. Clearing the glyph cache is the KEY step: ForceMeshUpdate alone does NOT
    /// re-check fallbacks (confirmed by Unity), which is why earlier refreshes failed. Rate-limited.</summary>
    public static void RefreshAllText()
    {
        try
        {
            float now = 0f; try { now = Time.unscaledTime; } catch { }
            if (now > 0f && now - _lastRefresh < 0.25f) return;
            _lastRefresh = now;

            var all = Resources.FindObjectsOfTypeAll(Il2CppType.Of<TMP_Text>());
            int n = 0;
            for (int i = 0; i < all.Length; i++)
            {
                var t = all[i].TryCast<TMP_Text>();
                if (t == null) continue;
                try
                {
                    if (!t.isActiveAndEnabled) continue;
                    if (string.IsNullOrEmpty(t.text)) continue;
                    // forceTextReparsing:true re-runs glyph resolution against the current fallback
                    // chain — this is the step my earlier ForceMeshUpdate(false,false) lacked, and
                    // this game's TMP has no TMP_ResourceManager.ClearFontAssetGlyphCache to call.
                    t.SetAllDirty();
                    t.ForceMeshUpdate(true, true);
                    n++;
                }
                catch { /* ignore per-object */ }
            }
            if (Plugin.Config.VerboseLogging.Value)
                Plugin.Log.LogInfo($"RefreshAllText: {n} text objects refreshed");
        }
        catch (Exception e) { Plugin.Log.LogWarning($"RefreshAllText: {e.Message}"); }
    }

    /// <summary>Attach the CJK donor as a TMP fallback so zh/ja/ko glyphs resolve (only while a CJK language is active).</summary>
    private static void InjectCjkInto(TMP_FontAsset fa, string name)
    {
        if (fa == null || !IsCjkActiveLanguage()) return;
        if (string.IsNullOrEmpty(CjkChars())) return;
        try
        {
            var donor = EnsureCjkDonor();
            if (donor == null) return;
            if (fa.GetInstanceID() == donor.GetInstanceID()) return;

            var list = fa.fallbackFontAssetTable;
            if (list == null) return;
            for (int i = 0; i < list.Count; i++)
                if (list[i] != null && list[i].GetInstanceID() == donor.GetInstanceID())
                    return;
            list.Insert(0, donor);   // front of fallback list so the active-language donor wins
            Plugin.Log.LogInfo($"TMP '{name}': CJK via donor fallback '{donor.name}'");
        }
        catch (Exception e) { Plugin.Log.LogWarning($"InjectCjkInto '{name}': {e.Message}"); }
    }

    /// <summary>True if any game font accepted Cyrillic glyphs.</summary>
    public static bool IsReady => _ready;

    /// <summary>Build + attach the CJK/JP donor synchronously for the active language BEFORE
    /// text is re-applied on a language switch. Prevents the first-switch tofu for the dynamic
    /// ja/ko donor whose atlas would otherwise populate too late. No-op for non-CJK languages.</summary>
    public static void PrepareForActiveLanguage()
    {
        try
        {
            // Register the global pan-Unicode fallback for the active language up front so the
            // re-applied text renders with real glyphs immediately (no first-switch tofu).
            if (!LanguageManager.IsEnglish) EnsureGlobalFallback();
            if (!IsCjkActiveLanguage()) return;
            _cjkResetDone = false;   // let MaybeResetForCjk re-run so donors reattach this pass
            _processed.Clear();
            EnsureCjkDonor();        // build (and, for ja, pre-bake) the donor now
            ApplyToScene();          // attach donor to all current TMP assets before text re-apply
        }
        catch (Exception e) { Plugin.Log.LogWarning($"PrepareForActiveLanguage: {e.Message}"); }
    }

    public static void ApplyToScene()
    {
        try
        {
            LogDirsOnce();
            EnsureSourceFontsHaveCyrillic();
            MaybeResetForCjk();
            // Primary fix: a complete pan-Unicode font in TMP's GLOBAL fallback so every text
            // resolves Cyrillic/CJK regardless of its own font. Cheap no-op once established.
            EnsureGlobalFallback();

            // Bound the processed-id cache so long sessions with many transient
            // TMP assets don't grow it without limit (re-processing is idempotent).
            if (_processed.Count > 20000) { _processed.Clear(); _loggedFace.Clear(); }

            var all = Resources.FindObjectsOfTypeAll(Il2CppType.Of<TMP_FontAsset>());
            int touched = 0;
            for (int i = 0; i < all.Length; i++)
            {
                var fa = all[i].TryCast<TMP_FontAsset>();
                if (fa == null) continue;
                if (ProcessFontAsset(fa)) touched++;
            }

            if (!_ready && _okCount > 0) _ready = true;

            if (Plugin.Config.VerboseLogging.Value)
                Plugin.Log.LogInfo(
                    $"Font pass: assets={all.Length}, touched={touched}, " +
                    $"ok={_okCount}, fail={_failCount}, ready={_ready}");
        }
        catch (Exception e)
        {
            Plugin.Log.LogError($"FontManager.ApplyToScene failed: {e}");
        }
    }

    /// <summary>No-op for older call sites (fonts are not swapped).</summary>
    public static void ApplyPrimary(TMP_Text tmp) { /* intentionally empty */ }

    public static void ApplyPrimary(UnityEngine.UI.Text ui) { /* intentionally empty */ }

    public static void EnsurePrimaryForText(TMP_Text tmp, string text) { /* intentionally empty */ }

    internal static void ProcessFontAssetPublic(TMP_FontAsset fa) => ProcessFontAsset(fa);

    // ------------------------------------------------------------- font swapping

    /// <summary>Original font per text component, so a switch back to Latin can restore it.</summary>
    private static readonly Dictionary<int, TMP_FontAsset> _swappedBack = new();

    private static bool SwapEnabled
    {
        get
        {
            try
            {
                return (Plugin.Config.UseDonorBundles?.Value ?? false)
                    && (Plugin.Config.SwapFonts?.Value ?? true);
            }
            catch { return false; }
        }
    }

    /// <summary>
    /// Replace the font on a text component with our baked equivalent, instead of hanging it
    /// off the fallback table.
    ///
    /// A fallback only supplies the glyphs the host lacks, so a line ends up split across two
    /// faces with two sets of metrics — which is exactly why Cyrillic looked wrong next to the
    /// game's own Latin. Our bundles carry Latin *and* Cyrillic in the matching design, so the
    /// component can render the whole string from one asset. This is what XUnity.AutoTranslator
    /// does with OverrideFontTextMeshPro, and it is only safe because the swap targets our own
    /// prebaked assets, never an in-game one (those have no Cyrillic and would break Latin).
    /// </summary>
    internal static void ApplyFontSwap(TMP_Text t)
    {
        if (!SwapEnabled || t == null) return;

        try
        {
            int id = t.GetInstanceID();
            var current = t.font;
            string name = current != null ? (current.name ?? "") : "";

            if (!NeedsSubstitute(LanguageManager.CurrentCode))
            {
                // back on a Latin language: undo our swap so the game looks stock again
                if (_swappedBack.TryGetValue(id, out var original) && original != null)
                {
                    t.font = original;
                    _swappedBack.Remove(id);
                }
                return;
            }

            if (IsOurDonorAsset(name)) return;      // already swapped
            if (current == null) return;

            var donor = DonorForHost(name);
            if (donor == null) return;
            if (!IsOurDonorAsset(donor.name ?? "")) return;   // only our baked assets are complete
            if (donor.GetInstanceID() == current.GetInstanceID()) return;

            if (!_swappedBack.ContainsKey(id)) _swappedBack[id] = current;
            t.font = donor;

            if (_fallbackLogged.Add("swap|" + name + "|" + donor.name))
                Plugin.Log.LogInfo($"Font swap: '{name}' -> '{donor.name}'");
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"ApplyFontSwap: {e.Message}");
        }
    }

    /// <summary>
    /// True for the prebaked assets this mod ships in its own bundles. They arrive complete —
    /// atlas, glyph table, metrics — and have no source font, so flipping them to Dynamic or
    /// calling TryAddCharacters on them only invalidates what is already correct.
    /// </summary>
    private static bool IsOurDonorAsset(string name) =>
        !string.IsNullOrEmpty(name) && name.IndexOf("(i18n)", StringComparison.OrdinalIgnoreCase) >= 0;

    private static bool ProcessFontAsset(TMP_FontAsset fa)
    {
        if (fa == null) return false;
        int id = fa.GetInstanceID();
        if (!_processed.Add(id)) return false;

        string name = "?";
        try { name = fa.name ?? "?"; } catch { /* ignore */ }

        // Skip TMP internals / multi-atlas clones
        if (name.StartsWith("TMP_Font Asset", StringComparison.OrdinalIgnoreCase))
            return false;

        // Our own prebaked donors must be left exactly as shipped.
        if (IsOurDonorAsset(name))
        {
            Plugin.Log.LogInfo($"TMP '{name}': prebaked donor, left untouched");
            return false;
        }

        try
        {
            LogFaceOnce(fa, name);

            // Dynamic atlas for runtime glyphs
            try
            {
                if (fa.atlasPopulationMode != AtlasPopulationMode.Dynamic)
                {
                    fa.atlasPopulationMode = AtlasPopulationMode.Dynamic;
                    try { fa.atlasRenderMode = GlyphRenderMode.SDFAA; } catch { /* ignore */ }
                    Plugin.Log.LogInfo($"TMP '{name}': Static → Dynamic");
                }
            }
            catch (Exception e)
            {
                Plugin.Log.LogWarning($"TMP '{name}': cannot set Dynamic: {e.Message}");
            }

            // Bake Cyrillic into this asset
            bool added = false;
            try
            {
                added = fa.TryAddCharacters(CyrillicSample, true);
            }
            catch (Exception e)
            {
                Plugin.Log.LogWarning($"TMP '{name}': TryAddCharacters threw: {e.Message}");
            }

            if (added)
            {
                _okCount++;
                _ready = true;
                RememberDonor(fa);
                Plugin.Log.LogInfo($"TMP '{name}': Cyrillic glyphs OK (face pt={SafePt(fa)})");
            }
            else
            {
                // The source face lacks Cyrillic. A static atlas cannot be fed an
                // external font (FontEngine.LoadFontFace never reaches TryAddCharacters),
                // so route the asset to a matching donor instead.
                if (AttachDonorFallback(fa, name))
                {
                    _okCount++;
                    _ready = true;
                }
                else
                {
                    // No own Cyrillic and no verified donor available YET (the donor font may load
                    // a frame later). Allow a bounded number of retries so this font picks up the
                    // donor once it exists, instead of being permanently stuck as tofu.
                    int tries = _noCyrRetry.TryGetValue(id, out var rc) ? rc : 0;
                    if (tries < 10)
                    {
                        _noCyrRetry[id] = tries + 1;
                        _processed.Remove(id); // re-process on the next pass
                    }
                    else
                    {
                        _failCount++;
                        Plugin.Log.LogWarning(
                            $"TMP '{name}': no Cyrillic glyphs (source font missing coverage). " +
                            $"Text using only this font may show tofu.");
                    }
                }
            }

            // Inject CJK (zh/ja/ko) glyphs when a CJK language is active.
            InjectCjkInto(fa, name);

            return true;
        }
        catch (Exception e)
        {
            _failCount++;
            Plugin.Log.LogError($"TMP '{name}': ProcessFontAsset failed: {e}");
            return false;
        }
    }

    private static int SafePt(TMP_FontAsset fa)
    {
        try { return fa.faceInfo.pointSize; } catch { return -1; }
    }

    private static void LogFaceOnce(TMP_FontAsset fa, string name)
    {
        int id = fa.GetInstanceID();
        if (!_loggedFace.Add(id)) return;
        try
        {
            FaceInfo f = fa.faceInfo;
            Plugin.Log.LogInfo(
                $"TMP '{name}' faceInfo: pt={f.pointSize} scale={f.scale} " +
                $"lineH={f.lineHeight} ascent={f.ascentLine} descent={f.descentLine} " +
                $"mode={fa.atlasPopulationMode}");
        }
        catch (Exception e)
        {
            Plugin.Log.LogInfo($"TMP '{name}': (faceInfo unreadable: {e.Message})");
        }
    }

    /// <summary>
    /// Language groups that decide whether a game face can be used as-is.
    /// Latin-only languages get the game's own typeface; anything needing Cyrillic —
    /// and Vietnamese, whose stacked diacritics several display faces only half cover —
    /// gets a substitute with the same voice.
    /// </summary>
    private static bool NeedsSubstitute(string code)
    {
        code = (code ?? "").ToLowerInvariant();
        switch (code)
        {
            case "ru":
            case "uk":
            case "be":
            case "bg":
            case "sr":
            case "mk":
            case "kk":
            case "vi":
            case "zh":
            case "ja":
            case "ko":
                return true;
            default:
                return false;   // en, de, es, fr, pl, pt, tr, it, cs — Latin faces cover these
        }
    }

    private static bool _fontsInventoryDone;

    /// <summary>
    /// Runtime TTF swap is not supported; log coverage once (inventory only).
    /// </summary>
    private static void EnsureSourceFontsHaveCyrillic()
    {
        if (_fontsInventoryDone) return;
        try
        {
            var all = Resources.FindObjectsOfTypeAll(Il2CppType.Of<Font>());
            if (all == null || all.Length == 0) return; // retry next pass until fonts exist
            _fontsInventoryDone = true;
            for (int i = 0; i < all.Length; i++)
            {
                var f = all[i].TryCast<Font>();
                if (f == null) continue;
                bool has = false;
                try { has = f.HasCharacter('Ж') || f.HasCharacter('А'); } catch { has = false; }
                Plugin.Log.LogInfo($"Unity Font '{f.name}': HasCyrillic={has}");
            }
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"List Unity Fonts: {e.Message}");
        }
    }


    private static void RememberDonor(TMP_FontAsset fa)
    {
        // Only remember a donor that VERIFIABLY has Cyrillic. Bakbak One ships no Cyrillic
        // coverage, so it must never become the donor — attaching its empty atlas as a fallback
        // was the whole tofu bug. Never replace a good donor with another; only fill/repair.
        if (fa == null || !DonorHasCyrillic(fa)) return;
        if (_cyrillicDonor == null || !DonorHasCyrillic(_cyrillicDonor))
            _cyrillicDonor = fa;
    }

    /// <summary>True only if this asset actually contains Cyrillic glyphs (source font covers
    /// Cyrillic and they baked into the atlas). Rejects empty donors like Bakbak One.</summary>
    private static bool DonorHasCyrillic(TMP_FontAsset fa)
    {
        if (fa == null) return false;
        try { return fa.HasCharacter('А') && fa.HasCharacter('я') && fa.HasCharacter('Ж'); }
        catch { return false; }
    }

    // ------------------------------------------------------------- per-host donors

    /// <summary>
    /// Every in-game TMP asset that can actually rasterise Cyrillic, by asset name.
    ///
    /// Why a pool instead of the bundled TTFs: TMP's <c>TryAddCharacters</c> always
    /// rasterises from the asset's OWN source font, so a TTF loaded through
    /// <c>FontEngine.LoadFontFace</c> is silently discarded — the game's static atlases
    /// cannot be fed an external face. The only lever that changes how Cyrillic *looks* is
    /// which asset lands in the host's fallback table, so pick that per host instead of
    /// dropping one Arial Unicode on everything.
    /// </summary>
    private static readonly Dictionary<string, TMP_FontAsset> _donorPool = new(StringComparer.OrdinalIgnoreCase);
    private static float _nextPoolScan;
    private static readonly HashSet<string> _fallbackLogged = new(StringComparer.Ordinal);

    // Heavy/condensed hosts want a heavy donor; body text wants the regular grotesque.
    private static readonly string[] DisplayDonorPrefs =
        { "Oswald", "Geologica", "Roboto-Bold", "RobotoBold", "Anton", "Roboto", "ARIALUNI", "Liberation", "Noto" };
    private static readonly string[] ChunkyDonorPrefs =
        { "Geologica", "Oswald", "Roboto-Bold", "Roboto", "ARIALUNI", "Liberation" };

    private static bool IsChunkyHost(string name) =>
        !string.IsNullOrEmpty(name) && name.IndexOf("Bakbak", StringComparison.OrdinalIgnoreCase) >= 0;
    private static readonly string[] BodyDonorPrefs =
        { "RobotoDialog", "Roboto", "Lato", "Liberation", "ARIALUNI", "Noto", "Oswald" };
    private static readonly string[] HandDonorPrefs =
        { "Caveat", "Segoe Script", "Lato", "Roboto", "ARIALUNI" };

    private static bool IsHandHost(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        return name.IndexOf("kinkie", StringComparison.OrdinalIgnoreCase) >= 0
            || name.IndexOf("Eraser", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static bool IsDisplayHost(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        return name.IndexOf("Anton", StringComparison.OrdinalIgnoreCase) >= 0
            || name.IndexOf("Bakbak", StringComparison.OrdinalIgnoreCase) >= 0
            || name.IndexOf("Oswald", StringComparison.OrdinalIgnoreCase) >= 0
            || name.IndexOf("Shadow", StringComparison.OrdinalIgnoreCase) >= 0
            || name.IndexOf("Outline", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    // --------------------------------------------------- prebuilt donor bundles

    private static bool _extraBundlesLoaded;

    /// <summary>
    /// Load every prebuilt TMP font-asset bundle from <c>fonts/</c> (files named
    /// <c>*_sdf_u2021</c>) and register them in the donor pool.
    ///
    /// This is the route that actually works on Unity 2021 + old TMP: the asset is baked
    /// ahead of time — atlas, glyph table, metrics — so nothing has to be rasterised at
    /// runtime from a face TMP refuses to read. The bundles are generated by
    /// <c>tools/make_tmp_bundle.py</c> (msdf-atlas-gen + UnityPy), not by hand.
    /// </summary>
    private static void LoadExtraDonorBundles()
    {
        if (_extraBundlesLoaded) return;
        _extraBundlesLoaded = true;
        try
        {
            bool enabled = false;
            try { enabled = Plugin.Config.UseDonorBundles?.Value ?? false; } catch { }
            if (!enabled)
            {
                Plugin.Log.LogInfo("Donor bundles disabled ([Fonts] UseDonorBundles=false)");
                return;
            }
            var dir = PluginDirectory();
            if (dir == null) return;
            var fonts = Path.Combine(dir, "fonts");
            if (!Directory.Exists(fonts)) return;

            foreach (var path in Directory.GetFiles(fonts))
            {
                var file = Path.GetFileName(path);
                if (file.IndexOf("_sdf_", StringComparison.OrdinalIgnoreCase) < 0) continue;
                if (file.StartsWith("arialuni", StringComparison.OrdinalIgnoreCase)) continue; // CJK path owns it
                if (file.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase)) continue;

                AssetBundle bundle;
                try { bundle = AssetBundle.LoadFromFile(path); }
                catch (Exception e)
                {
                    Plugin.Log.LogWarning($"Donor bundle '{file}': LoadFromFile: {e.Message}");
                    continue;
                }
                if (bundle == null)
                {
                    Plugin.Log.LogWarning($"Donor bundle '{file}': not a loadable bundle");
                    continue;
                }

                try
                {
                    var assets = bundle.LoadAllAssets(Il2CppType.Of<TMP_FontAsset>());
                    int added = 0;
                    if (assets != null)
                    {
                        for (int i = 0; i < assets.Length; i++)
                        {
                            var fa = assets[i]?.TryCast<TMP_FontAsset>();
                            if (fa == null) continue;
                            string n = fa.name ?? file;

                            // A donor with a broken material or a missing atlas does not just
                            // fail to draw its own glyphs — TMP renders fallback glyphs in a
                            // separate submesh, and an invalid one can take the whole text mesh
                            // down with it. That is how the game ended up with no letters at all.
                            bool sane;
                            try
                            {
                                sane = fa.material != null
                                       && fa.atlasTexture != null
                                       && DonorHasCyrillic(fa);
                            }
                            catch { sane = false; }

                            if (!sane)
                            {
                                Plugin.Log.LogWarning(
                                    $"Donor bundle '{file}' -> '{n}': rejected " +
                                    $"(material={fa.material != null}, atlas={fa.atlasTexture != null})");
                                continue;
                            }

                            UnityEngine.Object.DontDestroyOnLoad(fa);
                            _donorPool[n] = fa;
                            added++;
                            Plugin.Log.LogInfo($"Donor bundle '{file}' -> '{n}' accepted");
                        }
                    }
                    if (added == 0)
                        Plugin.Log.LogWarning($"Donor bundle '{file}': no TMP_FontAsset inside");
                }
                catch (Exception e)
                {
                    Plugin.Log.LogWarning($"Donor bundle '{file}': {e.Message}");
                }
            }
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"LoadExtraDonorBundles: {e.Message}");
        }
    }

    private static void ScanDonorPool()
    {
        try
        {
            LoadExtraDonorBundles();
            if (_donorPool.Count > 0 && Time.unscaledTime < _nextPoolScan) return;
            _nextPoolScan = Time.unscaledTime + 5f;

            var all = Resources.FindObjectsOfTypeAll(Il2CppType.Of<TMP_FontAsset>());
            if (all == null) return;
            for (int i = 0; i < all.Length; i++)
            {
                var fa = all[i].TryCast<TMP_FontAsset>();
                if (fa == null) continue;
                string n = fa.name ?? "";
                if (n.Length == 0) continue;
                if (n.StartsWith("TMP_Font Asset", StringComparison.OrdinalIgnoreCase)) continue;
                if (n.StartsWith("CyrDonor_", StringComparison.Ordinal)) continue;
                if (_donorPool.ContainsKey(n)) continue;

                if (!IsOurDonorAsset(n))
                {
                    try
                    {
                        if (fa.atlasPopulationMode != AtlasPopulationMode.Dynamic)
                            fa.atlasPopulationMode = AtlasPopulationMode.Dynamic;
                        fa.TryAddCharacters(CyrillicSample, true);
                    }
                    catch { /* ignore */ }
                }

                if (DonorHasCyrillic(fa))
                {
                    _donorPool[n] = fa;
                    Plugin.Log.LogInfo($"Cyrillic-capable asset found: '{n}'");
                }
            }
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"ScanDonorPool: {e.Message}");
        }
    }

    /// <summary>Best available Cyrillic donor for this host, by typographic role.</summary>
    private static TMP_FontAsset DonorForHost(string hostName)
    {
        ScanDonorPool();
        if (_donorPool.Count == 0) return _cyrillicDonor;

        var prefs = IsHandHost(hostName) ? HandDonorPrefs
                  : IsChunkyHost(hostName) ? ChunkyDonorPrefs
                  : IsDisplayHost(hostName) ? DisplayDonorPrefs
                  : BodyDonorPrefs;
        foreach (var want in prefs)
        {
            foreach (var kv in _donorPool)
            {
                if (kv.Value == null) continue;
                if (kv.Key.IndexOf(want, StringComparison.OrdinalIgnoreCase) < 0) continue;
                // never let a host fall back to itself
                if (string.Equals(kv.Key, hostName, StringComparison.Ordinal)) continue;
                return kv.Value;
            }
        }
        // Last resort: anything except a handwriting face, which would look absurd on body
        // text — this is exactly what happened when only the Caveat bundle managed to load.
        bool wantHand = IsHandHost(hostName);
        foreach (var kv in _donorPool)
        {
            if (kv.Value == null) continue;
            if (string.Equals(kv.Key, hostName, StringComparison.Ordinal)) continue;
            bool isHand = kv.Key.IndexOf("Caveat", StringComparison.OrdinalIgnoreCase) >= 0
                          || kv.Key.IndexOf("Script", StringComparison.OrdinalIgnoreCase) >= 0;
            if (isHand != wantHand) continue;
            return kv.Value;
        }
        return _cyrillicDonor;
    }

    private static bool AttachDonorFallback(TMP_FontAsset host, string name)
    {
        try
        {
            EnsureDonor();
            if (host == null) return false;

            // Role-matched donor from the pool (our prebaked bundles + verified in-game
            // assets); the plain Cyrillic donor is the last resort.
            TMP_FontAsset donor = DonorForHost(name) ?? _cyrillicDonor;
            if (donor == null) return false;
            if (host.GetInstanceID() == donor.GetInstanceID()) return false;

            if (_fallbackLogged.Add(name + "|" + (donor.name ?? "")))
            {
                Plugin.Log.LogInfo(
                    $"Cyrillic fallback for '{name}' -> '{donor.name}' " +
                    $"({(IsDisplayHost(name) ? "display" : "body")})");
            }

            int hostPt = SafePt(host);

            var list = host.fallbackFontAssetTable;
            if (list == null) return false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null && list[i].GetInstanceID() == donor.GetInstanceID())
                {
                    Plugin.Log.LogInfo($"TMP '{name}': donor fallback already set ('{donor.name}')");
                    return true;
                }
            }
            list.Add(donor);
            Plugin.Log.LogInfo(
                $"TMP '{name}': Cyrillic via donor fallback '{donor.name}' " +
                $"(hostPt={hostPt}, donorPt={SafePt(donor)})");
            return true;
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"AttachDonorFallback '{name}': {e.Message}");
            return false;
        }
    }

    private static bool _donorSearchWarned;

    private static void EnsureDonor()
    {
        // Keep an existing donor only if it truly has Cyrillic; otherwise re-search. This repairs
        // a bad donor that may have been remembered before a Cyrillic-capable font loaded.
        if (_cyrillicDonor != null && DonorHasCyrillic(_cyrillicDonor)) return;
        _cyrillicDonor = null;
        try
        {
            var all = Resources.FindObjectsOfTypeAll(Il2CppType.Of<TMP_FontAsset>());
            // Families whose source font actually ships Cyrillic. Bakbak One does NOT — skipping
            // it prevents its empty atlas from becoming a useless fallback (the tofu bug).
            string[] preferred = { "Roboto", "Oswald", "Liberation", "Noto", "Anton", "Arial", "DejaVu" };
            // Round 0: preferred families first; round 1: accept any asset that verifies.
            for (int round = 0; round < 2; round++)
            {
                for (int i = 0; i < all.Length; i++)
                {
                    var fa = all[i].TryCast<TMP_FontAsset>();
                    if (fa == null) continue;
                    string n = fa.name ?? "";
                    if (n.StartsWith("TMP_Font Asset", StringComparison.OrdinalIgnoreCase)) continue;
                    if (n.IndexOf("Bakbak", StringComparison.OrdinalIgnoreCase) >= 0) continue; // no Cyrillic
                    if (round == 0)
                    {
                        bool isPreferred = false;
                        for (int p = 0; p < preferred.Length; p++)
                            if (n.IndexOf(preferred[p], StringComparison.OrdinalIgnoreCase) >= 0) { isPreferred = true; break; }
                        if (!isPreferred) continue;
                    }
                    try
                    {
                        if (fa.atlasPopulationMode != AtlasPopulationMode.Dynamic)
                            fa.atlasPopulationMode = AtlasPopulationMode.Dynamic;
                        fa.TryAddCharacters(CyrillicSample, true);
                    }
                    catch { /* ignore */ }
                    if (DonorHasCyrillic(fa))
                    {
                        _cyrillicDonor = fa;
                        _donorSearchWarned = false;
                        Plugin.Log.LogInfo($"Cyrillic donor TMP: '{fa.name}' (verified)");
                        return;
                    }
                }
            }
            if (!_donorSearchWarned)
            {
                _donorSearchWarned = true;
                Plugin.Log.LogWarning("EnsureDonor: no Cyrillic-capable TMP asset loaded yet — will retry.");
            }
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"EnsureDonor: {e.Message}");
        }
    }

    // Per-asset retry counter for fonts that had no Cyrillic and no verified donor yet.
    private static readonly Dictionary<int, int> _noCyrRetry = new();

    private static string PluginDirectory()
    {
        try
        {
            var loc = typeof(Plugin).Assembly.Location;
            if (string.IsNullOrEmpty(loc))
                loc = Assembly.GetExecutingAssembly().Location;
            if (!string.IsNullOrEmpty(loc))
            {
                var d = Path.GetDirectoryName(loc);
                if (!string.IsNullOrEmpty(d) && Directory.Exists(d)) return d;
            }
        }
        catch { /* ignore */ }
        try
        {
            var d = Path.Combine(Paths.PluginPath, "SarahsHouseI18n");
            if (Directory.Exists(d)) return d;
        }
        catch { /* ignore */ }
        return null;
    }

    private static void LogDirsOnce()
    {
        if (_loggedDirs) return;
        _loggedDirs = true;
        var d = PluginDirectory();
        Plugin.Log.LogInfo($"Plugin directory: '{d ?? "<null>"}'");
        if (d == null) return;
        var fonts = Path.Combine(d, "fonts");
        Plugin.Log.LogInfo($"fonts/ exists={Directory.Exists(fonts)}");
        if (!Directory.Exists(fonts)) return;
        try
        {
            foreach (var f in Directory.GetFiles(fonts, "*.ttf"))
                Plugin.Log.LogInfo($"  ttf: {Path.GetFileName(f)}");
        }
        catch { /* ignore */ }
    }
}

/// <summary>Late-loaded TMP assets get the same in-place Cyrillic injection.</summary>
[HarmonyPatch]
internal static class FontPatches
{
    [HarmonyPatch(typeof(TMP_FontAsset), nameof(TMP_FontAsset.ReadFontAssetDefinition))]
    [HarmonyPostfix]
    public static void Postfix_ReadFontAssetDefinition(TMP_FontAsset __instance)
    {
        try { FontManager.ProcessFontAssetPublic(__instance); }
        catch (Exception e) { Plugin.Log.LogError($"FontPatches: {e}"); }
    }
}