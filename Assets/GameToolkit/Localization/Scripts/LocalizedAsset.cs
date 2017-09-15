// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;

namespace GameToolkit.Localization
{
	/// <summary>
	/// 
	/// </summary>
    public abstract class LocalizedAsset<TAsset> : ScriptableObject where TAsset : class
    {
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        protected abstract List<Locale<TAsset>> GetAssetList();

        /// <summary>
        /// Returns localized text regarding to Localization.Instance.CurrentLanguage.
        /// Returns fallback text or empty string if not found.
        /// </summary>
        /// <returns>Localized text.</returns>
        public TAsset Value
        {
            get
            {
                var assetList = GetAssetList();
                var val = GetValue(Localization.Instance.CurrentLanguage, assetList);
                if (val == null)
                {
                    if (assetList.Count > 0)
                    {
                        val = assetList[0].Value;
                    }
                    else
                    {
                        val = default(TAsset);
                    }
                }

                return val;
            }
        }

        /// <summary>
        /// Returns localized text regarding to language given; otherwise, null.
        /// </summary>
        /// <returns>Localized text.</returns>
        public TAsset GetValue(SystemLanguage language)
        {
            return GetValue(language, GetAssetList());
        }

        /// <summary>
        /// Returns the language given is whether exist or not.
        /// </summary>
        public bool Has(SystemLanguage language)
        {
            var assetList = GetAssetList();
            foreach (var asset in assetList)
            {
                if (asset.Language == language)
                {
                    return true;
                }
            }
            return false;
        }

        private TAsset GetValue(SystemLanguage language, List<Locale<TAsset>> assetList)
        {
            foreach (var asset in assetList)
            {
                if (asset.Language == language)
                {
                    return asset.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns LocalizedAsset value.
        /// </summary>
        /// <param name="asset">LocalizedAsset</param>
        public static implicit operator TAsset(LocalizedAsset<TAsset> asset)
        {
            return asset ? asset.Value : default(TAsset);
        }
    }
}
