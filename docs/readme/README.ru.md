<div align="center">

<img src="../assets/banner.png" alt="Sarah's House — i18n" width="920">

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

[![Release](https://img.shields.io/badge/RELEASE-v3.0.0-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.11.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

Мод переводит **Sarah's House** на 12 языков. Язык выбирается при первом запуске,
дальше меняется по **F10** в главном меню. Файлы игры мод не трогает.

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/btn_download.ru.png" width="410" alt="Скачать последний релиз"></a>

<img src="../assets/picker.png" alt="меню выбора языка" width="920">

</div>

<img src="../assets/h_install.ru.png" alt="Установка" height="48">

1. Скачайте `SarahsHouse-i18n-v3.0.0.zip` со [страницы релизов](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Распакуйте архив в папку игры — туда, где лежит `SarahsHouse.exe`.
3. Запустите игру. Первый запуск дольше обычного, минута-две: BepInEx один раз настраивается под вашу копию. Дальше меню выбора языка откроется само.

Должно получиться так:

```text
SarahsHouse/
├─ SarahsHouse.exe          ← игра
├─ winhttp.dll              ┐
├─ doorstop_config.ini      │  из архива
├─ dotnet/                  │
└─ BepInEx/                 ┘
```

<details>
<summary>Уже стоит BepInEx 6 IL2CPP</summary>
<br>

Достаточно скопировать `SarahsHouseI18n/` в `BepInEx/plugins/`. Подробности — в [docs/INSTALL.md](../INSTALL.md).

</details>

<br>

<img src="../assets/h_fix.ru.png" alt="Если что-то пошло не так" height="48">

<details>
<summary>Первый запуск очень долгий</summary>
<br>

Так и должно быть: BepInEx генерирует сборки под вашу копию игры. Это разовая операция, все следующие запуски — обычные.

</details>
<details>
<summary>Антивирус ругается на <code>winhttp.dll</code></summary>
<br>

Это загрузчик BepInEx, стандартный для Unity-модов. Верните файл из карантина и добавьте папку игры в исключения.

</details>
<details>
<summary>После обновления игры часть текста снова на английском</summary>
<br>

Новые и изменённые строки ещё не переведены — они показываются на английском, игра при этом работает нормально. Обновите мод, когда выйдет свежая версия.

</details>
<details>
<summary>Как удалить мод</summary>
<br>

Удалите папку `BepInEx/plugins/SarahsHouseI18n`. Если хотите убрать и сам BepInEx — удалите ещё `winhttp.dll`. Игра вернётся в исходное состояние.

</details>

<br>

<img src="../assets/h_translators.ru.png" alt="Переводчикам" height="48">

Пакеты переводов — обычные JSON в [`packs/`](../../packs): ключ — английская строка, значение — перевод. В установленном моде те же файлы лежат в `BepInEx/plugins/SarahsHouseI18n/i18n/` — правка видна после перезапуска игры.

Новый язык добавляется без пересборки мода — инструкция в [docs/ADD-LANGUAGE.md](../ADD-LANGUAGE.md). Пулреквесты приветствуются.

<div align="center">

<img src="../assets/divider.png" width="560">

<a href="https://github.com/elofaster/sarahs-house-i18n"><img src="../assets/star.ru.png" alt="Поставь звезду" width="720"></a>

<a href="https://github.com/elofaster"><img src="../assets/sign.png" alt="elofaster" height="66"></a>

шрифты — SIL OFL / Apache 2.0, список в [THIRD-PARTY-NOTICES.md](../../THIRD-PARTY-NOTICES.md) · работает на [BepInEx](https://github.com/BepInEx/BepInEx)

К автору игры отношения не имею.

</div>
