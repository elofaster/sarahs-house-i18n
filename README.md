<div align="center">

<img src="docs/assets/banner.png" alt="Sarah's House — i18n" width="920">

<kbd> ● English </kbd>
[<kbd> Русский </kbd>](docs/readme/README.ru.md)
[<kbd> Українська </kbd>](docs/readme/README.uk.md)
[<kbd> Deutsch </kbd>](docs/readme/README.de.md)
[<kbd> Español </kbd>](docs/readme/README.es.md)
[<kbd> Français </kbd>](docs/readme/README.fr.md)
[<kbd> Polski </kbd>](docs/readme/README.pl.md)
[<kbd> Português </kbd>](docs/readme/README.pt.md)
[<kbd> Türkçe </kbd>](docs/readme/README.tr.md)
[<kbd> Tiếng Việt </kbd>](docs/readme/README.vi.md)
[<kbd> 中文 </kbd>](docs/readme/README.zh.md)
[<kbd> 日本語 </kbd>](docs/readme/README.ja.md)
[<kbd> 한국어 </kbd>](docs/readme/README.ko.md)

<br>

[![Release](https://img.shields.io/badge/RELEASE-v3.0.0-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.11.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

The mod translates **Sarah's House** into 12 languages. The language is picked on the first
launch and can be changed later with **F10** in the main menu. Game files stay untouched.

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="docs/assets/btn_download.en.png" width="410" alt="Download latest release"></a>

<img src="docs/assets/picker.png" alt="language picker" width="920">

</div>

<img src="docs/assets/h_install.en.png" alt="Install" height="48">

1. Download `SarahsHouse-i18n-v3.0.0.zip` from the [releases page](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Extract the archive into the game folder — next to `SarahsHouse.exe`.
3. Start the game. The first launch takes a minute or two longer: BepInEx sets itself up for your copy once. Then the language picker opens on its own.

It should end up like this:

```text
SarahsHouse/
├─ SarahsHouse.exe          ← the game
├─ winhttp.dll              ┐
├─ doorstop_config.ini      │  from the archive
├─ dotnet/                  │
└─ BepInEx/                 ┘
```

<details>
<summary>Already running BepInEx 6 IL2CPP</summary>
<br>

Just copy `SarahsHouseI18n/` into `BepInEx/plugins/`. Details in [docs/INSTALL.md](docs/INSTALL.md).

</details>

<br>

<img src="docs/assets/h_fix.en.png" alt="If something went wrong" height="48">

<details>
<summary>The first launch takes very long</summary>
<br>

That's expected: BepInEx generates assemblies for your copy of the game. It happens once — every later launch is normal.

</details>
<details>
<summary>Antivirus complains about <code>winhttp.dll</code></summary>
<br>

That's the BepInEx loader, standard for Unity mods. Restore the file from quarantine and add the game folder to the exclusions.

</details>
<details>
<summary>Some text is English again after a game update</summary>
<br>

New and changed lines are not translated yet — they show up in English and the game keeps working. Update the mod when a fresh version is out.

</details>
<details>
<summary>How to remove the mod</summary>
<br>

Delete the `BepInEx/plugins/SarahsHouseI18n` folder. To remove BepInEx as well, also delete `winhttp.dll`. The game returns to its original state.

</details>

<br>

<img src="docs/assets/h_translators.en.png" alt="For translators" height="48">

Translation packs are plain JSON files in [`packs/`](packs): the key is the English line, the value is the translation. In an installed copy the same files live in `BepInEx/plugins/SarahsHouseI18n/i18n/` — edits show up after a game restart.

A new language can be added without rebuilding the mod — see [docs/ADD-LANGUAGE.md](docs/ADD-LANGUAGE.md). Pull requests are welcome.

<div align="center">

<img src="docs/assets/divider.png" width="560">

<a href="https://github.com/elofaster/sarahs-house-i18n"><img src="docs/assets/star.en.png" alt="Leave a star" width="720"></a>

<a href="https://github.com/elofaster"><img src="docs/assets/sign.png" alt="elofaster" height="66"></a>

fonts — SIL OFL / Apache 2.0, listed in [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md) · runs on [BepInEx](https://github.com/BepInEx/BepInEx)

Not affiliated with the game's author.

</div>
