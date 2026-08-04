<div align="center">

<img src="../assets/banner.png" alt="Sarah's House — i18n" width="920">

[<kbd> English </kbd>](../../README.md)
[<kbd> Русский </kbd>](README.ru.md)
[<kbd> Українська </kbd>](README.uk.md)
[<kbd> Deutsch </kbd>](README.de.md)
[<kbd> Español </kbd>](README.es.md)
[<kbd> Français </kbd>](README.fr.md)
<kbd> ● Polski </kbd>
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

Mod tłumaczy **Sarah's House** na 12 języków. Język wybiera się przy pierwszym uruchomieniu,
później zmienia się go klawiszem **F10** w menu głównym. Pliki gry pozostają nietknięte.

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/btn_download.pl.png" width="410" alt="Pobierz najnowsze wydanie"></a>

<img src="../assets/picker.png" alt="menu wyboru języka" width="920">

</div>

<img src="../assets/h_install.pl.png" alt="Instalacja" height="48">

1. Pobierz `SarahsHouse-i18n-v3.0.0.zip` ze [strony wydań](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Rozpakuj archiwum do folderu gry — tam, gdzie leży `SarahsHouse.exe`.
3. Uruchom grę. Pierwszy start trwa minutę-dwie dłużej: BepInEx jednorazowo konfiguruje się pod twoją kopię. Potem menu wyboru języka otworzy się samo.

Powinno wyglądać tak:

```text
SarahsHouse/
├─ SarahsHouse.exe          ← gra
├─ winhttp.dll              ┐
├─ doorstop_config.ini      │  z archiwum
├─ dotnet/                  │
└─ BepInEx/                 ┘
```

<details>
<summary>Masz już BepInEx 6 IL2CPP</summary>
<br>

Wystarczy skopiować `SarahsHouseI18n/` do `BepInEx/plugins/`. Szczegóły w [docs/INSTALL.md](../INSTALL.md).

</details>

<br>

<img src="../assets/h_fix.pl.png" alt="Gdy coś pójdzie nie tak" height="48">

<details>
<summary>Pierwsze uruchomienie trwa bardzo długo</summary>
<br>

Tak ma być: BepInEx generuje zestawy pod twoją kopię gry. To jednorazowa operacja — kolejne starty są normalne.

</details>
<details>
<summary>Antywirus czepia się <code>winhttp.dll</code></summary>
<br>

To loader BepInEx, standard w modach Unity. Przywróć plik z kwarantanny i dodaj folder gry do wyjątków.

</details>
<details>
<summary>Po aktualizacji gry część tekstu znów jest po angielsku</summary>
<br>

Nowe i zmienione linie nie są jeszcze przetłumaczone — wyświetlają się po angielsku, a gra działa normalnie. Zaktualizuj mod, gdy wyjdzie świeża wersja.

</details>
<details>
<summary>Jak usunąć mod</summary>
<br>

Usuń folder `BepInEx/plugins/SarahsHouseI18n`. Jeśli chcesz usunąć też BepInEx — skasuj dodatkowo `winhttp.dll`. Gra wróci do stanu pierwotnego.

</details>

<br>

<img src="../assets/h_translators.pl.png" alt="Dla tłumaczy" height="48">

Paczki tłumaczeń to zwykłe pliki JSON w [`packs/`](../../packs): klucz to angielska linia, wartość — tłumaczenie. W zainstalowanej kopii te same pliki leżą w `BepInEx/plugins/SarahsHouseI18n/i18n/` — zmiany widać po restarcie gry.

Nowy język można dodać bez przebudowy moda — instrukcja w [docs/ADD-LANGUAGE.md](../ADD-LANGUAGE.md). Pull requesty mile widziane.

<div align="center">

<img src="../assets/divider.png" width="560">

<a href="https://github.com/elofaster/sarahs-house-i18n"><img src="../assets/star.pl.png" alt="Zostaw gwiazdkę" width="720"></a>

<a href="https://github.com/elofaster"><img src="../assets/sign.png" alt="elofaster" height="66"></a>

fonty — SIL OFL / Apache 2.0, lista w [THIRD-PARTY-NOTICES.md](../../THIRD-PARTY-NOTICES.md) · działa na [BepInEx](https://github.com/BepInEx/BepInEx)

Nie mam związku z autorem gry.

</div>
