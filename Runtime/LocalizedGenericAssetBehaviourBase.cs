// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GameToolkit.Localization
{
    public abstract class LocalizedGenericAssetBehaviourBase : LocalizedAssetBehaviour
    {
        [SerializeField]
        private Component m_Component;

        [SerializeField]
        private string m_Property = "";

        private PropertyInfo m_PropertyInfo;

        protected virtual void Awake()
        {
            InitializePropertyIfNeeded();
        }

        protected abstract Type GetValueType();
        protected abstract bool HasLocalizedValue();
        protected abstract object GetLocalizedValue();

        protected override bool TryUpdateComponentLocalization(bool isOnValidate)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                InitializePropertyIfNeeded();
            }
#endif

            if (HasLocalizedValue() && m_PropertyInfo != null)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    UnityEditor.Undo.RecordObject(m_Component, "Locale value change");
                }
#endif
                m_PropertyInfo.SetValue(m_Component, GetLocalizedValue(), null);
                return true;
            }

            return false;
        }

        private void InitializePropertyIfNeeded()
        {
            if (m_PropertyInfo == null)
            {
                TryInitializeProperty();
            }
        }

        private bool TryInitializeProperty()
        {
            if (m_Component != null)
            {
                m_PropertyInfo = FindProperty(m_Component, m_Property);
                return m_PropertyInfo != null;
            }

            return false;
        }
        
        public bool TrySetComponentAndProperty<TComponent>(string propertyName)
            where TComponent : Component
        {
            m_Component = GetComponent<TComponent>();
            if (m_Component != null)
            {
                m_Property = propertyName;
                
                if (!TryInitializeProperty())
                {
                    m_Property = "";
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool TrySetComponentAndPropertyIfNotSet<TComponent>(string propertyName)
            where TComponent : Component
        {
            if (m_Component == null)
            {
                return TrySetComponentAndProperty<TComponent>(propertyName);
            }

            return false;
        }
        
        private PropertyInfo FindProperty(Component component, string propertyName)
        {
            return component.GetType().GetProperty(propertyName, GetValueType());
        }

        /// <summary>
        /// Finds list of localizable properties of specified component.
        /// </summary>
        internal List<PropertyInfo> FindProperties(Component component)
        {
            var valueType = GetValueType();
            var allProperties = component.GetType().GetProperties();
            var properties = new List<PropertyInfo>();
            foreach (var property in allProperties)
            {
                if (property.CanWrite && valueType.IsAssignableFrom(property.PropertyType))
                {
                    properties.Add(property);
                }
            }

            return properties;
        }
    }
}
