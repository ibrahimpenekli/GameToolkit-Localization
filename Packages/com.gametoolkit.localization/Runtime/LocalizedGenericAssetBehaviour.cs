// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace GameToolkit.Localization
{
    /// <summary>
    /// Base class that provides generic component & property selection by the asset type. 
    /// </summary>
    public class LocalizedGenericAssetBehaviour<TLocalizedAsset, T> : LocalizedGenericAssetBehaviourBase
        where TLocalizedAsset : LocalizedAsset<T> where T : class
    {
        [SerializeField, Tooltip("Text is used when text asset not attached.")]
        protected TLocalizedAsset m_LocalizedAsset;

        public TLocalizedAsset LocalizedAsset
        {
            get { return m_LocalizedAsset; }
            set
            {
                m_LocalizedAsset = value;
                ForceUpdateComponentLocalization();
            }
        }

        protected override Type GetValueType()
        {
            return typeof(T);
        }

        protected override bool HasLocalizedValue()
        {
            return LocalizedAsset != null;
        }

        protected override object GetLocalizedValue()
        {
            return GetValueOrDefault(m_LocalizedAsset);
        }
    }
}