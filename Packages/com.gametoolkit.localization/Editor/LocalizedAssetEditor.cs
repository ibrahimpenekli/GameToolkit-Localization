// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System.Linq;

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

        private GUIStyle TextAreaStyle
        {
            get
            {
                if (m_TextAreaStyle == null)
                {
                    m_TextAreaStyle = new GUIStyle(EditorStyles.textArea);
                    m_TextAreaStyle.wordWrap = true;
                }
                
                return m_TextAreaStyle;
            }
        }

        private void OnEnable()
        {
            var assetValueType = ((LocalizedAssetBase)target).ValueType;
            m_LocaleItems = serializedObject.FindLocaleItemsProperty();
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
                    var languageProperty = element.FindLanguageProperty();
                    EditorGUI.PropertyField(languageRect, languageProperty, GUIContent.none);

                    // Value field.
                    var valueRect = new Rect(languageRect.x + languageRect.width + 4, languageRect.y, rect.width - languageRect.width - 4, rect.height - 4);
                    var valueProperty = element.FindValueProperty();
                    if (assetValueType == typeof(string))
                    {
                        valueProperty.stringValue = EditorGUI.TextArea(valueRect, valueProperty.stringValue, TextAreaStyle);
                    }
                    else
                    {
                        EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none);
                    }
                };
                m_LocaleItemList.onCanRemoveCallback = (list) => list.count > 1;
                m_LocaleItemList.elementHeightCallback = (index) =>
                {
                    var element = m_LocaleItemList.serializedProperty.GetArrayElementAtIndex(index);
                    var valueProperty = element.FindValueProperty();
                    var elementHeight = EditorGUIUtility.singleLineHeight;

                    if (assetValueType == typeof(string))
                    {
                        var valueWidth = m_CurrentLayoutRect.width - LanguageFieldWidth - 30;
                        elementHeight = TextAreaStyle.CalcHeight(new GUIContent(valueProperty.stringValue), valueWidth);
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
                
                EditorGUILayout.HelpBox("Make sure that locale items variable name is declared as \"m_LocaleItems\" and it is serializable.",
                    MessageType.Error);
            }
        }

        private void AddAllLanguages()
        {
            AddLanguages(LocalizationSettings.Instance.AllLanguages);
        }

        private void AddLanguagesFromSettings()
        {
            AddLanguages(LocalizationSettings.Instance.AvailableLanguages);
        }

        private void AddLanguages(List<Language> languages)
        {
            var filteredLanguages = languages.Where(x => !IsLanguageExist(m_LocaleItems, x)).ToArray();

            var startIndex = m_LocaleItems.arraySize;
            m_LocaleItems.arraySize += filteredLanguages.Length;

            for (var i = 0; i < filteredLanguages.Length; i++)
            {
                var localeItem = m_LocaleItems.GetArrayElementAtIndex(startIndex + i);
                
                var localeItemValue = localeItem.FindValueProperty();
                localeItemValue.stringValue = "";
                
                var localeItemLanguage = localeItem.FindLanguageProperty();
                LanguageEditorUtility.SetLanguageProperty(localeItemLanguage, filteredLanguages[i]);
            }
        }
    
        private static bool IsLanguageExist(SerializedProperty localeItemsProperty, Language language)
        {
            for (var i = 0; i < localeItemsProperty.arraySize; i++)
            {
                var element = localeItemsProperty.GetArrayElementAtIndex(i);
                var languageProperty = element.FindLanguageProperty();
                var languageCode = languageProperty.FindLanguageCodeProperty().stringValue;
                if (languageCode == language.Code)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds a locale and the value or updates if specified language is exists.
        /// </summary>
        public static bool AddOrUpdateLocale(LocalizedAssetBase localizedAsset, Language language, object value)
        {
            var serializedObject = new SerializedObject(localizedAsset);
            serializedObject.Update();
            
            var elements = serializedObject.FindLocaleItemsProperty();
            if (elements != null && elements.arraySize > 0)
            {
                var index = Array.FindIndex(localizedAsset.LocaleItems, x => x.Language == language);
                if (index < 0)
                {
                    AddLocale(localizedAsset);
                    index = localizedAsset.LocaleItems.Length - 1;
                }

                var localeItem = localizedAsset.LocaleItems[index];
                localeItem.Language = language;
                localeItem.ObjectValue = value;
                return true;
            }

            return false;
        }

        
        /// <summary>
        /// Adds a locale end of the list by copying last one.
        /// </summary>
        public static bool AddLocale(LocalizedAssetBase localizedAsset)
        {
            var serializedObject = new SerializedObject(localizedAsset);
            serializedObject.Update();
            
            var elements = serializedObject.FindLocaleItemsProperty();
            if (elements != null)
            {
                elements.arraySize += 1;
                serializedObject.ApplyModifiedProperties();
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Removes specified locale item from the list.
        /// </summary>
        public static bool RemoveLocale(LocalizedAssetBase localizedAsset, LocaleItemBase localeItem)
        {
            var serializedObject = new SerializedObject(localizedAsset);
            serializedObject.Update();
            
            var elements = serializedObject.FindLocaleItemsProperty();
            if (elements != null && elements.arraySize > 1)
            {
                var index = Array.IndexOf(localizedAsset.LocaleItems, localeItem);
                if (index >= 0)
                {
                    elements.DeleteArrayElementAtIndex(index);
                    serializedObject.ApplyModifiedProperties();
                    return true;
                }
            }

            return false;
        }
    }

    internal static class LocalizedAssetEditorExtensions
    {
        public static SerializedProperty FindLocaleItemsProperty(this SerializedObject serializedObject)
        {
            return serializedObject.FindProperty("m_LocaleItems");
        }
    }

    internal static class LocaleItemEditorExtensions
    {
        public static SerializedProperty FindLanguageProperty(this SerializedProperty element)
        {
            return element.FindPropertyRelative("m_Language2");
        }
        
        public static SerializedProperty FindValueProperty(this SerializedProperty element)
        {
            return element.FindPropertyRelative("m_Value");
        }
    }
}
