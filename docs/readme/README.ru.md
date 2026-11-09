<div align="center">

<img src="../assets/banner.png" alt="Sarah's House – i18n" width="920">

[<kbd> English </kbd>](../../README.md)
<kbd> ● Русский </kbd>
[<kbd> Українська </kbd>](README.uk.md)
[<kbd> Deutsch </kbd>](README.de.md)
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

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/dl.png" width="500" alt="Скачать v3.1.1, 64,5 МБ"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/dc.png" width="320" alt="Наш Discord"></a>

</div>

<div align="center">
  <img src="../assets/game.jpg" width="880" alt="Sarah's House">
</div>

Игры в архиве нет. Нужна своя копия.

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="../assets/steam.png" width="260" alt="Купить в Steam"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="../assets/itch.png" width="220" alt="Забрать на itch.io"></a>
</p>

## Установка

1. Скачайте `SarahsHouse-i18n-v3.1.1.zip` со [страницы релизов](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Распакуйте архив в папку игры, туда, где лежит `SarahsHouse.exe`. Именно слить, а не вложить: папки из архива встают рядом с exe.
3. Запустите игру. Первый запуск идёт на минуту-две дольше: BepInEx один раз настраивается под вашу копию. Потом меню выбора языка откроется само.

В итоге папка выглядит так:

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

`winhttp.dll`, `doorstop_config.ini`, `dotnet/` и `BepInEx/` берутся из архива, сам мод это последняя папка.

<details>
<summary><b>Уже стоит BepInEx 6 IL2CPP?</b></summary>

Тогда нужен только мод: скопируйте `BepInEx/plugins/SarahsHouseI18n/` из архива в свою папку `BepInEx/plugins/` и запускайте игру. Остальное из архива не нужно, а выбранный язык запоминается для каждой установки.

</details>

<details>
<summary><b>Сменить язык позже</b></summary>

Нажмите <kbd>F10</kbd> в главном меню. Пикер открывается поверх сцены, текущий язык стоит в середине колеса, и после подтверждения текст меняется сразу.

</details>

## Выбор языка

<div align="center">
  <img src="../assets/picker.jpg" width="880" alt="Колесо языков">
</div>

Мышь, стрелки или геймпад. <kbd>Enter</kbd> подтверждает, <kbd>F10</kbd> открывает пикер снова.

## Если что-то пошло не так

<details>
<summary><b>Первый запуск тянется очень долго</b></summary>

Так и должно быть. BepInEx при первом запуске собирает сборки под вашу копию игры, минута-две, один раз. Все следующие запуски идут с обычной скоростью.
</details>

<details>
<summary><b>Антивирус ругается на <code>winhttp.dll</code></b></summary>

Это загрузчик BepInEx, он стандартный для модов на Unity. Верните файл из карантина и добавьте папку игры в исключения.
</details>

<details>
<summary><b>После обновления игры часть строк снова на английском</b></summary>

Новые и изменённые строки ещё не переведены, поэтому показываются на английском, а игра работает как обычно. Следующий релиз мода это закроет.
</details>

<details>
<summary><b>Как убрать мод?</b></summary>

Удалите `BepInEx/plugins/SarahsHouseI18n`. Если убрать и BepInEx, удалите ещё `winhttp.dll`, `doorstop_config.ini` и `dotnet/`. Игра вернётся к исходному виду.
</details>

<p align="center"><sub>
Шрифты поставляются по лицензиям SIL OFL и Apache 2.0. Работает на BepInEx. Неофициальный фанатский перевод, к AceStudio отношения не имеет.
</sub></p>
