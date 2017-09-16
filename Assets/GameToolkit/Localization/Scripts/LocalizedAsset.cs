// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
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
        /// Gets the defined locale items of the localized asset.
        /// </summary>
        public abstract LocaleItem<TAsset>[] LocaleItems { get; }

        /// <summary>
        /// Returns localized text regarding to Localization.Instance.CurrentLanguage.
        /// Returns fallback text or empty string if not found.
        /// </summary>
        /// <returns>Localized text.</returns>
        public TAsset Value
        {
            get
            {
                var value = GetLocaleValue(Localization.Instance.CurrentLanguage);
                if (value == null)
                {
                    var localeItem = LocaleItems.FirstOrDefault();
                    value = localeItem != null ? localeItem.Value : default(TAsset);
                }
                return value;
            }
        }

        /// <summary>
        /// Returns the language given is whether exist or not.
        /// </summary>
        public bool HasLocale(SystemLanguage language)
        {
            var localeItem = LocaleItems.FirstOrDefault(x => x.Language == language);
            return localeItem != null;
        }

        /// <summary>
        /// Returns localized text regarding to language given; otherwise, null.
        /// </summary>
        /// <returns>Localized text.</returns>
        public TAsset GetLocaleValue(SystemLanguage language)
        {
            var localeItem = LocaleItems.FirstOrDefault(x => x.Language == language);
            if (localeItem != null)
            {
                return localeItem.Value;
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
