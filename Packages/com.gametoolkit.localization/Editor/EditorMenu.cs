// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEditor;

namespace GameToolkit.Localization.Editor
{
    /// <summary>
    /// Unity Editor menu for changing localization under "Tools/Localization".
    /// </summary>
    public static class EditorMenu
    {
        public const string RootMenu = "Window/GameToolkit/Localization/";
        
        private const string SetLocaleRootMenu = RootMenu + "Set Locale/";

        [MenuItem(RootMenu + "Import .csv", false, 1)]
        private static void Import()
        {
            EditorSerialization.Import();
        }
        
        [MenuItem(RootMenu + "Export .csv", false, 2)]
        private static void Export()
        {
            EditorSerialization.Export();
        }
        
        [MenuItem(RootMenu + "Help", false, 3)]
        private static void OpenHelpUrl()
        {
            Application.OpenURL(Localization.HelpUrl);
        }

        [MenuItem(SetLocaleRootMenu + "Afrikaans")]
        private static void ChangeToAfrikaans()
        {
            SetLanguage(Language.Afrikaans);
        }

        [MenuItem(SetLocaleRootMenu + "Arabic")]
        private static void ChangeToArabic()
        {
            SetLanguage(Language.Arabic);
        }

        [MenuItem(SetLocaleRootMenu + "Basque")]
        private static void ChangeToBasque()
        {
            SetLanguage(Language.Basque);
        }

        [MenuItem(SetLocaleRootMenu + "Belarusian")]
        private static void ChangeToBelarusian()
        {
            SetLanguage(Language.Belarusian);
        }

        [MenuItem(SetLocaleRootMenu + "Bulgarian")]
        private static void ChangeToBulgarian()
        {
            SetLanguage(Language.Bulgarian);
        }

        [MenuItem(SetLocaleRootMenu + "Catalan")]
        private static void ChangeToCatalan()
        {
            SetLanguage(Language.Catalan);
        }

        [MenuItem(SetLocaleRootMenu + "Chinese")]
        private static void ChangeToChinese()
        {
            SetLanguage(Language.Chinese);
        }

        [MenuItem(SetLocaleRootMenu + "Czech")]
        private static void ChangeToCzech()
        {
            SetLanguage(Language.Czech);
        }

        [MenuItem(SetLocaleRootMenu + "Danish")]
        private static void ChangeToDanish()
        {
            SetLanguage(Language.Danish);
        }

        [MenuItem(SetLocaleRootMenu + "Dutch")]
        private static void ChangeToDutch()
        {
            SetLanguage(Language.Dutch);
        }

        [MenuItem(SetLocaleRootMenu + "English")]
        private static void ChangeToEnglish()
        {
            SetLanguage(Language.English);
        }

        [MenuItem(SetLocaleRootMenu + "Estonian")]
        private static void ChangeToEstonian()
        {
            SetLanguage(Language.Estonian);
        }

        [MenuItem(SetLocaleRootMenu + "Faroese")]
        private static void ChangeToFaroese()
        {
            SetLanguage(Language.Faroese);
        }

        [MenuItem(SetLocaleRootMenu + "Finnish")]
        private static void ChangeToFinnish()
        {
            SetLanguage(Language.Finnish);
        }

        [MenuItem(SetLocaleRootMenu + "French")]
        private static void ChangeToFrench()
        {
            SetLanguage(Language.French);
        }

        [MenuItem(SetLocaleRootMenu + "German")]
        private static void ChangeToGerman()
        {
            SetLanguage(Language.German);
        }

        [MenuItem(SetLocaleRootMenu + "Greek")]
        private static void ChangeToGreek()
        {
            SetLanguage(Language.Greek);
        }

        [MenuItem(SetLocaleRootMenu + "Hebrew")]
        private static void ChangeToHebrew()
        {
            SetLanguage(Language.Hebrew);
        }

        [MenuItem(SetLocaleRootMenu + "Hungarian")]
        private static void ChangeToHungarian()
        {
            SetLanguage(Language.Hungarian);
        }

        [MenuItem(SetLocaleRootMenu + "Hugarian")]
        private static void ChangeToHugarian()
        {
            SetLanguage(Language.Hungarian);
        }

