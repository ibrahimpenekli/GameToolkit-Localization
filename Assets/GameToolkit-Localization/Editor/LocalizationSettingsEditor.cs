// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;

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
                    var element = m_AvailableLanguagesList.serializedProperty.GetArrayElementAtIndex(index);
                    var position = new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight);

                    var isCustom = element.FindPropertyRelative("m_Custom").boolValue;
                    if (isCustom)
                    {
                        var languageName = element.FindPropertyRelative("m_Name");
                        var languageCode = element.FindPropertyRelative("m_Code");

                        var labelWidth = EditorGUIUtility.labelWidth;
                        
                        EditorGUIUtility.labelWidth = 40;
                        var r1 = new Rect(position.x, position.y, position.width / 2 - 2, position.height);
                        EditorGUI.PropertyField(r1, languageName, new GUIContent("Name"));
                        
                        EditorGUIUtility.labelWidth = 40;
                        var r2 = new Rect(position.x + r1.width + 4, position.y, position.width / 2 - 2, position.height);
                        EditorGUI.PropertyField(r2, languageCode, new GUIContent("Code"));
                        
                        EditorGUIUtility.labelWidth = labelWidth;
                    }
                    else
                    {
                        EditorGUI.PropertyField(position, element, GUIContent.none);
                    }
                };
                m_AvailableLanguagesList.onCanRemoveCallback = (ReorderableList list) =>
                {
                    return list.count > 1;
                };
                m_AvailableLanguagesList.onAddDropdownCallback = (Rect buttonRect, ReorderableList list) =>
                {
                    var menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Language", "Adds built-in language."), false, () =>
                    {
                        ReorderableList.defaultBehaviours.DoAddButton(list);
                        
                        var element = list.serializedProperty.GetArrayElementAtIndex(list.index);
                        element.FindPropertyRelative("m_Name").stringValue = Language.Afrikaans.Name;
                        element.FindPropertyRelative("m_Code").stringValue = Language.Afrikaans.Code;
                        element.FindPropertyRelative("m_Custom").boolValue = false;
                        
                        serializedObject.ApplyModifiedProperties();
                    });
                    menu.AddItem(new GUIContent("Custom language", "Adds custom language."), false, () =>
                    {
                        ReorderableList.defaultBehaviours.DoAddButton(list);

                        var element = list.serializedProperty.GetArrayElementAtIndex(list.index);
                        element.FindPropertyRelative("m_Name").stringValue = "";
                        element.FindPropertyRelative("m_Code").stringValue = "";
                        element.FindPropertyRelative("m_Custom").boolValue = true;
                        
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
            var enumNames = Enum.GetNames(typeof(SystemLanguage));
            var languages = FindUsedLanguages(enumNames);
            m_AvailableLanguages.arraySize = languages.Count;
            var size = m_AvailableLanguages.arraySize;
            for (int i = 0; i < size; i++)
            {
                var enumValueIndex = Array.IndexOf(enumNames, languages[i].ToString());
                m_AvailableLanguages.GetArrayElementAtIndex(i).enumValueIndex = enumValueIndex;
            }
        }

        private List<SystemLanguage> FindUsedLanguages(string[] enumNames)
        {
            var languages = new HashSet<SystemLanguage>();
            for (int i = 0; i < m_AvailableLanguages.arraySize; i++)
            {
                var enumName = enumNames[m_AvailableLanguages.GetArrayElementAtIndex(i).enumValueIndex];
                languages.Add((SystemLanguage)Enum.Parse(typeof(SystemLanguage), enumName));
            }

            var localizedAssets = Localization.FindAllLocalizedAssets();
            foreach (var localizedAsset in localizedAssets)
            {
                foreach (var locale in localizedAsset.LocaleItems)
                {
                    languages.Add(locale.Language);
                }
            }
            return new List<SystemLanguage>(languages);
        }
    }
}
