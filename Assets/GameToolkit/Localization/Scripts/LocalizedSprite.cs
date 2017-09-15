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
        private class LocaleSprite : Locale<Sprite> { };

        [SerializeField, Tooltip("Localized sprites. First item is fallback value.")]
        private List<LocaleSprite> m_Assets = new List<LocaleSprite> { new LocaleSprite() };

        protected override List<Locale<Sprite>> GetAssetList()
        {
            return m_Assets.ConvertAll(x => (Locale<Sprite>)x);
        }
    }
}
