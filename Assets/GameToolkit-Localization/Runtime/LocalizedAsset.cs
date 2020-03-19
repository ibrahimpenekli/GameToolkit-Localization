// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using UnityEngine;

namespace GameToolkit.Localization
{
    public abstract class LocalizedAsset<T> : LocalizedAssetBase where T : class
    {
        /// <inheritdoc cref="LocalizedAssetBase.ValueType"/>
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
            get { return (LocaleItem<T>[]) LocaleItems; }
        }

        /// <summary>
        /// Gets localized asset value regarding to <see cref="Localization.CurrentLanguage"/> if available.
        /// Gets first value of the asset if application is not playing.
        /// </summary>
        /// <seealso cref="Application.isPlaying"/>
        public T Value
        {
            get
            {
                var value = default(T);
                var isValueSet = false;
#if UNITY_EDITOR
                if (Application.isPlaying)
                {
#endif
                    isValueSet = TryGetLocaleValue(Localization.Instance.CurrentLanguage, out value);
#if UNITY_EDITOR
                }
                else
                {
                    // Get default language from settings if is not in Play mode.
                    var localizationSettings = LocalizationSettings.Instance;
                    if (localizationSettings.AvailableLanguages.Any())
                    {
                        isValueSet = TryGetLocaleValue(localizationSettings.AvailableLanguages.First(), out value);
                    }    
                }
#endif
                return isValueSet ? value : FirstValue;
            }
        }

        /// <summary>
        /// Gets the first locale value of the asset.
        /// </summary>
        public T FirstValue
        {
            get
            {
                var localeItem = TypedLocaleItems.FirstOrDefault();
                return localeItem != null ? localeItem.Value : default(T);
            }
        }

        /// <summary>
        /// Returns the language given is whether exist or not.
        /// </summary>
        public bool HasLocale(Language language)
        {
            return LocaleItems.Any(x => x.Language == language);
        }

        /// <summary>
        /// Returns localized text regarding to language given; otherwise, null.
        /// </summary>
        /// <returns>Localized text.</returns>
        [Obsolete("Use TryGetLocaleValue()", error: true)]
        public T GetLocaleValue(Language language)
        {
            T value;
            if (TryGetLocaleValue(language, out value))
            {
                return value;
            }

            return default(T);
        }

        /// <summary>
        /// Gets localized value if exist regarding to given language.
        /// </summary>
        /// <returns>True if exist; otherwise False</returns>
        public bool TryGetLocaleValue(Language language, out T value)
        {
            var index = Array.FindIndex(TypedLocaleItems, x => x.Language == language);
            if (index >= 0)
            {
                value = TypedLocaleItems[index].Value;
                return true;
            }

            value = default(T);
            return false;
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