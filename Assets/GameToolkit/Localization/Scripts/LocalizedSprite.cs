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
    [CreateAssetMenu(fileName = "LocalizedSprite", menuName = "GameToolkit/Localization/Sprite")]
    public class LocalizedSprite : LocalizedAsset<Sprite>
    {
        [Serializable]
        private class SpriteLocaleItem : LocaleItem<Sprite> { };

        [SerializeField, Tooltip("Localized sprites. First item is fallback value.")]
        private SpriteLocaleItem[] m_LocaleItems = new SpriteLocaleItem[0];

        public override LocaleItem<Sprite>[] LocaleItems { get { return m_LocaleItems; } }
    }
}
