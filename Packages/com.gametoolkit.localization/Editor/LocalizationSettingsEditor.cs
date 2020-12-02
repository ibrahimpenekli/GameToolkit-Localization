// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.IO;
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
        private SerializedProperty m_ImportLocation;
        private SerializedProperty m_GoogleAuthenticationFile;

        private void OnEnable()
        {
            m_AvailableLanguages = serializedObject.FindProperty("m_AvailableLanguages2");
            m_ImportLocation = serializedObject.FindProperty("m_ImportLocation");
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
                m_AvailableLanguagesList.drawElementCallback = (rect, index, isActive, isFocused) =>
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
                        EditorGUI.PropertyField(r1, languageName, new GUIContent(languageName.displayName, "Language name"));
                        
                        EditorGUIUtility.labelWidth = 40;
                        var r2 = new Rect(position.x + r1.width + 4, position.y, position.width / 2 - 2, position.height);
                        EditorGUI.PropertyField(r2, languageCode, new GUIContent(languageCode.displayName, "ISO-639-1 code"));
                        
                        EditorGUIUtility.labelWidth = labelWidth;
                    }
                    else
                    {
                        LanguageEditorUtility.LanguageField(position, languageProperty, GUIContent.none, true);
                    }
                };
                m_AvailableLanguagesList.onRemoveCallback = list =>
                {
                    var languageProperty = list.serializedProperty.GetArrayElementAtIndex(list.index);
                    var language = LanguageEditorUtility.GetLanguageValueFromProperty(languageProperty);
                    if (language.Custom)
                    {
                        var localizedAssets = Localization.FindAllLocalizedAssets();
                        if (localizedAssets.Any(x => 
                            x.LocaleItems.Any(y => y.Language == language)))
                        {
                            if (!EditorUtility.DisplayDialog("Remove \"" + language + "\" language?",
                                "\"" + language + "\" language is in-use by some localized assets." +
                                " Are you sure to remove?", "Remove", "Cancel"))
                            {
                                return; // Cancelled.
                            }
                        }
                    }
                    
                    ReorderableList.defaultBehaviours.DoRemoveButton(list);
                };
                m_AvailableLanguagesList.onCanRemoveCallback = list => list.count > 1;
                m_AvailableLanguagesList.onAddDropdownCallback = (buttonRect, list) =>
                {
                    var menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Language", "Adds built-in language."), false, () =>
                    {
                        ReorderableList.defaultBehaviours.DoAddButton(list);
                        
                        var languageProperty = list.serializedProperty.GetArrayElementAtIndex(list.index);
                        LanguageEditorUtility.SetLanguageProperty(languageProperty, Language.BuiltinLanguages[0]);

                        serializedObject.ApplyModifiedProperties();
                    });
                    menu.AddItem(new GUIContent("Custom language", "Adds custom language."), false, () =>
                    {
                        ReorderableList.defaultBehaviours.DoAddButton(list);

                        var languageProperty = list.serializedProperty.GetArrayElementAtIndex(list.index);
                        LanguageEditorUtility.SetLanguageProperty(languageProperty, "", "", true);

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

                EditorGUILayout.LabelField("Import/Export", EditorStyles.boldLabel);
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel(m_ImportLocation.displayName);
                if (GUILayout.Button(m_ImportLocation.stringValue, EditorStyles.objectField))
                {
                    var path = EditorUtility.OpenFolderPanel("Select folder for import location", "Assets/", "");
                    if (Directory.Exists(path))
                    {
                        path = "Assets" + path.Replace(Application.dataPath, "");
                        if (AssetDatabase.IsValidFolder(path))
                        {
                            m_ImportLocation.stringValue = path;    
                        }
                            
                    }
                }
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.Separator();
                
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
                LanguageEditorUtility.SetLanguageProperty(languageProperty, languages[i]);
            }
        }

        private Language[] FindUsedLanguages()
        {
            var languages = new HashSet<Language>();
            for (var i = 0; i < m_AvailableLanguages.arraySize; i++)
            {
                languages.Add(
                    LanguageEditorUtility.GetLanguageValueFromProperty(m_AvailableLanguages.GetArrayElementAtIndex(i)));
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
