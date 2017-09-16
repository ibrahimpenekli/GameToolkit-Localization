// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.UI;

namespace GameToolkit.Localization
{
	/// <summary>
	/// 
	/// </summary>
    [RequireComponent(typeof(Image))]
    public class LocalizedImageBehaviour : LocalizedAssetBehaviour
    {
        public LocalizedSprite LocalizedSprite;

        protected override void UpdateComponentValue()
        {
            if (LocalizedSprite)
            {
                GetComponent<Image>().sprite = LocalizedSprite.Value;
            }
        }
    }
}
