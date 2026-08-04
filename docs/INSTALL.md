# Install

## Requirements

- Sarah's House, Windows x64 build
- BepInEx 6 (IL2CPP) — the Mono builds of BepInEx will not work

## Steps

1. Unpack BepInEx 6 IL2CPP x64 into the game folder (next to `SarahsHouse.exe`).
2. Run the game once so BepInEx generates `BepInEx/interop`, then close it.
3. Copy `SarahsHouseI18n/` into `BepInEx/plugins/`. You should end up with:

```
BepInEx/plugins/SarahsHouseI18n/
  SarahsHouseI18n.dll
  i18n/     {code}.json packs, human.txt
  ui/       picker art
  fonts/    fonts + arialuni_sdf_u2021
```

4. Launch. The language picker appears on the first run.

## Using it

- First run: pick a language, then press the confirm button or `Enter`.
- Later: `F10` in the main menu, or the language chip in the top-right corner.
- `Right Alt` + `L` cycles languages without opening the panel.

## Config

`BepInEx/config/com.elofaster.sarahshouse.i18n.cfg`

| Key | Meaning |
|---|---|
| `[Language] Code` | active language code |
| `[Language] EnableInGameUi` | show the picker at all |
| `[Locale] ForceLocale` | keep Unity's locale pinned |

## Troubleshooting

- **Boxes instead of letters** — `fonts/arialuni_sdf_u2021` is missing from the plugin folder.
- **Picker never appears** — delete `i18n/.lang_selected` to re-arm the first-run gate.
- **Text stays English** — check `BepInEx/LogOutput.log` for `Sarah's House - i18n`; the pack
  for that code may be missing from `i18n/`.
