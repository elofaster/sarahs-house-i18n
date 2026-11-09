<div align="center">

<img src="../assets/banner.png" alt="Sarah's House – i18n" width="920">

[<kbd> English </kbd>](../../README.md)
[<kbd> Русский </kbd>](README.ru.md)
[<kbd> Українська </kbd>](README.uk.md)
[<kbd> Deutsch </kbd>](README.de.md)
[<kbd> Español </kbd>](README.es.md)
<kbd> ● Français </kbd>
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

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/dl.png" width="500" alt="Télécharger la v3.1.1, 64,5 Mo"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/dc.png" width="320" alt="Rejoindre notre Discord"></a>

## 💰 Soutenir les traductions · Commander la traduction de n'importe quel jeu

Vous aimez cette traduction ? Soutenez le projet avec un don en cryptomonnaie. Vous pouvez aussi **commander la traduction de n'importe quel jeu** pour un don **à partir de 20 $**.

📧 Commandes et contact: [novokex@gmail.com](mailto:novokex@gmail.com)

| Monnaie | Réseau | Adresse |
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

Le jeu n'est pas inclus. Il vous faut votre propre copie.

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="../assets/steam.png" width="260" alt="Acheter sur Steam"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="../assets/itch.png" width="220" alt="Le prendre sur itch.io"></a>
</p>

## Installation

1. Téléchargez `SarahsHouse-i18n-v3.1.1.zip` depuis la [page des versions](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Décompressez l'archive dans le dossier du jeu, celui où se trouve `SarahsHouse.exe`. Fusionnez, sans imbriquer : les dossiers de l'archive arrivent à côté de l'exe.
3. Lancez le jeu. Le premier démarrage prend une ou deux minutes de plus, le temps que BepInEx se configure pour votre copie, ensuite le sélecteur de langue s'ouvre tout seul.

Ensuite, le dossier ressemble à ceci :

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

`winhttp.dll`, `doorstop_config.ini`, `dotnet/` et `BepInEx/` viennent tous de l'archive ; le mod est le dernier dossier.

<details>
<summary><b>Vous avez déjà BepInEx 6 IL2CPP ?</b></summary>

Il vous faut alors seulement le mod : copiez `BepInEx/plugins/SarahsHouseI18n/` de l'archive dans votre propre `BepInEx/plugins/` et lancez le jeu. Rien d'autre n'est nécessaire, et la langue choisie est mémorisée pour chaque installation.

</details>

<details>
<summary><b>Changer de langue plus tard</b></summary>

Appuyez sur <kbd>F10</kbd> dans le menu principal. Le sélecteur s'ouvre sur la scène, la langue actuelle au milieu de la roue, et la validation change le texte immédiatement.

</details>

## Le sélecteur

<div align="center">
  <img src="../assets/picker.jpg" width="880" alt="La roue des langues">
</div>

Souris, flèches ou manette. <kbd>Entrée</kbd> valide, <kbd>F10</kbd> ramène le sélecteur plus tard.

## Si quelque chose a raté

<details>
<summary><b>Le premier démarrage est interminable</b></summary>

C'est normal. BepInEx génère les assemblys pour votre copie du jeu au premier lancement, une ou deux minutes, une seule fois. Les lancements suivants sont à vitesse normale.
</details>

<details>
<summary><b>Mon antivirus signale <code>winhttp.dll</code></b></summary>

Ce fichier est le chargeur de BepInEx, standard pour les mods Unity. Restaurez-le depuis la quarantaine et ajoutez le dossier du jeu aux exclusions.
</details>

<details>
<summary><b>Certaines lignes repassent en anglais après une mise à jour du jeu</b></summary>

Les lignes nouvelles et modifiées ne sont pas encore traduites : elles s'affichent en anglais et le jeu continue normalement. La prochaine version du mod comble le manque.
</details>

<details>
<summary><b>Comment retirer le mod ?</b></summary>

Supprimez `BepInEx/plugins/SarahsHouseI18n`. Pour retirer aussi BepInEx, supprimez également `winhttp.dll`, `doorstop_config.ini` et `dotnet/`. Le jeu redevient comme à l'origine.
</details>

<p align="center"><sub>
Les polices sont fournies sous SIL OFL et Apache 2.0. Fonctionne sur BepInEx. Traduction de fans non officielle, sans lien avec AceStudio.
</sub></p>
