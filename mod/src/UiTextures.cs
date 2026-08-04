using System;
using System.Collections.Generic;
using System.IO;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace SarahsHouseI18n;

/// <summary>
/// Procedural IMGUI art for the language picker: rounded plates with gradients,
/// hairline borders, soft glows, and composited flag tiles. Optional PNG art is read
/// from the plugin's <c>ui/</c> folder; when a file is missing the procedural
/// fallback keeps the UI intact, so art is a bonus and never a requirement.
/// Everything is cached by key — textures are built once per resolution.
/// </summary>
internal static class UiTextures
{
    private static readonly Dictionary<string, Texture2D> _cache = new(StringComparer.Ordinal);
    private static readonly HashSet<string> _missing = new(StringComparer.Ordinal);
    private static Font _font;
    private static bool _fontTried;
    private static Font _mono;
    private static bool _monoTried;

    /// <summary>Fallback flag colours, used when <c>ui/flags/{code}.png</c> is absent.</summary>
    private static readonly Dictionary<string, Color[]> _flagFallback = new(StringComparer.OrdinalIgnoreCase)
    {
        ["en"] = new[] { C(0x01, 0x21, 0x69), C(0x3B, 0x55, 0x9E) },
        ["ru"] = new[] { C(0xC4, 0x3C, 0x78), C(0xF0, 0x78, 0xB4) },
        ["uk"] = new[] { C(0x00, 0x5B, 0xBB), C(0xFF, 0xD5, 0x00) },
        ["de"] = new[] { C(0x2A, 0x2A, 0x32), C(0xDD, 0x00, 0x00) },
        ["es"] = new[] { C(0xC6, 0x0B, 0x1E), C(0xFF, 0xC4, 0x00) },
        ["pl"] = new[] { C(0xE8, 0xE8, 0xEE), C(0xDC, 0x14, 0x3C) },
        ["vi"] = new[] { C(0xDA, 0x25, 0x1D), C(0xFF, 0xD5, 0x00) },
        ["zh"] = new[] { C(0xDE, 0x29, 0x10), C(0xFF, 0xDE, 0x00) },
        ["ja"] = new[] { C(0xF2, 0xF2, 0xF6), C(0xBC, 0x00, 0x2D) },
        ["ko"] = new[] { C(0xF2, 0xF2, 0xF6), C(0x00, 0x47, 0xA0) },
        ["fr"] = new[] { C(0x00, 0x23, 0x95), C(0xED, 0x29, 0x39) },
        ["pt"] = new[] { C(0x00, 0x66, 0x47), C(0xDA, 0x29, 0x1C) },
        ["tr"] = new[] { C(0xE3, 0x0A, 0x17), C(0xF4, 0x5A, 0x62) },
        ["it"] = new[] { C(0x00, 0x8C, 0x45), C(0xCD, 0x21, 0x2A) },
        ["cs"] = new[] { C(0x11, 0x45, 0x7E), C(0xD7, 0x14, 0x1A) },
        ["pt-br"] = new[] { C(0x00, 0x9C, 0x3B), C(0xFF, 0xDF, 0x00) },
    };

    private static Color C(int r, int g, int b) => new Color(r / 255f, g / 255f, b / 255f, 1f);

    private static string UiDir
    {
        get
        {
            try
            {
                var dir = Path.GetDirectoryName(typeof(UiTextures).Assembly.Location) ?? ".";
                return Path.Combine(dir, "ui");
            }
            catch { return "ui"; }
        }
    }

    // ------------------------------------------------------------------ basics

    public static Texture2D Solid(Color c)
    {
        string key = $"s:{Hex(c)}";
        if (_cache.TryGetValue(key, out var t) && t != null) return t;

        t = New(2, 2);
        var px = new Color32[4];
        var c32 = (Color32)c;
        for (int i = 0; i < 4; i++) px[i] = c32;
        Apply(t, px);
        _cache[key] = t;
        return t;
    }

    /// <summary>1px-wide vertical gradient, handy for scrims and fades.</summary>
    public static Texture2D VGradient(Color top, Color bottom, int h = 128)
    {
        string key = $"vg:{Hex(top)}:{Hex(bottom)}:{h}";
        if (_cache.TryGetValue(key, out var t) && t != null) return t;

        t = New(1, h);
        var px = new Color32[h];
        for (int y = 0; y < h; y++)
        {
            // texture rows run bottom-up on screen
            float f = 1f - y / (float)Mathf.Max(1, h - 1);
            px[y] = (Color32)Color.Lerp(top, bottom, f);
        }
        Apply(t, px);
        _cache[key] = t;
        return t;
    }

