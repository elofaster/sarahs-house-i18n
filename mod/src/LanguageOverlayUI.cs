using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace SarahsHouseI18n;

/// <summary>
/// In-game language picker (IMGUI): a grid of flag tiles over the mod's key art.
/// The first-run cover pauses input until a language is chosen; afterwards the same
/// grid opens from the main menu via the corner chip or F10.
/// Gate/pause/click plumbing is deliberately unchanged — only the presentation is.
/// </summary>
internal sealed class LanguageOverlayUI : MonoBehaviour
{
    public LanguageOverlayUI(IntPtr ptr) : base(ptr) { }

    private bool _open;
    private bool _bootGate;
    private bool _bootGateDone;
    private bool _stylesReady;
    private bool _loggedScene;
    private bool _freezeApplied;
    private bool _pausedSystems;
    private float _savedTimeScale = 1f;
    private string _lastScene = "";
    private float _scrollY;
    private float _nextScenePoll;
    private float _bootAt;
    private float _anim;
    private string _status = "";
    private float _statusUntil;
    private float _listTop;
    private float _listBottom;
    private float _contentH;
    private float _ignoreClickUntil;
    private bool _mouseWasDown;
    private float _forceRestoreUntil; // keep forcing restore after gate ends
    private float _pendingGateCloseAt = -1f; // apply language under cover, then close
    private string _pendingGateCode;
    private readonly List<EventSystem> _disabledSystems = new();

    // keyboard navigation
    private int _cursor = -1;
    private int _cols = 4;
    private Vector3 _lastMouse;

    private GUIStyle _kicker;
    private GUIStyle _title;
    private GUIStyle _sub;
    private GUIStyle _tileName;
    private GUIStyle _tileNameOn;
    private GUIStyle _tileNameOff;
    private GUIStyle _tileMeta;
    private GUIStyle _badge;
    private GUIStyle _chip;
    private GUIStyle _hintKey;
    private GUIStyle _hint;
    private GUIStyle _check;
    private Texture2D _pink;
    private Texture2D _px;

    private Rect _chipHit;
    private Rect _closeHit;
    private Rect _confirmHit;
    private bool _confirmVisible;
    private Rect _linkHit;
    private bool _linkVisible;
    private GUIStyle _link;
    private GUIStyle _linkCaption;
    private GUIStyle _button;
    private GUIStyle _buttonOff;
    private bool _chipVisible;
    private bool _closeVisible;
    private readonly List<Rect> _rowHits = new();
    private readonly List<string> _rowCodes = new();
    private readonly List<bool> _rowEnabled = new();

    /// <summary>Public home of the translation packs — shown and clickable on the picker.</summary>
    private const string RepoUrl = "https://github.com/elofaster/sarahs-house-i18n";
    private const string RepoLabel = "elofaster/sarahs-house-i18n";

    private static readonly Color Pink = new Color(1f, 0.55f, 0.82f, 1f);
    private static readonly Color PinkHot = new Color(1f, 0.42f, 0.75f, 1f);

    private static string FlagPath
    {
        get
        {
            try
            {
                var dir = Path.GetDirectoryName(typeof(LanguageOverlayUI).Assembly.Location) ?? ".";
                return Path.Combine(dir, "i18n", ".lang_selected");
            }
            catch { return ".lang_selected"; }
        }
    }

    private bool GateActive => _bootGate && !_bootGateDone;

    private void Awake()
    {
        _bootAt = Time.unscaledTime + 0.02f;
        try { _bootGate = !File.Exists(FlagPath); }
        catch { _bootGate = true; }

        if (_bootGate)
        {
            _open = true;
            Plugin.Log.LogInfo("Language UI: first-run gate armed");

            // Until the player actually chooses, the game must stay in its original
            // language — the config default (ru) would otherwise pre-translate the
            // disclaimer behind the gate and pre-select Russian in the grid.
            try
            {
                if (!LanguageManager.IsEnglish)
                {
                    LanguageManager.SetLanguage("en", refreshUi: false);
                    Plugin.Log.LogInfo("Language UI: first run — defaulting to English until picked");
                }
            }
            catch (Exception e)
            {
                Plugin.Log.LogWarning($"first-run English default failed: {e.Message}");
            }
        }
    }

    private void OnDestroy()
    {
        // Restore input if this component is destroyed.
        ForceFullRestore("OnDestroy");
    }

    private void Update()
    {
        if (!Plugin.Config.EnableLanguageUi.Value)
        {
            ForceFullRestore("ui-disabled");
            return;
        }

        bool want = _open || GateActive;
        _anim = Mathf.MoveTowards(_anim, want ? 1f : 0f, Time.unscaledDeltaTime * 8f);

        // Finish boot pick after translations apply under the cover.
        if (_pendingGateCloseAt > 0f && Time.unscaledTime >= _pendingGateCloseAt)
        {
            _pendingGateCloseAt = -1f;
            _pendingGateCode = null;
            _bootGateDone = true;
            _bootGate = false;
            _open = false;
            _forceRestoreUntil = Time.unscaledTime + 2.5f;
            ForceFullRestore("language-picked-delayed");
            Plugin.Log.LogInfo("Language boot gate complete — disclaimer interactive");
        }

        if (GateActive)
        {
            PauseGameForGate();
            _open = true;
        }
        else if (_pausedSystems || _freezeApplied || Time.unscaledTime < _forceRestoreUntil)
        {
            // Briefly keep UI systems restored after the gate.
            ForceFullRestore("post-gate");
        }

        if (Time.unscaledTime >= _nextScenePoll)
        {
            _nextScenePoll = Time.unscaledTime + 0.25f;
            string scene = "";
            try { scene = SceneManager.GetActiveScene().name ?? ""; } catch { scene = ""; }
            if (!string.Equals(scene, _lastScene, StringComparison.Ordinal))
            {
                _lastScene = scene;
                _loggedScene = false;
            }
            if (!_loggedScene)
            {
                _loggedScene = true;
                Plugin.Log.LogInfo(
                    $"Language UI scene='{scene}' menu={IsMainMenu(scene)} boot={IsBoot(scene)} gate={GateActive}");
            }

            if (!_bootGateDone)
            {
                try
                {
                    if (!File.Exists(FlagPath) && (IsBoot(scene) || string.IsNullOrEmpty(scene)))
                    {
                        _bootGate = true;
                        _open = true;
                    }
                }
                catch
                {
                    if (IsBoot(scene) || string.IsNullOrEmpty(scene))
                    {
                        _bootGate = true;
                        _open = true;
                    }
                }
            }
        }

        if (GateActive && Time.unscaledTime >= _bootAt)
            _open = true;

        // keyboard shortcuts
        try
        {
            if (!GateActive && IsMainMenu(_lastScene))
            {
                if (Input.GetKeyDown(KeyCode.F10))
                {
                    _open = !_open;
                    _ignoreClickUntil = Time.unscaledTime + 0.15f;
                }
                if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.L))
                {
                    _open = !_open;
                    _ignoreClickUntil = Time.unscaledTime + 0.15f;
                }
                if (Input.GetKey(KeyCode.RightAlt) && Input.GetKeyDown(KeyCode.L))
                {
                    var next = LanguageManager.CycleNextLanguage();
                    SaveFlag(LanguageManager.CurrentCode);
                    Flash(LanguageManager.DisplayName(next));
                }
            }
            if (_open && !GateActive && Input.GetKeyDown(KeyCode.Escape))
                _open = false;

