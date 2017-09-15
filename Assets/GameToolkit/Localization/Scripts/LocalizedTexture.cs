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
        private class LocaleTexture : Locale<Texture> { };

        [SerializeField, Tooltip("Localized textures. First item is fallback value.")]
        private List<LocaleTexture> m_Assets = new List<LocaleTexture> { new LocaleTexture() };

        protected override List<Locale<Texture>> GetAssetList()
        {
            return m_Assets.ConvertAll(x => (Locale<Texture>)x);
        }
    }
}
