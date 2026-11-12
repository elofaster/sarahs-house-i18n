<p align="center">
  <img src="../assets/readme/sky.pt.svg" width="100%" alt="Sarah's House i18n: 12 idiomas. Tradução de fãs não oficial. v3.1.1 · para Sarah's House v0.12.6">
</p>

<p align="center">
  <a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/readme/download.pt.svg" height="56" alt="Baixar v3.1.1, 64,5 MB"></a>
  &nbsp;
  <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/readme/discord.svg" height="56" alt="Entre no nosso Discord"></a>
</p>

<p align="center">
<a href="../../README.md">English</a> &nbsp;✦&nbsp; <a href="README.ru.md">Русский</a> &nbsp;✦&nbsp; <a href="README.uk.md">Українська</a> &nbsp;✦&nbsp; <a href="README.de.md">Deutsch</a> &nbsp;✦&nbsp; <a href="README.es.md">Español</a> &nbsp;✦&nbsp; <a href="README.fr.md">Français</a> &nbsp;✦&nbsp; <a href="README.pl.md">Polski</a> &nbsp;✦&nbsp; <b>Português</b> &nbsp;✦&nbsp; <a href="README.tr.md">Türkçe</a> &nbsp;✦&nbsp; <a href="README.vi.md">Tiếng&nbsp;Việt</a> &nbsp;✦&nbsp; <a href="README.zh.md">中⁠文</a> &nbsp;✦&nbsp; <a href="README.ja.md">日⁠本⁠語</a> &nbsp;✦&nbsp; <a href="README.ko.md">한⁠국⁠어</a>
</p>

<br>

<img src="../assets/game.jpg" width="100%" alt="Sarah's House">

<p align="center"><sub>O jogo não está incluído. Você precisa da sua própria cópia. <a href="https://store.steampowered.com/app/4712060/Sarahs_House" title="Comprar na Steam">Steam</a> · <a href="https://ace-stud.itch.io/sarahs-house" title="Baixe no itch.io">itch.io</a></sub></p>

## ✦ Instalação

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

## ✦ O seletor

<img src="../assets/picker.jpg" width="100%" alt="A roda de idiomas">

Mouse, setas ou controle. <kbd>Enter</kbd> confirma, <kbd>F10</kbd> traz o seletor de volta depois.

## ✦ Se algo deu errado

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

---

<p align="center"><sub>
v3.1.1 · Sarah's House v0.12.6 · <a href="https://github.com/BepInEx/BepInEx">BepInEx</a> 6 IL2CPP<br>
As fontes vêm sob SIL OFL e Apache 2.0. Roda sobre BepInEx. Tradução de fãs não oficial, sem vínculo com a AceStudio.
</sub></p>
