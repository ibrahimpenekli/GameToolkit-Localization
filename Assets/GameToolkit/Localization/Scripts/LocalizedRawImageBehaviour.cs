// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.UI;

namespace GameToolkit.Localization
{
	/// <summary>
	/// 
	/// </summary>
    [RequireComponent(typeof(RawImage))]
    public class LocalizedRawImageBehaviour : LocalizedAssetBehaviour
    {
        [SerializeField]
        private LocalizedTexture m_LocalizedAsset;

        protected override bool IsAssetExist()
        {
            return m_LocalizedAsset;
        }

        protected override void UpdateComponentValue()
        {
            GetComponent<RawImage>().texture = m_LocalizedAsset.Value;
        }
    }
}
