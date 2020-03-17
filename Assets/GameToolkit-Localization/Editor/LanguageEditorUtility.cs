// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameToolkit.Localization.Editor
{
    internal static class LanguageEditorUtility
    {
        private static GUIContent[] m_Contents;

        public static void SetLanguageProperty(SerializedProperty languageProperty, Language language)
        {
            SetLanguageProperty(languageProperty, language.Name, language.Code, language.Custom);
        }
        
        public static void SetLanguageProperty(SerializedProperty languageProperty, string name, string code,
            bool custom)
        {
            var nameProperty = languageProperty.FindLanguageNameProperty();
            if (nameProperty == null)
            {
                throw new ArgumentException("Language.Name property could not be found");
            }
            
            var codeProperty = languageProperty.FindLanguageCodeProperty();
            if (codeProperty == null)
            {
                throw new ArgumentException("Language.Code property could not be found");
            }
            
            var customProperty = languageProperty.FindLanguageCustomProperty();
            if (customProperty == null)
            {
                throw new ArgumentException("Language.Custom property could not be found");
            }

            nameProperty.stringValue = name;
            codeProperty.stringValue = code;
            customProperty.boolValue = custom;
        }

        public static Language GetLanguageValueFromProperty(SerializedProperty languageProperty)
        {
            var nameProperty = languageProperty.FindLanguageNameProperty();
            if (nameProperty == null)
            {
                throw new ArgumentException("Language.Name property could not be found");
            }
            
            var codeProperty = languageProperty.FindLanguageCodeProperty();
            if (codeProperty == null)
            {
                throw new ArgumentException("Language.Code property could not be found");
            }
            
            var customProperty = languageProperty.FindLanguageCustomProperty();
            if (customProperty == null)
            {
                throw new ArgumentException("Language.Custom property could not be found");
            }
            
            return new Language(nameProperty.stringValue, codeProperty.stringValue, customProperty.boolValue);
        }

        /// <see cref="Language.Name"/>
        public static SerializedProperty FindLanguageNameProperty(this SerializedProperty languageProperty)
        {
            return languageProperty.FindPropertyRelative("m_Name");
        }
        
        /// <see cref="Language.Code"/>
        public static SerializedProperty FindLanguageCodeProperty(this SerializedProperty languageProperty)
        {
            return languageProperty.FindPropertyRelative("m_Code");
        }
        
        /// <see cref="Language.Custom"/>
        public static SerializedProperty FindLanguageCustomProperty(this SerializedProperty languageProperty)
        {
            return languageProperty.FindPropertyRelative("m_Custom");
        }

        public static Language LanguageField(Rect position, Language language, bool showOnlyBuiltin = false)
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

            var languageName = language.Name;
            var languageCode = language.Code;

            var currentValue = languages.FindIndex(x => x.Code == languageCode);
            if (currentValue < 0)
            {
                currentValue = languages.FindIndex(x => x == Language.Unknown);
                Debug.Assert(currentValue >= 0);
            }
            
            var newValue = EditorGUI.Popup(position, currentValue, m_Contents);
            if (newValue != currentValue)
            {
                return languages[newValue];
            }

            return language;
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

            var languageName = property.FindLanguageNameProperty();
            var languageCode = property.FindLanguageCodeProperty();

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