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

        [MenuItem(LocalizationEditorHelper.LocalizationMenu + "Help", false, 1)]
        private static void OpenHelpUrl()
        {
            LocalizationEditorHelper.OpenHelpUrl();
        }
        
        [MenuItem(ParentMenu + "Afrikaans")]
        private static void ChangeToAfrikaans()
        {
            SetLanguage(SystemLanguage.Afrikaans);
        }

        [MenuItem(ParentMenu + "Arabic")]
        private static void ChangeToArabic()
        {
            SetLanguage(SystemLanguage.Arabic);
        }

        [MenuItem(ParentMenu + "Basque")]
        private static void ChangeToBasque()
        {
            SetLanguage(SystemLanguage.Basque);
        }

        [MenuItem(ParentMenu + "Belarusian")]
        private static void ChangeToBelarusian()
        {
            SetLanguage(SystemLanguage.Belarusian);
        }

        [MenuItem(ParentMenu + "Bulgarian")]
        private static void ChangeToBulgarian()
        {
            SetLanguage(SystemLanguage.Bulgarian);
        }

        [MenuItem(ParentMenu + "Catalan")]
        private static void ChangeToCatalan()
        {
            SetLanguage(SystemLanguage.Catalan);
        }

        [MenuItem(ParentMenu + "Chinese")]
        private static void ChangeToChinese()
        {
            SetLanguage(SystemLanguage.Chinese);
        }

        [MenuItem(ParentMenu + "Czech")]
        private static void ChangeToCzech()
        {
            SetLanguage(SystemLanguage.Czech);
        }

        [MenuItem(ParentMenu + "Danish")]
        private static void ChangeToDanish()
        {
            SetLanguage(SystemLanguage.Danish);
        }

        [MenuItem(ParentMenu + "Dutch")]
        private static void ChangeToDutch()
        {
            SetLanguage(SystemLanguage.Dutch);
        }

        [MenuItem(ParentMenu + "English")]
        private static void ChangeToEnglish()
        {
            SetLanguage(SystemLanguage.English);
        }

        [MenuItem(ParentMenu + "Estonian")]
        private static void ChangeToEstonian()
        {
            SetLanguage(SystemLanguage.Estonian);
        }

        [MenuItem(ParentMenu + "Faroese")]
        private static void ChangeToFaroese()
        {
            SetLanguage(SystemLanguage.Faroese);
        }

        [MenuItem(ParentMenu + "Finnish")]
        private static void ChangeToFinnish()
        {
            SetLanguage(SystemLanguage.Finnish);
        }

        [MenuItem(ParentMenu + "French")]
        private static void ChangeToFrench()
        {
            SetLanguage(SystemLanguage.French);
        }

        [MenuItem(ParentMenu + "German")]
        private static void ChangeToGerman()
        {
            SetLanguage(SystemLanguage.German);
        }

        [MenuItem(ParentMenu + "Greek")]
        private static void ChangeToGreek()
        {
            SetLanguage(SystemLanguage.Greek);
        }

        [MenuItem(ParentMenu + "Hebrew")]
        private static void ChangeToHebrew()
        {
            SetLanguage(SystemLanguage.Hebrew);
        }

        [MenuItem(ParentMenu + "Hungarian")]
        private static void ChangeToHungarian()
        {
            SetLanguage(SystemLanguage.Hungarian);
        }

        [MenuItem(ParentMenu + "Hugarian")]
        private static void ChangeToHugarian()
        {
            SetLanguage(SystemLanguage.Hungarian);
        }

        [MenuItem(ParentMenu + "Icelandic")]
        private static void ChangeToIcelandic()
        {
            SetLanguage(SystemLanguage.Icelandic);
        }

        [MenuItem(ParentMenu + "Indonesian")]
        private static void ChangeToIndonesian()
        {
            SetLanguage(SystemLanguage.Indonesian);
        }

        [MenuItem(ParentMenu + "Italian")]
        private static void ChangeToItalian()
        {
            SetLanguage(SystemLanguage.Italian);
        }

        [MenuItem(ParentMenu + "Japanese")]
        private static void ChangeToJapanese()
        {
            SetLanguage(SystemLanguage.Japanese);
        }

        [MenuItem(ParentMenu + "Korean")]
        private static void ChangeToKorean()
        {
            SetLanguage(SystemLanguage.Korean);
        }

        [MenuItem(ParentMenu + "Latvian")]
        private static void ChangeToLatvian()
        {
            SetLanguage(SystemLanguage.Latvian);
        }

        [MenuItem(ParentMenu + "Lithuanian")]
        private static void ChangeToLithuanian()
        {
            SetLanguage(SystemLanguage.Lithuanian);
        }

        [MenuItem(ParentMenu + "Norwegian")]
        private static void ChangeToNorwegian()
        {
            SetLanguage(SystemLanguage.Norwegian);
        }

        [MenuItem(ParentMenu + "Polish")]
        private static void ChangeToPolish()
        {
            SetLanguage(SystemLanguage.Polish);
        }

        [MenuItem(ParentMenu + "Portuguese")]
        private static void ChangeToPortuguese()
        {
            SetLanguage(SystemLanguage.Portuguese);
        }

        [MenuItem(ParentMenu + "Romanian")]
        private static void ChangeToRomanian()
        {
            SetLanguage(SystemLanguage.Romanian);
        }

        [MenuItem(ParentMenu + "Russian")]
        private static void ChangeToRussian()
        {
            SetLanguage(SystemLanguage.Russian);
        }

        [MenuItem(ParentMenu + "SerboCroatian")]
        private static void ChangeToSerboCroatian()
        {
            SetLanguage(SystemLanguage.SerboCroatian);
        }

        [MenuItem(ParentMenu + "Slovak")]
        private static void ChangeToSlovak()
        {
            SetLanguage(SystemLanguage.Slovak);
        }

        [MenuItem(ParentMenu + "Slovenian")]
        private static void ChangeToSlovenian()
        {
            SetLanguage(SystemLanguage.Slovenian);
        }

        [MenuItem(ParentMenu + "Spanish")]
        private static void ChangeToSpanish()
        {
            SetLanguage(SystemLanguage.Spanish);
        }

        [MenuItem(ParentMenu + "Swedish")]
        private static void ChangeToSwedish()
        {
            SetLanguage(SystemLanguage.Swedish);
        }

        [MenuItem(ParentMenu + "Thai")]
        private static void ChangeToThai()
        {
            SetLanguage(SystemLanguage.Thai);
        }

        [MenuItem(ParentMenu + "Turkish")]
        private static void ChangeToTurkish()
        {
            SetLanguage(SystemLanguage.Turkish);
        }

        [MenuItem(ParentMenu + "Ukrainian")]
        private static void ChangeToUkrainian()
        {
            SetLanguage(SystemLanguage.Ukrainian);
        }

        [MenuItem(ParentMenu + "Vietnamese")]
        private static void ChangeToVietnamese()
        {
            SetLanguage(SystemLanguage.Vietnamese);
        }

        [MenuItem(ParentMenu + "ChineseSimplified")]
        private static void ChangeToChineseSimplified()
        {
            SetLanguage(SystemLanguage.ChineseSimplified);
        }

        [MenuItem(ParentMenu + "ChineseTraditional")]
        private static void ChangeToChineseTraditional()
        {
            SetLanguage(SystemLanguage.ChineseTraditional);
        }

        [MenuItem(ParentMenu + "Unknown")]
        private static void ChangeToUnknown()
        {
            SetLanguage(SystemLanguage.Unknown);
        }

        private static void SetLanguage(SystemLanguage currentLanguage)
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

        private static string GetMenuName(SystemLanguage language)
        {
            return ParentMenu + language;
        }
    }
}
