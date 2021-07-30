// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace GameToolkit.Localization
{
    [ExecuteInEditMode]
    [HelpURL(Localization.HelpUrl + "/localizing-components")]
    public abstract class LocalizedAssetBehaviour : MonoBehaviour
    {
        protected const string ComponentMenuRoot = "Localization/";

        // Is TryUpdateComponentLocalization() called by OnValidate() or not? 
        private bool m_IsOnValidate;
        
        /// <summary>
        /// Updates the component localization with meaningful way.
        /// </summary>
        /// <seealso cref="ComponentLocalizationChanged"/>
        public void ForceUpdateComponentLocalization()
        {
            if (TryUpdateComponentLocalization(m_IsOnValidate))
            {
                OnComponentLocalizationChanged();
            }

            m_IsOnValidate = false;
        }

        /// <summary>
        /// Updates component localization if possible.
        /// </summary>
        /// <remarks>
        /// Use <see cref="ForceUpdateComponentLocalization"/> to update component localization.
        /// Use this method to override only.
        /// </remarks>
        /// <param name="isOnValidate">This method is whether called by <see cref="OnValidate"/> or not.</param>
        /// <returns>True if component is updated successfully.</returns>
        protected abstract bool TryUpdateComponentLocalization(bool isOnValidate);
        
        protected virtual void OnEnable()
        {
            ForceUpdateComponentLocalization();

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
            m_IsOnValidate = true;
            ForceUpdateComponentLocalization();
        }

        private void Localization_LocaleChanged(object sender, LocaleChangedEventArgs e)
        {
            ForceUpdateComponentLocalization();
        }
        
        /// <summary>
        /// Gets the localized value safely.
        /// </summary>
        protected static T GetValueOrDefault<T>(LocalizedAsset<T> localizedAsset) where T : class
        {
            return localizedAsset ? localizedAsset.Value : default(T);
        }
        
        protected virtual void OnComponentLocalizationChanged()
        {
            var handler = ComponentLocalizationChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raised when component localization is updated with new value regarding to the
        /// <see cref="Localization.CurrentLanguage"/>.
        /// </summary>
        public event EventHandler<EventArgs> ComponentLocalizationChanged;
    }
}