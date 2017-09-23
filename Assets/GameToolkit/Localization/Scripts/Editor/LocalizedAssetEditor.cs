// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System;

namespace GameToolkit.Localization.Editor
{
    [CustomEditor(typeof(LocalizedAssetBase), editorForChildClasses: true)]
    public class LocalizedAssetEditor : UnityEditor.Editor
    {
        private const float LanguageFieldWidth = 100;

        private ReorderableList m_LocaleItemList;
        private SerializedProperty m_LocaleItems;

        private void OnEnable()
        {
            m_LocaleItems = serializedObject.FindProperty(LocalizationEditorHelper.LocalizedElementsSerializedProperty);
            if (m_LocaleItems != null)
            {
                m_LocaleItemList = new ReorderableList
                (
                    serializedObject: serializedObject,
                    elements: m_LocaleItems,
                    draggable: true,
                    displayHeader: true,
                    displayAddButton: true,
                    displayRemoveButton: true
                );

                m_LocaleItemList.drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, ObjectNames.NicifyVariableName(target.GetType().Name) + "s");
                };

                m_LocaleItemList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = m_LocaleItemList.serializedProperty.GetArrayElementAtIndex(index);

                    // Language field.
                    var r1 = new Rect(rect.x, rect.y + 2, LanguageFieldWidth, EditorGUIUtility.singleLineHeight);
                    EditorGUI.PropertyField(r1, element.FindPropertyRelative(LocalizationEditorHelper.LocaleLanguageSerializedProperty), GUIContent.none);

                    // Value field.
                    var r2 = new Rect(r1.x + r1.width + 4, r1.y, rect.width - r1.width - 4, r1.height);
                    EditorGUI.PropertyField(r2, element.FindPropertyRelative(LocalizationEditorHelper.LocaleValueSerializedProperty), GUIContent.none);
                };
                m_LocaleItemList.onCanRemoveCallback = (ReorderableList list) =>
                {
                    return list.count > 1;
                };
                m_LocaleItemList.onAddDropdownCallback = (Rect buttonRect, ReorderableList list) =>
                {
                    var menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Language", "Adds a language."), false, () =>
                    {
                        ReorderableList.defaultBehaviours.DoAddButton(list);
                        serializedObject.ApplyModifiedProperties();
                    });
                    menu.AddItem(new GUIContent("Add languages from settings", "Adds by searching used languages in assets."), false, () =>
                    {
                        AddLanguagesFromSettings();
                        serializedObject.ApplyModifiedProperties();
                    });
                    menu.AddItem(new GUIContent("Add all languages", "Adds all languages."), false, () =>
                    {
                        AddAllLanguages();
                        serializedObject.ApplyModifiedProperties();
                    });
                    menu.ShowAsContext();
                };
            }
        }

        public override void OnInspectorGUI()
        {
            if (m_LocaleItemList != null)
            {
                serializedObject.Update();
                m_LocaleItemList.DoLayoutList();
                serializedObject.ApplyModifiedProperties();

                EditorGUILayout.HelpBox("First locale item is used as fallback if needed.", MessageType.Info);
            }
            else
            {
                base.OnInspectorGUI();
            }
        }

        private void AddAllLanguages()
        {
            var enumNames = Enum.GetNames(typeof(SystemLanguage));
            m_LocaleItems.arraySize = enumNames.Length;
            for (int i = 0; i < m_LocaleItems.arraySize; i++)
            {
                var element = m_LocaleItems.GetArrayElementAtIndex(i);
                var languageProperty = element.FindPropertyRelative(LocalizationEditorHelper.LocaleLanguageSerializedProperty);
                languageProperty.enumValueIndex = i;
            }
        }

        private void AddLanguagesFromSettings()
        {
            var enumNames = Enum.GetNames(typeof(SystemLanguage));
            var uniqueLanguages = new HashSet<SystemLanguage>();
            for (int i = 0; i < m_LocaleItems.arraySize; i++)
            {
                var element = m_LocaleItems.GetArrayElementAtIndex(i);
                var languageProperty = element.FindPropertyRelative(LocalizationEditorHelper.LocaleLanguageSerializedProperty);
                var enumName = enumNames[languageProperty.enumValueIndex];
                uniqueLanguages.Add((SystemLanguage)Enum.Parse(typeof(SystemLanguage), enumName));
            }

            var availableLanguages = LocalizationSettings.Instance.AvailableLanguages;
            foreach (var lang in availableLanguages)
            {
                uniqueLanguages.Add(lang);
            }

            var localizedAssets = Localization.Instance.FindAllLocalizedAssets();
            foreach (var localizedAsset in localizedAssets)
            {
                foreach (var locale in localizedAsset.LocaleItems)
                {
                    uniqueLanguages.Add(locale.Language);
                }
            }

            var languages = new List<SystemLanguage>(uniqueLanguages);
            m_LocaleItems.arraySize = languages.Count;
            var size = m_LocaleItems.arraySize;
            for (int i = 0; i < size; i++)
            {
                var enumValueIndex = Array.IndexOf(enumNames, languages[i].ToString());
                var element = m_LocaleItems.GetArrayElementAtIndex(i);
                var languageProperty = element.FindPropertyRelative(LocalizationEditorHelper.LocaleLanguageSerializedProperty);
                languageProperty.enumValueIndex = enumValueIndex;
            }
        }
    }
}
