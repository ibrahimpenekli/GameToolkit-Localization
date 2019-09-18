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
        private Rect m_CurrentLayoutRect;
        private GUIStyle m_TextAreaStyle;

        private void OnEnable()
        {
            if (m_TextAreaStyle == null)
            {
                m_TextAreaStyle = new GUIStyle(EditorStyles.textArea);
                m_TextAreaStyle.wordWrap = true;
            }

            var assetValueType = ((LocalizedAssetBase)target).ValueType;
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

                m_LocaleItemList.drawHeaderCallback = (rect) =>
                {
                    EditorGUI.LabelField(rect, ObjectNames.NicifyVariableName(target.GetType().Name) + "s");
                };
                m_LocaleItemList.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var element = m_LocaleItemList.serializedProperty.GetArrayElementAtIndex(index);

                    // Language field.
                    var languageRect = new Rect(rect.x, rect.y + 2, LanguageFieldWidth, rect.height - 4);
                    var languageProperty = element.FindPropertyRelative(LocalizationEditorHelper.LocaleLanguageSerializedProperty);
                    EditorGUI.PropertyField(languageRect, languageProperty, GUIContent.none);

                    // Value field.
                    var valueRect = new Rect(languageRect.x + languageRect.width + 4, languageRect.y, rect.width - languageRect.width - 4, rect.height - 4);
                    var valueProperty = element.FindPropertyRelative(LocalizationEditorHelper.LocaleValueSerializedProperty);
                    if (assetValueType == typeof(string))
                    {
                        valueProperty.stringValue = EditorGUI.TextArea(valueRect, valueProperty.stringValue, m_TextAreaStyle);
                    }
                    else
                    {
                        EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none);
                    }
                };
                m_LocaleItemList.onCanRemoveCallback = (list) =>
                {
                    return list.count > 1;
                };
                m_LocaleItemList.elementHeightCallback = (index) =>
                {
                    var element = m_LocaleItemList.serializedProperty.GetArrayElementAtIndex(index);
                    var valueProperty = element.FindPropertyRelative(LocalizationEditorHelper.LocaleValueSerializedProperty);
                    var elementHeight = EditorGUIUtility.singleLineHeight;

                    if (assetValueType == typeof(string))
                    {
                        var valueWidth = m_CurrentLayoutRect.width - LanguageFieldWidth - 30;
                        elementHeight = m_TextAreaStyle.CalcHeight(new GUIContent(valueProperty.stringValue), valueWidth);
                    }
                    return Mathf.Max(EditorGUIUtility.singleLineHeight, elementHeight) + 4;
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
                m_CurrentLayoutRect = GUILayoutUtility.GetRect(0, m_LocaleItemList.GetHeight(), GUILayout.ExpandWidth(true));
                serializedObject.Update();
                m_LocaleItemList.DoList(m_CurrentLayoutRect);
                serializedObject.ApplyModifiedProperties();

                var helpRect = m_CurrentLayoutRect;
                helpRect.y += m_LocaleItemList.GetHeight() + 4;
                helpRect.height = EditorGUIUtility.singleLineHeight * 1.5f;
                EditorGUI.HelpBox(helpRect, "First locale item is used as fallback if needed.", MessageType.Info);
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
