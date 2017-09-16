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
        public LocalizedTexture LocalizedTexture;

        protected override void UpdateComponentValue()
        {
            if (LocalizedTexture)
            {
                GetComponent<RawImage>().texture = LocalizedTexture.Value;
            }
        }
    }
}
