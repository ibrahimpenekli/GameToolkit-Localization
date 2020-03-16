// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameToolkit.Localization.Editor
{
    internal static class EditorHelper
    {
        private const string HelpUrl = "https://hibrahimpenekli.gitbook.io/gametoolkit-localization";
        public const string LocalizationMenu = "Window/GameToolkit/Localization/";
        public const string LocalizedElementsSerializedProperty = "m_LocaleItems";
        
        private static GUIContent[] m_Contents;
        
        public static void OpenHelpUrl()
        {
            Application.OpenURL(HelpUrl);
        }

        public static void LanguageField(Rect position, SerializedProperty property, GUIContent label,
            bool showOnlyBuiltin = false)
        {
            var languages = new List<Language>();
            languages.AddRange(Language.BuiltinLanguages);
            
            if (!showOnlyBuiltin)
            {
                languages.AddRange(GetCustomLanguages());
            }

            if (m_Contents == null || m_Contents.Length != languages.Count)
            {
                m_Contents = new GUIContent[languages.Count];
            }
            
            for (var i = 0; i < languages.Count; i++)
            {
                m_Contents[i] = new GUIContent(languages[i].Name);
            }

            EditorGUI.BeginProperty(position, label, property);

            var languageName = property.FindPropertyRelative("m_Name");
            var languageCode = property.FindPropertyRelative("m_Code");

            var currentValue = languages.FindIndex(x => x.Code == languageCode.stringValue);
            if (currentValue < 0)
            {
                currentValue = languages.FindIndex(x => x == Language.Unknown);
                Debug.Assert(currentValue >= 0);
            }
            
            var newValue = EditorGUI.Popup(position, label, currentValue, m_Contents);
            if (newValue != currentValue)
            {
                languageName.stringValue = languages[newValue].Name;
                languageCode.stringValue = languages[newValue].Code;
                property.serializedObject.ApplyModifiedProperties();
            }
            
            EditorGUI.EndProperty();
        }

        private static Language[] GetCustomLanguages()
        {
            var localizationSettings = LocalizationSettings.Instance;
            if (localizationSettings != null)
            {
                var customLanguages 
                    = localizationSettings.AvailableLanguages.Where(x => x.Custom);
                return customLanguages.ToArray();
            }

            return new Language[0];
        }
    }
}