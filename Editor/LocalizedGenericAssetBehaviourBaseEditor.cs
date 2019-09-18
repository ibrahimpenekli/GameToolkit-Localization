// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameToolkit.Localization.Editor
{
    [CustomEditor(typeof(LocalizedGenericAssetBehaviourBase), editorForChildClasses: true)]
    public class LocalizedGenericAssetBehaviourBaseEditor : UnityEditor.Editor
    {
        private SerializedProperty m_Component;
        private SerializedProperty m_Property;

        private List<PropertyInfo> m_Properties;
        private string[] m_PropertieNames = new string[0];
        private int m_SelectedPropertyIdx = -1;

        private void OnEnable()
        {
            m_Component = serializedObject.FindProperty("m_Component");
            m_Property = serializedObject.FindProperty("m_Property");

            FindComponentProperties();

            if (!string.IsNullOrEmpty(m_Property.stringValue))
            {
                m_SelectedPropertyIdx = Array.IndexOf(m_PropertieNames, m_Property.stringValue);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(m_Component);
            if (EditorGUI.EndChangeCheck())
            {
                FindComponentProperties();
            }

            var selectedPropertyIdx = EditorGUILayout.Popup("Property", m_SelectedPropertyIdx, m_PropertieNames);
            if (m_SelectedPropertyIdx != selectedPropertyIdx)
            {
                m_Property.stringValue = m_PropertieNames[selectedPropertyIdx];
                m_SelectedPropertyIdx = selectedPropertyIdx;
            }

            // Draw other properties.
            DrawPropertiesExcluding(serializedObject, new string[] { "m_Script", m_Component.name, m_Property.name });
            serializedObject.ApplyModifiedProperties();
        }

        private void FindComponentProperties()
        {
            var localizedBehaviour = (LocalizedGenericAssetBehaviourBase)target;
            var component = (Component)m_Component.objectReferenceValue;
            if (component)
            {
                m_Properties = localizedBehaviour.FindProperties((Component)m_Component.objectReferenceValue);
                m_PropertieNames = new string[m_Properties.Count];
                for (int i = 0; i < m_Properties.Count; i++)
                {
                    m_PropertieNames[i] = m_Properties[i].Name;
                }
            }
        }
    }
}
