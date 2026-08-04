<div align="center">

<img src="../assets/banner.png" alt="Sarah's House — i18n" width="920">

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

[![Release](https://img.shields.io/badge/RELEASE-v3.0.0-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.11.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

この MOD は **Sarah's House** を 12 言語に翻訳します。言語は初回起動時に選び、
あとからメインメニューの **F10** で変更できます。ゲームのファイルには手を加えません。

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/btn_download.ja.png" width="410" alt="最新リリースをダウンロード"></a>

<img src="../assets/picker.png" alt="言語選択メニュー" width="920">

</div>

<img src="../assets/h_install.ja.png" alt="インストール" height="48">

1. [リリースページ](https://github.com/elofaster/sarahs-house-i18n/releases/latest)から `SarahsHouse-i18n-v3.0.0.zip` をダウンロード。
2. アーカイブをゲームフォルダ（`SarahsHouse.exe` のある場所）に展開。
3. ゲームを起動。初回だけ 1〜2 分長くかかります: BepInEx があなたのコピーに合わせて一度だけセットアップします。その後、言語選択メニューが自動で開きます。

こうなっていれば OK:

```text
SarahsHouse/
├─ SarahsHouse.exe          ← ゲーム本体
├─ winhttp.dll              ┐
├─ doorstop_config.ini      │  アーカイブから
├─ dotnet/                  │
└─ BepInEx/                 ┘
```

<details>
<summary>BepInEx 6 IL2CPP を導入済みの場合</summary>
<br>

`SarahsHouseI18n/` を `BepInEx/plugins/` にコピーするだけです。詳細は [docs/INSTALL.md](../INSTALL.md)。

</details>

<br>

<img src="../assets/h_fix.ja.png" alt="うまくいかないときは" height="48">

<details>
<summary>初回起動がとても長い</summary>
<br>

正常です: BepInEx があなたのゲームコピー用のアセンブリを生成しています。一度きりで、以降の起動は通常どおりです。

</details>
<details>
<summary>ウイルス対策ソフトが <code>winhttp.dll</code> に反応する</summary>
<br>

Unity MOD で標準の BepInEx ローダーです。ファイルを隔離から復元し、ゲームフォルダを除外設定に追加してください。

</details>
<details>
<summary>ゲーム更新後、一部が英語に戻った</summary>
<br>

新規・変更された行はまだ未翻訳のため英語で表示されますが、ゲームは正常に動きます。新しい版が出たら MOD を更新してください。

</details>
<details>
<summary>MOD の削除方法</summary>
<br>

`BepInEx/plugins/SarahsHouseI18n` フォルダを削除します。BepInEx ごと外すなら `winhttp.dll` も削除。ゲームは元の状態に戻ります。

</details>

<br>

<img src="../assets/h_translators.ja.png" alt="翻訳者の方へ" height="48">

翻訳パックは [`packs/`](../../packs) にあるただの JSON です: キーが英語原文、値が訳文。インストール済みのコピーでは同じファイルが `BepInEx/plugins/SarahsHouseI18n/i18n/` にあり、編集はゲーム再起動後に反映されます。

新しい言語は MOD を再ビルドせずに追加できます — 手順は [docs/ADD-LANGUAGE.md](../ADD-LANGUAGE.md)。Pull Request 歓迎です。

<div align="center">

<img src="../assets/divider.png" width="560">

<a href="https://github.com/elofaster/sarahs-house-i18n"><img src="../assets/star.ja.png" alt="スターを残す" width="720"></a>

<a href="https://github.com/elofaster"><img src="../assets/sign.png" alt="elofaster" height="66"></a>

フォント — SIL OFL / Apache 2.0、一覧は [THIRD-PARTY-NOTICES.md](../../THIRD-PARTY-NOTICES.md) · [BepInEx](https://github.com/BepInEx/BepInEx) 上で動作

ゲーム作者とは無関係です。

</div>
