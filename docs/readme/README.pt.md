<div align="center">

<img src="../assets/banner.png" alt="Sarah's House — i18n" width="920">

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

[![Release](https://img.shields.io/badge/RELEASE-v3.0.0-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.11.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

O mod traduz **Sarah's House** para 12 idiomas. O idioma é escolhido no primeiro arranque
e pode ser trocado depois com **F10** no menu principal. Os arquivos do jogo não são tocados.

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/btn_download.pt.png" width="410" alt="Baixar a versão mais recente"></a>

<img src="../assets/picker.png" alt="seletor de idioma" width="920">

</div>

<img src="../assets/h_install.pt.png" alt="Instalação" height="48">

1. Baixe `SarahsHouse-i18n-v3.0.0.zip` na [página de releases](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Extraia o arquivo para a pasta do jogo — onde está `SarahsHouse.exe`.
3. Inicie o jogo. O primeiro arranque leva um ou dois minutos a mais: o BepInEx se configura uma única vez para a sua cópia. Depois o seletor de idioma abre sozinho.

Deve ficar assim:

```text
SarahsHouse/
├─ SarahsHouse.exe          ← o jogo
├─ winhttp.dll              ┐
├─ doorstop_config.ini      │  do arquivo
├─ dotnet/                  │
└─ BepInEx/                 ┘
```

<details>
<summary>Já usa BepInEx 6 IL2CPP</summary>
<br>

Basta copiar `SarahsHouseI18n/` para `BepInEx/plugins/`. Detalhes em [docs/INSTALL.md](../INSTALL.md).

</details>

<br>

<img src="../assets/h_fix.pt.png" alt="Se algo der errado" height="48">

<details>
<summary>O primeiro arranque demora muito</summary>
<br>

É o esperado: o BepInEx gera os assemblies para a sua cópia do jogo. Acontece uma única vez — os próximos arranques são normais.

</details>
<details>
<summary>O antivírus reclama do <code>winhttp.dll</code></summary>
<br>

É o carregador do BepInEx, padrão em mods de Unity. Restaure o arquivo da quarentena e adicione a pasta do jogo às exceções.

</details>
<details>
<summary>Depois de uma atualização do jogo, parte do texto voltou ao inglês</summary>
<br>

Linhas novas ou alteradas ainda não têm tradução — aparecem em inglês e o jogo continua funcionando. Atualize o mod quando sair uma versão nova.

</details>
<details>
<summary>Como remover o mod</summary>
<br>

Apague a pasta `BepInEx/plugins/SarahsHouseI18n`. Para remover também o BepInEx, apague ainda o `winhttp.dll`. O jogo volta ao estado original.

</details>

<br>

<img src="../assets/h_translators.pt.png" alt="Para tradutores" height="48">

Os pacotes de tradução são JSON simples em [`packs/`](../../packs): a chave é a linha em inglês, o valor é a tradução. Numa cópia instalada, os mesmos arquivos ficam em `BepInEx/plugins/SarahsHouseI18n/i18n/` — as edições aparecem após reiniciar o jogo.

Um idioma novo pode ser adicionado sem recompilar o mod — instruções em [docs/ADD-LANGUAGE.md](../ADD-LANGUAGE.md). Pull requests são bem-vindos.

<div align="center">

<img src="../assets/divider.png" width="560">

<a href="https://github.com/elofaster/sarahs-house-i18n"><img src="../assets/star.pt.png" alt="Deixe uma estrela" width="720"></a>

<a href="https://github.com/elofaster"><img src="../assets/sign.png" alt="elofaster" height="66"></a>

fontes — SIL OFL / Apache 2.0, lista em [THIRD-PARTY-NOTICES.md](../../THIRD-PARTY-NOTICES.md) · roda sobre [BepInEx](https://github.com/BepInEx/BepInEx)

Sem relação com o autor do jogo.

</div>
