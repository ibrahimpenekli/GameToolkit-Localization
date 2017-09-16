// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace GameToolkit.Localization.Editor
{
    public abstract class LocalizedAssetEditor<TAsset> : UnityEditor.Editor where TAsset : class
    {
        private const float LanguageFieldWidth = 100;

        private ReorderableList m_LocaleItemList;

        private void OnEnable()
        {
            var elements = serializedObject.FindProperty("m_LocaleItems");
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
                EditorGUI.LabelField(rect, elements.displayName);
            };
            
            m_LocaleItemList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => 
            {
                var element = m_LocaleItemList.serializedProperty.GetArrayElementAtIndex(index);
                
                // Language field.
                var r1 = new Rect(rect.x, rect.y + 2, LanguageFieldWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(r1, element.FindPropertyRelative("m_Language"), GUIContent.none);

                // Value field.
                var r2 = new Rect(r1.x + r1.width + 4, r1.y, rect.width - r1.width - 4, r1.height);
                EditorGUI.PropertyField(r2, element.FindPropertyRelative("m_Value"), GUIContent.none);
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            m_LocaleItemList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
