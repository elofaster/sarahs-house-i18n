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
<kbd> ● Türkçe </kbd>
[<kbd> Tiếng Việt </kbd>](README.vi.md)
[<kbd> 中文 </kbd>](README.zh.md)
[<kbd> 日本語 </kbd>](README.ja.md)
[<kbd> 한국어 </kbd>](README.ko.md)

<br>

[![Release](https://img.shields.io/badge/RELEASE-v3.0.0-ff69b4?style=for-the-badge&labelColor=2b0f22)](https://github.com/elofaster/sarahs-house-i18n/releases/latest)
![Game](https://img.shields.io/badge/SARAH%27S%20HOUSE-v0.11.6-8a5cf5?style=for-the-badge&labelColor=2b0f22)
[![BepInEx](https://img.shields.io/badge/BEPINEX-6%20IL2CPP-4a4a55?style=for-the-badge&labelColor=2b0f22)](https://github.com/BepInEx/BepInEx)

Mod, **Sarah's House** oyununu 12 dile çevirir. Dil ilk açılışta seçilir,
sonra ana menüde **F10** ile değiştirilir. Oyun dosyalarına dokunulmaz.

<a href="https://github.com/elofaster/sarahs-house-i18n/releases/latest"><img src="../assets/btn_download.tr.png" width="410" alt="Son sürümü indir"></a>

<img src="../assets/picker.png" alt="dil seçme menüsü" width="920">

</div>

<img src="../assets/h_install.tr.png" alt="Kurulum" height="48">

1. [Sürümler sayfasından](https://github.com/elofaster/sarahs-house-i18n/releases/latest) `SarahsHouse-i18n-v3.0.0.zip` dosyasını indirin.
2. Arşivi oyun klasörüne çıkartın — `SarahsHouse.exe` neredeyse oraya.
3. Oyunu başlatın. İlk açılış bir-iki dakika daha uzun sürer: BepInEx kopyanıza göre kendini bir kez ayarlar. Sonra dil seçme menüsü kendiliğinden açılır.

Şöyle görünmeli:

```text
SarahsHouse/
├─ SarahsHouse.exe          ← oyun
├─ winhttp.dll              ┐
├─ doorstop_config.ini      │  arşivden
├─ dotnet/                  │
└─ BepInEx/                 ┘
```

<details>
<summary>BepInEx 6 IL2CPP zaten kurulu</summary>
<br>

`SarahsHouseI18n/` klasörünü `BepInEx/plugins/` içine kopyalamak yeterli. Ayrıntılar: [docs/INSTALL.md](../INSTALL.md).

</details>

<br>

<img src="../assets/h_about.tr.png" alt="Çeviri hakkında" height="48">

12 dilin tamamı **Claude Opus 4.8** (Anthropic) tarafından çevrildi — eksiksiz, dil başına yaklaşık 15 000 satır.

Çeviri satır satır yapılmadı: model sahnenin tamamını görür, bu yüzden replikler her karakterin sesini korur. Opus 4.8, Anthropic'in amiral gemisi modeli ve bugün çevirideki en güçlülerden biri.

<br>

<img src="../assets/h_fix.tr.png" alt="Bir şeyler ters giderse" height="48">

<details>
<summary>İlk açılış çok uzun sürüyor</summary>
<br>

Böyle olmalı: BepInEx, oyun kopyanıza özel derlemeler üretiyor. Bu tek seferlik bir işlem — sonraki açılışlar normal.

</details>
<details>
<summary>Antivirüs <code>winhttp.dll</code> dosyasına takıyor</summary>
<br>

Bu, Unity modlarında standart olan BepInEx yükleyicisidir. Dosyayı karantinadan geri alın ve oyun klasörünü istisnalara ekleyin.

</details>
<details>
<summary>Oyun güncellemesinden sonra bazı metinler yine İngilizce</summary>
<br>

Yeni ve değişen satırlar henüz çevrilmedi — İngilizce görünürler, oyun normal çalışmaya devam eder. Yeni sürüm çıkınca modu güncelleyin.

</details>
<details>
<summary>Mod nasıl kaldırılır</summary>
<br>

`BepInEx/plugins/SarahsHouseI18n` klasörünü silin. BepInEx'i de kaldırmak isterseniz `winhttp.dll` dosyasını da silin. Oyun ilk hâline döner.

</details>

<br>

<img src="../assets/h_translators.tr.png" alt="Çevirmenler için" height="48">

Çeviri paketleri [`packs/`](../../packs) içindeki düz JSON dosyalarıdır: anahtar İngilizce satır, değer çeviridir. Kurulu kopyada aynı dosyalar `BepInEx/plugins/SarahsHouseI18n/i18n/` içindedir — değişiklikler oyun yeniden başlatılınca görünür.

Yeni bir dil, modu yeniden derlemeden eklenebilir — yönerge: [docs/ADD-LANGUAGE.md](../ADD-LANGUAGE.md). Pull request'ler memnuniyetle karşılanır.

<img src="../assets/h_game.tr.png" alt="Oyun" height="48">

**Sarah's House**, **AceStudio** yapımıdır — mod yalnızca çeviriyi içerir, oyunun kendisi dahil değildir.

Çeviri işinize yaradıysa geliştiriciyi destekleyin: oyunu satın alın ve bir yorum bırakın.

[Steam](https://store.steampowered.com/app/4712060/Sarahs_House) · [itch.io](https://ace-stud.itch.io/sarahs-house)

<div align="center">

<img src="../assets/divider.png" width="560">

<a href="https://github.com/elofaster/sarahs-house-i18n"><img src="../assets/star.tr.png" alt="Yıldız bırak" width="720"></a>

<a href="https://github.com/elofaster"><img src="../assets/sign.png" alt="elofaster" height="66"></a>

yazı tipleri — SIL OFL / Apache 2.0, liste: [THIRD-PARTY-NOTICES.md](../../THIRD-PARTY-NOTICES.md) · [BepInEx](https://github.com/BepInEx/BepInEx) üzerinde çalışır

Oyunun yapımcısıyla bir bağım yok.

</div>
