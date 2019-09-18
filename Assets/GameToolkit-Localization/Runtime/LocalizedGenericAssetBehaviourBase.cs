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
    public abstract class LocalizedGenericAssetBehaviourBase : LocalizedAssetBehaviour
    {
        [SerializeField]
        protected Component m_Component;

        [SerializeField]
        protected string m_Property = "";

        protected PropertyInfo m_PropertyInfo;

        protected virtual void Awake()
        {
            InitializePropertyIfNeeded();
        }

        protected virtual void InitializePropertyIfNeeded()
        {
            if (m_PropertyInfo == null && m_Component)
            {
                m_PropertyInfo = FindProperty(m_Component, m_Property);
            }
        }

        public abstract PropertyInfo FindProperty(Component component, string name);
        public abstract List<PropertyInfo> FindProperties(Component component);
    }
}
