// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.UI;

namespace GameToolkit.Localization
{
	/// <summary>
	/// 
	/// </summary>
    [RequireComponent(typeof(Text))]
    public class LocalizedTextBehaviour : LocalizedAssetBehaviour
    {   
        [Tooltip("Text is used when text asset not attached.")]
        public LocalizedText LocalizedText;

        [Tooltip("Text is ignored when text asset is attached.")]
        public LocalizedTextAsset LocalizedTextAsset;

        [Tooltip("Text font is changed if attached.")]
        public LocalizedFont LocalizedFont;

        protected override void UpdateComponentValue()
        {
            if (LocalizedText)
            {
                GetComponent<Text>().text = LocalizedText.Value;
            }

            if (LocalizedTextAsset)
            {
                GetComponent<Text>().text = LocalizedTextAsset.Value.text;
            }
            
            if (LocalizedFont)
            {
                GetComponent<Text>().font = LocalizedFont.Value;
            }
        }
    }
}
