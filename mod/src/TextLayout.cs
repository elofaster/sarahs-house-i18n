using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SarahsHouseI18n;

/// <summary>
/// Fit translated text size on first paint (no delayed autosize).
/// </summary>
internal static class TextLayout
{
    private static readonly Dictionary<int, float> _tmpDesign = new();
    private static readonly Dictionary<int, int> _uiDesign = new();
    private static readonly HashSet<int> _logged = new();

    // Re-entrancy guard for fontSize writes.
    private static bool _busy;

    /// <summary>Set fontSize before assigning translated text.</summary>
    public static void PrepareBeforeSet(TMP_Text tmp, string oldText, string newText)
    {
        if (_busy || tmp == null || string.IsNullOrEmpty(newText)) return;
        try
        {
            if ((UnityEngine.Object)tmp == null) return;

            // Don't resize streaming dialogue lines.
            if (!Plugin.Config.ResizeDialogText.Value && IsDialogueText(tmp))
                return;

            string name;
            try { name = tmp.name ?? ""; } catch { return; }
            if (IsNonLabelName(name)) return;
            if (!IsSaneText(newText)) return;

            _busy = true;

            float design = CaptureDesignTmp(tmp);
            if (design < 1f || design > 500f) return;

            // Resize only large titles or much longer strings.
            float target = ComputeTargetSize(design, oldText, newText);
            if (target < 1f) target = design;

            // One-word titles shrink; long multi-word titles wrap.
            try
            {
                if (IsSingleToken(newText))
                {
                    tmp.enableWordWrapping = false;
                    // No wrap + Truncate/Ellipsis would cut the word — let it
                    // overflow instead (it is shrunk to fit anyway).
                    var om = tmp.overflowMode;
                    if (om == TextOverflowModes.Truncate || om == TextOverflowModes.Ellipsis)
                        tmp.overflowMode = TextOverflowModes.Overflow;
                }
                else if (SafeLen(newText) >= 18 && design >= 40f)
                {
                    tmp.enableWordWrapping = true;
                }
            }
            catch { /* ignore */ }

            if (Mathf.Abs(tmp.fontSize - target) >= 0.05f)
            {
                try { tmp.fontSize = target; }
                catch { return; }
            }

            LogOnce(tmp.GetInstanceID(), name, design, target, newText.Length);
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"TextLayout.Prepare TMP: {e.GetType().Name}: {e.Message}");
        }
        finally
        {
            _busy = false;
        }
    }

    public static void PrepareBeforeSet(UnityEngine.UI.Text ui, string oldText, string newText)
    {
        if (_busy || ui == null || string.IsNullOrEmpty(newText)) return;
        try
        {
            if ((UnityEngine.Object)ui == null) return;
            if (!Plugin.Config.ResizeDialogText.Value && IsDialogueUi(ui))
                return;
            string name;
            try { name = ui.name ?? ""; } catch { return; }
            if (IsNonLabelName(name)) return;
            if (!IsSaneText(newText)) return;

            _busy = true;
            int design = CaptureDesignUi(ui);
            if (design < 1 || design > 500) return;

            float targetF = ComputeTargetSize(design, oldText, newText);
            int target = Mathf.RoundToInt(targetF);
            if (target < 1) target = design;

            // UI.Text has no per-word wrap control: a long single word wraps
            // MID-WORD ("Понедель/ник"). Let single tokens overflow instead —
            // the ratio shrink above keeps them within the container anyway.
            try
            {
                if (IsSingleToken(newText))
                    ui.horizontalOverflow = HorizontalWrapMode.Overflow;
            }
            catch { /* ignore */ }

            if (ui.fontSize != target)
            {
                try { ui.fontSize = target; }
                catch { return; }
            }

            LogOnce(ui.GetInstanceID(), name, design, target, newText.Length);
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"TextLayout.Prepare UI: {e.GetType().Name}: {e.Message}");
        }
        finally
        {
            _busy = false;
        }
    }

    // ------------------------------------------------------------------
    // Text that arrives ALREADY localized (patched Unity.Localization
    // bundles) never passes the translate path, but long single Cyrillic
    // words still wrap MID-WORD ("Воскресе/нье"). Fit them here: kill the
    // mid-word wrap and shrink to the measured container width.
    // ------------------------------------------------------------------
    private static readonly Dictionary<int, int> _foreignFitted = new();

    public static void FitForeign(TMP_Text tmp, string text)
    {
        if (_busy || tmp == null) return;
        if (!IsFittableForeign(text)) return;
        try
        {
            if ((UnityEngine.Object)tmp == null) return;
            if (IsDialogueText(tmp)) return;
            int id = tmp.GetInstanceID();
            int hash = text.GetHashCode();
            if (_foreignFitted.TryGetValue(id, out int prev) && prev == hash) return;

            _busy = true;
            float design = CaptureDesignTmp(tmp);
            if (design < 1f || design > 500f) { _foreignFitted[id] = hash; return; }

            try
            {
                tmp.enableWordWrapping = false;
                var om = tmp.overflowMode;
                if (om == TextOverflowModes.Truncate || om == TextOverflowModes.Ellipsis)
                    tmp.overflowMode = TextOverflowModes.Overflow;
            }
            catch { /* ignore */ }

            float scale = 1f;
            bool measured = false;
            float width = 0f;
            try { width = tmp.rectTransform.rect.width; } catch { }
            if (width >= 10f)
            {
                try
                {
                    if (Mathf.Abs(tmp.fontSize - design) >= 0.05f) tmp.fontSize = design;
                    float pref = tmp.GetPreferredValues(text).x;
                    if (pref > 1f && !float.IsNaN(pref) && !float.IsInfinity(pref))
                    {
                        scale = pref > width ? Mathf.Clamp(width / pref, 0.5f, 1f) : 1f;
                        measured = true;
                    }
                }
                catch { /* fall back below */ }
            }
            if (!measured)
                scale = Mathf.Clamp(8f / text.Length, 0.62f, 1f);

            float target = design * scale;
            if (Mathf.Abs(tmp.fontSize - target) >= 0.05f)
            {
                try { tmp.fontSize = target; } catch { }
            }
            _foreignFitted[id] = hash;
            LogOnce(id, SafeNameOf(tmp), design, target, text.Length);
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"TextLayout.FitForeign TMP: {e.GetType().Name}: {e.Message}");
        }
        finally { _busy = false; }
    }

    public static void FitForeign(UnityEngine.UI.Text ui, string text)
    {
        if (_busy || ui == null) return;
        if (!IsFittableForeign(text)) return;
        try
        {
            if ((UnityEngine.Object)ui == null) return;
            if (IsDialogueUi(ui)) return;
            int id = ui.GetInstanceID();
            int hash = text.GetHashCode();
            if (_foreignFitted.TryGetValue(id, out int prev) && prev == hash) return;

            _busy = true;
            int design = CaptureDesignUi(ui);
            if (design < 1 || design > 500) { _foreignFitted[id] = hash; return; }

            try { ui.horizontalOverflow = HorizontalWrapMode.Overflow; }
            catch { /* ignore */ }

            float scale = 1f;
            bool measured = false;
            float width = 0f;
            try { width = ui.rectTransform.rect.width; } catch { }
            if (width >= 10f)
            {
                try
                {
                    if (ui.fontSize != design) ui.fontSize = design;
                    var settings = ui.GetGenerationSettings(Vector2.zero);
                    float ppu = ui.pixelsPerUnit;
                    if (ppu > 0.01f)
                    {
                        float pref = ui.cachedTextGeneratorForLayout.GetPreferredWidth(text, settings) / ppu;
                        if (pref > 1f && !float.IsNaN(pref) && !float.IsInfinity(pref))
                        {
                            scale = pref > width ? Mathf.Clamp(width / pref, 0.5f, 1f) : 1f;
                            measured = true;
                        }
                    }
                }
                catch { /* fall back below */ }
            }
            if (!measured)
                scale = Mathf.Clamp(8f / text.Length, 0.62f, 1f);

            int target = Mathf.RoundToInt(design * scale);
            if (target >= 1 && ui.fontSize != target)
            {
                try { ui.fontSize = target; } catch { }
            }
            _foreignFitted[id] = hash;
            LogOnce(id, SafeNameOf(ui), design, target, text.Length);
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"TextLayout.FitForeign UI: {e.GetType().Name}: {e.Message}");
        }
        finally { _busy = false; }
    }

    /// <summary>
    /// A single long word (6..40 chars, no whitespace) that contains a non-ASCII
    /// letter \u2014 Cyrillic, or Latin diacritics (de/pl/tr/\u2026), Greek, etc. Such words
    /// can wrap mid-word, so they get shrink-to-fit. ASCII-only words are left alone.
    /// </summary>
    private static bool IsFittableForeign(string s)
    {
        if (s == null) return false;
        int n = s.Length;
        if (n < 6 || n > 40) return false;
        bool foreign = false;
        for (int i = 0; i < n; i++)
        {
            char c = s[i];
            if (char.IsWhiteSpace(c)) return false;
            if (c > 0x7F && char.IsLetter(c)) foreign = true;
        }
        return foreign;
    }

    private static string SafeNameOf(UnityEngine.Object o)
    {
        try { return o?.name ?? "?"; } catch { return "?"; }
    }

    // Deprecated no-ops for older call sites.
    public static void Schedule(TMP_Text tmp) { }
    public static void Schedule(UnityEngine.UI.Text ui) { }
    public static void Fit(TMP_Text tmp) { }
    public static void Fit(UnityEngine.UI.Text ui) { }
    public static void AfterTranslate(TMP_Text tmp, string en, string ru)
        => PrepareBeforeSet(tmp, en, ru);
    public static void AfterTranslate(UnityEngine.UI.Text ui, string en, string ru)
        => PrepareBeforeSet(ui, en, ru);
    public static void ProcessQueue() { }

    private static float CaptureDesignTmp(TMP_Text tmp)
    {
        int id = tmp.GetInstanceID();
        float cur = tmp.fontSize;
        if (!_tmpDesign.TryGetValue(id, out float design))
        {
            design = cur;
            _tmpDesign[id] = design;
        }
        // If game restores a larger designer size later, track it.
        else if (cur > design + 0.5f && cur <= 500f)
        {
            design = cur;
            _tmpDesign[id] = design;
        }
        return design;
    }

    private static int CaptureDesignUi(UnityEngine.UI.Text ui)
    {
        int id = ui.GetInstanceID();
        int cur = ui.fontSize;
        if (!_uiDesign.TryGetValue(id, out int design))
        {
            design = cur;
            _uiDesign[id] = design;
        }
        else if (cur > design)
        {
            design = cur;
            _uiDesign[id] = design;
        }
        return design;
    }

    /// <summary>Estimate a fitted fontSize from design size and string length.</summary>
    private static float ComputeTargetSize(float design, string oldText, string newText)
    {
        int newLen = SafeLen(newText);
        int oldLen = SafeLen(oldText);
        if (newLen <= 0) return design;

        bool single = IsSingleToken(newText);

        // Small body type: leave alone — EXCEPT overflowing single words
        // (day names, captions): they cannot wrap nicely, so shrink by
        // length ratio to keep them on one line.
        if (design < 26f)
        {
            if (single && newLen >= 8 && oldLen >= 3 && newLen > oldLen)
                return design * Mathf.Clamp((float)oldLen / newLen, 0.62f, 1f);
            return design;
        }

        float cfgMin = 0.55f; // matches PluginConfig.MinFitScale default
        try { cfgMin = Mathf.Clamp(Plugin.Config.MinFitScale.Value, 0.4f, 1f); } catch { /* default */ }

        // Long text lives in wrapping containers (disclaimers, descriptions):
        // extra characters become extra LINES, not extra width. Trim gently only
        // when the translation is much longer than the original.
        if (newLen >= 60)
        {
            // Wide Cyrillic glyphs + longer text: ~0.85 keeps the block inside
            // the original panel (height scales roughly with size squared).
            if (oldLen >= 20 && newLen > oldLen * 1.4f) return design * 0.80f;
            return design * 0.85f;
        }

        // Approx chars that fit one line at design size.
        float capacity = design >= 55f ? 10f
            : design >= 45f ? 13f
            : design >= 35f ? 18f
            : 28f;

        float target = design;
        if (oldLen >= 4 && newLen > oldLen)
        {
            // Straight length ratio; no extra penalty multiplier.
            float ratio = (float)oldLen / newLen;
            target = design * Mathf.Clamp(ratio, cfgMin, 1f);
        }
        else if (newLen > capacity)
        {
            target = design * Mathf.Clamp(capacity / newLen, cfgMin, 1f);
        }

        // Short multi-word titles can wrap — budget for two lines.
        if (!IsSingleToken(newText) && newLen >= 18 && design >= 40f)
        {
            float twoLines = (design >= 55f ? 14f : 18f) * 2f;
            if (newLen > twoLines)
                target = Mathf.Min(target, design * Mathf.Clamp(twoLines / newLen, cfgMin, 1f));
        }

        // Single token cannot wrap — allowed to shrink a bit further.
        if (IsSingleToken(newText) && newLen >= 8)
        {
            float tokenCap = design >= 50f ? 9f : 14f;
            float tokenMin = Mathf.Max(0.5f, cfgMin - 0.15f);
            if (newLen > tokenCap)
                target = Mathf.Min(target, design * Mathf.Clamp(tokenCap / newLen, tokenMin, 1f));
        }

        // Readable minimum relative to the designer size.
        float floorScale = IsSingleToken(newText) ? Mathf.Max(0.5f, cfgMin - 0.15f) : cfgMin;
        float floor = design * floorScale;
        if (target < floor) target = floor;
        if (target > design) target = design;
        return target;
    }

    private static int SafeLen(string s)
    {
        if (s == null) return 0;
        try
        {
            int n = s.Length;
            if (n < 0 || n > 4000) return 0;
            // non-whitespace length
            int v = 0;
            for (int i = 0; i < n; i++)
            {
                char c = s[i];
                if (c == '\n' || c == '\r' || c == '\t') continue;
                v++;
            }
            return v;
        }
        catch { return 0; }
    }

    private static bool IsSaneText(string s)
    {
        if (s == null) return false;
        try
        {
            int n = s.Length;
            return n >= 1 && n <= 4000;
        }
        catch { return false; }
    }

    private static bool IsSingleToken(string s)
    {
        if (s == null || s.Length < 6) return false;
        for (int i = 0; i < s.Length; i++)
            if (char.IsWhiteSpace(s[i])) return false;
        return true;
    }

    private static bool IsNonLabelName(string name)
    {
        if (string.IsNullOrEmpty(name)) return true;
        if (name.Equals("Button", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.Equals("Image", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.Equals("RawImage", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.Equals("Panel", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.Equals("Background", StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    /// <summary>True for dialogue / subtitle / speech-bubble text.</summary>
    private static bool IsDialogueText(TMP_Text tmp)
    {
        try
        {
            var font = tmp.font;
            if (font != null)
            {
                var fn = font.name ?? "";
                if (ContainsDialogueToken(fn)) return true;
            }
        }
        catch { /* ignore */ }

        try
        {
            if (HierarchyLooksLikeDialogue(tmp.transform)) return true;
        }
        catch { /* ignore */ }

        return false;
    }

    private static bool IsDialogueUi(UnityEngine.UI.Text ui)
    {
        try
        {
            if (HierarchyLooksLikeDialogue(ui.transform)) return true;
        }
        catch { /* ignore */ }
        return false;
    }

    private static bool HierarchyLooksLikeDialogue(Transform t)
    {
        for (int i = 0; i < 8 && t != null; i++)
        {
            if (ContainsDialogueToken(t.name ?? "")) return true;
            t = t.parent;
        }
        return false;
    }

    private static bool ContainsDialogueToken(string s)
    {
        if (string.IsNullOrEmpty(s)) return false;
        // dialogue-related names/fonts
        if (s.IndexOf("Dialog", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (s.IndexOf("Dialogue", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (s.IndexOf("Speech", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (s.IndexOf("Subtitle", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (s.IndexOf("Typewriter", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (s.IndexOf("Bubble", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        return false;
    }

    private static void LogOnce(int id, string name, float design, float target, int len)
    {
        if (!Plugin.Config.VerboseLogging.Value) return;
        if (!_logged.Add(id)) return;
        if (Mathf.Abs(design - target) < 0.1f) return;
        Plugin.Log.LogInfo(
            $"Layout pre-size '{name}' {design:0.#}→{target:0.#} len={len} (before first paint)");
    }
}