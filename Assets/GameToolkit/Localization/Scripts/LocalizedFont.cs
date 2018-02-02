// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace GameToolkit.Localization
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "LocalizedFont", menuName = "GameToolkit/Localization/Font")]
    public class LocalizedFont : LocalizedAsset<Font>
    {
        [Serializable]
        private class FontLocaleItem : LocaleItem<Font> { };

        [SerializeField]
        private FontLocaleItem[] m_LocaleItems = new FontLocaleItem[1];

        public override LocaleItemBase[] LocaleItems { get { return m_LocaleItems; } }
    }
}