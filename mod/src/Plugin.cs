using System;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SarahsHouseI18n;

/// <summary>
/// Sarah's House multilanguage plugin: font injection, i18n packs, in-game language picker.
/// </summary>
[BepInPlugin(Guid, Name, Version)]
public class Plugin : BasePlugin
{
    public const string Guid = "com.elofaster.sarahshouse.i18n";
    public const string Name = "Sarah's House - i18n";
    public const string Version = "3.0.0";

    internal static new ManualLogSource Log;
    internal static new PluginConfig Config;

    public override void Load()
    {
        Log = base.Log;
        Config = new PluginConfig(base.Config);
        Log.LogInfo($"{Name} v{Version} loading…");

        LanguageManager.Initialize();

        var harmony = new Harmony(Guid);
        harmony.PatchAll(typeof(TextHooks));
        harmony.PatchAll(typeof(FontPatches));
        // SceneLoadPatches is best-effort; failures are non-fatal at patch time
        try { harmony.PatchAll(typeof(SceneLoadPatches)); }
        catch (Exception e) { Log.LogWarning($"SceneLoadPatches skip: {e.Message}"); }
        TextHooks.PatchDynamic(harmony);
        // Febucci TextAnimator: translate animated/typewriter/shake text at its source.
        try { TextAnimatorHooks.PatchDynamic(harmony); }
        catch (Exception e) { Log.LogWarning($"TextAnimatorHooks skip: {e.Message}"); }

        AddComponent<Bootstrap>();
        AddComponent<LanguageOverlayUI>();
        Log.LogInfo($"{Name} loaded (lang={LanguageManager.CurrentCode}, {TranslationDict.Count} translations, {TranslationDict.TemplateCount} templates)");
    }
}

internal sealed class Bootstrap : MonoBehaviour
{
    public Bootstrap(System.IntPtr ptr) : base(ptr) { }

    private int _tick;
    private float _next;
    private bool _warmed;
    private string _lastScene = "";
    private static int _burstFrames;
    private float _lastBurstScan;

    public static void RequestBurst(int frames)
    {
        if (frames > _burstFrames) _burstFrames = frames;
    }

    public static bool IsBursting => _burstFrames > 0;

    private void Update()
    {
        try
        {
            string sn = "";
            try { sn = SceneManager.GetActiveScene().name ?? ""; } catch { sn = ""; }
            if (!string.IsNullOrEmpty(sn) && sn != _lastScene)
            {
                _lastScene = sn;
                Plugin.Log.LogInfo($"Scene '{sn}' active — immediate translate");
                SafePass("scene-enter");
                RequestBurst(10);
            }
        }
        catch { /* ignore */ }

        if (_burstFrames > 0)
        {
            _burstFrames--;
            // Throttle the heavy full-scan during the burst — the setter/OnEnable
            // hooks translate new text immediately, so the burst only needs a few
            // spaced scans to catch late-populated UI (not one per frame).
            if (Time.unscaledTime - _lastBurstScan >= 0.1f)
            {
                _lastBurstScan = Time.unscaledTime;
                SafePass("scene-burst");
            }
            return;
        }

        if (Time.unscaledTime < _next) return;
        _tick++;
        float delay = _warmed ? 8f : Mathf.Min(3f, 0.4f * Mathf.Pow(2f, Mathf.Min(_tick - 1, 3)));
        _next = Time.unscaledTime + delay;

        FontManager.ApplyToScene();
        LocaleSwitcher.TryApplyDesired();
        MissingLogger.TryFlush();

        if (FontManager.IsReady)
        {
            TextScanner.ScanScene();
            if (!_warmed && _tick >= 3)
            {
                _warmed = true;
                Plugin.Log.LogInfo("Warm-up done; primary = setters/OnEnable + scene burst.");
            }
        }
    }

    private static void SafePass(string tag)
    {
        try { FontManager.ApplyToScene(); } catch (Exception e) { Plugin.Log.LogWarning($"{tag} fonts: {e.Message}"); }
        try { LocaleSwitcher.TryApplyDesired(); } catch { /* ignore */ }
        try { TextScanner.ScanScene(); } catch (Exception e) { Plugin.Log.LogWarning($"{tag} scan: {e.Message}"); }
    }
}