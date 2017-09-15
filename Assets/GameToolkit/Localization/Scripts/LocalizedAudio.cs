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
    [CreateAssetMenu(fileName = "LocalizedAudio", menuName = "GameToolkit/Localization/Audio")]
    public class LocalizedAudio : LocalizedAsset<AudioClip>
    {
        [Serializable]
        private class LocaleAudio : Locale<AudioClip> { };

        [SerializeField, Tooltip("Localized audios. First item is fallback value.")]
        private List<LocaleAudio> m_Assets = new List<LocaleAudio> { new LocaleAudio() };

        protected override List<Locale<AudioClip>> GetAssetList()
        {
            return m_Assets.ConvertAll(x => (Locale<AudioClip>)x);
        }
    }
}
