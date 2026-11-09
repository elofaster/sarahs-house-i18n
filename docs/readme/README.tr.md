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
<kbd> ● Türkçe </kbd>
[<kbd> Tiếng Việt </kbd>](README.vi.md)
[<kbd> 中文 </kbd>](README.zh.md)
[<kbd> 日本語 </kbd>](README.ja.md)
[<kbd> 한국어 </kbd>](README.ko.md)

<br>

[![Release](https://img.shields.io/badge/RELEASE-v3.1.1-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.12.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/dl.png" width="500" alt="v3.1.1 indir, 64,5 MB"></a> <a href="https://discord.gg/FXsWMq9Rv7"><img src="../assets/dc.png" width="320" alt="Discord'umuza katıl"></a>

## 💰 Çevirilere destek ol · Herhangi bir oyunun çevirisini sipariş et

Bu çeviriyi beğendiysen projeyi kripto bağışıyla destekleyebilirsin. Ayrıca **20 $ ve üzeri** bir bağışla **herhangi bir oyunun çevirisini** sipariş edebilirsin.

📧 Sipariş ve iletişim: [novokex@gmail.com](mailto:novokex@gmail.com)

| Coin | Ağ | Adres |
| :---: | :---: | :--- |
| **USDT** | TON | `UQDweoL-JmiDeyVCxIybr9NsH-4Ojnw9zplipMRLZGafX3jd` |
| **USDT** | TRON (TRC-20) | `TKWAByL6koBMLt6XZoasqVc2zxEtiCGxWL` |
| **USDT** | Ethereum (ERC-20) | `0x4718619DA946Aa243bedF9D22b90930f034567b3` |
| **USDT** | Solana | `HnzTfY39D2a1jbfS2mD1NV3SnaypW54KiGa54xbCGdJx` |
| **USDC** | Ethereum (ERC-20) | `0x4718619DA946Aa243bedF9D22b90930f034567b3` |
| **USDC** | Solana | `HnzTfY39D2a1jbfS2mD1NV3SnaypW54KiGa54xbCGdJx` |

</div>

<div align="center">
  <img src="../assets/game.jpg" width="880" alt="Sarah's House">
</div>

Oyun dahil değil. Kendi kopyana ihtiyacın var.

<p align="center">
  <a href="https://store.steampowered.com/app/4712060/Sarahs_House"><img src="../assets/steam.png" width="260" alt="Steam'den al"></a>
  <a href="https://ace-stud.itch.io/sarahs-house"><img src="../assets/itch.png" width="220" alt="itch.io'dan al"></a>
</p>

## Kurulum

1. `SarahsHouse-i18n-v3.1.1.zip` dosyasını [sürümler sayfasından](https://github.com/elofaster/sarahs-house-i18n/releases/latest) indir.
2. Arşivi `SarahsHouse.exe` dosyasının bulunduğu oyun klasörüne çıkar. Birleştir, iç içe koyma: arşivdeki klasörler exe'nin yanına gelir.
3. Oyunu başlat. İlk açılış bir iki dakika daha uzun sürer, BepInEx senin kopyan için bir kez kurulur, sonra dil seçici kendiliğinden açılır.

Sonrasında klasör şöyle görünür:

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

`winhttp.dll`, `doorstop_config.ini`, `dotnet/` ve `BepInEx/` arşivden gelir; modun kendisi son klasördür.

<details>
<summary><b>BepInEx 6 IL2CPP zaten kurulu mu?</b></summary>

O zaman sadece moda ihtiyacın var: arşivdeki `BepInEx/plugins/SarahsHouseI18n/` klasörünü kendi `BepInEx/plugins/` klasörüne kopyala ve oyunu başlat. Arşivden başka bir şey gerekmez, seçtiğin dil her kurulumda hatırlanır.

</details>

<details>
<summary><b>Dili sonradan değiştirme</b></summary>

Ana menüde <kbd>F10</kbd> tuşuna bas. Seçici sahnenin üzerinde açılır, mevcut dil çemberin ortasındadır ve onaylayınca metin hemen değişir.

</details>

## Dil seçici

<div align="center">
  <img src="../assets/picker.jpg" width="880" alt="Dil çemberi">
</div>

Fare, yön tuşları veya gamepad. <kbd>Enter</kbd> onaylar, <kbd>F10</kbd> seçiciyi sonra geri getirir.

## Bir şeyler ters gittiyse

<details>
<summary><b>İlk açılış bitmek bilmiyor</b></summary>

Bu normal. BepInEx ilk çalıştırmada oyununun kopyası için assembly üretir, bir iki dakika, tek seferlik. Sonraki açılışlar normal hızda.
</details>

<details>
<summary><b>Antivirüsüm <code>winhttp.dll</code> dosyasını işaretliyor</b></summary>

Bu dosya BepInEx yükleyicisidir ve Unity modlarında standarttır. Karantinadan geri yükle ve oyun klasörünü istisnalara ekle.
</details>

<details>
<summary><b>Oyun güncellendikten sonra bazı satırlar İngilizceye döndü</b></summary>

Yeni ve değişen satırlar henüz çevrilmedi, İngilizce görünür ve oyun normal çalışır. Sonraki mod sürümü bu açığı kapatır.
</details>

<details>
<summary><b>Modu nasıl kaldırırım?</b></summary>

`BepInEx/plugins/SarahsHouseI18n` klasörünü sil. BepInEx'i de kaldırmak istersen `winhttp.dll`, `doorstop_config.ini` ve `dotnet/` dosyalarını da sil. Oyun çıktığı hâline döner.
</details>

<p align="center"><sub>
Yazı tipleri SIL OFL ve Apache 2.0 kapsamında gelir. BepInEx üzerinde çalışır. Resmî olmayan hayran çevirisi, AceStudio ile bağlantılı değildir.
</sub></p>
