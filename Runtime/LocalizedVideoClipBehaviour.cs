// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.Video;

namespace GameToolkit.Localization
{
    [AddComponentMenu(ComponentMenuRoot + "Localized Video Clip")]
    public class LocalizedVideoClipBehaviour : LocalizedGenericAssetBehaviour<LocalizedVideoClip, VideoClip>
    {
        private void Reset()
        {
            m_Component = GetComponent<VideoPlayer>();
            if (m_Component)
            {
                m_Property = "clip";
            }
        }
    }
}