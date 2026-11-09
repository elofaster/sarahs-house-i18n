<div align="center">

<img src="../assets/banner.png" alt="Sarah's House – i18n" width="920">

[<kbd> English </kbd>](../../README.md)
[<kbd> Русский </kbd>](README.ru.md)
[<kbd> Українська </kbd>](README.uk.md)
<kbd> ● Deutsch </kbd>
[<kbd> Español </kbd>](README.es.md)
[<kbd> Français </kbd>](README.fr.md)
[<kbd> Polski </kbd>](README.pl.md)
[<kbd> Português </kbd>](README.pt.md)
[<kbd> Türkçe </kbd>](README.tr.md)
[<kbd> Tiếng Việt </kbd>](README.vi.md)
[<kbd> 中文 </kbd>](README.zh.md)
[<kbd> 日本語 </kbd>](README.ja.md)
[<kbd> 한국어 </kbd>](README.ko.md)

<br>

[![Release](https://img.shields.io/badge/RELEASE-v3.1.1-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.12.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/dl.png" width="500" alt="v3.1.1 herunterladen, 64,5 MB"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/dc.png" width="320" alt="Unserem Discord beitreten"></a>

## 💰 Übersetzungen unterstützen · Übersetzung eines beliebigen Spiels bestellen

Gefällt dir diese Übersetzung? Unterstütze das Projekt mit einer Krypto-Spende. Du kannst außerdem **die Übersetzung eines beliebigen Spiels** für eine Spende **ab 20 $** bestellen.

📧 Bestellungen & Kontakt: [novokex@gmail.com](mailto:novokex@gmail.com)

| Coin | Netzwerk | Adresse |
| :---: | :---: | :--- |
| **USDT** | TON | `UQDweoL-JmiDeyVCxIybr9NsH-4Ojnw9zplipMRLZGafX3jd` |
| **USDT** | TRON (TRC-20) | `TKWAByL6koBMLt6XZoasqVc2zxEtiCGxWL` |
| **USDT** | Ethereum (ERC-20) | `0x4718619DA946Aa243bedF9D22b90930f034567b3` |
| **USDT** | Solana | `HnzTfY39D2a1jbfS2mD1NV3SnaypW54KiGa54xbCGdJx` |
| **USDC** | Ethereum (ERC-20) | `0x4718619DA946Aa243bedF9D22b90930f034567b3` |
| **USDC** | Solana | `HnzTfY39D2a1jbfS2mD1NV3SnaypW54KiGa54xbCGdJx` |

</div>

<div align="center">
  <img src="../assets/game.jpg" width="880" alt="Sarah's House">
</div>

Das Spiel ist nicht enthalten. Du brauchst deine eigene Kopie.

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="../assets/steam.png" width="260" alt="Auf Steam kaufen"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="../assets/itch.png" width="220" alt="Auf itch.io holen"></a>
</p>

## Installation

1. Lade `SarahsHouse-i18n-v3.1.1.zip` von der [Release-Seite](https://github.com/elofaster/sarahs-house-i18n/releases/latest) herunter.
2. Entpacke das Archiv in den Spielordner, den mit `SarahsHouse.exe`. Zusammenführen, nicht verschachteln: die Ordner aus dem Archiv landen neben der exe.
3. Starte das Spiel. Der erste Start dauert ein bis zwei Minuten länger, weil BepInEx sich einmal für deine Kopie einrichtet, danach öffnet sich die Sprachauswahl von selbst.

Danach sieht der Ordner so aus:

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

`winhttp.dll`, `doorstop_config.ini`, `dotnet/` und `BepInEx/` kommen alle aus dem Archiv; der Mod selbst ist der letzte Ordner.

<details>
<summary><b>BepInEx 6 IL2CPP läuft schon?</b></summary>

Dann brauchst du nur den Mod: kopiere `BepInEx/plugins/SarahsHouseI18n/` aus dem Archiv in dein eigenes `BepInEx/plugins/` und starte das Spiel. Sonst ist nichts aus dem Archiv nötig, und die gewählte Sprache bleibt pro Installation gespeichert.

</details>

<details>
<summary><b>Sprache später ändern</b></summary>

Drücke im Hauptmenü <kbd>F10</kbd>. Die Auswahl öffnet sich über der Szene, die aktuelle Sprache steht in der Mitte des Rads, und mit dem Bestätigen wird der Text sofort getauscht.

</details>

## Die Sprachauswahl

<div align="center">
  <img src="../assets/picker.jpg" width="880" alt="Das Sprachrad">
</div>

Maus, Pfeiltasten oder Gamepad. <kbd>Enter</kbd> bestätigt, <kbd>F10</kbd> holt die Auswahl später zurück.

## Wenn etwas schiefgelaufen ist

<details>
<summary><b>Der erste Start dauert ewig</b></summary>

Das ist normal. BepInEx erzeugt beim ersten Start Assemblies für deine Kopie des Spiels, ein bis zwei Minuten, einmalig. Jeder weitere Start läuft normal schnell.
</details>

<details>
<summary><b>Mein Antivirus meldet <code>winhttp.dll</code></b></summary>

Die Datei ist der BepInEx-Loader und bei Unity-Mods überall Standard. Stelle sie aus der Quarantäne wieder her und nimm den Spielordner in die Ausnahmen auf.
</details>

<details>
<summary><b>Nach einem Spiel-Update sind einige Zeilen wieder englisch</b></summary>

Neue und geänderte Zeilen sind noch nicht übersetzt, sie fallen auf Englisch zurück und das Spiel läuft normal weiter. Das nächste Mod-Release schließt die Lücke.
</details>

<details>
<summary><b>Wie entferne ich den Mod?</b></summary>

Lösche `BepInEx/plugins/SarahsHouseI18n`. Wenn BepInEx auch weg soll, lösche zusätzlich `winhttp.dll`, `doorstop_config.ini` und `dotnet/`. Das Spiel ist dann wieder wie ausgeliefert.
</details>

<p align="center"><sub>
Die Schriften liegen unter SIL OFL und Apache 2.0 bei. Läuft auf BepInEx. Inoffizielle Fan-Übersetzung, nicht mit AceStudio verbunden.
</sub></p>
