// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.Video;

namespace GameToolkit.Localization
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(VideoPlayer))]
    public class LocalizedVideoPlayerBehaviour : LocalizedAssetBehaviour
    {
        
        public LocalizedVideoPlayer LocalizedTexture;

        protected override void UpdateComponentValue()
        {
            if (LocalizedTexture)
            {
                GetComponent<VideoPlayer>().clip = LocalizedTexture.Value;
            }
        }
    }
}
