<div align="center">

<img src="../assets/banner.png" alt="Sarah's House – i18n" width="920">

[<kbd> English </kbd>](../../README.md)
[<kbd> Русский </kbd>](README.ru.md)
<kbd> ● Українська </kbd>
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

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/dl.png" width="500" alt="Завантажити v3.1.1, 64,5 МБ"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/dc.png" width="320" alt="Наш Discord"></a>

</div>

<div align="center">
  <img src="../assets/game.jpg" width="880" alt="Sarah's House">
</div>

Гри в архіві немає. Потрібна власна копія.

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="../assets/steam.png" width="260" alt="Купити в Steam"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="../assets/itch.png" width="220" alt="Забрати на itch.io"></a>
</p>

## Встановлення

1. Завантажте `SarahsHouse-i18n-v3.1.1.zip` зі [сторінки релізів](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Розпакуйте архів у теку гри, туди, де лежить `SarahsHouse.exe`. Саме злити, а не вкласти: теки з архіву стають поруч з exe.
3. Запустіть гру. Перший запуск триває на хвилину-дві довше: BepInEx один раз налаштовується під вашу копію. Потім меню вибору мови відкриється саме.

У підсумку тека виглядає так:

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

`winhttp.dll`, `doorstop_config.ini`, `dotnet/` і `BepInEx/` беруться з архіву, сам мод це остання тека.

<details>
<summary><b>Уже стоїть BepInEx 6 IL2CPP?</b></summary>

Тоді потрібен лише мод: скопіюйте `BepInEx/plugins/SarahsHouseI18n/` з архіву до своєї теки `BepInEx/plugins/` і запускайте гру. Решта з архіву не потрібна, а обрана мова запам'ятовується для кожного встановлення.

</details>

<details>
<summary><b>Змінити мову пізніше</b></summary>

Натисніть <kbd>F10</kbd> у головному меню. Пікер відкривається поверх сцени, поточна мова стоїть у центрі колеса, і після підтвердження текст змінюється одразу.

</details>

## Вибір мови

<div align="center">
  <img src="../assets/picker.jpg" width="880" alt="Колесо мов">
</div>

Миша, стрілки або геймпад. <kbd>Enter</kbd> підтверджує, <kbd>F10</kbd> відкриває пікер знову.

## Якщо щось пішло не так

<details>
<summary><b>Перший запуск тягнеться дуже довго</b></summary>

Так і має бути. BepInEx під час першого запуску збирає збірки під вашу копію гри, хвилина-дві, один раз. Усі наступні запуски мають звичайну швидкість.
</details>

<details>
<summary><b>Антивірус скаржиться на <code>winhttp.dll</code></b></summary>

Це завантажувач BepInEx, він стандартний для модів на Unity. Поверніть файл з карантину й додайте теку гри до винятків.
</details>

<details>
<summary><b>Після оновлення гри частина рядків знову англійською</b></summary>

Нові та змінені рядки ще не перекладені, тому показуються англійською, а гра працює як завжди. Наступний реліз мода це закриє.
</details>

<details>
<summary><b>Як прибрати мод?</b></summary>

Видаліть `BepInEx/plugins/SarahsHouseI18n`. Якщо прибрати й BepInEx, видаліть ще `winhttp.dll`, `doorstop_config.ini` і `dotnet/`. Гра повернеться до початкового вигляду.
</details>

<p align="center"><sub>
Шрифти постачаються за ліцензіями SIL OFL і Apache 2.0. Працює на BepInEx. Неофіційний фанатський переклад, до AceStudio відношення не має.
</sub></p>
