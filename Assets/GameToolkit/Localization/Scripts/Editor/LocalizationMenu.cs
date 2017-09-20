// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEditor;

namespace GameToolkit.Localization.Editor
{
	/// <summary>
	/// Unity Editor menu for changing localization under "Tools/Localization".
	/// </summary>
    public static class LocalizationMenu
    {
        private const string ParentMenu = LocalizationEditorHelper.LocalizationMenu + "Set Locale/";

        [MenuItem(ParentMenu + "Afrikaans")]
        static void ChangeToAfrikaans()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Afrikaans;
        }

        [MenuItem(ParentMenu + "Arabic")]
        static void ChangeToArabic()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Arabic;
        }

        [MenuItem(ParentMenu + "Basque")]
        static void ChangeToBasque()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Basque;
        }

        [MenuItem(ParentMenu + "Belarusian")]
        static void ChangeToBelarusian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Belarusian;
        }

        [MenuItem(ParentMenu + "Bulgarian")]
        static void ChangeToBulgarian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Bulgarian;
        }

        [MenuItem(ParentMenu + "Catalan")]
        static void ChangeToCatalan()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Catalan;
        }

        [MenuItem(ParentMenu + "Chinese")]
        static void ChangeToChinese()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Chinese;
        }

        [MenuItem(ParentMenu + "Czech")]
        static void ChangeToCzech()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Czech;
        }

        [MenuItem(ParentMenu + "Danish")]
        static void ChangeToDanish()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Danish;
        }

        [MenuItem(ParentMenu + "Dutch")]
        static void ChangeToDutch()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Dutch;
        }

        [MenuItem(ParentMenu + "English")]
        static void ChangeToEnglish()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.English;
        }

        [MenuItem(ParentMenu + "Estonian")]
        static void ChangeToEstonian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Estonian;
        }

        [MenuItem(ParentMenu + "Faroese")]
        static void ChangeToFaroese()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Faroese;
        }

        [MenuItem(ParentMenu + "Finnish")]
        static void ChangeToFinnish()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Finnish;
        }

        [MenuItem(ParentMenu + "French")]
        static void ChangeToFrench()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.French;
        }

        [MenuItem(ParentMenu + "German")]
        static void ChangeToGerman()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.German;
        }

        [MenuItem(ParentMenu + "Greek")]
        static void ChangeToGreek()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Greek;
        }

        [MenuItem(ParentMenu + "Hebrew")]
        static void ChangeToHebrew()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Hebrew;
        }

        [MenuItem(ParentMenu + "Hungarian")]
        static void ChangeToHungarian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Hungarian;
        }

        [MenuItem(ParentMenu + "Hugarian")]
        static void ChangeToHugarian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Hungarian;
        }

        [MenuItem(ParentMenu + "Icelandic")]
        static void ChangeToIcelandic()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Icelandic;
        }

        [MenuItem(ParentMenu + "Indonesian")]
        static void ChangeToIndonesian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Indonesian;
        }

        [MenuItem(ParentMenu + "Italian")]
        static void ChangeToItalian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Italian;
        }

        [MenuItem(ParentMenu + "Japanese")]
        static void ChangeToJapanese()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Japanese;
        }

        [MenuItem(ParentMenu + "Korean")]
        static void ChangeToKorean()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Korean;
        }

        [MenuItem(ParentMenu + "Latvian")]
        static void ChangeToLatvian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Latvian;
        }

        [MenuItem(ParentMenu + "Lithuanian")]
        static void ChangeToLithuanian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Lithuanian;
        }

        [MenuItem(ParentMenu + "Norwegian")]
        static void ChangeToNorwegian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Norwegian;
        }

        [MenuItem(ParentMenu + "Polish")]
        static void ChangeToPolish()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Polish;
        }

        [MenuItem(ParentMenu + "Portuguese")]
        static void ChangeToPortuguese()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Portuguese;
        }

        [MenuItem(ParentMenu + "Romanian")]
        static void ChangeToRomanian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Romanian;
        }

        [MenuItem(ParentMenu + "Russian")]
        static void ChangeToRussian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Russian;
        }

        [MenuItem(ParentMenu + "SerboCroatian")]
        static void ChangeToSerboCroatian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.SerboCroatian;
        }

        [MenuItem(ParentMenu + "Slovak")]
        static void ChangeToSlovak()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Slovak;
        }

        [MenuItem(ParentMenu + "Slovenian")]
        static void ChangeToSlovenian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Slovenian;
        }

        [MenuItem(ParentMenu + "Spanish")]
        static void ChangeToSpanish()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Spanish;
        }

        [MenuItem(ParentMenu + "Swedish")]
        static void ChangeToSwedish()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Swedish;
        }

        [MenuItem(ParentMenu + "Thai")]
        static void ChangeToThai()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Thai;
        }

        [MenuItem(ParentMenu + "Turkish")]
        static void ChangeToTurkish()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Turkish;
        }

        [MenuItem(ParentMenu + "Ukrainian")]
        static void ChangeToUkrainian()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Ukrainian;
        }

        [MenuItem(ParentMenu + "Vietnamese")]
        static void ChangeToVietnamese()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Vietnamese;
        }

        [MenuItem(ParentMenu + "ChineseSimplified")]
        static void ChangeToChineseSimplified()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.ChineseSimplified;
        }

        [MenuItem(ParentMenu + "ChineseTraditional")]
        static void ChangeToChineseTraditional()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.ChineseTraditional;
        }

        [MenuItem(ParentMenu + "Unknown")]
        static void ChangeToUnknown()
        {
            Localization.Instance.CurrentLanguage = SystemLanguage.Unknown;
        }
    }
}
