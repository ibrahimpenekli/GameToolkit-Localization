// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using UnityEngine;

namespace GameToolkit.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class LocalizedAsset<T> : LocalizedAssetBase where T : class
    {
        /// <summary>
        /// Gets the value type.
        /// </summary>
        public override Type ValueType
        {
            get { return GetValueType(); }
        }

        /// <summary>
        /// Gets the value type.
        /// </summary>
        public static Type GetValueType()
        {
            return typeof(T);
        }

        /// <summary>
        /// Gets the defined locale items of the localized asset with concrete type.
        /// </summary>
        public LocaleItem<T>[] TypedLocaleItems
        {
            get { return (LocaleItem<T>[])LocaleItems; }
        }

        /// <summary>
        /// Returns localized text regarding to Localization.Instance.CurrentLanguage.
        /// Returns fallback text or empty string if not found.
        /// </summary>
        /// <returns>Localized text.</returns>
        public T Value
        {
            get
            {
                var value = GetLocaleValue(Localization.Instance.CurrentLanguage);
                if (value == null)
                {
                    var localeItem = TypedLocaleItems.FirstOrDefault();
                    value = localeItem != null ? localeItem.Value : default(T);
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
        public T GetLocaleValue(SystemLanguage language)
        {
            var localeItem = TypedLocaleItems.FirstOrDefault(x => x.Language == language);
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
        public static implicit operator T(LocalizedAsset<T> asset)
        {
            return asset ? asset.Value : default(T);
        }
    }
}
