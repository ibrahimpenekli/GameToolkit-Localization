// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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
        private TLocalizedAsset m_LocalizedAsset;

        protected override void UpdateComponentValue()
        {
            if (m_LocalizedAsset && m_PropertyInfo != null)
            {
                m_PropertyInfo.SetValue(m_Component, m_LocalizedAsset.Value, null);
            }
        }

        public override PropertyInfo FindProperty(Component component, string name)
        {
            return component.GetType().GetProperty(name, typeof(T));
        }

        public override List<PropertyInfo> FindProperties(Component component)
        {
            var type = component.GetType();
            var allProperties = type.GetProperties();
            var properties = new List<PropertyInfo>();
            foreach (var property in allProperties)
            {

                if (property.CanWrite && typeof(T).IsAssignableFrom(property.PropertyType))
                {
                    properties.Add(property);
                }
            }
            return properties;
        }
    }
}
