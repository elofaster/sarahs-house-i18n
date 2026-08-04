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
<kbd> ● 中文 </kbd>
[<kbd> 日本語 </kbd>](README.ja.md)
[<kbd> 한국어 </kbd>](README.ko.md)

<br>

[![Release](https://img.shields.io/badge/RELEASE-v3.0.0-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.11.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

本模组将 **Sarah's House** 翻译成 12 种语言。首次启动时选择语言，
之后在主菜单按 **F10** 更换。不改动任何游戏文件。

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/btn_download.zh.png" width="410" alt="下载最新版本"></a>

<img src="../assets/picker.png" alt="语言选择菜单" width="920">

</div>

<img src="../assets/h_install.zh.png" alt="安装" height="48">

1. 从[发布页](https://github.com/elofaster/sarahs-house-i18n/releases/latest)下载 `SarahsHouse-i18n-v3.0.0.zip`。
2. 把压缩包解压到游戏目录——`SarahsHouse.exe` 所在的位置。
3. 启动游戏。首次启动会多花一两分钟：BepInEx 为你的拷贝做一次性配置，随后语言选择菜单会自动打开。

解压后应该是这样：

```text
SarahsHouse/
├─ SarahsHouse.exe          ← 游戏本体
├─ winhttp.dll              ┐
├─ doorstop_config.ini      │  来自压缩包
├─ dotnet/                  │
└─ BepInEx/                 ┘
```

<details>
<summary>已装有 BepInEx 6 IL2CPP</summary>
<br>

把 `SarahsHouseI18n/` 复制到 `BepInEx/plugins/` 即可。详见 [docs/INSTALL.md](../INSTALL.md)。

</details>

<br>

<img src="../assets/h_fix.zh.png" alt="遇到问题时" height="48">

<details>
<summary>首次启动特别久</summary>
<br>

这是正常的：BepInEx 在为你的游戏拷贝生成程序集。只发生一次，之后每次启动都正常。

</details>
<details>
<summary>杀毒软件报告 <code>winhttp.dll</code></summary>
<br>

这是 BepInEx 的加载器，Unity 模组的标准组件。把文件从隔离区还原，并把游戏目录加入白名单。

</details>
<details>
<summary>游戏更新后部分文字又变回英文</summary>
<br>

新增和改动的文本尚未翻译——它们以英文显示，游戏照常运行。等新版本发布后更新模组即可。

</details>
<details>
<summary>如何卸载模组</summary>
<br>

删除 `BepInEx/plugins/SarahsHouseI18n` 文件夹。若想连 BepInEx 一起移除，再删除 `winhttp.dll`。游戏即恢复原样。

</details>

<br>

<img src="../assets/h_translators.zh.png" alt="致翻译者" height="48">

翻译包是 [`packs/`](../../packs) 里的纯 JSON 文件：键为英文原文，值为译文。已安装的拷贝中，同样的文件位于 `BepInEx/plugins/SarahsHouseI18n/i18n/`——改完重启游戏即可生效。

无需重新构建模组即可添加新语言——见 [docs/ADD-LANGUAGE.md](../ADD-LANGUAGE.md)。欢迎提交 Pull Request。

<div align="center">

<img src="../assets/divider.png" width="560">

<a href="https://github.com/elofaster/sarahs-house-i18n"><img src="../assets/star.zh.png" alt="点个星标" width="720"></a>

<a href="https://github.com/elofaster"><img src="../assets/sign.png" alt="elofaster" height="66"></a>

字体 — SIL OFL / Apache 2.0，清单见 [THIRD-PARTY-NOTICES.md](../../THIRD-PARTY-NOTICES.md) · 基于 [BepInEx](https://github.com/BepInEx/BepInEx) 运行

与游戏作者无关。

</div>
