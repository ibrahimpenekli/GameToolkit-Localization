// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;

namespace GameToolkit.Localization.Editor
{
    [CustomEditor(typeof(LocalizationSettings), editorForChildClasses: true)]
    public class LocalizationSettingsEditor : UnityEditor.Editor
    {
        private ReorderableList m_AvailableLanguagesList;
        private SerializedProperty m_AvailableLanguages;
        private SerializedProperty m_GoogleAuthenticationFile;

        private void OnEnable()
        {
            m_AvailableLanguages = serializedObject.FindProperty("m_AvailableLanguages2");
            m_GoogleAuthenticationFile = serializedObject.FindProperty("GoogleAuthenticationFile");
            if (m_AvailableLanguages != null)
            {
                m_AvailableLanguagesList = new ReorderableList
                (
                    serializedObject: serializedObject,
                    elements: m_AvailableLanguages,
                    draggable: true,
                    displayHeader: true,
                    displayAddButton: true,
                    displayRemoveButton: true
                );
                m_AvailableLanguagesList.drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, "Available Languages");
                };
                m_AvailableLanguagesList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var languageProperty = m_AvailableLanguagesList.serializedProperty.GetArrayElementAtIndex(index);
                    var position = new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight);

                    var isCustom = languageProperty.FindLanguageCustomProperty().boolValue;
                    if (isCustom)
                    {
                        var languageName = languageProperty.FindLanguageNameProperty();
                        var languageCode = languageProperty.FindLanguageCodeProperty();

                        var labelWidth = EditorGUIUtility.labelWidth;
                        
                        EditorGUIUtility.labelWidth = 40;
                        var r1 = new Rect(position.x, position.y, position.width / 2 - 2, position.height);
                        EditorGUI.PropertyField(r1, languageName, new GUIContent(languageName.displayName));
                        
                        EditorGUIUtility.labelWidth = 40;
                        var r2 = new Rect(position.x + r1.width + 4, position.y, position.width / 2 - 2, position.height);
                        EditorGUI.PropertyField(r2, languageCode, new GUIContent(languageCode.displayName));
                        
                        EditorGUIUtility.labelWidth = labelWidth;
                    }
                    else
                    {
                        EditorHelper.LanguageField(position, languageProperty, GUIContent.none, true);
                    }
                };
                m_AvailableLanguagesList.onCanRemoveCallback = list => list.count > 1;
                m_AvailableLanguagesList.onAddDropdownCallback = (buttonRect, list) =>
                {
                    var menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Language", "Adds built-in language."), false, () =>
                    {
                        ReorderableList.defaultBehaviours.DoAddButton(list);
                        
                        var languageProperty = list.serializedProperty.GetArrayElementAtIndex(list.index);
                        EditorHelper.SetLanguageProperty(languageProperty, Language.BuiltinLanguages[0]);

                        serializedObject.ApplyModifiedProperties();
                    });
                    menu.AddItem(new GUIContent("Custom language", "Adds custom language."), false, () =>
                    {
                        ReorderableList.defaultBehaviours.DoAddButton(list);

                        var languageProperty = list.serializedProperty.GetArrayElementAtIndex(list.index);
                        EditorHelper.SetLanguageProperty(languageProperty, "", "", true);

                        serializedObject.ApplyModifiedProperties();
                    });
                    menu.AddItem(new GUIContent("Adds languages in-use", "Adds by searching used languages in assets."), false, () =>
                    {
                        AddUsedLocales();
                        serializedObject.ApplyModifiedProperties();
                    });
                    menu.ShowAsContext();
                };
            }
        }

        public override void OnInspectorGUI()
        {
            if (m_AvailableLanguagesList != null)
            {
                serializedObject.Update();
                m_AvailableLanguagesList.DoLayoutList();

                EditorGUILayout.LabelField("Google Translate", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(m_GoogleAuthenticationFile);
                if (!m_GoogleAuthenticationFile.objectReferenceValue)
                {
                    EditorGUILayout.HelpBox("If you want to use Google Translate in editor or in-game, attach the service account or API key file claimed from Google Cloud.", MessageType.Info);
                }
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                base.OnInspectorGUI();
            }
        }

        private void AddUsedLocales()
        {
            var languages = FindUsedLanguages();
            m_AvailableLanguages.arraySize = languages.Length;
            for (var i = 0; i < m_AvailableLanguages.arraySize; i++)
            {
                var languageProperty = m_AvailableLanguages.GetArrayElementAtIndex(i);
                EditorHelper.SetLanguageProperty(languageProperty, languages[i]);
            }
        }

        private Language[] FindUsedLanguages()
        {
            var languages = new HashSet<Language>();
            for (var i = 0; i < m_AvailableLanguages.arraySize; i++)
            {
                languages.Add(
                    EditorHelper.GetLanguageValueFromProperty(m_AvailableLanguages.GetArrayElementAtIndex(i)));
            }

            var localizedAssets = Localization.FindAllLocalizedAssets();
            foreach (var localizedAsset in localizedAssets)
            {
                foreach (var locale in localizedAsset.LocaleItems)
                {
                    languages.Add(locale.Language);
                }
            }
            
            return languages.ToArray();
        }
    }
}
