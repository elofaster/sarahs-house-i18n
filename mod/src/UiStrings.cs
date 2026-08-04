using System;
using System.Collections.Generic;

namespace SarahsHouseI18n;

/// <summary>
/// UI strings for the picker itself. The picker is the one screen that cannot rely on
/// the translation packs — it is shown *before* a language exists — so these few
/// labels are compiled in. The chrome (title, confirm button, key hints) follows the
/// language currently under the cursor, and each tile describes itself in its own
/// language, so you always read the row you are pointing at.
/// </summary>
internal static class UiStrings
{
    internal sealed class Pack
    {
        public string Title;      // big heading
        public string Confirm;    // confirm button verb
        public string Lines;      // "{0} lines" — pack size
        public string NoPack;     // pack missing
        public string Original;   // the untranslated original
        public string Draft;      // "draft · {0}"
        public string HintPick;   // arrows / click
        public string HintOk;     // Enter
        public string HintLater;  // F10
        public string PickFirst;  // nothing selected yet
        public string Project;    // caption above the GitHub link
    }

    private static readonly Pack En = new Pack
    {
        Title = "Choose your language", Confirm = "Confirm", Lines = "{0} lines",
        NoPack = "no pack", Original = "original", Draft = "draft · {0}",
        HintPick = "select", HintOk = "confirm", HintLater = "change later",
        PickFirst = "select a language", Project = "translation project",
    };

    private static readonly Dictionary<string, Pack> Packs = new(StringComparer.OrdinalIgnoreCase)
    {
        ["en"] = En,

        ["ru"] = new Pack
        {
            Title = "Выберите язык", Confirm = "Подтвердить", Lines = "{0} строк",
            NoPack = "нет пака", Original = "оригинал", Draft = "черновик · {0}",
            HintPick = "выбрать", HintOk = "подтвердить", HintLater = "сменить позже",
            PickFirst = "выберите язык", Project = "проект перевода",
        },
        ["uk"] = new Pack
        {
            Title = "Виберіть мову", Confirm = "Підтвердити", Lines = "{0} рядків",
            NoPack = "немає пакета", Original = "оригінал", Draft = "чернетка · {0}",
            HintPick = "вибрати", HintOk = "підтвердити", HintLater = "змінити пізніше",
            PickFirst = "виберіть мову", Project = "проєкт перекладу",
        },
        ["de"] = new Pack
        {
            Title = "Sprache wählen", Confirm = "Bestätigen", Lines = "{0} Zeilen",
            NoPack = "kein Paket", Original = "Original", Draft = "Entwurf · {0}",
            HintPick = "auswählen", HintOk = "bestätigen", HintLater = "später ändern",
            PickFirst = "Sprache auswählen", Project = "Übersetzungsprojekt",
        },
        ["es"] = new Pack
        {
            Title = "Elige tu idioma", Confirm = "Confirmar", Lines = "{0} líneas",
            NoPack = "sin paquete", Original = "original", Draft = "borrador · {0}",
            HintPick = "seleccionar", HintOk = "confirmar", HintLater = "cambiar después",
            PickFirst = "elige un idioma", Project = "proyecto de traducción",
        },
        ["pl"] = new Pack
        {
            Title = "Wybierz język", Confirm = "Potwierdź", Lines = "{0} wierszy",
            NoPack = "brak paczki", Original = "oryginał", Draft = "szkic · {0}",
            HintPick = "wybierz", HintOk = "potwierdź", HintLater = "zmień później",
            PickFirst = "wybierz język", Project = "projekt tłumaczenia",
        },
        ["vi"] = new Pack
        {
            Title = "Chọn ngôn ngữ", Confirm = "Xác nhận", Lines = "{0} dòng",
            NoPack = "chưa có gói", Original = "bản gốc", Draft = "bản nháp · {0}",
            HintPick = "chọn", HintOk = "xác nhận", HintLater = "đổi sau",
            PickFirst = "chọn một ngôn ngữ", Project = "dự án dịch thuật",
        },
        ["zh"] = new Pack
        {
            Title = "选择语言", Confirm = "确认", Lines = "{0} 行",
            NoPack = "无语言包", Original = "原文", Draft = "草稿 · {0}",
            HintPick = "选择", HintOk = "确认", HintLater = "稍后更改",
            PickFirst = "请选择语言", Project = "翻译项目",
        },
        ["ja"] = new Pack
        {
            Title = "言語を選択", Confirm = "決定", Lines = "{0} 行",
            NoPack = "パックなし", Original = "オリジナル", Draft = "下書き · {0}",
            HintPick = "選択", HintOk = "決定", HintLater = "後で変更",
            PickFirst = "言語を選択してください", Project = "翻訳プロジェクト",
        },
        ["ko"] = new Pack
        {
            Title = "언어 선택", Confirm = "확인", Lines = "{0}줄",
            NoPack = "팩 없음", Original = "원문", Draft = "초안 · {0}",
            HintPick = "선택", HintOk = "확인", HintLater = "나중에 변경",
            PickFirst = "언어를 선택하세요", Project = "번역 프로젝트",
        },
        ["fr"] = new Pack
        {
            Title = "Choisissez votre langue", Confirm = "Confirmer", Lines = "{0} lignes",
            NoPack = "pas de pack", Original = "original", Draft = "brouillon · {0}",
            HintPick = "sélectionner", HintOk = "confirmer", HintLater = "changer plus tard",
            PickFirst = "choisissez une langue", Project = "projet de traduction",
        },
        ["pt"] = new Pack
        {
            Title = "Escolha o seu idioma", Confirm = "Confirmar", Lines = "{0} linhas",
            NoPack = "sem pacote", Original = "original", Draft = "rascunho · {0}",
            HintPick = "selecionar", HintOk = "confirmar", HintLater = "alterar depois",
            PickFirst = "escolha um idioma", Project = "projeto de tradução",
        },
        ["tr"] = new Pack
        {
            Title = "Dilinizi seçin", Confirm = "Onayla", Lines = "{0} satır",
            NoPack = "paket yok", Original = "özgün", Draft = "taslak · {0}",
            HintPick = "seç", HintOk = "onayla", HintLater = "sonra değiştir",
            PickFirst = "bir dil seçin", Project = "çeviri projesi",
        },
        ["it"] = new Pack
        {
            Title = "Scegli la lingua", Confirm = "Conferma", Lines = "{0} righe",
            NoPack = "nessun pacchetto", Original = "originale", Draft = "bozza · {0}",
            HintPick = "seleziona", HintOk = "conferma", HintLater = "cambia dopo",
            PickFirst = "scegli una lingua", Project = "progetto di traduzione",
        },
        ["cs"] = new Pack
        {
            Title = "Vyberte jazyk", Confirm = "Potvrdit", Lines = "{0} řádků",
            NoPack = "bez balíčku", Original = "originál", Draft = "koncept · {0}",
            HintPick = "vybrat", HintOk = "potvrdit", HintLater = "změnit později",
            PickFirst = "vyberte jazyk", Project = "projekt překladu",
        },
    };

    /// <summary>Strings for a language code, English when the code is unknown.</summary>
    public static Pack For(string code)
    {
        if (!string.IsNullOrEmpty(code) && Packs.TryGetValue(code, out var p) && p != null) return p;
        return En;
    }

    public static Pack English => En;

    /// <summary>True when this code has its own picker strings (not just the English fallback).</summary>
    public static bool Has(string code) =>
        !string.IsNullOrEmpty(code) && Packs.ContainsKey(code);
}
