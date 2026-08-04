# Adding a language

A language pack is one UTF-8 JSON file: a flat map from the game's original English
string to the translation.

```json
{
  "New Game": "Nouvelle partie",
  "Tuesday": "Mardi",
  "Sarah's room": "La chambre de Sarah"
}
```

1. Name it `{code}.json` using the ISO 639-1 code (`fr`, `pt`, `cs`, …) and put it in `packs/`.
2. Add a display name for the code in `LanguageManager.DisplayNames` (native spelling).
3. Optional but recommended: add the picker's own labels for that code in `UiStrings`
   — otherwise the picker chrome falls back to English while your language is focused.
4. Optional: a flag tile at `mod/assets/ui/flags/{code}.png` (256×160). Without it the tile
   falls back to a two-colour gradient from `UiTextures._flagFallback`.
5. Rebuild and install: `cd mod; .\build.ps1 -Reset`.

## Rules for the values

- Keep placeholders intact: `{0}`, `@{player}`, `{tags}`.
- Keep TMP markup intact: `<b>`, `<i>`, `<color=...>`, `<sprite=...>`.
- Keys are matched after trimming and normalising whitespace, so leading/trailing spaces
  in a key do not matter.
- A missing key simply falls through to English — a partial pack is safe to ship.

## Marking a pack as human-reviewed

`packs/human.txt`, one code per line, `#` for comments. Listed codes show **HUMAN** in the
picker; everything else shows **AI**. Read at runtime — no rebuild required.
