(() => {
  document.documentElement.classList.remove("no-js");

  const I18N = {
    ru: {
      "meta.title": "Sarah’s House — Translation Mod",
      "meta.description": "Неофициальный multilingual-мод для Sarah’s House. RU готов (100%). DE/UK/ES/FR в планах.",
      "meta.og": "Неофициальный мод перевода для Sarah’s House 0.11.2",
      "nav.languages": "Языки",
      "nav.install": "Установка",
      "nav.gallery": "Галерея",
      "nav.contribute": "Правки",
      "nav.download": "Скачать",
      "hero.title": "Перевод",
      "hero.lede": "Вся игра на родном языке. Скачай архив, распакуй в папку игры и играй.",
      "hero.download": "Скачать мод",
      "hero.downloadSub": "ZIP · soon",
      "packs.eyebrow": "Языки",
      "packs.title": "Статус пакетов",
      "packs.ingame": "В игре:",
      "packs.cycle": "цикл:",
      "packs.latestTitle": "Последний завершённый перевод",
      "packs.latestSub": "самый свежий готовый языковой пакет",
      "packs.othersTitle": "Все остальные языки",
      "packs.othersSub": "компактные пакеты в общем списке",
      "packs.author": "Автор",
      "packs.offer": "Хотите предложить авторский перевод?",
      "packs.offer.step1": "Сделайте fork репозитория elofaster/sarahs-house-i18n.",
      "packs.offer.step2": "В mod/i18n/ создайте файл языка, например de.json, es.json, fr.json или uk.json.",
      "packs.offer.step3": "В начале файла укажите ник автора ключом \"__meta.author\".",
      "packs.offer.step4": "Добавьте строки перевода в формате \"english text\": \"ваш текст\".",
      "packs.offer.step5": "Откройте Pull Request в main — после ревью пакет появится на сайте.",
      "packs.offer.cta": "Создать файл перевода на GitHub",
      "packs.lines": "Строки",
      "packs.completeness": "Завершённость",
      "status.ready": "готов",
      "status.wip": "в работе",
      "lang.ru": "Русский",
      "lang.en": "English",
      "lang.de": "Deutsch",
      "lang.fr": "Français",
      "lang.uk": "Українська",
      "lang.es": "Español",
      "install.eyebrow": "Установка",
      "install.title": "Как поставить",
      "install.need": "Нужна Sarah's House v0.11.2. Игра в архив не входит.",
      "install.downloadZip": "Скачать ZIP",
      "install.step1Title": "Скачать",
      "install.step2Title": "В папку игры",
      "install.step2Text": "рядом с SarahsHouse.exe",
      "install.step3Title": "Запуск",
      "install.step3Text": "первый старт 1–3 мин",
      "install.remove": "Удаление:",
      "gallery.eyebrow": "Галерея",
      "gallery.title": "Из игры",
      "contribute.eyebrow": "Правки",
      "contribute.title": "Хотите предложить правки?",
      "contribute.cta": "Предложить правки",
      "finale.title": "Скачать мод v2.9.0",
      "finale.sub": "ZIP · soon · free for everyone",
      "footer.note": "Неофициальный мод. Не связан с AceStudio.",
      "footer.game": "Игра",
    },
    en: {
      "meta.title": "Sarah’s House — Translation Mod",
      "meta.description": "Unofficial multilingual mod for Sarah’s House. RU complete (100%). DE/UK/ES/FR planned.",
      "meta.og": "Unofficial translation mod for Sarah’s House 0.11.2",
      "nav.languages": "Languages",
      "nav.install": "Install",
      "nav.gallery": "Gallery",
      "nav.contribute": "Contribute",
      "nav.download": "Download",
      "hero.title": "Translation",
      "hero.lede": "The whole game in your language. Download the archive, unpack it into the game folder and play.",
      "hero.download": "Download mod",
      "hero.downloadSub": "ZIP · soon",
      "packs.eyebrow": "Languages",
      "packs.title": "Pack status",
      "packs.ingame": "In game:",
      "packs.cycle": "cycle:",
      "packs.latestTitle": "Latest completed translation",
      "packs.latestSub": "the newest finished language pack",
      "packs.othersTitle": "All other languages",
      "packs.othersSub": "compact packs in one list",
      "packs.author": "Author",
      "packs.offer": "Want to contribute an author translation?",
      "packs.offer.step1": "Fork the elofaster/sarahs-house-i18n repository.",
      "packs.offer.step2": "Create a language file in mod/i18n/, e.g. de.json, es.json, fr.json, or uk.json.",
      "packs.offer.step3": "At the top of the file set your nickname with the \"__meta.author\" key.",
      "packs.offer.step4": "Add translation lines as \"english text\": \"your text\".",
      "packs.offer.step5": "Open a Pull Request to main — after review the pack appears on the site.",
      "packs.offer.cta": "Create a translation file on GitHub",
      "packs.lines": "Lines",
      "packs.completeness": "Completeness",
      "status.ready": "ready",
      "status.wip": "in progress",
      "lang.ru": "Russian",
      "lang.en": "English",
      "lang.de": "German",
      "lang.fr": "French",
      "lang.uk": "Ukrainian",
      "lang.es": "Spanish",
      "install.eyebrow": "Install",
      "install.title": "How to install",
      "install.need": "Requires Sarah's House v0.11.2. The game is not included in the archive.",
      "install.downloadZip": "Download ZIP",
      "install.step1Title": "Download",
      "install.step2Title": "Into the game folder",
      "install.step2Text": "next to SarahsHouse.exe",
      "install.step3Title": "Launch",
      "install.step3Text": "first start takes 1–3 min",
      "install.remove": "Uninstall:",
      "gallery.eyebrow": "Gallery",
      "gallery.title": "From the game",
      "contribute.eyebrow": "Contribute",
      "contribute.title": "Want to suggest fixes?",
      "contribute.cta": "Suggest fixes",
      "finale.title": "Download mod v2.9.0",
      "finale.sub": "ZIP · soon · free for everyone",
      "footer.note": "Unofficial mod. Not affiliated with AceStudio.",
      "footer.game": "Game",
    },
    de: {
      "meta.title": "Sarah’s House — Übersetzungsmod",
      "meta.description": "Inoffizieller Multilingual-Mod für Sarah’s House. RU fertig (100%). DE/UK/ES/FR geplant.",
      "meta.og": "Inoffizieller Übersetzungsmod für Sarah’s House 0.11.2",
      "nav.languages": "Sprachen",
      "nav.install": "Installation",
      "nav.gallery": "Galerie",
      "nav.contribute": "Mitwirken",
      "nav.download": "Download",
      "hero.title": "Übersetzung",
      "hero.lede": "Das ganze Spiel in deiner Sprache. Archiv laden, in den Spielordner entpacken und losspielen.",
      "hero.download": "Mod herunterladen",
      "hero.downloadSub": "ZIP · soon",
      "packs.eyebrow": "Sprachen",
      "packs.title": "Paketstatus",
      "packs.ingame": "Im Spiel:",
      "packs.cycle": "Zyklus:",
      "packs.latestTitle": "Zuletzt abgeschlossene Übersetzung",
      "packs.latestSub": "das neueste fertige Sprachpaket",
      "packs.othersTitle": "Alle anderen Sprachen",
      "packs.othersSub": "kompakte Pakete in einer Liste",
      "packs.author": "Autor",
      "packs.offer": "Eigenen Übersetzungspack vorschlagen?",
      "packs.offer.step1": "Forke das Repository elofaster/sarahs-house-i18n.",
      "packs.offer.step2": "Erstelle in mod/i18n/ eine Sprachdatei, z. B. de.json, es.json, fr.json oder uk.json.",
      "packs.offer.step3": "Setze oben in der Datei deinen Nick mit dem Schlüssel \"__meta.author\".",
      "packs.offer.step4": "Füge Übersetzungszeilen im Format \"english text\": \"dein text\" hinzu.",
      "packs.offer.step5": "Öffne einen Pull Request nach main — nach Review erscheint das Paket auf der Seite.",
      "packs.offer.cta": "Übersetzungsdatei auf GitHub erstellen",
      "packs.lines": "Zeilen",
      "packs.completeness": "Fortschritt",
      "status.ready": "fertig",
      "status.wip": "in Arbeit",
      "lang.ru": "Russisch",
      "lang.en": "Englisch",
      "lang.de": "Deutsch",
      "lang.fr": "Französisch",
      "lang.uk": "Ukrainisch",
      "lang.es": "Spanisch",
      "install.eyebrow": "Installation",
      "install.title": "So installierst du es",
      "install.need": "Benötigt Sarah's House v0.11.2. Das Spiel ist nicht im Archiv enthalten.",
      "install.downloadZip": "ZIP herunterladen",
      "install.step1Title": "Herunterladen",
      "install.step2Title": "In den Spielordner",
      "install.step2Text": "neben SarahsHouse.exe",
      "install.step3Title": "Starten",
      "install.step3Text": "erster Start 1–3 Min.",
      "install.remove": "Deinstallieren:",
      "gallery.eyebrow": "Galerie",
      "gallery.title": "Aus dem Spiel",
      "contribute.eyebrow": "Mitwirken",
      "contribute.title": "Korrekturen vorschlagen?",
      "contribute.cta": "Korrektur vorschlagen",
      "finale.title": "Mod v2.9.0 herunterladen",
      "finale.sub": "ZIP · soon · free for everyone",
      "footer.note": "Inoffizieller Mod. Nicht mit AceStudio verbunden.",
      "footer.game": "Spiel",
    },
    uk: {
      "meta.title": "Sarah’s House — Translation Mod",
      "meta.description": "Неофіційний multilingual-мод для Sarah’s House. RU готово (100%). DE/UK/ES/FR у планах.",
      "meta.og": "Неофіційний мод перекладу для Sarah’s House 0.11.2",
      "nav.languages": "Мови",
      "nav.install": "Встановлення",
      "nav.gallery": "Галерея",
      "nav.contribute": "Правки",
      "nav.download": "Завантажити",
      "hero.title": "Переклад",
      "hero.lede": "Уся гра рідною мовою. Завантаж архів, розпакуй у теку гри та грай.",
      "hero.download": "Завантажити мод",
      "hero.downloadSub": "ZIP · soon",
      "packs.eyebrow": "Мови",
      "packs.title": "Статус пакетів",
      "packs.ingame": "У грі:",
      "packs.cycle": "цикл:",
      "packs.latestTitle": "Останній завершений переклад",
      "packs.latestSub": "найсвіжіший готовий мовний пакет",
      "packs.othersTitle": "Усі інші мови",
      "packs.othersSub": "компактні пакети в загальному списку",
      "packs.author": "Автор",
      "packs.offer": "Хочете запропонувати авторський переклад?",
      "packs.offer.step1": "Зробіть fork репозиторію elofaster/sarahs-house-i18n.",
      "packs.offer.step2": "У mod/i18n/ створіть файл мови, наприклад de.json, es.json, fr.json або uk.json.",
      "packs.offer.step3": "На початку файлу вкажіть нік автора ключем \"__meta.author\".",
      "packs.offer.step4": "Додайте рядки перекладу у форматі \"english text\": \"ваш текст\".",
      "packs.offer.step5": "Відкрийте Pull Request у main — після рев’ю пакет з’явиться на сайті.",
      "packs.offer.cta": "Створити файл перекладу на GitHub",
      "packs.lines": "Рядки",
      "packs.completeness": "Завершеність",
      "status.ready": "готово",
      "status.wip": "в роботі",
      "lang.ru": "Російська",
      "lang.en": "English",
      "lang.de": "Deutsch",
      "lang.fr": "Français",
      "lang.uk": "Українська",
      "lang.es": "Español",
      "install.eyebrow": "Встановлення",
      "install.title": "Як встановити",
      "install.need": "Потрібна Sarah's House v0.11.2. Гра не входить до архіву.",
      "install.downloadZip": "Завантажити ZIP",
      "install.step1Title": "Завантажити",
      "install.step2Title": "У теку гри",
      "install.step2Text": "поруч із SarahsHouse.exe",
      "install.step3Title": "Запуск",
      "install.step3Text": "перший старт 1–3 хв",
      "install.remove": "Видалення:",
      "gallery.eyebrow": "Галерея",
      "gallery.title": "З гри",
      "contribute.eyebrow": "Правки",
      "contribute.title": "Хочете запропонувати правки?",
      "contribute.cta": "Запропонувати правки",
      "finale.title": "Завантажити мод v2.9.0",
      "finale.sub": "ZIP · soon · free for everyone",
      "footer.note": "Неофіційний мод. Не пов’язаний з AceStudio.",
      "footer.game": "Гра",
    },
    es: {
      "meta.title": "Sarah’s House — Translation Mod",
      "meta.description": "Mod multilingüe no oficial para Sarah’s House. RU completado (100%). DE/UK/ES/FR planificados.",
      "meta.og": "Mod de traducción no oficial para Sarah’s House 0.11.2",
      "nav.languages": "Idiomas",
      "nav.install": "Instalación",
      "nav.gallery": "Galería",
      "nav.contribute": "Aportes",
      "nav.download": "Descargar",
      "hero.title": "Traducción",
      "hero.lede": "Todo el juego en tu idioma. Descarga el archivo, descomprímelo en la carpeta del juego y juega.",
      "hero.download": "Descargar mod",
      "hero.downloadSub": "ZIP · soon",
      "packs.eyebrow": "Idiomas",
      "packs.title": "Estado de paquetes",
      "packs.ingame": "En el juego:",
      "packs.cycle": "ciclo:",
      "packs.latestTitle": "Última traducción completada",
      "packs.latestSub": "el paquete de idioma terminado más reciente",
      "packs.othersTitle": "Todos los demás idiomas",
      "packs.othersSub": "paquetes compactos en una lista",
      "packs.author": "Autor",
      "packs.offer": "¿Quieres proponer una traducción de autor?",
      "packs.offer.step1": "Haz un fork del repositorio elofaster/sarahs-house-i18n.",
      "packs.offer.step2": "Crea un archivo de idioma en mod/i18n/, p. ej. de.json, es.json, fr.json o uk.json.",
      "packs.offer.step3": "Al inicio del archivo indica tu nick con la clave \"__meta.author\".",
      "packs.offer.step4": "Añade líneas de traducción como \"english text\": \"tu texto\".",
      "packs.offer.step5": "Abre un Pull Request a main: tras la revisión el paquete aparecerá en el sitio.",
      "packs.offer.cta": "Crear archivo de traducción en GitHub",
      "packs.lines": "Líneas",
      "packs.completeness": "Completado",
      "status.ready": "listo",
      "status.wip": "en progreso",
      "lang.ru": "Ruso",
      "lang.en": "Inglés",
      "lang.de": "Alemán",
      "lang.fr": "Francés",
      "lang.uk": "Ucraniano",
      "lang.es": "Español",
      "install.eyebrow": "Instalación",
      "install.title": "Cómo instalar",
      "install.need": "Necesitas Sarah's House v0.11.2. El juego no está en el archivo.",
      "install.downloadZip": "Descargar ZIP",
      "install.step1Title": "Descargar",
      "install.step2Title": "A la carpeta del juego",
      "install.step2Text": "junto a SarahsHouse.exe",
      "install.step3Title": "Iniciar",
      "install.step3Text": "el primer inicio tarda 1–3 min",
      "install.remove": "Desinstalación:",
      "gallery.eyebrow": "Galería",
      "gallery.title": "Del juego",
      "contribute.eyebrow": "Aportes",
      "contribute.title": "¿Quieres proponer correcciones?",
      "contribute.cta": "Proponer correcciones",
      "finale.title": "Descargar mod v2.9.0",
      "finale.sub": "ZIP · soon · free for everyone",
      "footer.note": "Mod no oficial. No afiliado a AceStudio.",
      "footer.game": "Juego",
    },
    fr: {
      "meta.title": "Sarah’s House — Translation Mod",
      "meta.description": "Mod multilingue non officiel pour Sarah’s House. RU terminé (100%). DE/UK/ES/FR prévus.",
      "meta.og": "Mod de traduction non officiel pour Sarah’s House 0.11.2",
      "nav.languages": "Langues",
      "nav.install": "Installation",
      "nav.gallery": "Galerie",
      "nav.contribute": "Contribuer",
      "nav.download": "Télécharger",
      "hero.title": "Traduction",
      "hero.lede": "Tout le jeu dans ta langue. Télécharge l’archive, décompresse-la dans le dossier du jeu et joue.",
      "hero.download": "Télécharger le mod",
      "hero.downloadSub": "ZIP · soon",
      "packs.eyebrow": "Langues",
      "packs.title": "État des packs",
      "packs.ingame": "En jeu :",
      "packs.cycle": "cycle :",
      "packs.latestTitle": "Dernière traduction terminée",
      "packs.latestSub": "le pack de langue terminé le plus récent",
      "packs.othersTitle": "Toutes les autres langues",
      "packs.othersSub": "packs compacts dans une seule liste",
      "packs.author": "Auteur",
      "packs.offer": "Proposer une traduction d’auteur ?",
      "packs.offer.step1": "Faites un fork du dépôt elofaster/sarahs-house-i18n.",
      "packs.offer.step2": "Créez un fichier de langue dans mod/i18n/, par ex. de.json, es.json, fr.json ou uk.json.",
      "packs.offer.step3": "En haut du fichier indiquez votre pseudo avec la clé \"__meta.author\".",
      "packs.offer.step4": "Ajoutez les lignes au format \"english text\": \"votre texte\".",
      "packs.offer.step5": "Ouvrez une Pull Request vers main — après relecture le pack apparaît sur le site.",
      "packs.offer.cta": "Créer un fichier de traduction sur GitHub",
      "packs.lines": "Lignes",
      "packs.completeness": "Avancement",
      "status.ready": "prêt",
      "status.wip": "en cours",
      "lang.ru": "Russe",
      "lang.en": "Anglais",
      "lang.de": "Allemand",
      "lang.fr": "Français",
      "lang.uk": "Ukrainien",
      "lang.es": "Espagnol",
      "install.eyebrow": "Installation",
      "install.title": "Comment installer",
      "install.need": "Sarah's House v0.11.2 est requis. Le jeu n'est pas inclus dans l'archive.",
      "install.downloadZip": "Télécharger le ZIP",
      "install.step1Title": "Télécharger",
      "install.step2Title": "Dans le dossier du jeu",
      "install.step2Text": "à côté de SarahsHouse.exe",
      "install.step3Title": "Lancer",
      "install.step3Text": "premier démarrage 1–3 min",
      "install.remove": "Désinstallation :",
      "gallery.eyebrow": "Galerie",
      "gallery.title": "En jeu",
      "contribute.eyebrow": "Contribuer",
      "contribute.title": "Proposer des corrections ?",
      "contribute.cta": "Proposer des corrections",
      "finale.title": "Télécharger le mod v2.9.0",
      "finale.sub": "ZIP · soon · free for everyone",
      "footer.note": "Mod non officiel. Non affilié à AceStudio.",
      "footer.game": "Jeu",
    },
    zh: {
      "meta.title": "Sarah’s House — 翻译模组",
      "meta.description": "Sarah’s House 非官方多语言模组。8 种语言全部完成（100%）：俄语、乌克兰语、德语、西班牙语、法语、中文、土耳其语、葡萄牙语。",
      "meta.og": "Sarah’s House 0.11.2 非官方翻译模组",
      "nav.languages": "语言",
      "nav.install": "安装",
      "nav.gallery": "画廊",
      "nav.contribute": "贡献",
      "nav.download": "下载",
      "hero.title": "现在会说中文了",
      "hero.lede": "整个游戏都是你的语言。下载压缩包，解压到游戏目录，开始游玩。",
      "hero.download": "下载模组",
      "hero.downloadSub": "ZIP · 65 MB",
      "packs.eyebrow": "语言",
      "packs.title": "语言包状态",
      "packs.ingame": "游戏内：",
      "packs.cycle": "周期：",
      "packs.latestTitle": "最新完成的翻译",
      "packs.latestSub": "最新完成的语言包",
      "packs.othersTitle": "其他所有语言",
      "packs.othersSub": "列表中的全部语言包",
      "packs.author": "作者",
      "packs.offer": "想提交自己的翻译？",
      "packs.offer.step1": "Fork 仓库 elofaster/sarahs-house-i18n。",
      "packs.offer.step2": "在 mod/i18n/ 中创建语言文件，例如 it.json、pl.json、ja.json 或 ko.json。",
      "packs.offer.step3": "在文件开头用 \"__meta.author\" 键写上你的昵称。",
      "packs.offer.step4": "按 \"english text\": \"你的文本\" 的格式添加翻译行。",
      "packs.offer.step5": "向 main 分支提交 Pull Request——审核通过后语言包就会出现在网站上。",
      "packs.offer.cta": "在 GitHub 上创建翻译文件",
      "packs.lines": "行数",
      "packs.completeness": "完成度",
      "status.ready": "已完成",
      "status.wip": "进行中",
      "lang.ru": "俄语",
      "lang.en": "英语",
      "lang.de": "德语",
      "lang.fr": "法语",
      "lang.uk": "乌克兰语",
      "lang.es": "西班牙语",
      "lang.zh": "中文",
      "lang.tr": "土耳其语",
      "lang.pt": "葡萄牙语",
      "lang.ja": "日语",
      "packs.totalLines": "翻译总行数：",
      "install.eyebrow": "安装",
      "install.title": "如何安装",
      "install.need": "需要 Sarah's House v0.11.2。压缩包内不含游戏本体。",
      "install.downloadZip": "下载 ZIP",
      "install.step1Title": "下载",
      "install.step2Title": "放入游戏目录",
      "install.step2Text": "与 SarahsHouse.exe 同目录",
      "install.step3Title": "启动",
      "install.step3Text": "首次启动约 1–3 分钟",
      "install.remove": "卸载：",
      "gallery.eyebrow": "画廊",
      "gallery.title": "游戏截图",
      "contribute.eyebrow": "贡献",
      "contribute.title": "想提出修改建议？",
      "contribute.cta": "提交修改",
      "finale.title": "下载模组 v2.9.0",
      "finale.sub": "ZIP · 65 MB · 完全免费",
      "footer.note": "非官方模组，与 AceStudio 无关。",
      "footer.game": "游戏",
      "a11y.skip": "跳到正文",
      "hero.free": "对所有人免费",
      "footer.legal": "凡署名作者的翻译，版权归其作者所有。由神经网络完成的翻译归本项目所有。除作者包外，所有翻译均以开源形式发布——作者包经其作者同意后发布。",
      "footer.contact": "如果这里收录了你的翻译——请联系我们：",
      "footer.project": "项目",
      "footer.translators": "致翻译者",
      "footer.top": "回到顶部",
      "contribute.addLang": "添加你的语言",
      "contribute.sub": "发现错别字或别扭的句子？通过 pull request 修改只需几分钟——直接在浏览器里完成。",
      "finale.started": "下载已开始",
      "packs.ingamePath": "Preferences → Language",
      "dialog.hint": "点击——切换语言",
      "dialog.line": "现在你能看懂每一个字。",
      "gallery.open": "查看截图",
      "gallery.cap1": "清晨的浴室",
      "gallery.cap2": "周日午后",
      "gallery.cap3": "房间里的谈话",
      "gallery.cap4": "门厅",
      "lightbox.close": "关闭",
      "lightbox.prev": "上一张",
      "lightbox.next": "下一张",
    },
    tr: {
      "meta.title": "Sarah’s House — Çeviri Modu",
      "meta.description": "Sarah’s House için gayri resmi çoklu dil modu. 8 dil tamamlandı (%100): RU, UK, DE, ES, FR, ZH, TR, PT.",
      "meta.og": "Sarah’s House 0.11.2 için gayri resmi çeviri modu",
      "nav.languages": "Diller",
      "nav.install": "Kurulum",
      "nav.gallery": "Galeri",
      "nav.contribute": "Katkı",
      "nav.download": "İndir",
      "hero.title": "artık Türkçe konuşuyor",
      "hero.lede": "Oyunun tamamı kendi dilinde. Arşivi indir, oyun klasörüne çıkart ve oyna.",
      "hero.download": "Modu indir",
      "hero.downloadSub": "ZIP · 65 MB",
      "packs.eyebrow": "Diller",
      "packs.title": "Paket durumu",
      "packs.ingame": "Oyunda:",
      "packs.cycle": "döngü:",
      "packs.latestTitle": "Son tamamlanan çeviri",
      "packs.latestSub": "en yeni hazır dil paketi",
      "packs.othersTitle": "Diğer tüm diller",
      "packs.othersSub": "tek listede derli toplu paketler",
      "packs.author": "Yazar",
      "packs.offer": "Kendi çevirini önermek ister misin?",
      "packs.offer.step1": "elofaster/sarahs-house-i18n deposunu fork'la.",
      "packs.offer.step2": "mod/i18n/ içinde bir dil dosyası oluştur, örneğin it.json, pl.json, ja.json veya ko.json.",
      "packs.offer.step3": "Dosyanın başında \"__meta.author\" anahtarıyla takma adını belirt.",
      "packs.offer.step4": "Çeviri satırlarını \"english text\": \"senin metnin\" formatında ekle.",
      "packs.offer.step5": "main dalına Pull Request aç — incelemeden sonra paket sitede görünür.",
      "packs.offer.cta": "GitHub'da çeviri dosyası oluştur",
      "packs.lines": "Satır",
      "packs.completeness": "Tamamlanma",
      "status.ready": "hazır",
      "status.wip": "devam ediyor",
      "lang.ru": "Rusça",
      "lang.en": "İngilizce",
      "lang.de": "Almanca",
      "lang.fr": "Fransızca",
      "lang.uk": "Ukraynaca",
      "lang.es": "İspanyolca",
      "lang.zh": "Çince",
      "lang.tr": "Türkçe",
      "lang.pt": "Portekizce",
      "lang.ja": "Japonca",
      "packs.totalLines": "Toplam çeviri satırı:",
      "install.eyebrow": "Kurulum",
      "install.title": "Nasıl kurulur",
      "install.need": "Sarah's House v0.11.2 gerekli. Oyun arşive dahil değildir.",
      "install.downloadZip": "ZIP indir",
      "install.step1Title": "İndir",
      "install.step2Title": "Oyun klasörüne",
      "install.step2Text": "SarahsHouse.exe'nin yanına",
      "install.step3Title": "Başlat",
      "install.step3Text": "ilk açılış 1–3 dk sürer",
      "install.remove": "Kaldırma:",
      "gallery.eyebrow": "Galeri",
      "gallery.title": "Oyundan",
      "contribute.eyebrow": "Katkı",
      "contribute.title": "Düzeltme önermek ister misin?",
      "contribute.cta": "Düzeltme öner",
      "finale.title": "Modu indir v2.9.0",
      "finale.sub": "ZIP · 65 MB · herkese ücretsiz",
      "footer.note": "Gayri resmi mod. AceStudio ile bağlantısı yoktur.",
      "footer.game": "Oyun",
      "a11y.skip": "İçeriğe atla",
      "hero.free": "herkese ücretsiz",
      "footer.legal": "Yazarı belirtilen tüm çeviriler yazarlarına aittir. Yapay sinir ağlarıyla yapılan çeviriler projeye aittir. Yazar paketleri hariç tüm çeviriler açık kaynak olarak dağıtılır — yazar paketleri, yazarlarının onayıyla yayımlanır.",
      "footer.contact": "Çevirin burada yer alıyorsa — bize yaz:",
      "footer.project": "Proje",
      "footer.translators": "Çevirmenlere",
      "footer.top": "Yukarı",
      "contribute.addLang": "Kendi dilini ekle",
      "contribute.sub": "Yazım hatası mı, tuhaf bir cümle mi? Pull request ile düzeltmek birkaç dakika sürer — doğrudan tarayıcıda.",
      "finale.started": "İndirme başladı",
      "packs.ingamePath": "Preferences → Language",
      "dialog.hint": "Tıkla — sonraki dil",
      "dialog.line": "Artık her kelimeyi anlıyorsun.",
      "gallery.open": "Kareyi aç",
      "gallery.cap1": "Banyoda sabah",
      "gallery.cap2": "Pazar günü",
      "gallery.cap3": "Odada bir sohbet",
      "gallery.cap4": "Antre",
      "lightbox.close": "Kapat",
      "lightbox.prev": "Geri",
      "lightbox.next": "İleri",
    },
    ja: {
      "meta.title": "Sarah’s House — 翻訳MOD",
      "meta.description": "Sarah’s House 非公式マルチ言語MOD。8言語が完成（100%）：露・宇・独・西・仏・中・土・葡。",
      "meta.og": "Sarah’s House 0.11.2 非公式翻訳MOD",
      "nav.languages": "言語",
      "nav.install": "インストール",
      "nav.gallery": "ギャラリー",
      "nav.contribute": "貢献",
      "nav.download": "ダウンロード",
      "hero.title": "もう言葉の壁はない",
      "hero.lede": "ゲーム全編があなたの言語に。アーカイブをダウンロードして、ゲームフォルダに解凍するだけ。",
      "hero.download": "MODをダウンロード",
      "hero.downloadSub": "ZIP · 65 MB",
      "packs.eyebrow": "言語",
      "packs.title": "パッケージの状況",
      "packs.ingame": "ゲーム内：",
      "packs.cycle": "サイクル：",
      "packs.latestTitle": "最新の完成翻訳",
      "packs.latestSub": "いちばん新しい言語パック",
      "packs.othersTitle": "その他の言語",
      "packs.othersSub": "コンパクトなパック一覧",
      "packs.author": "作者",
      "packs.offer": "自作の翻訳を提案しませんか？",
      "packs.offer.step1": "elofaster/sarahs-house-i18n リポジトリをフォークします。",
      "packs.offer.step2": "mod/i18n/ に言語ファイルを作成します。例：it.json、pl.json、ko.json など。",
      "packs.offer.step3": "ファイル冒頭に \"__meta.author\" キーでニックネームを記載します。",
      "packs.offer.step4": "\"english text\": \"あなたの訳文\" の形式で翻訳行を追加します。",
      "packs.offer.step5": "main への Pull Request を送信——レビュー後にパックがサイトに掲載されます。",
      "packs.offer.cta": "GitHubで翻訳ファイルを作成",
      "packs.lines": "行数",
      "packs.completeness": "進捗",
      "status.ready": "完成",
      "status.wip": "作業中",
      "lang.ru": "ロシア語",
      "lang.en": "英語",
      "lang.de": "ドイツ語",
      "lang.fr": "フランス語",
      "lang.uk": "ウクライナ語",
      "lang.es": "スペイン語",
      "lang.zh": "中国語",
      "lang.tr": "トルコ語",
      "lang.pt": "ポルトガル語",
      "lang.ja": "日本語",
      "packs.totalLines": "翻訳行数合計：",
      "install.eyebrow": "インストール",
      "install.title": "インストール方法",
      "install.need": "Sarah's House v0.11.2 が必要です。ゲーム本体は含まれません。",
      "install.downloadZip": "ZIPをダウンロード",
      "install.step1Title": "ダウンロード",
      "install.step2Title": "ゲームフォルダへ",
      "install.step2Text": "SarahsHouse.exe と同じ場所に",
      "install.step3Title": "起動",
      "install.step3Text": "初回起動は1〜3分かかります",
      "install.remove": "アンインストール：",
      "gallery.eyebrow": "ギャラリー",
      "gallery.title": "ゲーム画面",
      "contribute.eyebrow": "貢献",
      "contribute.title": "修正を提案しませんか？",
      "contribute.cta": "修正を提案",
      "finale.title": "MOD v2.9.0 をダウンロード",
      "finale.sub": "ZIP · 65 MB · 誰でも無料",
      "footer.note": "非公式MODです。AceStudioとは無関係です。",
      "footer.game": "ゲーム",
      "a11y.skip": "本文へスキップ",
      "hero.free": "誰でも無料",
      "footer.legal": "作者名が明記された翻訳の著作権は各作者に帰属します。ニューラルネットワークによる翻訳は本プロジェクトに帰属します。作者パック以外のすべての翻訳はオープンソースで配布されます——作者パックは作者の承諾を得て掲載しています。",
      "footer.contact": "あなたの翻訳が掲載されている場合はご連絡ください：",
      "footer.project": "プロジェクト",
      "footer.translators": "翻訳者の皆さんへ",
      "footer.top": "トップへ",
      "contribute.addLang": "自分の言語を追加",
      "contribute.sub": "誤字や不自然な表現を見つけたら？pull request での修正は数分で完了——ブラウザから直接どうぞ。",
      "finale.started": "ダウンロード開始",
      "packs.ingamePath": "Preferences → Language",
      "dialog.hint": "クリックで次の言語へ",
      "dialog.line": "もう、すべての言葉がわかる。",
      "gallery.open": "画像を開く",
      "gallery.cap1": "バスルームの朝",
      "gallery.cap2": "日曜日の午後",
      "gallery.cap3": "部屋での会話",
      "gallery.cap4": "玄関ホール",
      "lightbox.close": "閉じる",
      "lightbox.prev": "前へ",
      "lightbox.next": "次へ",
    },
  };

  // v3: refreshed & new strings — original dictionaries above stay intact
  const I18N_PATCH = {
    ru: {
      "hero.title": "заговорила по-русски",
      "hero.lede": "Вся игра на родном языке. Скачай архив, распакуй в папку игры и играй.",
      "hero.downloadSub": "ZIP · 65 МБ",
      "finale.sub": "ZIP · 65 МБ · бесплатно",
      "a11y.skip": "К содержанию",
      "hero.free": "бесплатно для всех",
      "footer.legal": "Все переводы принадлежат своим авторам, если автор указан. Переводы, выполненные с помощью нейросетей, принадлежат проекту. Все переводы распространяются как open source, кроме авторских — они размещаются с одобрения своих авторов.",
      "footer.contact": "Если здесь размещён ваш перевод — напишите нам:",
      "footer.project": "Проект",
      "footer.translators": "Переводчикам",
      "footer.top": "Наверх",
      "contribute.addLang": "Добавить свой язык",
      "contribute.sub": "Опечатка или кривая формулировка? Правка через pull request занимает пару минут — прямо в браузере.",
      "finale.started": "Загрузка началась",
      "packs.ingamePath": "Preferences → Language",
      "dialog.hint": "Клик — следующий язык",
      "dialog.line": "Теперь ты понимаешь каждое слово.",
      "gallery.open": "Открыть кадр",
      "gallery.cap1": "Весь класс в сборе",
      "gallery.cap2": "Школьный двор",
      "gallery.cap3": "Город на закате",
      "gallery.cap4": "Утренняя электричка",
      "lightbox.close": "Закрыть",
      "lightbox.prev": "Назад",
      "lightbox.next": "Вперёд"
    },
    en: {
      "hero.title": "now speaks your language",
      "hero.lede": "The whole game in your language. Download the archive, unpack it into the game folder and play.",
      "hero.downloadSub": "ZIP · 65 MB",
      "finale.sub": "ZIP · 65 MB · free for everyone",
      "a11y.skip": "Skip to content",
      "hero.free": "free for everyone",
      "footer.legal": "All translations belong to their authors when an author is credited. Translations made with neural networks belong to the project. All translations are open source, except author packs — those are published with their authors’ approval.",
      "footer.contact": "If your translation is listed here — contact us:",
      "footer.project": "Project",
      "footer.translators": "For translators",
      "footer.top": "Back to top",
      "contribute.addLang": "Add your language",
      "contribute.sub": "Spotted a typo or an awkward line? A pull-request edit takes a couple of minutes — right in your browser.",
      "finale.started": "Download started",
      "packs.ingamePath": "Preferences → Language",
      "dialog.hint": "Click — next language",
      "dialog.line": "Now you understand every single word.",
      "gallery.open": "Open shot",
      "gallery.cap1": "The whole class",
      "gallery.cap2": "School courtyard",
      "gallery.cap3": "The city at dusk",
      "gallery.cap4": "The morning train",
      "lightbox.close": "Close",
      "lightbox.prev": "Previous",
      "lightbox.next": "Next"
    },
    de: {
      "hero.title": "spricht jetzt deine Sprache",
      "hero.lede": "Das ganze Spiel in deiner Sprache. Archiv laden, in den Spielordner entpacken und losspielen.",
      "hero.downloadSub": "ZIP · 65 MB",
      "finale.sub": "ZIP · 65 MB · kostenlos für alle",
      "a11y.skip": "Zum Inhalt springen",
      "hero.free": "kostenlos für alle",
      "footer.legal": "Alle Übersetzungen gehören ihren Autoren, sofern ein Autor angegeben ist. Mit neuronalen Netzen erstellte Übersetzungen gehören dem Projekt. Alle Übersetzungen sind Open Source, außer Autoren-Packs — diese erscheinen mit Zustimmung ihrer Autoren.",
      "footer.contact": "Wenn deine Übersetzung hier gelistet ist — schreib uns:",
      "footer.project": "Projekt",
      "footer.translators": "Für Übersetzer",
      "footer.top": "Nach oben",
      "contribute.addLang": "Deine Sprache hinzufügen",
      "contribute.sub": "Tippfehler oder holprige Stelle? Eine Korrektur per Pull Request dauert nur ein paar Minuten — direkt im Browser.",
      "finale.started": "Download gestartet",
      "packs.ingamePath": "Preferences → Language",
      "dialog.hint": "Klick — nächste Sprache",
      "dialog.line": "Jetzt verstehst du jedes einzelne Wort.",
      "gallery.open": "Bild öffnen",
      "gallery.cap1": "Die ganze Klasse",
      "gallery.cap2": "Schulhof",
      "gallery.cap3": "Stadt bei Sonnenuntergang",
      "gallery.cap4": "Der Morgenzug",
      "lightbox.close": "Schließen",
      "lightbox.prev": "Zurück",
      "lightbox.next": "Weiter"
    },
    uk: {
      "hero.title": "тепер твоєю мовою",
      "hero.lede": "Уся гра рідною мовою. Завантаж архів, розпакуй у теку гри та грай.",
      "hero.downloadSub": "ZIP · 65 МБ",
      "finale.sub": "ZIP · 65 МБ · безкоштовно",
      "a11y.skip": "До вмісту",
      "hero.free": "безкоштовно для всіх",
      "footer.legal": "Усі переклади належать своїм авторам, якщо автора вказано. Переклади, виконані нейромережами, належать проєкту. Усі переклади поширюються як open source, крім авторських — вони розміщуються зі згоди своїх авторів.",
      "footer.contact": "Якщо тут розміщено ваш переклад — напишіть нам:",
      "footer.project": "Проєкт",
      "footer.translators": "Перекладачам",
      "footer.top": "Догори",
      "contribute.addLang": "Додати свою мову",
      "contribute.sub": "Одруківка чи криве формулювання? Правка через pull request займає кілька хвилин — просто у браузері.",
      "finale.started": "Завантаження почалося",
      "packs.ingamePath": "Preferences → Language",
      "dialog.hint": "Клік — наступна мова",
      "dialog.line": "Тепер ти розумієш кожне слово.",
      "gallery.open": "Відкрити кадр",
      "gallery.cap1": "Увесь клас у зборі",
      "gallery.cap2": "Шкільне подвір'я",
      "gallery.cap3": "Місто на заході сонця",
      "gallery.cap4": "Ранкова електричка",
      "lightbox.close": "Закрити",
      "lightbox.prev": "Назад",
      "lightbox.next": "Далі"
    },
    es: {
      "hero.title": "ahora habla tu idioma",
      "hero.lede": "Todo el juego en tu idioma. Descarga el archivo, descomprímelo en la carpeta del juego y juega.",
      "hero.downloadSub": "ZIP · 65 MB",
      "finale.sub": "ZIP · 65 MB · gratis para todos",
      "a11y.skip": "Ir al contenido",
      "hero.free": "gratis para todos",
      "footer.legal": "Todas las traducciones pertenecen a sus autores cuando se indica el autor. Las traducciones hechas con redes neuronales pertenecen al proyecto. Todas las traducciones son open source, salvo los paquetes de autor, publicados con la aprobación de sus autores.",
      "footer.contact": "Si tu traducción aparece aquí — escríbenos:",
      "footer.project": "Proyecto",
      "footer.translators": "Para traductores",
      "footer.top": "Arriba",
      "contribute.addLang": "Añade tu idioma",
      "contribute.sub": "¿Una errata o una frase rara? Corregirla por pull request lleva un par de minutos — directamente en el navegador.",
      "finale.started": "Descarga iniciada",
      "packs.ingamePath": "Preferences → Language",
      "dialog.hint": "Clic — siguiente idioma",
      "dialog.line": "Ahora entiendes cada palabra.",
      "gallery.open": "Abrir captura",
      "gallery.cap1": "Toda la clase reunida",
      "gallery.cap2": "El patio de la escuela",
      "gallery.cap3": "La ciudad al atardecer",
      "gallery.cap4": "El tren de la mañana",
      "lightbox.close": "Cerrar",
      "lightbox.prev": "Anterior",
      "lightbox.next": "Siguiente"
    },
    fr: {
      "hero.title": "parle désormais ta langue",
      "hero.lede": "Tout le jeu dans ta langue. Télécharge l’archive, décompresse-la dans le dossier du jeu et joue.",
      "hero.downloadSub": "ZIP · 65 Mo",
      "finale.sub": "ZIP · 65 Mo · gratuit pour tous",
      "a11y.skip": "Aller au contenu",
      "hero.free": "gratuit pour tous",
      "footer.legal": "Toutes les traductions appartiennent à leurs auteurs lorsqu’un auteur est indiqué. Les traductions réalisées avec des réseaux de neurones appartiennent au projet. Toutes les traductions sont open source, sauf les packs d’auteur — publiés avec l’accord de leurs auteurs.",
      "footer.contact": "Si votre traduction figure ici — écrivez-nous :",
      "footer.project": "Projet",
      "footer.translators": "Aux traducteurs",
      "footer.top": "Haut de page",
      "contribute.addLang": "Ajouter ta langue",
      "contribute.sub": "Une coquille ou une tournure maladroite ? Une correction par pull request prend deux minutes — directement dans le navigateur.",
      "finale.started": "Téléchargement lancé",
      "packs.ingamePath": "Preferences → Language",
      "dialog.hint": "Clic — langue suivante",
      "dialog.line": "Maintenant, tu comprends chaque mot.",
      "gallery.open": "Ouvrir l'image",
      "gallery.cap1": "Toute la classe réunie",
      "gallery.cap2": "La cour de l'école",
      "gallery.cap3": "La ville au crépuscule",
      "gallery.cap4": "Le train du matin",
      "lightbox.close": "Fermer",
      "lightbox.prev": "Précédent",
      "lightbox.next": "Suivant"
    }
  };
  for (const l in I18N_PATCH) Object.assign(I18N[l], I18N_PATCH[l]);

  // v4: six packs done (ru/uk/de/es/fr/zh) — refreshed statuses, zh language, fixed gallery captions
  const I18N_PATCH2 = {
    ru: {
      "meta.description": "Неофициальный multilingual-мод для Sarah’s House. Готовы 8 языков (100%): RU, UK, DE, ES, FR, ZH, TR, PT.",
      "packs.offer.step2": "В mod/i18n/ создайте файл языка, например it.json, pl.json, ja.json или ko.json.",
      "lang.zh": "Китайский",
      "lang.tr": "Турецкий",
      "lang.pt": "Португальский",
      "lang.ja": "Японский",
      "packs.totalLines": "Всего строк перевода:",
      "gallery.cap1": "Утро в ванной",
      "gallery.cap2": "Воскресный день",
      "gallery.cap3": "Разговор в комнате",
      "gallery.cap4": "Прихожая"
    },
    en: {
      "meta.description": "Unofficial multilingual mod for Sarah’s House. 8 languages complete (100%): RU, UK, DE, ES, FR, ZH, TR, PT.",
      "packs.offer.step2": "Create a language file in mod/i18n/, e.g. it.json, pl.json, ja.json, or ko.json.",
      "lang.zh": "Chinese",
      "lang.tr": "Turkish",
      "lang.pt": "Portuguese",
      "lang.ja": "Japanese",
      "packs.totalLines": "Total translated lines:",
      "gallery.cap1": "Morning in the bathroom",
      "gallery.cap2": "Sunday afternoon",
      "gallery.cap3": "A talk in the room",
      "gallery.cap4": "The hallway"
    },
    de: {
      "meta.description": "Inoffizieller Multilingual-Mod für Sarah’s House. 8 Sprachen fertig (100%): RU, UK, DE, ES, FR, ZH, TR, PT.",
      "packs.offer.step2": "Erstelle in mod/i18n/ eine Sprachdatei, z. B. it.json, pl.json, ja.json oder ko.json.",
      "lang.zh": "Chinesisch",
      "lang.tr": "Türkisch",
      "lang.pt": "Portugiesisch",
      "lang.ja": "Japanisch",
      "packs.totalLines": "Übersetzte Zeilen gesamt:",
      "gallery.cap1": "Morgen im Bad",
      "gallery.cap2": "Sonntagnachmittag",
      "gallery.cap3": "Gespräch im Zimmer",
      "gallery.cap4": "Der Flur"
    },
    uk: {
      "meta.description": "Неофіційний multilingual-мод для Sarah’s House. Готово 8 мов (100%): RU, UK, DE, ES, FR, ZH, TR, PT.",
      "packs.offer.step2": "У mod/i18n/ створіть файл мови, наприклад it.json, pl.json, ja.json або ko.json.",
      "lang.zh": "Китайська",
      "lang.tr": "Турецька",
      "lang.pt": "Португальська",
      "lang.ja": "Японська",
      "packs.totalLines": "Всього рядків перекладу:",
      "gallery.cap1": "Ранок у ванній",
      "gallery.cap2": "Недільний день",
      "gallery.cap3": "Розмова в кімнаті",
      "gallery.cap4": "Передпокій"
    },
    es: {
      "meta.description": "Mod multilingüe no oficial para Sarah’s House. 8 idiomas completados (100%): RU, UK, DE, ES, FR, ZH, TR, PT.",
      "packs.offer.step2": "Crea un archivo de idioma en mod/i18n/, p. ej. it.json, pl.json, ja.json o ko.json.",
      "lang.zh": "Chino",
      "lang.tr": "Turco",
      "lang.pt": "Portugués",
      "lang.ja": "Japonés",
      "packs.totalLines": "Líneas traducidas en total:",
      "gallery.cap1": "Mañana en el baño",
      "gallery.cap2": "Tarde de domingo",
      "gallery.cap3": "Charla en la habitación",
      "gallery.cap4": "El recibidor"
    },
    fr: {
      "meta.description": "Mod multilingue non officiel pour Sarah’s House. 8 langues terminées (100 %) : RU, UK, DE, ES, FR, ZH, TR, PT.",
      "packs.offer.step2": "Créez un fichier de langue dans mod/i18n/, par ex. it.json, pl.json, ja.json ou ko.json.",
      "lang.zh": "Chinois",
      "lang.tr": "Turc",
      "lang.pt": "Portugais",
      "lang.ja": "Japonais",
      "packs.totalLines": "Lignes traduites au total :",
      "gallery.cap1": "Matin dans la salle de bain",
      "gallery.cap2": "Dimanche après-midi",
      "gallery.cap3": "Discussion dans la chambre",
      "gallery.cap4": "L’entrée"
    }
  };
  for (const l in I18N_PATCH2) Object.assign(I18N[l], I18N_PATCH2[l]);

  // v5: ja + ko packs shipped — 10 languages. Full Korean UI + lang.ko labels + refreshed counts.
  I18N.ko = {
    "meta.title": "Sarah’s House — 번역 모드",
    "meta.description": "Sarah’s House 비공식 다국어 모드. 10개 언어 완성(100%): RU, UK, DE, ES, FR, ZH, TR, PT, JA, KO.",
    "meta.og": "Sarah’s House 0.11.2 비공식 번역 모드",
    "nav.languages": "언어",
    "nav.install": "설치",
    "nav.gallery": "갤러리",
    "nav.contribute": "수정",
    "nav.download": "다운로드",
    "hero.title": "이제 한국어로",
    "hero.lede": "게임 전체가 네 언어로. 압축 파일을 받아 게임 폴더에 풀고 플레이하면 끝.",
    "hero.download": "모드 다운로드",
    "hero.downloadSub": "ZIP · 65 MB",
    "packs.eyebrow": "언어",
    "packs.title": "패키지 상태",
    "packs.ingame": "게임 내:",
    "packs.cycle": "주기:",
    "packs.latestTitle": "가장 최근 완성된 번역",
    "packs.latestSub": "가장 최신 언어 패키지",
    "packs.othersTitle": "그 외 모든 언어",
    "packs.othersSub": "목록에 담긴 콤팩트 패키지",
    "packs.author": "작성자",
    "packs.offer": "직접 만든 번역을 제안하시겠어요?",
    "packs.offer.step1": "elofaster/sarahs-house-i18n 저장소를 포크하세요.",
    "packs.offer.step2": "mod/i18n/ 에 언어 파일을 만드세요. 예: it.json, pl.json, ja.json, ko.json.",
    "packs.offer.step3": "파일 맨 위에 \"__meta.author\" 키로 닉네임을 적으세요.",
    "packs.offer.step4": "\"english text\": \"번역문\" 형식으로 번역 줄을 추가하세요.",
    "packs.offer.step5": "main 으로 Pull Request 를 여세요 — 검토 후 사이트에 패키지가 표시됩니다.",
    "packs.offer.cta": "GitHub에서 번역 파일 만들기",
    "packs.lines": "줄 수",
    "packs.completeness": "완성도",
    "status.ready": "완성",
    "status.wip": "진행 중",
    "lang.ru": "러시아어",
    "lang.en": "영어",
    "lang.de": "독일어",
    "lang.fr": "프랑스어",
    "lang.uk": "우크라이나어",
    "lang.es": "스페인어",
    "lang.zh": "중국어",
    "lang.tr": "튀르키예어",
    "lang.pt": "포르투갈어",
    "lang.ja": "일본어",
    "lang.ko": "한국어",
    "packs.totalLines": "총 번역 줄 수:",
    "install.eyebrow": "설치",
    "install.title": "설치 방법",
    "install.need": "Sarah's House v0.11.2 가 필요합니다. 게임 본체는 포함되지 않습니다.",
    "install.downloadZip": "ZIP 다운로드",
    "install.step1Title": "다운로드",
    "install.step2Title": "게임 폴더에",
    "install.step2Text": "SarahsHouse.exe 와 같은 위치에",
    "install.step3Title": "실행",
    "install.step3Text": "첫 실행은 1~3분 걸립니다",
    "install.remove": "제거:",
    "gallery.eyebrow": "갤러리",
    "gallery.title": "게임 화면",
    "contribute.eyebrow": "수정",
    "contribute.title": "수정을 제안하시겠어요?",
    "contribute.cta": "수정 제안",
    "finale.title": "모드 v2.9.0 다운로드",
    "finale.sub": "ZIP · 65 MB · 모두 무료",
    "footer.note": "비공식 모드입니다. AceStudio와 무관합니다.",
    "footer.game": "게임",
    "a11y.skip": "본문으로 건너뛰기",
    "hero.free": "모두 무료",
    "footer.legal": "작성자가 표기된 모든 번역의 저작권은 각 작성자에게 있습니다. 신경망으로 만든 번역은 본 프로젝트에 귀속됩니다. 작성자 패키지를 제외한 모든 번역은 오픈 소스로 배포되며, 작성자 패키지는 작성자의 동의를 얻어 게재됩니다.",
    "footer.contact": "여기에 당신의 번역이 실렸다면 — 연락 주세요:",
    "footer.project": "프로젝트",
    "footer.translators": "번역가분들께",
    "footer.top": "맨 위로",
    "contribute.addLang": "내 언어 추가",
    "contribute.sub": "오타나 어색한 문장을 발견했나요? pull request 로 몇 분이면 수정 — 브라우저에서 바로.",
    "finale.started": "다운로드 시작됨",
    "packs.ingamePath": "Preferences → Language",
    "dialog.hint": "클릭 — 다음 언어",
    "dialog.line": "이제 모든 단어가 이해돼.",
    "gallery.open": "이미지 열기",
    "gallery.cap1": "욕실의 아침",
    "gallery.cap2": "일요일 오후",
    "gallery.cap3": "방에서의 대화",
    "gallery.cap4": "현관",
    "lightbox.close": "닫기",
    "lightbox.prev": "이전",
    "lightbox.next": "다음",
  };

  // Add the Korean language label + refresh SEO description to 10 languages for every UI locale.
  const I18N_ADD = {
    ru: { "hero.title": "теперь на русском", "footer.brandSub": "Мод перевода", "packs.offer.exVal": "Как дела?", "lang.en": "Английский", "lang.de": "Немецкий", "lang.fr": "Французский", "lang.uk": "Украинский", "lang.es": "Испанский", "lang.ko": "Корейский", "meta.description": "Неофициальный multilingual-мод для Sarah’s House. Готовы 10 языков (100%): RU, UK, DE, ES, FR, ZH, TR, PT, JA, KO." },
    en: { "hero.title": "now in your language", "footer.brandSub": "Translation Mod", "packs.offer.exVal": "How are you?", "lang.ko": "Korean", "meta.description": "Unofficial multilingual mod for Sarah’s House. 10 languages complete (100%): RU, UK, DE, ES, FR, ZH, TR, PT, JA, KO." },
    de: { "hero.title": "jetzt auf Deutsch", "footer.brandSub": "Übersetzungsmod", "packs.offer.exVal": "Wie geht's?", "lang.ko": "Koreanisch", "meta.description": "Inoffizieller Multilingual-Mod für Sarah’s House. 10 Sprachen fertig (100%): RU, UK, DE, ES, FR, ZH, TR, PT, JA, KO." },
    uk: { "hero.title": "тепер українською", "footer.brandSub": "Мод перекладу", "packs.offer.exVal": "Як справи?", "lang.en": "Англійська", "lang.de": "Німецька", "lang.fr": "Французька", "lang.es": "Іспанська", "lang.ko": "Корейська", "meta.description": "Неофіційний multilingual-мод для Sarah’s House. Готово 10 мов (100%): RU, UK, DE, ES, FR, ZH, TR, PT, JA, KO." },
    es: { "hero.title": "ahora en español", "footer.brandSub": "Mod de traducción", "packs.offer.exVal": "¿Qué tal?", "lang.ko": "Coreano", "meta.description": "Mod multilingüe no oficial para Sarah’s House. 10 idiomas completados (100%): RU, UK, DE, ES, FR, ZH, TR, PT, JA, KO." },
    fr: { "hero.title": "désormais en français", "footer.brandSub": "Mod de traduction", "packs.offer.exVal": "Ça va ?", "lang.ko": "Coréen", "meta.description": "Mod multilingue non officiel pour Sarah’s House. 10 langues terminées (100 %) : RU, UK, DE, ES, FR, ZH, TR, PT, JA, KO." },
    zh: { "hero.title": "现在有了中文", "footer.brandSub": "翻译模组", "packs.offer.exVal": "你好吗？", "lang.ko": "韩语", "meta.description": "Sarah’s House 非官方多语言模组。10 种语言全部完成（100%）：俄语、乌克兰语、德语、西班牙语、法语、中文、土耳其语、葡萄牙语、日语、韩语。" },
    tr: { "hero.title": "artık Türkçe", "footer.brandSub": "Çeviri Modu", "packs.offer.exVal": "Nasılsın?", "lang.ko": "Korece", "meta.description": "Sarah’s House için gayri resmi çoklu dil modu. 10 dil tamamlandı (%100): RU, UK, DE, ES, FR, ZH, TR, PT, JA, KO." },
    ja: { "hero.title": "いま、日本語で", "footer.brandSub": "翻訳MOD", "packs.offer.exVal": "元気？", "lang.ko": "韓国語", "meta.description": "Sarah’s House 非公式マルチ言語MOD。10言語が完成（100%）：露・宇・独・西・仏・中・土・葡・日・韓。" },
    ko: { "hero.title": "이제 한국어로", "footer.brandSub": "번역 모드", "packs.offer.exVal": "잘 지내?" },
  };
  for (const l in I18N_ADD) if (I18N[l]) Object.assign(I18N[l], I18N_ADD[l]);

  // v6: reworked install steps (5 steps, screenshots on 4 & 5).
  const I18N_INSTALL = {
    ru: { "install.step2Title": "Распаковать", "install.step2Text": "архиватором в папку игры, рядом с SarahsHouse.exe", "install.step4Title": "Меню языков", "install.step4Text": "открой переключатель в правом верхнем углу", "install.step5Title": "Выбор языка", "install.step5Text": "выбери нужный язык и играй" },
    en: { "install.step2Title": "Unpack", "install.step2Text": "with an archiver into the game folder, next to SarahsHouse.exe", "install.step4Title": "Language menu", "install.step4Text": "open the switcher in the top-right corner", "install.step5Title": "Choose language", "install.step5Text": "pick your language and play" },
    de: { "install.step2Title": "Entpacken", "install.step2Text": "mit einem Archivprogramm in den Spielordner, neben SarahsHouse.exe", "install.step4Title": "Sprachmenü", "install.step4Text": "öffne den Umschalter oben rechts", "install.step5Title": "Sprache wählen", "install.step5Text": "wähle deine Sprache und spiel los" },
    uk: { "install.step2Title": "Розпакувати", "install.step2Text": "архіватором у теку гри, поруч із SarahsHouse.exe", "install.step4Title": "Меню мов", "install.step4Text": "відкрий перемикач у правому верхньому куті", "install.step5Title": "Вибір мови", "install.step5Text": "обери потрібну мову та грай" },
    es: { "install.step2Title": "Descomprimir", "install.step2Text": "con un archivador en la carpeta del juego, junto a SarahsHouse.exe", "install.step4Title": "Menú de idiomas", "install.step4Text": "abre el selector en la esquina superior derecha", "install.step5Title": "Elegir idioma", "install.step5Text": "elige tu idioma y juega" },
    fr: { "install.step2Title": "Décompresser", "install.step2Text": "avec un archiveur dans le dossier du jeu, à côté de SarahsHouse.exe", "install.step4Title": "Menu des langues", "install.step4Text": "ouvre le sélecteur en haut à droite", "install.step5Title": "Choisir la langue", "install.step5Text": "choisis ta langue et joue" },
    zh: { "install.step2Title": "解压", "install.step2Text": "用解压软件解压到游戏目录，与 SarahsHouse.exe 同级", "install.step4Title": "语言菜单", "install.step4Text": "点击右上角的语言切换按钮", "install.step5Title": "选择语言", "install.step5Text": "选择想要的语言，开始游玩" },
    tr: { "install.step2Title": "Çıkart", "install.step2Text": "bir arşivleyiciyle oyun klasörüne, SarahsHouse.exe'nin yanına", "install.step4Title": "Dil menüsü", "install.step4Text": "sağ üstteki dil düğmesini aç", "install.step5Title": "Dil seç", "install.step5Text": "istediğin dili seç ve oyna" },
    ja: { "install.step2Title": "解凍", "install.step2Text": "解凍ソフトでゲームフォルダ（SarahsHouse.exe と同じ場所）へ", "install.step4Title": "言語メニュー", "install.step4Text": "右上の言語スイッチャーを開く", "install.step5Title": "言語を選ぶ", "install.step5Text": "好きな言語を選んでプレイ" },
    ko: { "install.step2Title": "압축 해제", "install.step2Text": "압축 프로그램으로 게임 폴더(SarahsHouse.exe 옆)에", "install.step4Title": "언어 메뉴", "install.step4Text": "오른쪽 위의 언어 전환 버튼 열기", "install.step5Title": "언어 선택", "install.step5Text": "원하는 언어를 고르고 플레이" },
  };
  for (const l in I18N_INSTALL) if (I18N[l]) Object.assign(I18N[l], I18N_INSTALL[l]);

    I18N.pl = {"meta.title": "Sarah’s House — mod z tłumaczeniem", "meta.description": "Nieoficjalny wielojęzyczny mod do Sarah’s House. 10 języków ukończonych (100%): RU, UK, DE, ES, FR, ZH, TR, PT, JA, KO.", "meta.og": "Nieoficjalny mod z tłumaczeniem do Sarah’s House 0.11.2", "nav.languages": "Języki", "nav.install": "Instalacja", "nav.gallery": "Galeria", "nav.contribute": "Współtwórz", "nav.download": "Pobierz", "hero.title": "teraz w twoim języku", "hero.lede": "Cała gra w twoim języku. Pobierz archiwum, rozpakuj je do folderu gry i graj.", "hero.download": "Pobierz mod", "hero.downloadSub": "ZIP · 65 MB", "packs.eyebrow": "Języki", "packs.title": "Status paczek", "packs.ingame": "W grze:", "packs.cycle": "przełączanie:", "packs.latestTitle": "Najnowsze ukończone tłumaczenie", "packs.latestSub": "najświeższa gotowa paczka językowa", "packs.othersTitle": "Wszystkie pozostałe języki", "packs.othersSub": "kompaktowe paczki na jednej liście", "packs.author": "Autor", "packs.offer": "Chcesz dodać autorskie tłumaczenie?", "packs.offer.step1": "Zrób fork repozytorium elofaster/sarahs-house-i18n.", "packs.offer.step2": "Utwórz plik języka w mod/i18n/, np. it.json, pl.json, ja.json lub ko.json.", "packs.offer.step3": "Na początku pliku ustaw swój nick kluczem \"__meta.author\".", "packs.offer.step4": "Dodawaj wiersze tłumaczenia w formacie \"english text\": \"twój tekst\".", "packs.offer.step5": "Otwórz Pull Request do main — po weryfikacji paczka pojawi się na stronie.", "packs.offer.cta": "Utwórz plik tłumaczenia na GitHubie", "packs.lines": "Wiersze", "packs.completeness": "Ukończenie", "status.ready": "gotowe", "status.wip": "w trakcie", "lang.ru": "Rosyjski", "lang.en": "Angielski", "lang.de": "Niemiecki", "lang.fr": "Francuski", "lang.uk": "Ukraiński", "lang.es": "Hiszpański", "install.eyebrow": "Instalacja", "install.title": "Jak zainstalować", "install.need": "Wymaga Sarah's House v0.11.2. Gra nie jest dołączona do archiwum.", "install.downloadZip": "Pobierz ZIP", "install.step1Title": "Pobierz", "install.step2Title": "Rozpakuj", "install.step2Text": "archiwizatorem do folderu gry, obok SarahsHouse.exe", "install.step3Title": "Uruchom", "install.step3Text": "pierwsze uruchomienie trwa 1–3 min", "install.remove": "Odinstalowanie:", "gallery.eyebrow": "Galeria", "gallery.title": "Z gry", "contribute.eyebrow": "Współtwórz", "contribute.title": "Chcesz zaproponować poprawki?", "contribute.cta": "Zaproponuj poprawki", "finale.title": "Pobierz mod v2.9.0", "finale.sub": "ZIP · 65 MB · za darmo dla każdego", "footer.note": "Nieoficjalny mod. Niepowiązany z AceStudio.", "footer.game": "Gra", "a11y.skip": "Przejdź do treści", "hero.free": "za darmo dla każdego", "footer.legal": "Wszystkie tłumaczenia należą do swoich autorów, jeśli autor jest wskazany. Tłumaczenia wykonane przez sieci neuronowe należą do projektu. Wszystkie tłumaczenia są open source, z wyjątkiem paczek autorskich — te są publikowane za zgodą ich autorów.", "footer.contact": "Jeśli twoje tłumaczenie jest tu wymienione — napisz do nas:", "footer.project": "Projekt", "footer.translators": "Dla tłumaczy", "footer.top": "Do góry", "contribute.addLang": "Dodaj swój język", "contribute.sub": "Zauważyłeś literówkę albo niezgrabne zdanie? Poprawka przez pull request zajmuje kilka minut — prosto w przeglądarce.", "finale.started": "Pobieranie rozpoczęte", "packs.ingamePath": "Preferences → Language", "dialog.hint": "Kliknij — następny język", "dialog.line": "Teraz rozumiesz każde słowo.", "gallery.open": "Otwórz zrzut", "gallery.cap1": "Poranek w łazience", "gallery.cap2": "Niedzielne popołudnie", "gallery.cap3": "Rozmowa w pokoju", "gallery.cap4": "Przedpokój", "lightbox.close": "Zamknij", "lightbox.prev": "Poprzednie", "lightbox.next": "Następne", "lang.zh": "Chiński", "lang.tr": "Turecki", "lang.pt": "Portugalski", "lang.ja": "Japoński", "packs.totalLines": "Łącznie przetłumaczonych wierszy:", "footer.brandSub": "Mod z tłumaczeniem", "packs.offer.exVal": "Jak się masz?", "lang.ko": "Koreański", "install.step4Title": "Menu języków", "install.step4Text": "otwórz przełącznik w prawym górnym rogu", "install.step5Title": "Wybierz język", "install.step5Text": "wybierz swój język i graj", "lang.pl": "Polski"};
  (function(){var N={"ru": "Польский", "en": "Polish", "de": "Polnisch", "uk": "Польська", "es": "Polaco", "fr": "Polonais", "zh": "波兰语", "tr": "Lehçe", "ja": "ポーランド語", "ko": "폴란드어", "pl": "Polski"};for(var _l in I18N){if(I18N[_l]&&!I18N[_l]["lang.pl"])I18N[_l]["lang.pl"]=N[_l]||"Polski";}})();
  const SUPPORTED = Object.keys(I18N);
  const STORAGE_KEY = "hc-site-lang";

  const t = (lang, key) =>
    (I18N[lang] && I18N[lang][key]) || I18N.ru[key] || key;

  const applyLang = (lang) => {
    if (!I18N[lang]) lang = "ru";
    document.documentElement.lang = lang;
    try { localStorage.setItem(STORAGE_KEY, lang); } catch (e) { /* sandboxed view */ }

    document.querySelectorAll("[data-i18n]").forEach((el) => {
      const key = el.getAttribute("data-i18n");
      const val = t(lang, key);
      if (val != null) el.textContent = val;
    });

    document.querySelectorAll("[data-i18n-content]").forEach((el) => {
      const key = el.getAttribute("data-i18n-content");
      const val = t(lang, key);
      if (val != null) el.setAttribute("content", val);
    });

    document.querySelectorAll("[data-i18n-aria]").forEach((el) => {
      const key = el.getAttribute("data-i18n-aria");
      const val = t(lang, key);
      if (val != null) el.setAttribute("aria-label", val);
    });

    document.querySelectorAll("#langSwitch [data-lang]").forEach((btn) => {
      const on = btn.getAttribute("data-lang") === lang;
      btn.setAttribute("aria-pressed", on ? "true" : "false");
      btn.classList.toggle("is-active", on);
    });

    if (window.__vnSync) window.__vnSync(lang);
  };

  const detectLang = () => {
    let saved = null;
    try { saved = localStorage.getItem(STORAGE_KEY); } catch (e) { /* sandboxed view */ }
    if (saved && I18N[saved]) return saved;

    const candidates = [];
    if (Array.isArray(navigator.languages)) candidates.push(...navigator.languages);
    if (navigator.language) candidates.push(navigator.language);
    if (navigator.userLanguage) candidates.push(navigator.userLanguage);

    for (const raw of candidates) {
      const code = String(raw || "").toLowerCase().replace(/_/g, "-");
      if (!code) continue;
      const short = code.slice(0, 2);
      // uk-UA / uk, zh etc. — only our supported packs
      if (SUPPORTED.includes(short)) return short;
      if (SUPPORTED.includes(code)) return code;
    }
    return "en";
  };

  // language switcher — cinematic pill morph on change
  const switchRoot = document.getElementById("langSwitch");
  const pillEl = document.getElementById("pill");
  let langMorphBusy = false;
  const applyLangCinematic = (lang) => {
    if (!I18N[lang]) lang = "ru";
    const reduced = window.matchMedia && window.matchMedia("(prefers-reduced-motion: reduce)").matches;
    if (!pillEl || reduced || langMorphBusy || lang === document.documentElement.lang) {
      applyLang(lang);
      return;
    }
    langMorphBusy = true;
    const w0 = pillEl.getBoundingClientRect().width;
    // old labels quietly dissolve
    pillEl.classList.add("is-lang-morph", "is-lang-fading");
    setTimeout(() => {
      applyLang(lang);
      const w1 = pillEl.getBoundingClientRect().width;
      const settle = (delay) => setTimeout(() => {
        pillEl.style.width = "";
        pillEl.style.transition = "";
        pillEl.classList.remove("is-lang-morph");
        langMorphBusy = false;
      }, delay);
      if (Math.abs(w1 - w0) < 1) {
        // same width — a plain gentle crossfade is enough
        pillEl.classList.remove("is-lang-fading");
        settle(260);
        return;
      }
      // one continuous glide to the new width, labels fade back in mid-motion
      pillEl.style.width = w0 + "px";
      pillEl.style.transition = "width .55s cubic-bezier(.33,1,.68,1)";
      void pillEl.offsetWidth;
      pillEl.style.width = w1 + "px";
      setTimeout(() => pillEl.classList.remove("is-lang-fading"), 140);
      settle(620);
    }, 200);
  };
  if (switchRoot) {
    switchRoot.addEventListener("click", (e) => {
      const btn = e.target.closest("[data-lang]");
      if (!btn) return;
      applyLangCinematic(btn.getAttribute("data-lang"));
    });
  }

  applyLang(detectLang());


  // pack authors from i18n-meta.json
  const applyPackAuthors = (meta) => {
    if (!meta || typeof meta !== "object") return;
    document.querySelectorAll("[data-pack-author]").forEach((el) => {
      const code = el.getAttribute("data-pack-author");
      const info = meta[code] || {};
      const name = (info.author || "").trim();
      if (name) {
        el.textContent = name;
        el.classList.remove("none");
      } else {
        el.textContent = "—";
        el.classList.add("none");
      }
    });
  };

  fetch(new URL("i18n-meta.json", window.location.href).toString(), { cache: "no-cache" })
    .then((r) => (r.ok ? r.json() : null))
    .then((meta) => applyPackAuthors(meta))
    .catch(() => {});

  // author-pack instructions
  const offerToggle = document.getElementById("packsOfferToggle");
  const offerPanel = document.getElementById("packsOfferPanel");
  if (offerToggle && offerPanel) {
    offerToggle.addEventListener("click", () => {
      const open = offerPanel.hasAttribute("hidden");
      if (open) offerPanel.removeAttribute("hidden");
      else offerPanel.setAttribute("hidden", "");
      offerToggle.setAttribute("aria-expanded", open ? "true" : "false");
    });
  }

  const REDUCED = window.matchMedia && window.matchMedia("(prefers-reduced-motion: reduce)").matches;

  // VN dialogue — one line, typed across all languages
  (() => {
    const box = document.getElementById("vnDialog");
    const nameEl = document.getElementById("vnName");
    const textEl = document.getElementById("vnText");
    const srEl = document.getElementById("vnSr");
    if (!box || !nameEl || !textEl) return;

    const ORDER = ["ru", "en", "de", "uk", "es", "fr", "tr", "zh", "ja", "ko"];
    const NATIVE = { ru: "Русский", en: "English", de: "Deutsch", uk: "Українська", es: "Español", fr: "Français", zh: "中文", tr: "Türkçe", ja: "日本語", ko: "한국어" };
    const TYPE_MS = 34;
    const HOLD_MS = 2600;
    let idx = 0;
    let timer = null;
    let visible = true;

    const stop = () => { if (timer) { clearTimeout(timer); timer = null; } };

    const render = (code, animate) => {
      const line = t(code, "dialog.line");
      nameEl.textContent = NATIVE[code] || code;
      if (srEl) srEl.textContent = line;
      stop();
      box.classList.remove("is-done");
      if (!animate || REDUCED) {
        textEl.textContent = line;
        box.classList.add("is-done");
        if (!REDUCED) timer = setTimeout(next, HOLD_MS);
        return;
      }
      textEl.textContent = "";
      let i = 0;
      const tick = () => {
        i += 1;
        textEl.textContent = line.slice(0, i);
        if (i < line.length) {
          timer = setTimeout(tick, TYPE_MS);
        } else {
          box.classList.add("is-done");
          timer = setTimeout(next, HOLD_MS);
        }
      };
      timer = setTimeout(tick, TYPE_MS);
    };

    const next = () => {
      if (!visible) return;
      idx = (idx + 1) % ORDER.length;
      render(ORDER[idx], true);
    };

    window.__vnSync = (code) => {
      idx = Math.max(0, ORDER.indexOf(code));
      render(ORDER[idx], !REDUCED);
    };

    box.addEventListener("click", next);
    box.addEventListener("keydown", (e) => {
      if (e.key === "Enter" || e.key === " ") { e.preventDefault(); next(); }
    });

    if ("IntersectionObserver" in window) {
      new IntersectionObserver((entries) => {
        entries.forEach((en) => {
          visible = en.isIntersecting;
          if (!visible) stop();
          else if (!timer) render(ORDER[idx], !REDUCED);
        });
      }, { threshold: 0.2 }).observe(box);
    }

    window.__vnSync(document.documentElement.lang || "ru");
  })();

  // gallery lightbox
  (() => {
    const lightbox = document.getElementById("lightbox");
    const lbImg = document.getElementById("lbImg");
    const lbCap = document.getElementById("lbCap");
    const lbCount = document.getElementById("lbCount");
    const shots = [...document.querySelectorAll(".shot-btn")];
    if (!lightbox || !lbImg || !shots.length) return;

    let index = 0;
    let opener = null;

    const render = () => {
      const btn = shots[index];
      const img = btn.querySelector("img");
      const lang = document.documentElement.lang || "ru";
      const cap = btn.getAttribute("data-cap");
      lbImg.src = img.currentSrc || img.src;
      lbImg.alt = cap ? t(lang, cap) : "";
      if (lbCap) lbCap.textContent = cap ? t(lang, cap) : "";
      if (lbCount) lbCount.textContent = (index + 1) + " / " + shots.length;
    };
    const open = (i) => {
      index = i;
      opener = document.activeElement;
      render();
      lightbox.removeAttribute("hidden");
      requestAnimationFrame(() => lightbox.classList.add("is-open"));
      document.body.style.overflow = "hidden";
      const closeBtn = document.getElementById("lbClose");
      if (closeBtn) closeBtn.focus();
    };
    const close = () => {
      lightbox.classList.remove("is-open");
      const done = () => {
        lightbox.setAttribute("hidden", "");
        document.body.style.overflow = "";
        if (opener && opener.focus) opener.focus();
      };
      if (REDUCED) done(); else setTimeout(done, 250);
    };
    const step = (d) => {
      index = (index + d + shots.length) % shots.length;
      render();
    };

    shots.forEach((btn, i) => btn.addEventListener("click", () => open(i)));
    const closeBtn = document.getElementById("lbClose");
    const prevBtn = document.getElementById("lbPrev");
    const nextBtn = document.getElementById("lbNext");
    if (closeBtn) closeBtn.addEventListener("click", close);
    if (prevBtn) prevBtn.addEventListener("click", () => step(-1));
    if (nextBtn) nextBtn.addEventListener("click", () => step(1));
    lightbox.addEventListener("click", (e) => { if (e.target === lightbox) close(); });
    document.addEventListener("keydown", (e) => {
      if (lightbox.hasAttribute("hidden")) return;
      if (e.key === "Escape") close();
      else if (e.key === "ArrowLeft") step(-1);
      else if (e.key === "ArrowRight") step(1);
    });
  })();

  // cinematic tilt: the game frame settles flat as it scrolls into view
  (() => {
    const frame = document.querySelector(".hero-frame");
    if (!frame || REDUCED) return;
    let raf = null;
    const update = () => {
      raf = null;
      const r = frame.getBoundingClientRect();
      const vh = window.innerHeight || 1;
      const p = Math.min(1, Math.max(0, 1 - (r.top - vh * 0.1) / (vh * 0.65)));
      const tilt = (1 - p) * 7;
      frame.style.transform = tilt > 0.05
        ? "perspective(1200px) rotateX(" + tilt.toFixed(2) + "deg)"
        : "";
    };
    const onTiltScroll = () => { if (!raf) raf = requestAnimationFrame(update); };
    window.addEventListener("scroll", onTiltScroll, { passive: true });
    window.addEventListener("resize", onTiltScroll, { passive: true });
    update();
  })();

  // RU progress bar fills and the big percentage counts up on reveal
  (() => {
    const fill = document.querySelector(".hero-strip-bar > span[data-fill]");
    const pct = document.querySelector(".hero-strip-pct");
    if (!fill || REDUCED || !("IntersectionObserver" in window)) return;
    fill.style.width = "0%";
    if (pct) pct.textContent = "0%";
    const countPct = () => {
      if (!pct) return;
      const D = 1150;
      const t0 = performance.now();
      const step = (now) => {
        const p = Math.min(1, (now - t0) / D);
        const eased = 1 - Math.pow(1 - p, 3);
        pct.textContent = Math.round(eased * 100) + "%";
        if (p < 1) requestAnimationFrame(step);
      };
      requestAnimationFrame(step);
    };
    const fio = new IntersectionObserver((entries) => {
      entries.forEach((en) => {
        if (!en.isIntersecting) return;
        requestAnimationFrame(() => {
          fill.style.width = fill.getAttribute("data-fill") + "%";
        });
        countPct();
        fio.unobserve(en.target);
      });
    }, { threshold: 0.35 });
    fio.observe(fill.closest(".hero-strip") || fill);
  })();

  // pointer parallax inside the game frame (dialogue stays put — depth)
  (() => {
    const frame = document.querySelector(".hero-frame");
    const img = frame && frame.querySelector("img");
    if (!frame || !img || REDUCED) return;
    if (!window.matchMedia("(hover: hover) and (pointer: fine)").matches) return;
    let raf = null;
    frame.addEventListener("pointermove", (e) => {
      const r = frame.getBoundingClientRect();
      const tx = ((e.clientX - r.left) / r.width - 0.5) * -14;
      const ty = ((e.clientY - r.top) / r.height - 0.5) * -10;
      if (!raf) raf = requestAnimationFrame(() => {
        raf = null;
        img.style.transform = "translate(" + tx.toFixed(1) + "px," + ty.toFixed(1) + "px) scale(1.06)";
      });
    });
    frame.addEventListener("pointerleave", () => {
      if (raf) { cancelAnimationFrame(raf); raf = null; }
      img.style.transform = "";
    });
  })();

  // finale download — cinematic celebration: ripple, morphing button, petal burst
  (() => {
    const btn = document.getElementById("finaleDownload");
    if (!btn) return;
    let busy = false;

    const celebrate = (cx, cy) => {
      const canvas = document.createElement("canvas");
      canvas.className = "fx-canvas";
      const dpr = Math.min(2, window.devicePixelRatio || 1);
      canvas.width = window.innerWidth * dpr;
      canvas.height = window.innerHeight * dpr;
      document.body.appendChild(canvas);
      const ctx = canvas.getContext("2d");
      ctx.scale(dpr, dpr);

      const COLORS = ["#f0c2b0", "#e8a48c", "#de8fae", "#b48cff", "#8fd0b5", "#fff1e6"];
      const parts = [];
      for (let i = 0; i < 84; i++) {
        const ang = (-90 + (Math.random() * 140 - 70)) * Math.PI / 180;
        const speed = 6.5 + Math.random() * 9.5;
        parts.push({
          x: cx, y: cy,
          vx: Math.cos(ang) * speed,
          vy: Math.sin(ang) * speed,
          g: 0.2 + Math.random() * 0.14,
          drag: 0.986,
          rot: Math.random() * Math.PI * 2,
          vr: (Math.random() - 0.5) * 0.32,
          w: 5 + Math.random() * 7,
          h: 3 + Math.random() * 4.5,
          color: COLORS[i % COLORS.length],
          spark: Math.random() < 0.28,
          sway: Math.random() * Math.PI * 2,
          life: 1,
          decay: 0.0075 + Math.random() * 0.008
        });
      }

      let raf = null;
      const step = () => {
        ctx.clearRect(0, 0, window.innerWidth, window.innerHeight);
        let alive = 0;
        for (const p of parts) {
          if (p.life <= 0) continue;
          alive++;
          p.vx *= p.drag;
          p.vy = p.vy * p.drag + p.g;
          p.sway += 0.08;
          p.x += p.vx + Math.sin(p.sway) * 0.6;
          p.y += p.vy;
          p.rot += p.vr;
          p.life -= p.decay;
          ctx.save();
          ctx.globalAlpha = Math.max(0, Math.min(1, p.life * 1.5));
          ctx.translate(p.x, p.y);
          ctx.rotate(p.rot);
          ctx.fillStyle = p.color;
          ctx.beginPath();
          if (p.spark) ctx.arc(0, 0, p.h * 0.45, 0, Math.PI * 2);
          else ctx.ellipse(0, 0, p.w * 0.5, p.h * 0.5, 0, 0, Math.PI * 2);
          ctx.fill();
          ctx.restore();
        }
        if (alive > 0) raf = requestAnimationFrame(step);
        else { if (raf) cancelAnimationFrame(raf); canvas.remove(); }
      };
      raf = requestAnimationFrame(step);
      setTimeout(() => canvas.remove(), 4500);
    };

    btn.addEventListener("click", (e) => {
      if (busy) return;
      busy = true;
      if (REDUCED) {
        btn.classList.add("is-done");
        setTimeout(() => { btn.classList.remove("is-done"); busy = false; }, 2600);
        return;
      }
      const r = btn.getBoundingClientRect();
      const px = e.clientX && e.clientY ? e.clientX - r.left : r.width / 2;
      const py = e.clientX && e.clientY ? e.clientY - r.top : r.height / 2;

      const rip = document.createElement("span");
      rip.className = "fb-ripple";
      const size = Math.max(r.width, r.height) * 2.3;
      rip.style.width = rip.style.height = size + "px";
      rip.style.left = px + "px";
      rip.style.top = py + "px";
      btn.appendChild(rip);
      setTimeout(() => rip.remove(), 750);

      btn.classList.add("is-firing", "is-done");
      celebrate(r.left + r.width / 2, r.top + r.height * 0.35);
      setTimeout(() => btn.classList.remove("is-firing"), 550);
      setTimeout(() => { btn.classList.remove("is-done"); busy = false; }, 3200);
    });
  })();

  // nav scroll
  const pill = document.getElementById("pill");
  const progressBar = document.getElementById("progressBar");
  const navLinks = [...document.querySelectorAll("[data-nav]")];
  const sections = ["languages", "install", "gallery", "contribute"]
    .map((id) => document.getElementById(id))
    .filter(Boolean);

  const onScroll = () => {
    const scrolled = window.scrollY > 24;
    if (pill) pill.classList.toggle("is-compact", scrolled);
    if (switchRoot) switchRoot.classList.toggle("is-hidden", scrolled);
    if (progressBar) {
      const max = document.documentElement.scrollHeight - window.innerHeight;
      progressBar.style.width = (max > 0 ? (window.scrollY / max) * 100 : 0) + "%";
    }
    const y = window.scrollY + 120;
    let current = null;
    for (const sec of sections) if (sec.offsetTop <= y) current = sec.id;
    navLinks.forEach((a) => {
      const href = a.getAttribute("href") || "";
      a.classList.toggle("is-active", !!current && href === `#${current}`);
    });
  };
  onScroll();
  window.addEventListener("scroll", onScroll, { passive: true });

  const nodes = document.querySelectorAll("[data-reveal]");
  if (!("IntersectionObserver" in window)) {
    nodes.forEach((n) => n.classList.add("is-in"));
    return;
  }
  const io = new IntersectionObserver(
    (entries) => {
      for (const entry of entries) {
        if (!entry.isIntersecting) continue;
        entry.target.classList.add("is-in");
        io.unobserve(entry.target);
      }
    },
    { threshold: 0.12, rootMargin: "0px 0px -8% 0px" }
  );

  nodes.forEach((el, i) => {
    el.style.transitionDelay = `${Math.min(i % 7, 6) * 50}ms`;
    io.observe(el);
  });
})();