    /// <summary>
    /// Rounded rectangle with antialiased edges, a vertical fill gradient, an optional
    /// hairline border and an optional soft outer falloff (used for glows/shadows).
    /// </summary>
    public static Texture2D Rounded(int w, int h, float radius, Color top, Color bottom,
                                    Color border, float borderPx = 0f, float softPx = 0f,
                                    string tag = "")
    {
        w = Mathf.Clamp(w, 2, 4096);
        h = Mathf.Clamp(h, 2, 4096);
        string key = $"r:{tag}:{w}x{h}:{radius:F1}:{Hex(top)}:{Hex(bottom)}:{Hex(border)}:{borderPx:F1}:{softPx:F1}";
        if (_cache.TryGetValue(key, out var cached) && cached != null) return cached;

        var t = New(w, h);
        var px = new Color32[w * h];

        float inset = softPx;
        float rw = w - inset * 2f;
        float rh = h - inset * 2f;
        float r = Mathf.Min(radius, Mathf.Min(rw, rh) * 0.5f);

        for (int y = 0; y < h; y++)
        {
            float gy = 1f - y / (float)Mathf.Max(1, h - 1);
            Color fill = Color.Lerp(top, bottom, gy);
            int row = y * w;

            for (int x = 0; x < w; x++)
            {
                float d = Sdf(x + 0.5f - inset, y + 0.5f - inset, rw, rh, r);

                Color c = fill;
                c.a *= Mathf.Clamp01(0.5f - d);

                if (borderPx > 0.01f && border.a > 0.001f)
                {
                    float ring = 1f - Mathf.Clamp01(Mathf.Abs(d + borderPx * 0.5f) - borderPx * 0.5f + 0.5f);
                    if (ring > 0.001f)
                    {
                        var b = border;
                        b.a *= ring;
                        c = Over(b, c);
                    }
                }

                if (softPx > 0.01f && d > 0f)
                {
                    float f = 1f - Mathf.Clamp01(d / softPx);
                    f = f * f * (3f - 2f * f);
                    var sc = fill;
                    sc.a = fill.a * f;
                    c = Over(sc, c);
                }

                px[row + x] = (Color32)c;
            }
        }

        Apply(t, px);
        _cache[key] = t;
        return t;
    }

    // ------------------------------------------------------------------ flag tiles

    public const int StateIdle = 0;
    public const int StateHover = 1;
    public const int StateActive = 2;      // language currently applied
    public const int StateDraft = 3;       // no pack behind it
    public const int StateSelected = 4;    // cursor: about to be confirmed
    public const int StateActiveSel = 5;   // applied *and* under the cursor

