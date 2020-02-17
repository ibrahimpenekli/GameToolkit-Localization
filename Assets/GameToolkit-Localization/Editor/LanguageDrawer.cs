// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameToolkit.Localization.Editor
{
    [CustomPropertyDrawer(typeof(Language))]
    public class LanguageDrawer : PropertyDrawer
    {
        private static readonly List<Language> BuiltInLanguages = new List<Language>
        {
            Language.Afrikaans,
            Language.Arabic,
            Language.Basque,
            Language.Belarusian,
            Language.Bulgarian,
            Language.Catalan,
            Language.Chinese,
            Language.Czech,
            Language.Danish,
            Language.Dutch,
            Language.English,
            Language.Estonian,
            Language.Faroese,
            Language.Finnish,
            Language.French,
            Language.German,
            Language.Greek,
            Language.Hebrew,
            Language.Hungarian,
            Language.Icelandic,
            Language.Indonesian,
            Language.Italian,
            Language.Japanese,
            Language.Korean,
            Language.Latvian,
            Language.Lithuanian,
            Language.Norwegian,
            Language.Polish,
            Language.Portuguese,
            Language.Romanian,
            Language.Russian,
            Language.SerboCroatian,
            Language.Slovak,
            Language.Slovenian,
            Language.Spanish,
            Language.Swedish,
            Language.Thai,
            Language.Turkish,
            Language.Ukrainian,
            Language.Vietnamese,
            Language.ChineseSimplified,
            Language.ChineseTraditional,
            Language.Unknown
        };

        private List<Language> m_Languages;
        private GUIContent[] m_Contents;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (m_Languages == null)
            {
                m_Languages = new List<Language>();
                m_Languages.AddRange(BuiltInLanguages);
                
                var localizationSettings = LocalizationSettings.Instance;
                if (localizationSettings != null)
                {
                    var customLanguages 
                        = localizationSettings.AvailableLanguages.Where(x => x.Custom);
                    m_Languages.AddRange(customLanguages);
                }
            }
            
            if (m_Contents == null || m_Contents.Length != m_Languages.Count)
            {
                m_Contents = m_Languages.Select(x => new GUIContent(x.Name)).ToArray();
            }

            EditorGUI.BeginProperty(position, label, property);

            var languageName = property.FindPropertyRelative("m_Name");
            var languageCode = property.FindPropertyRelative("m_Code");

            var currentValue = m_Languages.FindIndex(x => x.Code == languageCode.stringValue);
            currentValue = currentValue >= 0 ? currentValue : BuiltInLanguages.Count - 1;
            
            var newValue = EditorGUI.Popup(position, label, currentValue, m_Contents);
            if (newValue != currentValue)
            {
                languageName.stringValue = m_Languages[newValue].Name;
                languageCode.stringValue = m_Languages[newValue].Code;
                property.serializedObject.ApplyModifiedProperties();
            }
            
            EditorGUI.EndProperty();
        }
    }
}