// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace GameToolkit.Localization.Editor
{
    [CustomEditor(typeof(LocalizationSettings), editorForChildClasses: true)]
    public class LocalizationSettingsEditor : UnityEditor.Editor
    {
        private ReorderableList m_AvailableLanguages;
        private SerializedProperty m_AvailableLanguagesProperty;
        private SerializedProperty m_GoogleAuthenticationFile;

        private void OnEnable()
        {
            m_AvailableLanguagesProperty = serializedObject.FindProperty("m_AvailableLanguages");
            m_GoogleAuthenticationFile = serializedObject.FindProperty("GoogleAuthenticationFile");
            if (m_AvailableLanguagesProperty != null)
            {
                m_AvailableLanguages = new ReorderableList
                (
                    serializedObject: serializedObject,
                    elements: m_AvailableLanguagesProperty,
                    draggable: true,
                    displayHeader: true,
                    displayAddButton: true,
                    displayRemoveButton: true
                );
                m_AvailableLanguages.drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, m_AvailableLanguagesProperty.displayName);
                };
                m_AvailableLanguages.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = m_AvailableLanguages.serializedProperty.GetArrayElementAtIndex(index);
                    var position = new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight);
                    EditorGUI.PropertyField(position, element, GUIContent.none);
                };
                m_AvailableLanguages.onCanRemoveCallback = (ReorderableList list) =>
                {
                    return list.count > 1;
                };
            }
        }

        public override void OnInspectorGUI()
        {
            if (m_AvailableLanguages != null)
            {
                serializedObject.Update();
                m_AvailableLanguages.DoLayoutList();
                

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
    }
}
