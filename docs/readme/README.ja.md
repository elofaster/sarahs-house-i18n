<div align="center">

<img src="../assets/banner.png" alt="Sarah's House – i18n" width="920">

[<kbd> English </kbd>](../../README.md)
[<kbd> Русский </kbd>](README.ru.md)
[<kbd> Українська </kbd>](README.uk.md)
[<kbd> Deutsch </kbd>](README.de.md)
[<kbd> Español </kbd>](README.es.md)
[<kbd> Français </kbd>](README.fr.md)
[<kbd> Polski </kbd>](README.pl.md)
[<kbd> Português </kbd>](README.pt.md)
[<kbd> Türkçe </kbd>](README.tr.md)
[<kbd> Tiếng Việt </kbd>](README.vi.md)
[<kbd> 中文 </kbd>](README.zh.md)
<kbd> ● 日本語 </kbd>
[<kbd> 한국어 </kbd>](README.ko.md)

<br>

[![Release](https://img.shields.io/badge/RELEASE-v3.1.1-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.12.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/dl.png" width="500" alt="v3.1.1 をダウンロード（64.5 MB）"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/dc.png" width="320" alt="Discord に参加する"></a>

</div>

<div align="center">
  <img src="../assets/game.jpg" width="880" alt="Sarah's House">
</div>

ゲーム本体は含まれていません。ご自分のコピーが必要です。

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="../assets/steam.png" width="260" alt="Steam で購入"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="../assets/itch.png" width="220" alt="itch.io で入手"></a>
</p>

## インストール

1. [リリースページ](https://github.com/elofaster/sarahs-house-i18n/releases/latest)から `SarahsHouse-i18n-v3.1.1.zip` をダウンロードします。
2. `SarahsHouse.exe` があるゲームフォルダに解凍します。フォルダを入れ子にせず、そのまま統合してください。圧縮ファイル内のフォルダが exe の隣に並びます。
3. ゲームを起動します。初回だけ 1〜2 分長くなります（BepInEx があなたのコピー用に一度だけ設定します）。そのあと言語選択画面が自動で開きます。

終わるとフォルダはこうなります:

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

`winhttp.dll`、`doorstop_config.ini`、`dotnet/`、`BepInEx/` はすべて圧縮ファイルのものです。Mod 本体は最後のフォルダです。

<details>
<summary><b>BepInEx 6 IL2CPP をすでに使っていますか？</b></summary>

その場合は Mod だけで十分です。圧縮ファイル内の `BepInEx/plugins/SarahsHouseI18n/` を自分の `BepInEx/plugins/` にコピーしてゲームを起動してください。ほかのファイルは不要で、選んだ言語はインストールごとに記憶されます。

</details>

<details>
<summary><b>あとから言語を変える</b></summary>

メインメニューで <kbd>F10</kbd> を押します。現在の言語が輪の中央にある状態で選択画面がシーンの上に開き、決定するとすぐにテキストが切り替わります。

</details>

## 言語選択

<div align="center">
  <img src="../assets/picker.jpg" width="880" alt="言語の輪">
</div>

マウス、方向キー、ゲームパッドに対応。<kbd>Enter</kbd> で決定、<kbd>F10</kbd> でいつでも呼び出せます。

## うまくいかないとき

<details>
<summary><b>初回起動がとても長い</b></summary>

これは正常です。初回だけ BepInEx がゲームのコピー用にアセンブリを生成するため、1〜2 分かかります。2 回目以降は通常の速さです。
</details>

<details>
<summary><b>ウイルス対策が <code>winhttp.dll</code> を検出する</b></summary>

このファイルは BepInEx のローダーで、Unity の Mod では一般的なものです。隔離から復元し、ゲームフォルダを除外に追加してください。
</details>

<details>
<summary><b>ゲーム更新後、一部の文章が英語に戻った</b></summary>

新しく変わった行はまだ未訳のため英語で表示されますが、ゲームは普通に動きます。次の Mod の更新で埋まります。
</details>

<details>
<summary><b>Mod を削除するには？</b></summary>

`BepInEx/plugins/SarahsHouseI18n` を削除します。BepInEx も消すなら `winhttp.dll`、`doorstop_config.ini`、`dotnet/` も削除してください。ゲームは元の状態に戻ります。
</details>

<p align="center"><sub>
フォントは SIL OFL と Apache 2.0 で同梱しています。BepInEx 上で動作します。非公式のファン翻訳で、AceStudio とは関係ありません。
</sub></p>
