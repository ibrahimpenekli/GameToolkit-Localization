// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace GameToolkit.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalizedTextAssetBehaviour : LocalizedGenericAssetBehaviour<LocalizedTextAsset, TextAsset>
    {
        protected override Type GetValueType()
        {
            return typeof(string);
        }

        protected override object GetLocalizedValue()
        {
            return m_LocalizedAsset ? m_LocalizedAsset.Value.text : null;
        }
    }
}