        [MenuItem(SetLocaleRootMenu + "Icelandic")]
        private static void ChangeToIcelandic()
        {
            SetLanguage(Language.Icelandic);
        }

        [MenuItem(SetLocaleRootMenu + "Indonesian")]
        private static void ChangeToIndonesian()
        {
            SetLanguage(Language.Indonesian);
        }

        [MenuItem(SetLocaleRootMenu + "Italian")]
        private static void ChangeToItalian()
        {
            SetLanguage(Language.Italian);
        }

        [MenuItem(SetLocaleRootMenu + "Japanese")]
        private static void ChangeToJapanese()
        {
            SetLanguage(Language.Japanese);
        }

        [MenuItem(SetLocaleRootMenu + "Korean")]
        private static void ChangeToKorean()
        {
            SetLanguage(Language.Korean);
        }

        [MenuItem(SetLocaleRootMenu + "Latvian")]
        private static void ChangeToLatvian()
        {
            SetLanguage(Language.Latvian);
        }

        [MenuItem(SetLocaleRootMenu + "Lithuanian")]
        private static void ChangeToLithuanian()
        {
            SetLanguage(Language.Lithuanian);
        }

        [MenuItem(SetLocaleRootMenu + "Norwegian")]
        private static void ChangeToNorwegian()
        {
            SetLanguage(Language.Norwegian);
        }

        [MenuItem(SetLocaleRootMenu + "Polish")]
        private static void ChangeToPolish()
        {
            SetLanguage(Language.Polish);
        }

        [MenuItem(SetLocaleRootMenu + "Portuguese")]
        private static void ChangeToPortuguese()
        {
            SetLanguage(Language.Portuguese);
        }

        [MenuItem(SetLocaleRootMenu + "Romanian")]
        private static void ChangeToRomanian()
        {
            SetLanguage(Language.Romanian);
        }

        [MenuItem(SetLocaleRootMenu + "Russian")]
        private static void ChangeToRussian()
        {
            SetLanguage(Language.Russian);
        }

        [MenuItem(SetLocaleRootMenu + "SerboCroatian")]
        private static void ChangeToSerboCroatian()
        {
            SetLanguage(Language.SerboCroatian);
        }

        [MenuItem(SetLocaleRootMenu + "Slovak")]
        private static void ChangeToSlovak()
        {
            SetLanguage(Language.Slovak);
        }

        [MenuItem(SetLocaleRootMenu + "Slovenian")]
        private static void ChangeToSlovenian()
        {
            SetLanguage(Language.Slovenian);
        }

        [MenuItem(SetLocaleRootMenu + "Spanish")]
        private static void ChangeToSpanish()
        {
            SetLanguage(Language.Spanish);
        }

        [MenuItem(SetLocaleRootMenu + "Swedish")]
        private static void ChangeToSwedish()
        {
            SetLanguage(Language.Swedish);
        }

        [MenuItem(SetLocaleRootMenu + "Thai")]
        private static void ChangeToThai()
        {
            SetLanguage(Language.Thai);
        }

        [MenuItem(SetLocaleRootMenu + "Turkish")]
        private static void ChangeToTurkish()
        {
            SetLanguage(Language.Turkish);
        }

        [MenuItem(SetLocaleRootMenu + "Ukrainian")]
        private static void ChangeToUkrainian()
        {
            SetLanguage(Language.Ukrainian);
        }

        [MenuItem(SetLocaleRootMenu + "Vietnamese")]
        private static void ChangeToVietnamese()
        {
            SetLanguage(Language.Vietnamese);
        }

        [MenuItem(SetLocaleRootMenu + "ChineseSimplified")]
        private static void ChangeToChineseSimplified()
        {
            SetLanguage(Language.ChineseSimplified);
        }

        [MenuItem(SetLocaleRootMenu + "ChineseTraditional")]
        private static void ChangeToChineseTraditional()
        {
            SetLanguage(Language.ChineseTraditional);
        }

        [MenuItem(SetLocaleRootMenu + "Unknown")]
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
            return SetLocaleRootMenu + language;
        }
    }
}
