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
    [CreateAssetMenu(fileName = "LocalizedText", menuName = "GameToolkit/Localization/Text")]
    public class LocalizedText : LocalizedAsset<string>
    {
        [Serializable]
        private class TextLocaleItem : LocaleItem<string> { };

        [SerializeField, Tooltip("Localized texts. First item is fallback value.")]
        private TextLocaleItem[] m_LocaleItems = new TextLocaleItem[0];
        
        public override LocaleItemBase[] LocaleItems { get { return m_LocaleItems; } }
    }
}
