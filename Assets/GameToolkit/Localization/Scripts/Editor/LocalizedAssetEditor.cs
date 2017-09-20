// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace GameToolkit.Localization.Editor
{
    [CustomEditor(typeof(LocalizedAssetBase), editorForChildClasses: true)]
    public class LocalizedAssetEditor : UnityEditor.Editor
    {
        private const float LanguageFieldWidth = 100;

        private ReorderableList m_LocaleItemList;

        private void OnEnable()
        {
            var elements = serializedObject.FindProperty(LocalizationEditorHelper.LocalizedElementsSerializedProperty);
            if (elements != null)
            {
                m_LocaleItemList = new ReorderableList
                (
                    serializedObject: serializedObject,
                    elements: elements,
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
    }
}
