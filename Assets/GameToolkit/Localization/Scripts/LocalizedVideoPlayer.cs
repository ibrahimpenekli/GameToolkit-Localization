// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEngine.Video;

namespace GameToolkit.Localization
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "LocalizedVideoClip", menuName = "GameToolkit/Localization/VideoPlayer")]
    public class LocalizedVideoPlayer : LocalizedAsset<VideoClip>
    {
        [Serializable]
        private class VideoPlayerLocaleItem : LocaleItem<VideoClip> { };

        [SerializeField]
        private VideoPlayerLocaleItem[] m_LocaleItems = new VideoPlayerLocaleItem[1];

        public override LocaleItemBase[] LocaleItems { get { return m_LocaleItems; } }
    }
}
