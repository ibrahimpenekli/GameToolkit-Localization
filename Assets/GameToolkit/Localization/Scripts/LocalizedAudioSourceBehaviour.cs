// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace GameToolkit.Localization
{
	/// <summary>
	/// 
	/// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class LocalizedAudioSourceBehaviour : LocalizedAssetBehaviour
    {
        [SerializeField]
        private LocalizedAudioClip m_LocalizedAsset;

        protected override bool IsAssetExist()
        {
            return m_LocalizedAsset;
        }

        protected override void UpdateComponentValue()
        {
            GetComponent<AudioSource>().clip = m_LocalizedAsset.Value;
        }
    }
}
