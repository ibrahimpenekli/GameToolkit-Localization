// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace GameToolkit.Localization
{
    /// <summary>
    /// 
    /// </summary>
    [ExecuteInEditMode]
    public abstract class LocalizedAssetBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        protected abstract void UpdateComponentValue();

        protected virtual void OnEnable()
        {
            UpdateComponentValue();
            Localization.Instance.LocaleChanged += Localization_LocaleChanged;
        }

        protected virtual void OnDisable()
        {
            Localization.Instance.LocaleChanged -= Localization_LocaleChanged;
        }

        private void OnValidate()
        {
            UpdateComponentValue();
        }

        private void Localization_LocaleChanged(object sender, LocaleChangedEventArgs e)
        {
            UpdateComponentValue();
        }
    }
}
