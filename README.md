<div align="center">

<img src="docs/assets/banner.png" alt="Sarah's House – i18n" width="920">

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

[![Release](https://img.shields.io/badge/RELEASE-v3.1.1-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.12.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="docs/assets/dl.png" width="500" alt="Download v3.1.1, 64.5 MB"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="docs/assets/dc.png" width="320" alt="Join our Discord"></a>

## 💰 Support the translations · Order a translation of any game

If you enjoy this translation, you can support the project with a crypto donation. You can also **order a translation of any game** for a donation **from $20**.

📧 Orders & contact: [novokex@gmail.com](mailto:novokex@gmail.com)

| Coin | Network | Address |
| :---: | :---: | :--- |
| **USDT** | TON | `UQDweoL-JmiDeyVCxIybr9NsH-4Ojnw9zplipMRLZGafX3jd` |
| **USDT** | TRON (TRC-20) | `TKWAByL6koBMLt6XZoasqVc2zxEtiCGxWL` |
| **USDT** | Ethereum (ERC-20) | `0x4718619DA946Aa243bedF9D22b90930f034567b3` |
| **USDT** | Solana | `HnzTfY39D2a1jbfS2mD1NV3SnaypW54KiGa54xbCGdJx` |
| **USDC** | Ethereum (ERC-20) | `0x4718619DA946Aa243bedF9D22b90930f034567b3` |
| **USDC** | Solana | `HnzTfY39D2a1jbfS2mD1NV3SnaypW54KiGa54xbCGdJx` |

</div>

<div align="center">
  <img src="docs/assets/game.jpg" width="880" alt="Sarah's House">
</div>

The game is not included. You need your own copy.

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="docs/assets/steam.png" width="260" alt="Buy on Steam"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="docs/assets/itch.png" width="220" alt="Get it on itch.io"></a>
</p>

## Install

1. Download `SarahsHouse-i18n-v3.1.1.zip` from the [releases page](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Unzip it into the game folder, the one with `SarahsHouse.exe` in it. Merge, do not nest: the folders from the archive land next to the exe.
3. Launch the game. The first start takes a minute or two longer than usual while BepInEx sets itself up for your copy, then the language picker opens on its own.

After that the folder looks like this:

```text
SarahsHouse/
├─ SarahsHouse.exe
├─ winhttp.dll
├─ doorstop_config.ini
├─ dotnet/
└─ BepInEx/
    └─ plugins/
        └─ SarahsHouseI18n/
```

`winhttp.dll`, `doorstop_config.ini`, `dotnet/` and `BepInEx/` all come from the archive; the mod itself is the last folder.

<details>
<summary><b>Already running BepInEx 6 IL2CPP?</b></summary>

Then you only need the mod itself: copy `BepInEx/plugins/SarahsHouseI18n/` from the archive into your own `BepInEx/plugins/` and start the game. Nothing else from the archive is required, and the language you pick is remembered per install.

</details>

<details>
<summary><b>Changing the language later</b></summary>

Press <kbd>F10</kbd> in the main menu. The picker opens over the scene with the current language in the middle of the wheel, and confirming swaps the text right away.

</details>

## The picker

<div align="center">
  <img src="docs/assets/picker.jpg" width="880" alt="The language wheel">
</div>

Mouse, arrow keys or a gamepad. <kbd>Enter</kbd> confirms, <kbd>F10</kbd> brings the picker back later.

## If something went wrong

<details>
<summary><b>The first launch takes forever</b></summary>

That one is expected. BepInEx generates assemblies for your copy of the game the first time it runs, a minute or two, once. Every launch after that is normal speed.
</details>

<details>
<summary><b>My antivirus flags <code>winhttp.dll</code></b></summary>

That file is the BepInEx loader and it is standard for Unity mods everywhere. Restore it from quarantine and add the game folder to your exclusions.
</details>

<details>
<summary><b>Some lines went back to English after a game update</b></summary>

New and changed lines are not translated yet, so they fall back to English and the game keeps running normally. The next mod release closes the gap.
</details>

<details>
<summary><b>How do I remove the mod?</b></summary>

Delete `BepInEx/plugins/SarahsHouseI18n`. To take BepInEx out as well, also delete `winhttp.dll`, `doorstop_config.ini` and `dotnet/`. The game goes back to how it shipped.
</details>

<p align="center"><sub>
Fonts are bundled under SIL OFL and Apache 2.0. Built on <a href="https://github.com/BepInEx/BepInEx">BepInEx</a>. Unofficial fan translation, not affiliated with AceStudio.
</sub></p>
