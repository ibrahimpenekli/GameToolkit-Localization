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
    [CreateAssetMenu(fileName = "LocalizedAudioClip", menuName = "GameToolkit/Localization/AudioClip")]
    public class LocalizedAudioClip : LocalizedAsset<AudioClip>
    {
        [Serializable]
        private class AudioClipLocaleItem : LocaleItem<AudioClip> { };

        [SerializeField, Tooltip("Localized audios. First item is fallback value.")]
        private AudioClipLocaleItem[] m_LocaleItems = new AudioClipLocaleItem[0];

        public override LocaleItem<AudioClip>[] LocaleItems { get { return m_LocaleItems; } }
    }
}
