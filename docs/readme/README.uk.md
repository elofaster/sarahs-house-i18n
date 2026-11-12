<p align="center">
  <img src="../assets/readme/sky.uk.svg" width="100%" alt="Sarah's House i18n: 12 мов. Неофіційний фанатський переклад. v3.1.1 · для Sarah's House v0.12.6">
</p>

<p align="center">
  <a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/readme/download.uk.svg" height="56" alt="Завантажити v3.1.1, 64,5 МБ"></a>
  &nbsp;
  <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/readme/discord.svg" height="56" alt="Наш Discord"></a>
</p>

<p align="center">
<a href="../../README.md">English</a> &nbsp;✦&nbsp; <a href="README.ru.md">Русский</a> &nbsp;✦&nbsp; <b>Українська</b> &nbsp;✦&nbsp; <a href="README.de.md">Deutsch</a> &nbsp;✦&nbsp; <a href="README.es.md">Español</a> &nbsp;✦&nbsp; <a href="README.fr.md">Français</a> &nbsp;✦&nbsp; <a href="README.pl.md">Polski</a> &nbsp;✦&nbsp; <a href="README.pt.md">Português</a> &nbsp;✦&nbsp; <a href="README.tr.md">Türkçe</a> &nbsp;✦&nbsp; <a href="README.vi.md">Tiếng&nbsp;Việt</a> &nbsp;✦&nbsp; <a href="README.zh.md">中⁠文</a> &nbsp;✦&nbsp; <a href="README.ja.md">日⁠本⁠語</a> &nbsp;✦&nbsp; <a href="README.ko.md">한⁠국⁠어</a>
</p>

<br>

<img src="../assets/game.jpg" width="100%" alt="Sarah's House">

<p align="center"><sub>Гри в архіві немає. Потрібна власна копія. <a href="https://store.steampowered.com/app/4712060/Sarahs_House" title="Купити в Steam">Steam</a> · <a href="https://ace-stud.itch.io/sarahs-house" title="Забрати на itch.io">itch.io</a></sub></p>

## ✦ Встановлення

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

## ✦ Вибір мови

<img src="../assets/picker.jpg" width="100%" alt="Колесо мов">

Миша, стрілки або геймпад. <kbd>Enter</kbd> підтверджує, <kbd>F10</kbd> відкриває пікер знову.

## ✦ Якщо щось пішло не так

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

---

<p align="center"><sub>
v3.1.1 · Sarah's House v0.12.6 · <a href="https://github.com/BepInEx/BepInEx">BepInEx</a> 6 IL2CPP<br>
Шрифти постачаються за ліцензіями SIL OFL і Apache 2.0. Працює на BepInEx. Неофіційний фанатський переклад, до AceStudio відношення не має.
</sub></p>
