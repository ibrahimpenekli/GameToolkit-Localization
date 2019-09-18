// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
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

        [SerializeField]
        private PrefabLocaleItem[] m_LocaleItems = new PrefabLocaleItem[1];

        public override LocaleItemBase[] LocaleItems { get { return m_LocaleItems; } }
    }
}