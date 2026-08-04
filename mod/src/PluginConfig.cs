using BepInEx.Configuration;

namespace SarahsHouseI18n;

/// <summary>BepInEx config (com.elofaster.sarahshouse.i18n.cfg).</summary>
internal sealed class PluginConfig
{
    public ConfigEntry<bool> ForceLocale;
    public ConfigEntry<string> LanguageCode;
    public ConfigEntry<bool> EnableLanguageUi;
    public ConfigEntry<bool> VerboseLogging;
    public ConfigEntry<bool> LogMissingTranslations;
    public ConfigEntry<bool> ResizeDialogText;
    public ConfigEntry<float> MinFitScale;
    public ConfigEntry<bool> UseDonorBundles;
    public ConfigEntry<bool> SwapFonts;

    // Exact strings that must not be translated (logo layers).
    public ConfigEntry<string> SkipTextPatterns;

    public PluginConfig(ConfigFile cfg)
    {
        LanguageCode = cfg.Bind(
            "Language", "Code", "ru",
            "Active language code: en, ru, de, uk, ... English (en) is passthrough. "
            + "Other codes load i18n/{code}.json (EN→target).");

        EnableLanguageUi = cfg.Bind(
            "Language", "EnableInGameUi", true,
            "Show in-game language panel (F10 / RightCtrl+L). RightAlt+L cycles usable languages.");

        ForceLocale = cfg.Bind(
            "Locale", "ForceLocale", true,
            "Keep Unity Localization SelectedLocale aligned with Language.Code (re-assert after scenes).");

        // ForceRussian kept for old configs; ForceLocale is the real setting.

        VerboseLogging = cfg.Bind(
            "Diagnostics", "Verbose", false,
            "Log every text replacement and font swap. Disable in release.");

        LogMissingTranslations = cfg.Bind(
            "Diagnostics", "LogMissing", true,
            "Periodically write every English string seen at runtime that has no "
            + "translation to BepInEx/missing_translations.txt. Useful for translators.");

        ResizeDialogText = cfg.Bind(
            "Layout", "ResizeDialogText", false,
            "If true, apply length-based fontSize adaptation to dialogue/speech "
            + "bubbles (RobotoDialog / Dialog* hierarchy). Default false: dialogue "
            + "is typewriter/streamed, so resizing causes a visible jump when the "
            + "line finishes. Menu/UI labels still auto-fit.");

        MinFitScale = cfg.Bind(
            "Layout", "MinScale", 0.55f,
            "Lower bound for auto-fitted labels/titles, as a fraction of the "
            + "designer font size (0.4-1.0). Raise if translated text looks too "
            + "small, lower if it overflows. Paragraphs of 60+ characters wrap "
            + "and use a fixed 0.85 (0.80 when much longer); single words "
            + "without spaces may go ~0.15 below this value.");

        UseDonorBundles = cfg.Bind(
            "Fonts", "UseDonorBundles", true,
            "Load the prebuilt TMP font bundles from fonts/*_sdf_* and use them as per-face "
            + "Cyrillic donors, so each game typeface keeps its own design instead of falling "
            + "back to one Arial Unicode. Turn OFF to rule the bundles out when "
            + "debugging a text issue — the mod then falls back to the in-game donors.");

        SwapFonts = cfg.Bind(
            "Fonts", "SwapFonts", true,
            "With UseDonorBundles on, replace the font on each text component with the baked "
            + "equivalent instead of adding it as a fallback. A fallback only fills in missing "
            + "glyphs, so a line gets drawn from two faces with two sets of metrics; a swap "
            + "renders the whole string from one asset.");

        SkipTextPatterns = cfg.Bind(
            "Skip", "TextPatterns", @"SARAH'S HOUSE,SARAH'S\nHOUSE",
            "Comma-separated EXACT text values (case-insensitive, trimmed) that "
            + "the plugin will NOT translate. Use \\n to encode a literal newline. "
            + "Default covers the main game-logo strings so all copies stay "
            + "identical and don't drift apart visually after translation. Keep "
            + "patterns long and specific — short patterns like \"HOUSE\" alone "
            + "could accidentally block legitimate dialog lines.");
    }
}