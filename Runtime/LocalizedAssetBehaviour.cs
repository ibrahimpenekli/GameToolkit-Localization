// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace GameToolkit.Localization
{
    [ExecuteInEditMode]
    public abstract class LocalizedAssetBehaviour : MonoBehaviour
    {
        protected const string ComponentMenuRoot = "Localization/";

        /// <summary>
        /// Component value is updated with the localized asset value.
        /// </summary>
        protected abstract void UpdateComponentValue();

        protected virtual void OnEnable()
        {
            UpdateComponentValue();

            if (Application.isPlaying)
            {
                Localization.Instance.LocaleChanged += Localization_LocaleChanged;
            }
        }

        protected virtual void OnDisable()
        {
            if (Application.isPlaying)
            {
                Localization.Instance.LocaleChanged -= Localization_LocaleChanged;
            }
        }

        private void OnValidate()
        {
            UpdateComponentValue();
        }

        private void Localization_LocaleChanged(object sender, LocaleChangedEventArgs e)
        {
            UpdateComponentValue();
        }
        
        /// <summary>
        /// Gets the localized value safely.
        /// </summary>
        protected static T GetValueOrDefault<T>(LocalizedAsset<T> localizedAsset) where T : class
        {
            return localizedAsset ? localizedAsset.Value : default(T);
        }
    }
}