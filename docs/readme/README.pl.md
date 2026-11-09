<div align="center">

<img src="../assets/banner.png" alt="Sarah's House – i18n" width="920">

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

[![Release](https://img.shields.io/badge/RELEASE-v3.1.1-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.12.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/dl.png" width="500" alt="Pobierz v3.1.1, 64,5 MB"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/dc.png" width="320" alt="Dołącz do naszego Discorda"></a>

</div>

<div align="center">
  <img src="../assets/game.jpg" width="880" alt="Sarah's House">
</div>

Gra nie jest dołączona. Potrzebujesz własnej kopii.

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="../assets/steam.png" width="260" alt="Kup na Steam"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="../assets/itch.png" width="220" alt="Pobierz z itch.io"></a>
</p>

## Instalacja

1. Pobierz `SarahsHouse-i18n-v3.1.1.zip` ze [strony wydań](https://github.com/elofaster/sarahs-house-i18n/releases/latest).
2. Rozpakuj archiwum do folderu gry, tego z plikiem `SarahsHouse.exe`. Scal, nie zagnieżdżaj: foldery z archiwum lądują obok exe.
3. Uruchom grę. Pierwsze uruchomienie trwa minutę lub dwie dłużej, bo BepInEx konfiguruje się raz dla Twojej kopii, potem wybór języka otwiera się sam.

Potem folder wygląda tak:

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

`winhttp.dll`, `doorstop_config.ini`, `dotnet/` i `BepInEx/` pochodzą z archiwum; sam mod to ostatni folder.

<details>
<summary><b>Masz już BepInEx 6 IL2CPP?</b></summary>

Wtedy potrzebujesz tylko moda: skopiuj `BepInEx/plugins/SarahsHouseI18n/` z archiwum do własnego `BepInEx/plugins/` i uruchom grę. Nic więcej z archiwum nie jest potrzebne, a wybrany język jest zapamiętywany dla danej instalacji.

</details>

<details>
<summary><b>Zmiana języka później</b></summary>

Naciśnij <kbd>F10</kbd> w menu głównym. Wybór otwiera się nad sceną, aktualny język jest na środku koła, a zatwierdzenie od razu podmienia tekst.

</details>

## Wybór języka

<div align="center">
  <img src="../assets/picker.jpg" width="880" alt="Koło języków">
</div>

Mysz, strzałki lub pad. <kbd>Enter</kbd> zatwierdza, <kbd>F10</kbd> przywraca wybór później.

## Jeśli coś poszło nie tak

<details>
<summary><b>Pierwsze uruchomienie trwa wiecznie</b></summary>

To normalne. BepInEx generuje zestawy dla Twojej kopii gry przy pierwszym uruchomieniu, minutę lub dwie, jednorazowo. Każde kolejne uruchomienie jest już normalne.
</details>

<details>
<summary><b>Antywirus zgłasza <code>winhttp.dll</code></b></summary>

Ten plik to loader BepInEx, standardowy w modach do Unity. Przywróć go z kwarantanny i dodaj folder gry do wyjątków.
</details>

<details>
<summary><b>Po aktualizacji gry część linii wróciła do angielskiego</b></summary>

Nowe i zmienione linie nie są jeszcze przetłumaczone, więc pokazują się po angielsku, a gra działa normalnie. Następne wydanie moda uzupełni braki.
</details>

<details>
<summary><b>Jak usunąć moda?</b></summary>

Usuń `BepInEx/plugins/SarahsHouseI18n`. Jeśli chcesz usunąć też BepInEx, usuń dodatkowo `winhttp.dll`, `doorstop_config.ini` i `dotnet/`. Gra wróci do stanu z wydania.
</details>

<p align="center"><sub>
Czcionki dołączone na licencjach SIL OFL i Apache 2.0. Działa na BepInEx. Nieoficjalne tłumaczenie fanowskie, niezwiązane z AceStudio.
</sub></p>
