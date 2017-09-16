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
        public LocalizedAudioClip LocalizedAudioClip;

        protected override void UpdateComponentValue()
        {
            if (LocalizedAudioClip)
            {
                GetComponent<AudioSource>().clip = LocalizedAudioClip.Value;
            }
        }
    }
}