    /// <summary>
    /// A language tile: the flag cover-fitted into a rounded plate, darkened towards the
    /// bottom so the label reads, with a per-state tint and border. Composited into a
    /// single texture because IMGUI cannot clip a drawn texture to rounded corners.
    /// </summary>
    public static Texture2D Tile(string code, int w, int h, float radius, int state)
    {
        w = Mathf.Clamp(w, 8, 1024);
        h = Mathf.Clamp(h, 8, 1024);
        string key = $"t:{code}:{w}x{h}:{radius:F0}:{state}";
        if (_cache.TryGetValue(key, out var cached) && cached != null) return cached;

        var flag = Png($"flags/{code}.png");
        Color fa = Color.gray, fb = Color.gray;
        if (flag == null)
        {
            if (_flagFallback.TryGetValue(code, out var pal)) { fa = pal[0]; fb = pal[1]; }
            else { fa = C(0x5A, 0x50, 0x66); fb = C(0x8A, 0x7E, 0x96); }
        }

        float fw = flag != null ? flag.width : 1f;
        float fh = flag != null ? flag.height : 1f;
        float scale = flag != null ? Mathf.Max(w / fw, h / fh) : 1f;   // cover
        float offX = flag != null ? (fw * scale - w) * 0.5f : 0f;
        float offY = flag != null ? (fh * scale - h) * 0.5f : 0f;

        var t = New(w, h);
        var px = new Color32[w * h];
        float r = Mathf.Min(radius, Mathf.Min(w, h) * 0.5f);

        Color tint = state switch
        {
            StateActive => new Color(1f, 0.55f, 0.82f, 0.10f),
            StateSelected => new Color(1f, 1f, 1f, 0.16f),
            StateActiveSel => new Color(1f, 0.72f, 0.90f, 0.18f),
            StateHover => new Color(1f, 1f, 1f, 0.09f),
            _ => new Color(0f, 0f, 0f, 0f)
        };
        Color shadeCol = new Color(0.06f, 0.03f, 0.09f, 1f);

        for (int y = 0; y < h; y++)
        {
            int sy = h - 1 - y;                    // screen-space row (top = 0)
            float ty = sy / (float)Mathf.Max(1, h - 1);
            float shadeA = 0.27f + 0.65f * Mathf.Pow(Mathf.Max(0f, (ty - 0.34f) / 0.66f), 1.25f);
            int row = y * w;

            for (int x = 0; x < w; x++)
            {
                Color c;
                if (flag != null)
                {
                    float u = (x + offX) / (fw * scale);
                    float v = (y + offY) / (fh * scale);
                    c = flag.GetPixelBilinear(u, v);
                    c.a = 1f;
                }
                else
                {
                    c = Color.Lerp(fa, fb, ty);
                }

                c = Color.Lerp(c, shadeCol, shadeA);
                if (state == StateDraft) c = Color.Lerp(c, shadeCol, 0.55f);
                if (tint.a > 0f) c = Over(tint, c);

                float d = Sdf(x + 0.5f, y + 0.5f, w, h, r);
                c.a = Mathf.Clamp01(0.5f - d);

                // border: white = cursor/selection, pink = the applied language
                float bpx = state switch
                {
                    StateSelected => 3f,
                    StateActiveSel => 3f,
                    StateActive => 2f,
                    StateHover => 2f,
                    _ => 1f
                };
                Color bc = state switch
                {
                    StateSelected => new Color(1f, 1f, 1f, 0.95f),
                    StateActiveSel => new Color(1f, 0.80f, 0.93f, 1f),
                    StateActive => new Color(1f, 0.55f, 0.82f, 0.70f),
                    StateHover => new Color(1f, 1f, 1f, 0.50f),
                    _ => new Color(1f, 1f, 1f, 0.20f)
                };
                float ring = 1f - Mathf.Clamp01(Mathf.Abs(d + bpx * 0.5f) - bpx * 0.5f + 0.5f);
                if (ring > 0.001f)
                {
                    bc.a *= ring;
                    c = Over(bc, c);
                }

                px[row + x] = (Color32)c;
            }
        }

        Apply(t, px);
        _cache[key] = t;
        return t;
    }

    private static float Sdf(float x, float y, float w, float h, float r)
    {
        float cx = Mathf.Abs(x - w * 0.5f) - (w * 0.5f - r);
        float cy = Mathf.Abs(y - h * 0.5f) - (h * 0.5f - r);
        float dx = Mathf.Max(cx, 0f);
        float dy = Mathf.Max(cy, 0f);
        return Mathf.Sqrt(dx * dx + dy * dy) + Mathf.Min(Mathf.Max(cx, cy), 0f) - r;
    }

    private static Color Over(Color src, Color dst)
    {
        float a = src.a + dst.a * (1f - src.a);
        if (a <= 0.0001f) return new Color(0, 0, 0, 0);
        return new Color(
            (src.r * src.a + dst.r * dst.a * (1f - src.a)) / a,
            (src.g * src.a + dst.g * dst.a * (1f - src.a)) / a,
            (src.b * src.a + dst.b * dst.a * (1f - src.a)) / a,
            a);
    }

    // ------------------------------------------------------------------ png art

    /// <summary>Load <c>ui/{file}</c> once; null when absent or undecodable.</summary>
    public static Texture2D Png(string file)
    {
        string key = "p:" + file;
        if (_cache.TryGetValue(key, out var t) && t != null) return t;
        if (_missing.Contains(key)) return null;

        try
        {
            var path = Path.Combine(UiDir, file.Replace('/', Path.DirectorySeparatorChar));
            if (!File.Exists(path))
            {
                _missing.Add(key);
                Plugin.Log.LogInfo($"UI art '{file}' not present — procedural fallback");
                return null;
            }

            var bytes = File.ReadAllBytes(path);
            var tex = New(2, 2);
            bool ok;
            try { ok = ImageConversion.LoadImage(tex, new Il2CppStructArray<byte>(bytes), false); }
            catch (Exception e)
            {
                Plugin.Log.LogWarning($"UI art '{file}' decode failed: {e.GetType().Name}: {e.Message}");
                ok = false;
            }

            if (!ok || tex.width < 2)
            {
                _missing.Add(key);
                return null;
            }

            tex.wrapMode = TextureWrapMode.Clamp;
            tex.filterMode = FilterMode.Bilinear;
            _cache[key] = tex;
            Plugin.Log.LogInfo($"UI art '{file}' loaded ({tex.width}x{tex.height})");
            return tex;
        }
        catch (Exception e)
        {
            _missing.Add(key);
            Plugin.Log.LogWarning($"UI art '{file}': {e.Message}");
            return null;
        }
    }

