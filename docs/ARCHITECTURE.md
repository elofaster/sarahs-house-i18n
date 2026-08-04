# Architecture

## Why a runtime overlay

The game (Unity IL2CPP + Addressables + Unity Localization) ships string tables where
only English is populated — the `ru`/`de`/`uk` tables exist but are nearly empty, and a
large share of the UI is hardcoded in scenes and asset bundles rather than in tables.
Switching Unity's locale therefore yields blank or English text.

So the plugin does the opposite: it pins Unity Localization to the **English source**,
lets the game emit its original strings, and translates them as they reach the text
components. One flat `EN -> target` map per language is all a translation needs.

## Pieces

| File | Responsibility |
|---|---|
| `Plugin.cs` | BepInEx entry point, Harmony patching, component bootstrap |
| `LanguageManager.cs` | active language, available languages, pack discovery and caching |
| `TranslationDict.cs` | the `EN -> target` map, lookup, templates, reverse lookup for `en` |
| `TextHooks.cs` | Harmony hooks on TMP text setters / `OnEnable` — the primary path |
| `TextAnimatorHooks.cs` | Febucci TextAnimator: typewriter / animated text, translated at source |
| `TextScanner.cs` | sweeps a scene for text the hooks did not catch |
| `TextLayout.cs` | per-string layout fixes (autofit, line breaks) for longer translations |
| `FontManager.cs` | injects glyph coverage: bundled TTFs, OS donors, prebuilt Arial Unicode SDF bundle |
| `LocaleSwitcher.cs` | keeps Unity's `SelectedLocale` where the plugin wants it |
| `LanguageOverlayUI.cs` | the picker: first-run gate, flag tiles, keyboard nav, GitHub credit |
| `UiTextures.cs` | procedural IMGUI art (rounded plates, glows, flag tiles) + PNG loading |
| `UiStrings.cs` | the picker's own labels in 15 languages — it renders before a language exists |
| `LangStats.cs` | pack sizes (streamed count) and AI/HUMAN attribution |
| `MissingLogger.cs` | logs untranslated strings for the next pass |

## Fonts

Two problems stack here: the game's TMP atlases carry no Cyrillic or CJK, and the game
uses eight different typefaces. Replacing them all with one or two donors is what makes a
translation look "fontless", so the donor is chosen per face **and** per language.

`FontManager.PickTtfForAsset` keeps the original typeface whenever it can render the
active language, and substitutes only where the original has no glyphs:

| Game TMP asset | Latin languages | Cyrillic / Vietnamese / CJK |
|---|---|---|
| `Anton SDF` | Anton | **Oswald Bold** — same tall condensed voice |
| `BakbakOne` + Outline / Shadowed / ShadowedRed | Bakbak One | **Geologica Bold** — closest match in weight and width |
| `Oswald Bold SDF`, `OswaldBolOutline` | Oswald Bold | Oswald Bold (covers both) |
| `Lato-Regular` | Lato | Lato — 2196 glyphs, Cyrillic and Greek included |
| `RobotoDialog` | Roboto | Roboto — Cyrillic, Greek, Vietnamese |
| `EraserRegular`, `kinkie` | Caveat Bold | Caveat Bold — OFL handwriting with Cyrillic |
| `ARIALUNI SDF`, `LiberationSans SDF` | Liberation Sans | Liberation Sans |

Coverage behind those decisions was measured from each file's `cmap`, not from vendor
descriptions: Anton and Bakbak One have **zero** Cyrillic, and Bakbak One covers only half
of Vietnamese — which is why `vi` is grouped with the scripts that need a substitute.

Every shipped face is SIL OFL or Apache 2.0. Weights that Google Fonts no longer ships as
static files (Oswald Bold, Nunito Black, Caveat Bold, Geologica Bold, Roboto) were
instanced from the variable originals with `fontTools`, because TMP's `LoadFontFace` takes a
plain TTF and would otherwise render the variable font's default Regular weight.

`[Fonts] SourcePriority` in the config overrides the whole table when non-empty — that is
also how to try the alternates that ship unused: `Geologica-Black`, `Nunito-Black`,
`Roboto-Bold`.

CJK is separate: `fonts/arialuni_sdf_u2021` is a prebuilt Arial Unicode SDF asset bundle
covering Latin + Cyrillic + full CJK, and for `ja` an OS-installed Noto Sans JP donor is
preferred so kanji use Japanese rather than Chinese forms. Without that bundle, CJK is tofu.

## The picker

`i18n/.lang_selected` records the choice. While it is absent the first-run gate is armed:
the game is paused, its `EventSystem`s are disabled so the disclaimer underneath cannot be
clicked, and the language stays English until the player confirms. Selecting a tile only
moves focus — the language is applied by `Enter`, a second click, or the confirm button.

The picker's own text cannot come from a pack (no language is chosen yet), so `UiStrings`
carries those ~11 labels compiled in. The chrome follows the tile under the cursor; each
tile describes itself in its own language.