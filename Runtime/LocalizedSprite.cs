// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
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

        [SerializeField]
        private SpriteLocaleItem[] m_LocaleItems = new SpriteLocaleItem[1];

        public override LocaleItemBase[] LocaleItems { get { return m_LocaleItems; } }
    }
}
