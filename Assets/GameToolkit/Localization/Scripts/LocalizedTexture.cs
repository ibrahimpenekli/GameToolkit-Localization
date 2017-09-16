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
    [CreateAssetMenu(fileName = "LocalizedTexture", menuName = "GameToolkit/Localization/Texture")]
    public class LocalizedTexture : LocalizedAsset<Texture>
    {
        [Serializable]
        private class TextureLocaleItem : LocaleItem<Texture> { };

        [SerializeField, Tooltip("Localized textures. First item is fallback value.")]
        private TextureLocaleItem[] m_LocaleItems = new TextureLocaleItem[0];

        public override LocaleItem<Texture>[] LocaleItems { get { return m_LocaleItems; } }
    }
}
