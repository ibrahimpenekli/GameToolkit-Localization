// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
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
            m_AvailableLanguages = serializedObject.FindProperty("m_AvailableLanguages");
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
                    EditorGUI.LabelField(rect, m_AvailableLanguages.displayName);
                };
                m_AvailableLanguagesList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = m_AvailableLanguagesList.serializedProperty.GetArrayElementAtIndex(index);
                    var position = new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight);
                    EditorGUI.PropertyField(position, element, GUIContent.none);
                };
                m_AvailableLanguagesList.onCanRemoveCallback = (ReorderableList list) =>
                {
                    return list.count > 1;
                };
                m_AvailableLanguagesList.onAddDropdownCallback = (Rect buttonRect, ReorderableList list) =>
                {
                    var menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Language", "Adds a language."), false, () =>
                    {
                        ReorderableList.defaultBehaviours.DoAddButton(list);
                        serializedObject.ApplyModifiedProperties();
                    });
                    menu.AddItem(new GUIContent("Add used languages", "Adds by searching used languages in assets."), false, () =>
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

            var localizedAssets = Localization.Instance.FindAllLocalizedAssets();
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
