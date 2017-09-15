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
        [SerializeField]
        private LocalizedText m_LocalizedAsset;

        protected override bool IsAssetExist()
        {
            return m_LocalizedAsset;
        }

        protected override void UpdateComponentValue()
        {
            GetComponent<Text>().text = m_LocalizedAsset.Value;
        }
    }
}