            if (_open || GateActive)
                HandleGridKeys();
        }
        catch { /* ignore */ }

        // Clicks via Input only (IMGUI MouseDown double-toggles).
        try
        {
            bool down = Input.GetMouseButton(0);
            if (down && !_mouseWasDown && Time.unscaledTime >= _ignoreClickUntil)
            {
                var m = GuiMouseFromInput();
                if (TryClick(m, "input"))
                    _ignoreClickUntil = Time.unscaledTime + 0.20f;
            }
            _mouseWasDown = down;

            if ((_open || GateActive) && Mathf.Abs(Input.mouseScrollDelta.y) > 0.01f)
            {
                float maxScroll = Mathf.Max(0f, _contentH - Mathf.Max(1f, _listBottom - _listTop));
                _scrollY = Mathf.Clamp(_scrollY - Input.mouseScrollDelta.y * 42f, 0f, maxScroll);
            }

            if ((Input.mousePosition - _lastMouse).sqrMagnitude > 4f)
            {
                _lastMouse = Input.mousePosition;
            }
        }
        catch { /* ignore */ }
    }

    /// <summary>Arrow-key / Enter navigation over the tile grid.</summary>
    private void HandleGridKeys()
    {
        if (_rowCodes.Count == 0) return;

        int n = _rowCodes.Count;
        int cols = Mathf.Max(1, _cols);
        int move = 0;

        if (Input.GetKeyDown(KeyCode.RightArrow)) move = 1;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) move = -1;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) move = cols;
        else if (Input.GetKeyDown(KeyCode.UpArrow)) move = -cols;

        if (move != 0)
        {
            if (_cursor < 0)
            {
                _cursor = Mathf.Max(0, IndexOf(LanguageManager.CurrentCode));
            }
            else
            {
                int next = _cursor + move;
                if (next >= 0 && next < n) _cursor = next;
            }
            return;
        }

        // Space also just moves focus forward — never applies on its own.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _cursor = _cursor < 0 ? 0 : (_cursor + 1) % n;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            int i = _cursor >= 0 ? _cursor : IndexOf(LanguageManager.CurrentCode);
            if (i < 0 || i >= n) return;
            if (i < _rowEnabled.Count && !_rowEnabled[i])
            {
                Flash(NoPackMessage(_rowCodes[i]));
                return;
            }
            Plugin.Log.LogInfo($"Language UI select '{_rowCodes[i]}' via keyboard");
            Pick(_rowCodes[i]);
        }
    }

    /// <summary>Code under the cursor — drives every label on the picker chrome.</summary>
    private string FocusCode()
    {
        if (_cursor >= 0 && _cursor < _rowCodes.Count) return _rowCodes[_cursor];
        return LanguageManager.CurrentCode ?? "en";
    }

    /// <summary>"no pack" in the tile's own language, plus English when that differs.</summary>
    private static string NoPackMessage(string code)
    {
        var own = UiStrings.For(code).NoPack;
        var en = UiStrings.English.NoPack;
        return string.Equals(own, en, StringComparison.Ordinal) ? own : own + " · " + en;
    }

    private int IndexOf(string code)
    {
        for (int i = 0; i < _rowCodes.Count; i++)
        {
            if (string.Equals(_rowCodes[i], code, StringComparison.OrdinalIgnoreCase)) return i;
        }
        return -1;
    }

    private void OnGUI()
    {
        if (!Plugin.Config.EnableLanguageUi.Value) return;

        try
        {
            GUI.depth = -2000;
            EnsureStyles();
            LangStats.Pump();

            float sw = Screen.width;
            float sh = Screen.height;
            if (sw < 16f || sh < 16f) return;

            bool menu = IsMainMenu(_lastScene);
            bool gate = GateActive;
            bool showPicker = _open || gate || _anim > 0.02f;

            _rowHits.Clear();
            _rowCodes.Clear();
            _rowEnabled.Clear();
            _chipHit = default;
            _closeHit = default;
            _chipVisible = false;
            _closeVisible = false;
            _confirmVisible = false;
            _linkVisible = false;

            if (menu && !gate)
                DrawChip(sw, sh);

            if (!showPicker) return;

            DrawBackdrop(sw, sh, gate);
            DrawPicker(sw, sh, gate);
            // Clicks handled in Update via Input.
        }
        catch (Exception e)
        {
            try { Plugin.Log.LogWarning($"LanguageOverlayUI.OnGUI: {e.GetType().Name}: {e.Message}"); }
            catch { /* ignore */ }
        }
    }

    /// <summary>Key art behind the gate; a plain scrim when opened from the menu.</summary>
    private void DrawBackdrop(float sw, float sh, bool gate)
    {
        Color prev = GUI.color;
        float t = gate ? 1f : EaseOut(Mathf.Clamp01(_anim));

        if (gate)
        {
            var art = UiTextures.Png("langbg.png");
            if (art != null)
            {
                GUI.color = new Color(1f, 1f, 1f, t);
                GUI.DrawTexture(UiTextures.CoverRect(art, sw, sh), art);
                // slight scrim so white type always reads over the art
                GUI.color = new Color(0.05f, 0.02f, 0.08f, 0.28f * t);
                GUI.DrawTexture(new Rect(0, 0, sw, sh), _px);
            }
            else
            {
                GUI.color = new Color(1f, 1f, 1f, t);
                GUI.DrawTexture(new Rect(0, 0, sw, sh),
                    UiTextures.VGradient(new Color(0.10f, 0.05f, 0.15f, 1f),
                                         new Color(0.05f, 0.02f, 0.09f, 1f)));
                GUI.color = new Color(0.55f, 0.18f, 0.38f, 0.10f * t);
                GUI.DrawTexture(new Rect(sw * 0.18f, sh * 0.12f, sw * 0.64f, sh * 0.76f), _px);
            }
        }
        else
        {
            GUI.color = new Color(0.05f, 0.02f, 0.08f, 0.80f * t);
            GUI.DrawTexture(new Rect(0, 0, sw, sh), _px);
        }

        GUI.color = prev;
    }

    private void DrawChip(float sw, float sh)
    {
        string code = (LanguageManager.CurrentCode ?? "en").ToUpperInvariant();
        float h = 42f;
        float w = 124f;
        _chipHit = new Rect(sw - w - 18f, 14f, w, h);
        _chipVisible = true;

        Color prev = GUI.color;
        GUI.color = Color.white;
        GUI.DrawTexture(_chipHit, UiTextures.Rounded((int)w, (int)h, 12f,
            new Color(0.12f, 0.06f, 0.17f, 0.86f), new Color(0.08f, 0.04f, 0.12f, 0.86f),
            new Color(1f, 0.55f, 0.82f, 0.42f), 1.5f, 0f, "chip"));

        var flagTex = UiTextures.Png($"flags/{(LanguageManager.CurrentCode ?? "en").ToLowerInvariant()}.png");
        var flagRect = new Rect(_chipHit.x + 10f, _chipHit.y + 12f, 27f, 18f);
        if (flagTex != null) GUI.DrawTexture(flagRect, flagTex);
        else
        {
            GUI.color = Pink;
            GUI.DrawTexture(new Rect(flagRect.x + 12f, flagRect.y, 2f, flagRect.height), _px);
            GUI.color = Color.white;
        }

        GUI.Label(new Rect(_chipHit.x + 46f, _chipHit.y, _chipHit.width - 50f, _chipHit.height),
            code + "  ▾", _chip);
        GUI.color = prev;
    }

    private void DrawPicker(float sw, float sh, bool gate)
    {
        float t = EaseOut(Mathf.Clamp01(gate ? 1f : _anim));
        Color prev = GUI.color;
        GUI.color = new Color(1f, 1f, 1f, t);

        var langs = BuildLangList();
        if (langs.Count == 0) { GUI.color = prev; return; }

        // keep a valid selection at all times so the confirm button always has a target
        if (_cursor < 0 || _cursor >= langs.Count)
        {
            _cursor = 0;
            for (int i = 0; i < langs.Count; i++)
            {
                if (langs[i].Active) { _cursor = i; break; }
            }
        }

        // ---- scale: design units are 1080p
        float s = Mathf.Clamp(sh / 1080f, 0.62f, 2.6f);
        float tw = 252f * s, th = 158f * s, gap = 22f * s;
        float headerH = 288f * s, footerH = 172f * s;

        int cols = Mathf.Clamp(Mathf.FloorToInt((sw - 120f * s + gap) / (tw + gap)), 2, 5);
        cols = Mathf.Min(cols, langs.Count);
        int rows = Mathf.CeilToInt(langs.Count / (float)cols);

        // shrink to fit vertically (small windows / ultrawide)
        float need = headerH + rows * th + (rows - 1) * gap + footerH;
        if (need > sh)
        {
            float k = sh / need;
            s *= k; tw *= k; th *= k; gap *= k; headerH *= k; footerH *= k;
        }
        _cols = cols;

        float gridH = rows * th + (rows - 1) * gap;
        float gridTop = headerH;
        _listTop = gridTop;
        _listBottom = gridTop + gridH;
        _contentH = gridH;
        _scrollY = 0f;

        // ---- header: titled in the language under the cursor, English underneath
        float cx = sw * 0.5f;
        float yOff = (1f - t) * 12f * s;
        string focus = langs[_cursor].Code;
        var L = UiStrings.For(focus);

        GUI.Label(new Rect(0f, 92f * s + yOff, sw, 22f * s),
            "SARAH'S HOUSE   ·   MULTILANG  " + Plugin.Version, _kicker);
        GUI.Label(new Rect(0f, 118f * s + yOff, sw, 66f * s), L.Title, _title);
        GUI.Label(new Rect(0f, 186f * s + yOff, sw, 30f * s),
            L == UiStrings.English ? LanguageManager.DisplayName(focus) : UiStrings.English.Title, _sub);

        // divider with a small heart
        float dy = 236f * s + yOff;
        GUI.color = new Color(Pink.r, Pink.g, Pink.b, 0.38f * t);
        GUI.DrawTexture(new Rect(cx - 150f * s, dy, 132f * s, Mathf.Max(1f, 2f * s)), _px);
        GUI.DrawTexture(new Rect(cx + 18f * s, dy, 132f * s, Mathf.Max(1f, 2f * s)), _px);
        GUI.color = new Color(1f, 1f, 1f, t);
        var heart = UiTextures.Png("heart.png");
        if (heart != null)
            GUI.DrawTexture(new Rect(cx - 9f * s, dy - 7f * s, 18f * s, 16f * s), heart);
        else
        {
            GUI.color = new Color(PinkHot.r, PinkHot.g, PinkHot.b, t);
            GUI.DrawTexture(new Rect(cx - 5f * s, dy - 4f * s, 10f * s, 10f * s), _px);
            GUI.color = new Color(1f, 1f, 1f, t);
        }

        if (!gate)
        {
            _closeVisible = true;
            _closeHit = new Rect(sw - 62f * s, 18f * s, 44f * s, 44f * s);
            GUI.Label(_closeHit, "✕", _sub);
        }

        // ---- tile grid, last row centred
        Vector2 mouse;
        try { mouse = Event.current != null ? Event.current.mousePosition : GuiMouseFromInput(); }
        catch { mouse = GuiMouseFromInput(); }

        int idx = 0;
        for (int r = 0; r < rows; r++)
        {
            int inRow = Mathf.Min(cols, langs.Count - r * cols);
            float rowW = inRow * tw + (inRow - 1) * gap;
            float x0 = (sw - rowW) * 0.5f;
            float y = gridTop + r * (th + gap) + yOff;

            for (int c = 0; c < inRow; c++, idx++)
            {
                var e = langs[idx];
                var rect = new Rect(x0 + c * (tw + gap), y, tw, th);

                _rowHits.Add(rect);
                _rowCodes.Add(e.Code);
                _rowEnabled.Add(e.Enabled);

                bool selected = idx == _cursor;
                bool hover = e.Enabled && !selected && rect.Contains(mouse);
                DrawTile(rect, e, hover, selected, s, t);
            }
        }

        // ---- footer: confirm button, then key hints
        float fy = gridTop + gridH + 34f * s + yOff;
        DrawConfirm(sw, fy, s, t, langs);

        float hy = fy + 74f * s;
        if (!string.IsNullOrEmpty(_status) && Time.unscaledTime < _statusUntil)
            GUI.Label(new Rect(0f, hy, sw, 28f * s), _status, _sub);
        else
            DrawHints(sw, hy, s, gate);

        DrawCornerLink(sw, sh, s, t, focus);

        GUI.color = prev;
    }

    private void DrawTile(Rect r, LangEntry e, bool hover, bool selected, float s, float t)
    {
        int state = !e.Enabled ? UiTextures.StateDraft
                  : selected && e.Active ? UiTextures.StateActiveSel
                  : selected ? UiTextures.StateSelected
                  : e.Active ? UiTextures.StateActive
                  : hover ? UiTextures.StateHover
                  : UiTextures.StateIdle;

        Color prev = GUI.color;

        // soft glow behind whatever is focused right now
        if (selected && e.Enabled)
        {
            float pad = 26f * s;
            GUI.color = new Color(1f, 1f, 1f, 0.60f * t);
            GUI.DrawTexture(new Rect(r.x - pad, r.y - pad, r.width + pad * 2, r.height + pad * 2),
                UiTextures.Rounded((int)(r.width + pad * 2), (int)(r.height + pad * 2),
                    16f * s + pad, new Color(1f, 0.72f, 0.90f, 0.40f), new Color(1f, 0.60f, 0.86f, 0.40f),
                    new Color(0, 0, 0, 0), 0f, pad, "glow"));
        }

        GUI.color = new Color(1f, 1f, 1f, t);
        GUI.DrawTexture(r, UiTextures.Tile(e.Code, (int)r.width, (int)r.height, 16f * s, state));

        // name + meta
        var nameStyle = !e.Enabled ? _tileNameOff : (e.Active ? _tileNameOn : _tileName);
        nameStyle.fontSize = Mathf.RoundToInt(22f * s);
        GUI.Label(new Rect(r.x + 14f * s, r.yMax - 54f * s, r.width - 28f * s, 30f * s), e.Name, nameStyle);

        _tileMeta.fontSize = Mathf.RoundToInt(12f * s);
        GUI.Label(new Rect(r.x + 15f * s, r.yMax - 26f * s, r.width - 30f * s, 20f * s), e.Meta, _tileMeta);

        // attribution badge, top-right: how this pack was produced
        var src = LangStats.SourceFor(e.Code);
        if (src != LangStats.Source.None)
        {
            bool human = src == LangStats.Source.Human;
            string tag = human ? "HUMAN" : "AI";
            Color hue = human ? new Color(0.55f, 1f, 0.74f, 1f) : new Color(0.62f, 0.86f, 1f, 1f);

            _badge.fontSize = Mathf.RoundToInt(11f * s);
            float bw = _badge.CalcSize(new GUIContent(tag)).x + 16f * s;
            float bh = 20f * s;
            var br = new Rect(r.xMax - bw - 10f * s, r.y + 10f * s, bw, bh);
            GUI.DrawTexture(br, UiTextures.Rounded((int)bw, (int)bh, 9f * s,
                new Color(0.05f, 0.03f, 0.08f, 0.72f), new Color(0.05f, 0.03f, 0.08f, 0.72f),
                new Color(hue.r, hue.g, hue.b, 0.42f), 1f, 0f, "badge" + tag));
            _badge.normal.textColor = new Color(hue.r, hue.g, hue.b, 0.92f);
            GUI.Label(br, tag, _badge);
        }

        // active check
        if (e.Active)
        {
            float cs = 28f * s;
            var cr = new Rect(r.xMax - cs - 12f * s, r.yMax - cs - 16f * s, cs, cs);
            GUI.DrawTexture(cr, UiTextures.Rounded((int)cs, (int)cs, cs * 0.5f,
                PinkHot, PinkHot, new Color(0, 0, 0, 0), 0f, 0f, "dot"));
            _check.fontSize = Mathf.RoundToInt(15f * s);
            GUI.Label(cr, "✓", _check);
        }

        GUI.color = prev;
    }

    /// <summary>
    /// Explicit confirm step: picking a tile only moves the focus, this applies it.
    /// Keeps browsing the list non-destructive (Enter does the same thing).
    /// </summary>
    private void DrawConfirm(float sw, float y, float s, float t, List<LangEntry> langs)
    {
        if (_cursor < 0 || _cursor >= langs.Count) return;
        var e = langs[_cursor];
        bool ok = e.Enabled;

        var L = UiStrings.For(e.Code);
        string label = ok
            ? L.Confirm + "  ·  " + e.Name
            : char.ToUpperInvariant(L.NoPack[0]) + L.NoPack.Substring(1) + "  ·  " + e.Name;
        var st = ok ? _button : _buttonOff;
        st.fontSize = Mathf.RoundToInt(17f * s);

        float w = Mathf.Max(280f * s, st.CalcSize(new GUIContent(label)).x + 70f * s);
        float h = 50f * s;
        _confirmHit = new Rect((sw - w) * 0.5f, y, w, h);
        _confirmVisible = true;

        Vector2 mouse;
        try { mouse = Event.current != null ? Event.current.mousePosition : GuiMouseFromInput(); }
        catch { mouse = GuiMouseFromInput(); }
        bool hot = ok && _confirmHit.Contains(mouse);

        Color prev = GUI.color;

        if (ok)
        {
            float pad = 18f * s;
            GUI.color = new Color(1f, 1f, 1f, (hot ? 0.55f : 0.34f) * t);
            GUI.DrawTexture(new Rect(_confirmHit.x - pad, _confirmHit.y - pad,
                                     w + pad * 2, h + pad * 2),
                UiTextures.Rounded((int)(w + pad * 2), (int)(h + pad * 2), h * 0.5f + pad,
                    new Color(1f, 0.55f, 0.82f, 0.40f), new Color(1f, 0.45f, 0.78f, 0.40f),
                    new Color(0, 0, 0, 0), 0f, pad, "btnglow"));

            GUI.color = new Color(1f, 1f, 1f, t);
            GUI.DrawTexture(_confirmHit, UiTextures.Rounded((int)w, (int)h, h * 0.5f,
                hot ? new Color(1f, 0.68f, 0.88f, 1f) : new Color(1f, 0.58f, 0.83f, 1f),
                hot ? new Color(1f, 0.52f, 0.80f, 1f) : new Color(0.98f, 0.44f, 0.75f, 1f),
                new Color(1f, 1f, 1f, 0.30f), 1.5f, 0f, "btn"));
        }
        else
        {
            GUI.color = new Color(1f, 1f, 1f, t);
            GUI.DrawTexture(_confirmHit, UiTextures.Rounded((int)w, (int)h, h * 0.5f,
                new Color(1f, 1f, 1f, 0.10f), new Color(1f, 1f, 1f, 0.06f),
                new Color(1f, 1f, 1f, 0.20f), 1f, 0f, "btnoff"));
        }

        GUI.Label(_confirmHit, label, st);
        GUI.color = prev;
    }

    private void DrawHints(float sw, float y, float s, bool gate)
    {
        // measured, then centred as one row of key pills
        var L = UiStrings.For(FocusCode());
        var keys = new[] { "← → ↑ ↓", "Enter", "F10" };
        var texts = new[] { L.HintPick, L.HintOk, L.HintLater };

        _hintKey.fontSize = Mathf.RoundToInt(13f * s);
        _hint.fontSize = Mathf.RoundToInt(13f * s);

        float pad = 10f * s, gapKV = 8f * s, gapSeg = 34f * s;
        float total = 0f;
        var kw = new float[keys.Length];
        var tww = new float[keys.Length];
        for (int i = 0; i < keys.Length; i++)
        {
            kw[i] = _hintKey.CalcSize(new GUIContent(keys[i])).x + pad * 2f;
            tww[i] = _hint.CalcSize(new GUIContent(texts[i])).x;
            total += kw[i] + gapKV + tww[i];
            if (i < keys.Length - 1) total += gapSeg;
        }

        float x = (sw - total) * 0.5f;
        float h = 26f * s;
        for (int i = 0; i < keys.Length; i++)
        {
            var kr = new Rect(x, y, kw[i], h);
            GUI.DrawTexture(kr, UiTextures.Rounded((int)kw[i], (int)h, 7f * s,
                new Color(1f, 1f, 1f, 0.10f), new Color(1f, 1f, 1f, 0.06f),
                new Color(1f, 1f, 1f, 0.20f), 1f, 0f, "key"));
            GUI.Label(kr, keys[i], _hintKey);
            x += kw[i] + gapKV;
            GUI.Label(new Rect(x, y, tww[i], h), texts[i], _hint);
            x += tww[i] + gapSeg;
        }
    }

    /// <summary>
    /// Project credit in the bottom-left corner: a dark link button with the GitHub mark
    /// and the repo slug in monospace, so it reads as a code reference rather than UI copy.
    /// Sits on a soft shadow because the art under that corner is a bright sunlit floor.
    /// The localized caption only appears on hover, keeping the resting state to one line.
    /// </summary>
    private void DrawCornerLink(float sw, float sh, float s, float t, string focus)
    {
        _link.fontSize = Mathf.RoundToInt(13f * s);
        _linkCaption.fontSize = Mathf.RoundToInt(11f * s);

        float icon = 20f * s;
        float padL = 16f * s, gap = 11f * s, padR = 16f * s;
        float arrow = 14f * s;
        float textW = _link.CalcSize(new GUIContent(RepoLabel)).x;
        float w = padL + icon + gap + textW + 10f * s + arrow + padR;
        float h = 38f * s;
        float r = h * 0.5f;

        float margin = 28f * s;
        _linkHit = new Rect(margin, sh - margin - h, w, h);
        _linkVisible = true;

        Vector2 mouse;
        try { mouse = Event.current != null ? Event.current.mousePosition : GuiMouseFromInput(); }
        catch { mouse = GuiMouseFromInput(); }
        bool hot = _linkHit.Contains(mouse);

        Color prev = GUI.color;
        GUI.color = new Color(1f, 1f, 1f, t);

        // No drop shadow: the art in this corner is dark violet (~40,22,40), so a shadow
        // is the only hard edge in the frame and reads as grime. A crisp hairline plus a
        // lit top edge carry the elevation instead.
        GUI.DrawTexture(_linkHit, UiTextures.Rounded((int)w, (int)h, r,
            new Color(0.075f, 0.038f, 0.105f, hot ? 0.93f : 0.87f),
            new Color(0.045f, 0.020f, 0.065f, hot ? 0.95f : 0.89f),
            new Color(1f, 0.85f, 0.94f, hot ? 0.68f : 0.44f), 1.4f, 0f, "linkpill"));

        // lit top edge, inset so it never touches the rounded ends
        GUI.color = new Color(1f, 1f, 1f, (hot ? 0.20f : 0.13f) * t);
        GUI.DrawTexture(new Rect(_linkHit.x + r - 4f * s, _linkHit.y + 1f * s,
                                 w - (r - 4f * s) * 2f, Mathf.Max(1f, 1f * s)), _px);
        GUI.color = new Color(1f, 1f, 1f, t);

        var mark = UiTextures.Png("github.png");
        var ir = new Rect(_linkHit.x + padL, _linkHit.y + (h - icon) * 0.5f, icon, icon);
        if (mark != null)
        {
            GUI.color = new Color(1f, 1f, 1f, (hot ? 1f : 0.88f) * t);
            GUI.DrawTexture(ir, mark);
        }

        GUI.color = new Color(1f, 1f, 1f, t);
        _link.normal.textColor = new Color(1f, 0.97f, 0.99f, hot ? 0.99f : 0.84f);
        GUI.Label(new Rect(ir.xMax + gap, _linkHit.y, textW + 4f * s, h), RepoLabel, _link);

        _linkCaption.alignment = TextAnchor.MiddleCenter;
        _linkCaption.normal.textColor = new Color(1f, 0.55f, 0.82f, hot ? 0.94f : 0.58f);
        GUI.Label(new Rect(_linkHit.xMax - padR - arrow, _linkHit.y, arrow + 2f * s, h), "↗", _linkCaption);

        // localized caption, hover only
        if (hot)
        {
            var L = UiStrings.For(focus);
            _linkCaption.alignment = TextAnchor.MiddleLeft;
            _linkCaption.normal.textColor = new Color(1f, 0.82f, 0.93f, 0.72f);
            GUI.Label(new Rect(_linkHit.x + padL, _linkHit.y - 22f * s, w, 18f * s),
                L.Project, _linkCaption);
        }

        GUI.color = prev;
    }

    private bool TryClick(Vector2 m, string source)
    {
        if (_chipVisible && _chipHit.width > 1f && _chipHit.Contains(m))
        {
            _open = !_open;
            Plugin.Log.LogInfo($"Language UI chip toggle open={_open} via {source} at {m}");
            return true;
        }

        bool open = _open || GateActive || _anim > 0.5f;
        if (!open) return false;

        if (_linkVisible && _linkHit.width > 1f && _linkHit.Contains(m))
        {
            try
            {
                Application.OpenURL(RepoUrl);
                Flash(RepoLabel);
                Plugin.Log.LogInfo($"Language UI: opened {RepoUrl}");
            }
            catch (Exception e)
            {
                Flash(RepoLabel);
                Plugin.Log.LogWarning($"OpenURL failed: {e.Message}");
            }
            return true;
        }

        if (_confirmVisible && _confirmHit.width > 1f && _confirmHit.Contains(m))
        {
            int ci = _cursor;
            if (ci < 0 || ci >= _rowCodes.Count)
            {
                Flash(UiStrings.For(FocusCode()).PickFirst);
                return true;
            }
            if (ci < _rowEnabled.Count && !_rowEnabled[ci])
            {
                Flash(NoPackMessage(_rowCodes[ci]));
                return true;
            }
            Plugin.Log.LogInfo($"Language UI confirm '{_rowCodes[ci]}' via button");
            Pick(_rowCodes[ci]);
            return true;
        }

        if (!GateActive && _closeVisible && _closeHit.width > 1f && _closeHit.Contains(m))
        {
            _open = false;
            Plugin.Log.LogInfo($"Language UI close via {source}");
            return true;
        }

        for (int i = 0; i < _rowHits.Count; i++)
        {
            var hit = _rowHits[i];
            if (hit.width <= 1f || hit.height <= 1f) continue;
            if (!hit.Contains(m)) continue;

            if (i >= _rowEnabled.Count || !_rowEnabled[i])
            {
                _cursor = i;
                Flash(NoPackMessage(_rowCodes[i]));
                return true;
            }

            string code = _rowCodes[i];

            // First click highlights, a second click on the same tile confirms —
            // so browsing the list never changes the language by accident.
            if (_cursor != i)
            {
                _cursor = i;
                Plugin.Log.LogInfo($"Language UI focus '{code}' via {source} at {m}");
                return true;
            }

            Plugin.Log.LogInfo($"Language UI confirm '{code}' via {source} at {m}");
            Pick(code);
            return true;
        }

        // During gate: swallow every non-tile click so the disclaimer cannot be pressed underneath.
        if (GateActive)
            return true;

        // Keep menu open until language / X / Esc / chip.
        return false;
    }

    private void Pick(string code)
    {
        bool wasGate = GateActive;
        try
        {
            // Apply language under the cover before revealing UI.
            bool changed = LanguageManager.SetLanguage(code, refreshUi: true);
            SaveFlag(code);
            Flash(LanguageManager.DisplayName(code));
            Plugin.Log.LogInfo($"Language picked: {code} changed={changed} now={LanguageManager.CurrentCode} gate={wasGate}");
        }
        catch (Exception e)
        {
            Flash("error");
            Plugin.Log.LogWarning($"Language pick failed: {e}");
            return;
        }

        if (wasGate)
        {
            // Hold cover while scanner rewrites TMP.
            _pendingGateCode = code;
            _pendingGateCloseAt = Time.unscaledTime + 0.35f;
            _open = true; // stay on language UI under cover
            Plugin.Log.LogInfo("Language boot gate: applying under cover, close scheduled");
            return;
        }

        // Keep panel open after a main-menu pick.
    }

    private void PauseGameForGate()
    {
        // pause
        try
        {
            if (!_freezeApplied)
            {
                _savedTimeScale = Time.timeScale;
                if (_savedTimeScale <= 0.0001f) _savedTimeScale = 1f;
                _freezeApplied = true;
                Plugin.Log.LogInfo($"Language UI freeze on (saved timeScale={_savedTimeScale})");
            }
            if (Mathf.Abs(Time.timeScale) > 0.0001f)
                Time.timeScale = 0f;
        }
        catch { /* ignore */ }

        // Disable EventSystems under the cover.
        if (_pausedSystems) return;
        try
        {
            _disabledSystems.Clear();
            var systems = FindAllEventSystems();
            for (int i = 0; i < systems.Count; i++)
            {
                var es = systems[i];
                if (es == null) continue;
                if (!es.enabled) continue;
                es.enabled = false;
                _disabledSystems.Add(es);
            }
            _pausedSystems = true;
            Plugin.Log.LogInfo($"Language UI paused {_disabledSystems.Count} EventSystem(s)");
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"PauseGameForGate EventSystem: {e.Message}");
        }
    }

    private void ForceFullRestore(string reason)
    {
        // restore timeScale
        try
        {
            float target = _savedTimeScale > 0.0001f ? _savedTimeScale : 1f;
            if (Mathf.Abs(Time.timeScale - target) > 0.0001f || _freezeApplied)
            {
                Time.timeScale = target;
                if (_freezeApplied)
                    Plugin.Log.LogInfo($"Language UI freeze off ({reason}) timeScale={Time.timeScale}");
            }
            _freezeApplied = false;
        }
        catch { /* ignore */ }

        // re-enable EventSystems
        try
        {
            int restored = 0;

            for (int i = 0; i < _disabledSystems.Count; i++)
            {
                var es = _disabledSystems[i];
                if (es == null) continue;
                if (!es.enabled)
                {
                    es.enabled = true;
                    restored++;
                }
            }
            _disabledSystems.Clear();

            // enable any remaining EventSystems
            var all = FindAllEventSystems();
            for (int i = 0; i < all.Count; i++)
            {
                var es = all[i];
                if (es == null) continue;
                if (!es.enabled)
                {
                    es.enabled = true;
                    restored++;
                }
            }

            if (_pausedSystems || restored > 0)
                Plugin.Log.LogInfo($"Language UI EventSystem restore ({reason}) reenabled~{restored}, total={all.Count}");

            _pausedSystems = false;
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"ForceFullRestore EventSystem ({reason}): {e.Message}");
            _pausedSystems = false;
        }
    }

    private static List<EventSystem> FindAllEventSystems()
    {
        var list = new List<EventSystem>();

        void add(EventSystem es)
        {
            if (es == null) return;
            for (int i = 0; i < list.Count; i++)
            {
                if (ReferenceEquals(list[i], es)) return;
            }
            list.Add(es);
        }

        try { add(EventSystem.current); } catch { /* ignore */ }

        // Try includeInactive overload first (Unity 2020+ / this game).
        try
        {
            var objs = UnityEngine.Object.FindObjectsOfType<EventSystem>(true);
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                    add(objs[i]);
                return list;
            }
        }
        catch { /* fall through */ }

        try
        {
            var objs = UnityEngine.Object.FindObjectsOfType<EventSystem>();
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                    add(objs[i]);
            }
        }
        catch { /* ignore */ }

        return list;
    }

    private void Flash(string msg)
    {
        _status = msg ?? "";
        _statusUntil = Time.unscaledTime + 1.8f;
    }

    private static void SaveFlag(string code)
    {
        try
        {
            var p = FlagPath;
            var dir = Path.GetDirectoryName(p);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(p, code ?? "en");
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"lang flag: {e.Message}");
        }
    }

    private static Vector2 GuiMouseFromInput()
    {
        var p = Input.mousePosition;
        return new Vector2(p.x, Screen.height - p.y);
    }

    private struct LangEntry
    {
        public string Code;
        public string Name;
        public string Meta;
        public bool Enabled;
        public bool Active;
    }

    private List<LangEntry> BuildLangList()
    {
        var result = new List<LangEntry>();
        var langs = LanguageManager.GetAvailableLanguages();
        var usable = new List<string>();
        var stubs = new List<string>();

        foreach (var c in langs)
        {
            bool ok;
            try { ok = LanguageManager.IsLanguageUsable(c); }
            catch
            {
                ok = string.Equals(c, "en", StringComparison.OrdinalIgnoreCase)
                     || LanguageManager.HasDictFile(c);
            }

            if (!ok && (string.Equals(c, "en", StringComparison.OrdinalIgnoreCase)
                        || string.Equals(c, "ru", StringComparison.OrdinalIgnoreCase)))
                ok = true;

            if (ok) usable.Add(c);
            else stubs.Add(c);
        }

        usable.Sort((a, b) => Rank(a).CompareTo(Rank(b)));
        stubs.Sort((a, b) => Rank(a).CompareTo(Rank(b)));

        string cur = LanguageManager.CurrentCode ?? "en";
        foreach (var c in usable)
        {
            result.Add(new LangEntry
            {
                Code = c,
                Name = LanguageManager.DisplayName(c),
                Meta = MetaFor(c, true),
                Enabled = true,
                Active = string.Equals(c, cur, StringComparison.OrdinalIgnoreCase)
            });
        }

        int n = 0;
        foreach (var c in stubs)
        {
            if (n >= 3) break;
            result.Add(new LangEntry
            {
                Code = c,
                Name = LanguageManager.DisplayName(c),
                Meta = MetaFor(c, false),
                Enabled = false,
                Active = false
            });
            n++;
        }

        if (result.Count == 0)
        {
            result.Add(new LangEntry { Code = "en", Name = "English", Meta = "оригинал", Enabled = true, Active = cur == "en" });
            result.Add(new LangEntry { Code = "ru", Name = "Русский", Meta = MetaFor("ru", true), Enabled = true, Active = cur == "ru" });
        }
        return result;
    }

    /// <summary>Second line on a tile: pack size, or why the pack is unusable.</summary>
    private static string MetaFor(string code, bool usable)
    {
        // each tile speaks its own language, so the row you point at reads natively
        var L = UiStrings.For(code);
        int n = LangStats.Count(code);
        if (n == -2) return L.Original;
        if (n == -1) return usable ? "…" : L.NoPack;
        if (n <= 0) return L.NoPack;
        if (!usable) return string.Format(L.Draft, Grouped(n));
        return string.Format(L.Lines, Grouped(n));
    }

    /// <summary>1234567 → "1 234 567" (grouping, culture independent).</summary>
    private static string Grouped(int n)
    {
        string s = n.ToString();
        if (s.Length <= 3) return s;
        var sb = new System.Text.StringBuilder(s.Length + 3);
        int lead = s.Length % 3;
        if (lead > 0) sb.Append(s, 0, lead);
        for (int i = lead; i < s.Length; i += 3)
        {
            if (sb.Length > 0) sb.Append(' ');
            sb.Append(s, i, 3);
        }
        return sb.ToString();
    }

    private static int Rank(string code)
    {
        code = (code ?? "").ToLowerInvariant();
        return code switch
        {
            "en" => 0,
            "ru" => 1,
            "uk" => 2,
            "de" => 3,
            "es" => 4,
            "pl" => 5,
            "vi" => 6,
            "zh" => 7,
            "ja" => 8,
            "ko" => 9,
            "fr" => 10,
            "pt" => 11,
            "it" => 12,
            "tr" => 13,
            "cs" => 14,
            _ => 50
        };
    }

    private static bool IsMainMenu(string scene)
    {
        if (string.IsNullOrEmpty(scene)) return false;
        if (scene.IndexOf("MainMenu", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (scene.Equals("Menu", StringComparison.OrdinalIgnoreCase)) return true;
        if (scene.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (scene.IndexOf("StartMenu", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        return false;
    }

    private static bool IsBoot(string scene)
    {
        if (string.IsNullOrEmpty(scene)) return true;
        if (scene.IndexOf("Disclamer", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (scene.IndexOf("Disclaimer", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (scene.IndexOf("Boot", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (scene.IndexOf("Splash", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (scene.IndexOf("Loading", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (scene.IndexOf("Init", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        if (scene.IndexOf("Intro", StringComparison.OrdinalIgnoreCase) >= 0) return true;
        return false;
    }

    private void EnsureStyles()
    {
        if (_stylesReady && _title != null && _px != null && _button != null && _link != null) return;
        _stylesReady = true;

        _pink = UiTextures.Solid(Pink);
        _px = UiTextures.Solid(Color.white);

        var font = UiTextures.UiFont();

        _kicker = Mk(font, 13, FontStyle.Bold, new Color(1f, 0.62f, 0.86f, 0.70f), TextAnchor.MiddleCenter);
        _title = Mk(font, 48, FontStyle.Bold, new Color(1f, 0.985f, 0.995f, 1f), TextAnchor.MiddleCenter);
        _sub = Mk(font, 21, FontStyle.Normal, new Color(1f, 0.89f, 0.95f, 0.62f), TextAnchor.MiddleCenter);
        _tileName = Mk(font, 22, FontStyle.Bold, new Color(1f, 0.99f, 1f, 0.98f), TextAnchor.MiddleLeft);
        _tileNameOn = Mk(font, 22, FontStyle.Bold, new Color(1f, 0.99f, 1f, 1f), TextAnchor.MiddleLeft);
        _tileNameOff = Mk(font, 22, FontStyle.Bold, new Color(1f, 1f, 1f, 0.45f), TextAnchor.MiddleLeft);
        _tileMeta = Mk(font, 12, FontStyle.Normal, new Color(1f, 0.90f, 0.96f, 0.50f), TextAnchor.MiddleLeft);
        _badge = Mk(font, 11, FontStyle.Bold, new Color(1f, 1f, 1f, 0.82f), TextAnchor.MiddleCenter);
        _chip = Mk(font, 15, FontStyle.Bold, new Color(1f, 0.94f, 0.97f, 0.95f), TextAnchor.MiddleLeft);
        _hintKey = Mk(font, 13, FontStyle.Bold, new Color(1f, 0.98f, 0.99f, 0.88f), TextAnchor.MiddleCenter);
        _hint = Mk(font, 13, FontStyle.Normal, new Color(1f, 0.92f, 0.96f, 0.52f), TextAnchor.MiddleLeft);
        _check = Mk(font, 15, FontStyle.Bold, new Color(0.16f, 0.04f, 0.12f, 1f), TextAnchor.MiddleCenter);
        _button = Mk(font, 17, FontStyle.Bold, new Color(0.17f, 0.04f, 0.13f, 1f), TextAnchor.MiddleCenter);
        _buttonOff = Mk(font, 17, FontStyle.Bold, new Color(1f, 1f, 1f, 0.45f), TextAnchor.MiddleCenter);
        _link = Mk(UiTextures.MonoFont() ?? font, 13, FontStyle.Bold,
                   new Color(1f, 0.96f, 0.99f, 0.80f), TextAnchor.MiddleLeft);
        _linkCaption = Mk(font, 11, FontStyle.Bold, new Color(1f, 0.72f, 0.90f, 0.42f), TextAnchor.MiddleCenter);
    }

    private static GUIStyle Mk(Font font, int size, FontStyle style, Color c, TextAnchor align)
    {
        var s = new GUIStyle(GUI.skin.label)
        {
            fontSize = size,
            fontStyle = style,
            alignment = align,
            wordWrap = false,
            clipping = TextClipping.Clip
        };
        if (font != null) s.font = font;
        s.normal.textColor = c;
        s.hover.textColor = c;
        s.active.textColor = c;
        return s;
    }

    private static float EaseOut(float x)
    {
        x = Mathf.Clamp01(x);
        return 1f - (1f - x) * (1f - x);
    }
}
