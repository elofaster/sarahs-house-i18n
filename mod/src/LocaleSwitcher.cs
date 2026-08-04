using System;
using UnityEngine.Localization.Settings;

namespace SarahsHouseI18n;

/// <summary>
/// Keep Unity Localization locale aligned with LanguageManager.
/// </summary>
internal static class LocaleSwitcher
{
    private static bool _localesLogged;
    private static string _desired = "ru";
    private static float _nextRetryUnscaled;
    private static int _failStreak;

    public static string DesiredCode => _desired;

    public static void SetDesiredLocale(string code)
    {
        _desired = LanguageManager.NormalizeCode(code);
        _failStreak = 0;
        _nextRetryUnscaled = 0f;
    }

    /// <summary>Back-compat entry for older call sites.</summary>
    public static void TryForceRussian() => TryApplyDesired(force: false);

    public static void TryApplyDesired(bool force = false)
    {
        if (!Plugin.Config.ForceLocale.Value && !force) return;

        try
        {
            float now = UnityEngine.Time.unscaledTime;
            if (!force && _failStreak >= 5 && now < _nextRetryUnscaled) return;
        }
        catch { /* Time may be unavailable very early */ }

        try
        {
            if (LocalizationSettings.Instance == null) return;
            var localesProvider = LocalizationSettings.AvailableLocales;
            if (localesProvider == null) return;
            var locales = localesProvider.Locales;
            if (locales == null || locales.Count == 0) return;

            if (!_localesLogged)
            {
                _localesLogged = true;
                Plugin.Log.LogInfo($"Available locales ({locales.Count}):");
                for (int i = 0; i < locales.Count; i++)
                {
                    UnityEngine.Localization.Locale l = locales[i];
                    Plugin.Log.LogInfo($"  [{i}] code='{l?.Identifier.Code}' name='{l?.LocaleName}'");
                }
            }

            var desired = LanguageManager.NormalizeCode(_desired);
            UnityEngine.Localization.Locale current = LocalizationSettings.SelectedLocale;
            var currentCode = current?.Identifier.Code ?? "";
            if (!string.IsNullOrEmpty(desired) && currentCode.StartsWith(desired, StringComparison.OrdinalIgnoreCase))
            {
                _failStreak = 0;
                return;
            }

            UnityEngine.Localization.Locale target = null;
            for (int i = 0; i < locales.Count; i++)
            {
                UnityEngine.Localization.Locale l = locales[i];
                if (l == null) continue;
                var code = l.Identifier.Code ?? "";
                if (code.Equals(desired, StringComparison.OrdinalIgnoreCase)
                    || code.StartsWith(desired + "-", StringComparison.OrdinalIgnoreCase)
                    || code.StartsWith(desired + "_", StringComparison.OrdinalIgnoreCase))
                { target = l; break; }
            }

            if (target == null)
            {
                string needle = desired switch
                {
                    "ru" => "russian",
                    "en" => "english",
                    "de" => "german",
                    "uk" => "ukrain",
                    _ => desired
                };
                for (int i = 0; i < locales.Count; i++)
                {
                    UnityEngine.Localization.Locale l = locales[i];
                    if (l == null) continue;
                    var name = l.LocaleName ?? "";
                    if (name.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0)
                    { target = l; break; }
                    if (desired == "ru" && name.IndexOf("русск", StringComparison.OrdinalIgnoreCase) >= 0)
                    { target = l; break; }
                }
            }

            if (target == null)
            {
                _failStreak++;
                try { _nextRetryUnscaled = UnityEngine.Time.unscaledTime + 5f; } catch { }
                if (_failStreak <= 3)
                    Plugin.Log.LogWarning($"Locale '{desired}' not found in AvailableLocales");
                return;
            }

            LocalizationSettings.SelectedLocale = target;
            _failStreak = 0;
            Plugin.Log.LogInfo(
                $"Locale switched to '{target.Identifier.Code}' ({target.LocaleName}) " +
                $"(was '{currentCode}', desired='{desired}')");
        }
        catch (Exception e)
        {
            _failStreak++;
            try { _nextRetryUnscaled = UnityEngine.Time.unscaledTime + 3f; } catch { }
            Plugin.Log.LogWarning($"TryApplyDesired: {e.Message}");
        }
    }
}