// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GameToolkit.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalizedGenericAssetBehaviour<TLocalizedAsset, T> : LocalizedGenericAssetBehaviourBase
                    where TLocalizedAsset : LocalizedAsset<T> where T : class
    {
        [SerializeField, Tooltip("Text is used when text asset not attached.")]
        protected TLocalizedAsset m_LocalizedAsset;

        public virtual TLocalizedAsset LocalizedAsset
        {
            get
            {
                return m_LocalizedAsset;
            }
            set
            {
                m_LocalizedAsset = value;
                UpdateComponentValue();
            }
        }

        protected virtual Type GetValueType()
        {
            return typeof(T);
        }

        protected virtual object GetLocalizedValue()
        {
            return m_LocalizedAsset ? m_LocalizedAsset.Value : default(T);
        }

        protected override void UpdateComponentValue()
        {
            if (m_LocalizedAsset && m_PropertyInfo != null)
            {
                m_PropertyInfo.SetValue(m_Component, GetLocalizedValue(), null);
            }
        }

        public override PropertyInfo FindProperty(Component component, string name)
        {
            return component.GetType().GetProperty(name, typeof(T));
        }

        public override List<PropertyInfo> FindProperties(Component component)
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
