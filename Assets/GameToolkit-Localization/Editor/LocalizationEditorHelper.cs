// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace GameToolkit.Localization.Editor
{
    public static class LocalizationEditorHelper
    {
        private const string HelpUrl = "https://hibrahimpenekli.gitbook.io/gametoolkit-localization";
        public const string LocalizationMenu = "Window/GameToolkit/Localization/";
        public const string LocalizedElementsSerializedProperty = "m_LocaleItems";
        public const string LocaleLanguageSerializedProperty = "m_Language";
        public const string LocaleValueSerializedProperty = "m_Value";

        public static void OpenHelpUrl()
        {
            Application.OpenURL(HelpUrl);
        }
    }
}