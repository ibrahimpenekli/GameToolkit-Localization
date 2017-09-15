// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameToolkit.Localization
{
	/// <summary>
	/// 
	/// </summary>
    [CreateAssetMenu(fileName = "LocalizedText", menuName = "GameToolkit/Localization/Text")]
    public class LocalizedText : LocalizedAsset<string>
    {
        [Serializable]
        private class LocaleText : Locale<string> { };

        [SerializeField, Tooltip("Localized texts. First item is fallback value.")]
        private List<LocaleText> m_Assets = new List<LocaleText> { new LocaleText() };

        protected override List<Locale<string>> GetAssetList()
        {
            return m_Assets.ConvertAll(x => (Locale<string>)x);
        }
    }
}
