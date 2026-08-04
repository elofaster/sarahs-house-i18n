using System;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace SarahsHouseI18n;

/// <summary>Best-effort scene-load hook (skipped if method is missing).</summary>
[HarmonyPatch]
internal static class SceneLoadPatches
{
    [HarmonyPatch(typeof(SceneManager), "Internal_SceneLoaded")]
    [HarmonyPostfix]
    public static void Postfix_Internal_SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try
        {
            Plugin.Log.LogInfo($"Internal_SceneLoaded '{scene.name}' mode={mode}");
            FontManager.ApplyToScene();
            TextScanner.ScanScene();
            Bootstrap.RequestBurst(10);
        }
        catch (Exception e)
        {
            Plugin.Log.LogWarning($"Internal_SceneLoaded handler: {e.Message}");
        }
    }
}