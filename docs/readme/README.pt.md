<div align="center">

<img src="../assets/banner.png" alt="Sarah's House – i18n" width="920">

[<kbd> English </kbd>](../../README.md)
[<kbd> Русский </kbd>](README.ru.md)
[<kbd> Українська </kbd>](README.uk.md)
[<kbd> Deutsch </kbd>](README.de.md)
[<kbd> Español </kbd>](README.es.md)
[<kbd> Français </kbd>](README.fr.md)
[<kbd> Polski </kbd>](README.pl.md)
<kbd> ● Português </kbd>
[<kbd> Türkçe </kbd>](README.tr.md)
[<kbd> Tiếng Việt </kbd>](README.vi.md)
[<kbd> 中文 </kbd>](README.zh.md)
[<kbd> 日本語 </kbd>](README.ja.md)
[<kbd> 한국어 </kbd>](README.ko.md)

<br>

[![Release](https://img.shields.io/badge/RELEASE-v3.1.1-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.12.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/dl.png" width="500" alt="Baixar v3.1.1, 64,5 MB"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/dc.png" width="320" alt="Entre no nosso Discord"></a>

</div>

<div align="center">
  <img src="../assets/game.jpg" width="880" alt="Sarah's House">
</div>

O jogo não está incluído. Você precisa da sua própria cópia.

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="../assets/steam.png" width="260" alt="Comprar na Steam"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="../assets/itch.png" width="220" alt="Baixe no itch.io"></a>
</p>

## Instalação

1. Baixe o `SarahsHouse-i18n-v3.1.1.zip` na [página de versões](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Extraia o arquivo na pasta do jogo, a que tem o `SarahsHouse.exe`. Mescle, sem aninhar: as pastas do arquivo ficam ao lado do exe.
3. Abra o jogo. A primeira inicialização demora um ou dois minutos a mais enquanto o BepInEx se configura para a sua cópia, depois o seletor de idioma abre sozinho.

Depois disso a pasta fica assim:

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

`winhttp.dll`, `doorstop_config.ini`, `dotnet/` e `BepInEx/` vêm todos do arquivo; o mod é a última pasta.

<details>
<summary><b>Já usa BepInEx 6 IL2CPP?</b></summary>

Então você só precisa do mod: copie `BepInEx/plugins/SarahsHouseI18n/` do arquivo para a sua pasta `BepInEx/plugins/` e abra o jogo. Nada mais do arquivo é necessário, e o idioma escolhido fica salvo por instalação.

</details>

<details>
<summary><b>Mudar o idioma depois</b></summary>

Aperte <kbd>F10</kbd> no menu principal. O seletor abre sobre a cena com o idioma atual no centro da roda, e ao confirmar o texto troca na hora.

</details>

## O seletor

<div align="center">
  <img src="../assets/picker.jpg" width="880" alt="A roda de idiomas">
</div>

Mouse, setas ou controle. <kbd>Enter</kbd> confirma, <kbd>F10</kbd> traz o seletor de volta depois.

## Se algo deu errado

<details>
<summary><b>A primeira inicialização demora uma eternidade</b></summary>

Isso é normal. O BepInEx gera os assemblies para a sua cópia do jogo na primeira vez, um ou dois minutos, uma única vez. As próximas inicializações são normais.
</details>

<details>
<summary><b>Meu antivírus acusa <code>winhttp.dll</code></b></summary>

Esse arquivo é o carregador do BepInEx e é padrão em mods de Unity. Restaure-o da quarentena e adicione a pasta do jogo às exclusões.
</details>

<details>
<summary><b>Algumas falas voltaram ao inglês depois de atualizar o jogo</b></summary>

As falas novas e alteradas ainda não estão traduzidas, então caem no inglês e o jogo continua funcionando. A próxima versão do mod fecha a lacuna.
</details>

<details>
<summary><b>Como removo o mod?</b></summary>

Apague `BepInEx/plugins/SarahsHouseI18n`. Para tirar o BepInEx também, apague `winhttp.dll`, `doorstop_config.ini` e `dotnet/`. O jogo volta a como veio.
</details>

<p align="center"><sub>
As fontes vêm sob SIL OFL e Apache 2.0. Roda sobre BepInEx. Tradução de fãs não oficial, sem vínculo com a AceStudio.
</sub></p>
