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
        private const string ParentMenu = EditorHelper.LocalizationMenu + "Set Locale/";

        [MenuItem(EditorHelper.LocalizationMenu + "Help", false, 1)]
        private static void OpenHelpUrl()
        {
            EditorHelper.OpenHelpUrl();
        }

        [MenuItem(ParentMenu + "Afrikaans")]
        private static void ChangeToAfrikaans()
        {
            SetLanguage(Language.Afrikaans);
        }

        [MenuItem(ParentMenu + "Arabic")]
        private static void ChangeToArabic()
        {
            SetLanguage(Language.Arabic);
        }

        [MenuItem(ParentMenu + "Basque")]
        private static void ChangeToBasque()
        {
            SetLanguage(Language.Basque);
        }

        [MenuItem(ParentMenu + "Belarusian")]
        private static void ChangeToBelarusian()
        {
            SetLanguage(Language.Belarusian);
        }

        [MenuItem(ParentMenu + "Bulgarian")]
        private static void ChangeToBulgarian()
        {
            SetLanguage(Language.Bulgarian);
        }

        [MenuItem(ParentMenu + "Catalan")]
        private static void ChangeToCatalan()
        {
            SetLanguage(Language.Catalan);
        }

        [MenuItem(ParentMenu + "Chinese")]
        private static void ChangeToChinese()
        {
            SetLanguage(Language.Chinese);
        }

        [MenuItem(ParentMenu + "Czech")]
        private static void ChangeToCzech()
        {
            SetLanguage(Language.Czech);
        }

        [MenuItem(ParentMenu + "Danish")]
        private static void ChangeToDanish()
        {
            SetLanguage(Language.Danish);
        }

        [MenuItem(ParentMenu + "Dutch")]
        private static void ChangeToDutch()
        {
            SetLanguage(Language.Dutch);
        }

        [MenuItem(ParentMenu + "English")]
        private static void ChangeToEnglish()
        {
            SetLanguage(Language.English);
        }

        [MenuItem(ParentMenu + "Estonian")]
        private static void ChangeToEstonian()
        {
            SetLanguage(Language.Estonian);
        }

        [MenuItem(ParentMenu + "Faroese")]
        private static void ChangeToFaroese()
        {
            SetLanguage(Language.Faroese);
        }

        [MenuItem(ParentMenu + "Finnish")]
        private static void ChangeToFinnish()
        {
            SetLanguage(Language.Finnish);
        }

        [MenuItem(ParentMenu + "French")]
        private static void ChangeToFrench()
        {
            SetLanguage(Language.French);
        }

        [MenuItem(ParentMenu + "German")]
        private static void ChangeToGerman()
        {
            SetLanguage(Language.German);
        }

        [MenuItem(ParentMenu + "Greek")]
        private static void ChangeToGreek()
        {
            SetLanguage(Language.Greek);
        }

        [MenuItem(ParentMenu + "Hebrew")]
        private static void ChangeToHebrew()
        {
            SetLanguage(Language.Hebrew);
        }

        [MenuItem(ParentMenu + "Hungarian")]
        private static void ChangeToHungarian()
        {
            SetLanguage(Language.Hungarian);
        }

        [MenuItem(ParentMenu + "Hugarian")]
        private static void ChangeToHugarian()
        {
            SetLanguage(Language.Hungarian);
        }

        [MenuItem(ParentMenu + "Icelandic")]
        private static void ChangeToIcelandic()
        {
            SetLanguage(Language.Icelandic);
        }

        [MenuItem(ParentMenu + "Indonesian")]
        private static void ChangeToIndonesian()
        {
            SetLanguage(Language.Indonesian);
        }

        [MenuItem(ParentMenu + "Italian")]
        private static void ChangeToItalian()
        {
            SetLanguage(Language.Italian);
        }

        [MenuItem(ParentMenu + "Japanese")]
        private static void ChangeToJapanese()
        {
            SetLanguage(Language.Japanese);
        }

        [MenuItem(ParentMenu + "Korean")]
        private static void ChangeToKorean()
        {
            SetLanguage(Language.Korean);
        }

        [MenuItem(ParentMenu + "Latvian")]
        private static void ChangeToLatvian()
        {
            SetLanguage(Language.Latvian);
        }

        [MenuItem(ParentMenu + "Lithuanian")]
        private static void ChangeToLithuanian()
        {
            SetLanguage(Language.Lithuanian);
        }

        [MenuItem(ParentMenu + "Norwegian")]
        private static void ChangeToNorwegian()
        {
            SetLanguage(Language.Norwegian);
        }

        [MenuItem(ParentMenu + "Polish")]
        private static void ChangeToPolish()
        {
            SetLanguage(Language.Polish);
        }

        [MenuItem(ParentMenu + "Portuguese")]
        private static void ChangeToPortuguese()
        {
            SetLanguage(Language.Portuguese);
        }

        [MenuItem(ParentMenu + "Romanian")]
        private static void ChangeToRomanian()
        {
            SetLanguage(Language.Romanian);
        }

        [MenuItem(ParentMenu + "Russian")]
        private static void ChangeToRussian()
        {
            SetLanguage(Language.Russian);
        }

        [MenuItem(ParentMenu + "SerboCroatian")]
        private static void ChangeToSerboCroatian()
        {
            SetLanguage(Language.SerboCroatian);
        }

        [MenuItem(ParentMenu + "Slovak")]
        private static void ChangeToSlovak()
        {
            SetLanguage(Language.Slovak);
        }

        [MenuItem(ParentMenu + "Slovenian")]
        private static void ChangeToSlovenian()
        {
            SetLanguage(Language.Slovenian);
        }

        [MenuItem(ParentMenu + "Spanish")]
        private static void ChangeToSpanish()
        {
            SetLanguage(Language.Spanish);
        }

        [MenuItem(ParentMenu + "Swedish")]
        private static void ChangeToSwedish()
        {
            SetLanguage(Language.Swedish);
        }

        [MenuItem(ParentMenu + "Thai")]
        private static void ChangeToThai()
        {
            SetLanguage(Language.Thai);
        }

        [MenuItem(ParentMenu + "Turkish")]
        private static void ChangeToTurkish()
        {
            SetLanguage(Language.Turkish);
        }

        [MenuItem(ParentMenu + "Ukrainian")]
        private static void ChangeToUkrainian()
        {
            SetLanguage(Language.Ukrainian);
        }

        [MenuItem(ParentMenu + "Vietnamese")]
        private static void ChangeToVietnamese()
        {
            SetLanguage(Language.Vietnamese);
        }

        [MenuItem(ParentMenu + "ChineseSimplified")]
        private static void ChangeToChineseSimplified()
        {
            SetLanguage(Language.ChineseSimplified);
        }

        [MenuItem(ParentMenu + "ChineseTraditional")]
        private static void ChangeToChineseTraditional()
        {
            SetLanguage(Language.ChineseTraditional);
        }

        [MenuItem(ParentMenu + "Unknown")]
        private static void ChangeToUnknown()
        {
            SetLanguage(Language.Unknown);
        }

        private static void SetLanguage(Language currentLanguage)
        {
            if (!Application.isPlaying)
            {
                Debug.LogWarning("Setting language only available when application is playing.");
                return;
            }
            
            var previousLanguage = Localization.Instance.CurrentLanguage;
            Localization.Instance.CurrentLanguage = currentLanguage;
            
            Menu.SetChecked(GetMenuName(previousLanguage), false);
            Menu.SetChecked(GetMenuName(currentLanguage), true);
        }

        private static string GetMenuName(Language language)
        {
            return ParentMenu + language;
        }
    }
}
