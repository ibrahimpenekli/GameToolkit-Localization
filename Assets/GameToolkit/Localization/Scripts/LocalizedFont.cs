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
    [CreateAssetMenu(fileName = "LocalizedFont", menuName = "GameToolkit/Localization/Font")]
    public class LocalizedFont : LocalizedAsset<Font>
    {
        [Serializable]
        private class FontLocaleItem : LocaleItem<Font> { };

        [SerializeField, Tooltip("Localized fonts. First item is fallback value.")]
        private FontLocaleItem[] m_LocaleItems = new FontLocaleItem[0];
        
        public override LocaleItem<Font>[] LocaleItems { get { return m_LocaleItems; } }
    }
}