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
        public LocalizedText LocalizedText;
        public LocalizedFont LocalizedFont;

        protected override void UpdateComponentValue()
        {
            if (LocalizedText)
            {
                GetComponent<Text>().text = LocalizedText.Value;
            }
            
            if (LocalizedFont)
            {
                GetComponent<Text>().font = LocalizedFont.Value;
            }
        }
    }
}