    /// <summary>Rect that scales <paramref name="tex"/> to cover the screen (aspect-fill).</summary>
    public static Rect CoverRect(Texture2D tex, float sw, float sh)
    {
        if (tex == null || tex.width < 1 || tex.height < 1) return new Rect(0, 0, sw, sh);
        float sa = sw / sh;
        float ta = tex.width / (float)tex.height;
        if (ta > sa)
        {
            float w = sh * ta;
            return new Rect((sw - w) * 0.5f, 0f, w, sh);
        }
        float hh = sw / ta;
        return new Rect(0f, (sh - hh) * 0.5f, sw, hh);
    }

    // ------------------------------------------------------------------ font

    /// <summary>
    /// A dynamic OS font so native names render — the built-in IMGUI font has no CJK
    /// glyphs, which would turn 日本語 / 한국어 / 中文 into tofu boxes.
    /// </summary>
    public static Font UiFont()
    {
        if (_fontTried) return _font;
        _fontTried = true;

        string[] names =
        {
            "Segoe UI", "Yu Gothic UI", "Malgun Gothic", "Microsoft YaHei",
            "Arial Unicode MS", "Tahoma", "Arial"
        };

        try
        {
            _font = Font.CreateDynamicFontFromOSFont(new Il2CppStringArray(names), 28);
            if (_font != null)
            {
                Plugin.Log.LogInfo($"Language UI font: {_font.name}");
                return _font;
            }
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"UI font (multi) failed: {e.Message}");
        }

        foreach (var n in names)
        {
            try
            {
                _font = Font.CreateDynamicFontFromOSFont(n, 28);
                if (_font != null)
                {
                    Plugin.Log.LogInfo($"Language UI font: {n}");
                    return _font;
                }
            }
            catch { /* try next */ }
        }

        Plugin.Log.LogWarning("Language UI font: falling back to the built-in GUI font");
        return null;
    }

    /// <summary>
    /// Monospace OS font for the repo link — a slug like <c>owner/repo</c> reads as code,
    /// and the fixed pitch keeps it from looking like body copy. Falls back to the UI font.
    /// </summary>
    public static Font MonoFont()
    {
        if (_monoTried) return _mono ?? UiFont();
        _monoTried = true;

        string[] names = { "Consolas", "Cascadia Mono", "Cascadia Code", "Lucida Console", "Courier New" };
        try
        {
            _mono = Font.CreateDynamicFontFromOSFont(new Il2CppStringArray(names), 24);
            if (_mono != null)
            {
                Plugin.Log.LogInfo($"Language UI mono font: {_mono.name}");
                return _mono;
            }
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"mono font (multi) failed: {e.Message}");
        }

        foreach (var n in names)
        {
            try
            {
                _mono = Font.CreateDynamicFontFromOSFont(n, 24);
                if (_mono != null)
                {
                    Plugin.Log.LogInfo($"Language UI mono font: {n}");
                    return _mono;
                }
            }
            catch { /* try next */ }
        }

        Plugin.Log.LogInfo("Language UI mono font: none, using the UI font");
        return UiFont();
    }

    // ------------------------------------------------------------------ helpers

    private static Texture2D New(int w, int h)
    {
        return new Texture2D(w, h, TextureFormat.RGBA32, false)
        {
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear,
            hideFlags = HideFlags.HideAndDontSave
        };
    }

    private static void Apply(Texture2D t, Color32[] px)
    {
        t.SetPixels32(new Il2CppStructArray<Color32>(px));
        t.Apply(false, false);
    }

    private static string Hex(Color c) =>
        $"{Mathf.RoundToInt(c.r * 255):X2}{Mathf.RoundToInt(c.g * 255):X2}{Mathf.RoundToInt(c.b * 255):X2}{Mathf.RoundToInt(c.a * 255):X2}";
}
