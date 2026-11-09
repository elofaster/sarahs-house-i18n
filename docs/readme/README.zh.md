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
<kbd> ● 中文 </kbd>
[<kbd> 日本語 </kbd>](README.ja.md)
[<kbd> 한국어 </kbd>](README.ko.md)

<br>

[![Release](https://img.shields.io/badge/RELEASE-v3.1.1-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.12.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/dl.png" width="500" alt="下载 v3.1.1，64.5 MB"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/dc.png" width="320" alt="加入我们的 Discord"></a>

</div>

<div align="center">
  <img src="../assets/game.jpg" width="880" alt="Sarah's House">
</div>

模组不含游戏本体，你需要自备一份。

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="../assets/steam.png" width="260" alt="在 Steam 购买"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="../assets/itch.png" width="220" alt="在 itch.io 获取"></a>
</p>

## 安装

1. 从[发行版页面](https://github.com/elofaster/sarahs-house-i18n/releases/latest)下载 `SarahsHouse-i18n-v3.1.1.zip`。
2. 把压缩包解压到游戏文件夹，也就是有 `SarahsHouse.exe` 的那个。直接合并，不要多套一层：压缩包里的文件夹要落在 exe 旁边。
3. 启动游戏。第一次启动会多花一两分钟，BepInEx 要为你的游戏副本做一次配置，之后语言选择界面会自动打开。

完成后文件夹是这样的：

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

`winhttp.dll`、`doorstop_config.ini`、`dotnet/` 和 `BepInEx/` 都来自压缩包；模组本身是最后一个文件夹。

<details>
<summary><b>已经装了 BepInEx 6 IL2CPP？</b></summary>

那你只需要模组：把压缩包里的 `BepInEx/plugins/SarahsHouseI18n/` 复制到你自己的 `BepInEx/plugins/`，然后启动游戏。压缩包里的其他文件都不需要，所选语言按安装记住。

</details>

<details>
<summary><b>以后再换语言</b></summary>

在主菜单按 <kbd>F10</kbd>。选择界面会浮在场景上方，当前语言位于转轮中央，确认后文字立刻切换。

</details>

## 语言选择

<div align="center">
  <img src="../assets/picker.jpg" width="880" alt="语言转轮">
</div>

鼠标、方向键或手柄。<kbd>Enter</kbd> 确认，<kbd>F10</kbd> 之后可以再次打开。

## 如果出了问题

<details>
<summary><b>第一次启动特别久</b></summary>

这是正常的。第一次运行时 BepInEx 要为你的游戏副本生成程序集，一两分钟，只此一次。之后每次启动都是正常速度。
</details>

<details>
<summary><b>杀毒软件拦截 <code>winhttp.dll</code></b></summary>

这个文件是 BepInEx 加载器，所有 Unity 模组都用它。从隔离区恢复它，并把游戏文件夹加入信任列表。
</details>

<details>
<summary><b>游戏更新后部分文字又变回英文</b></summary>

新增和改动过的文本还没有翻译，会回落到英文，游戏照常运行。下一个模组版本会补上。
</details>

<details>
<summary><b>怎么卸载模组？</b></summary>

删除 `BepInEx/plugins/SarahsHouseI18n`。如果连 BepInEx 一起删掉，还要删除 `winhttp.dll`、`doorstop_config.ini` 和 `dotnet/`。游戏就回到最初的样子。
</details>

<p align="center"><sub>
随包字体使用 SIL OFL 和 Apache 2.0 授权。基于 BepInEx 运行。非官方爱好者翻译，与 AceStudio 无关。
</sub></p>
