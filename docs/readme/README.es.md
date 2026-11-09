<div align="center">

<img src="../assets/banner.png" alt="Sarah's House – i18n" width="920">

[<kbd> English </kbd>](../../README.md)
[<kbd> Русский </kbd>](README.ru.md)
[<kbd> Українська </kbd>](README.uk.md)
[<kbd> Deutsch </kbd>](README.de.md)
<kbd> ● Español </kbd>
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

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/dl.png" width="500" alt="Descargar v3.1.1, 64,5 MB"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/dc.png" width="320" alt="Únete a nuestro Discord"></a>

## 💰 Apoya las traducciones · Encarga la traducción de cualquier juego

¿Te gusta esta traducción? Apoya el proyecto con una donación en criptomonedas. También puedes **encargar la traducción de cualquier juego** por una donación **desde 20 $**.

📧 Encargos y contacto: [novokex@gmail.com](mailto:novokex@gmail.com)

| Moneda | Red | Dirección |
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

El juego no está incluido. Necesitas tu propia copia.

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="../assets/steam.png" width="260" alt="Comprar en Steam"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="../assets/itch.png" width="220" alt="Consíguelo en itch.io"></a>
</p>

## Instalación

1. Descarga `SarahsHouse-i18n-v3.1.1.zip` desde la [página de versiones](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Descomprime el archivo en la carpeta del juego, la que contiene `SarahsHouse.exe`. Combina, no anides: las carpetas del archivo quedan junto al exe.
3. Inicia el juego. El primer arranque tarda un par de minutos más mientras BepInEx se configura para tu copia, después el selector de idioma se abre solo.

Después, la carpeta queda así:

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

`winhttp.dll`, `doorstop_config.ini`, `dotnet/` y `BepInEx/` vienen del archivo; el mod es la última carpeta.

<details>
<summary><b>¿Ya tienes BepInEx 6 IL2CPP?</b></summary>

Entonces solo necesitas el mod: copia `BepInEx/plugins/SarahsHouseI18n/` del archivo a tu propia carpeta `BepInEx/plugins/` y abre el juego. No hace falta nada más del archivo, y el idioma que elijas se recuerda en cada instalación.

</details>

<details>
<summary><b>Cambiar el idioma más tarde</b></summary>

Pulsa <kbd>F10</kbd> en el menú principal. El selector aparece sobre la escena con el idioma actual en el centro de la rueda, y al confirmar el texto cambia al instante.

</details>

## El selector

<div align="center">
  <img src="../assets/picker.jpg" width="880" alt="La rueda de idiomas">
</div>

Ratón, flechas o mando. <kbd>Enter</kbd> confirma, <kbd>F10</kbd> vuelve a abrir el selector.

## Si algo salió mal

<details>
<summary><b>El primer arranque tarda una eternidad</b></summary>

Es normal. BepInEx genera los ensamblados para tu copia del juego la primera vez, uno o dos minutos, una sola vez. Los siguientes arranques van a velocidad normal.
</details>

<details>
<summary><b>Mi antivirus marca <code>winhttp.dll</code></b></summary>

Ese archivo es el cargador de BepInEx y es habitual en los mods de Unity. Restáuralo de la cuarentena y añade la carpeta del juego a las exclusiones.
</details>

<details>
<summary><b>Algunas líneas vuelven al inglés tras actualizar el juego</b></summary>

Las líneas nuevas y modificadas aún no están traducidas, así que caen al inglés y el juego sigue funcionando. La siguiente versión del mod cierra el hueco.
</details>

<details>
<summary><b>¿Cómo quito el mod?</b></summary>

Borra `BepInEx/plugins/SarahsHouseI18n`. Si quieres quitar también BepInEx, borra además `winhttp.dll`, `doorstop_config.ini` y `dotnet/`. El juego vuelve a como venía.
</details>

<p align="center"><sub>
Las fuentes se incluyen bajo SIL OFL y Apache 2.0. Funciona sobre BepInEx. Traducción de fans no oficial, sin relación con AceStudio.
</sub></p>
