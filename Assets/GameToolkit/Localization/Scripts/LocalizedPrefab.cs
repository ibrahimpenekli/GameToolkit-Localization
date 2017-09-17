// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameToolkit.Localization
{
	/// <summary>
	/// 
	/// </summary>
    [CreateAssetMenu(fileName = "LocalizedPrefab", menuName = "GameToolkit/Localization/Prefab")]
    public class LocalizedPrefab : LocalizedAsset<GameObject>
    {
        [Serializable]
        private class PrefabLocaleItem : LocaleItem<GameObject> { };

        [SerializeField, Tooltip("Localized prefabs. First item is fallback value.")]
        private PrefabLocaleItem[] m_LocaleItems = new PrefabLocaleItem[0];
        
        public override LocaleItemBase[] LocaleItems { get { return m_LocaleItems; } }
    }
}