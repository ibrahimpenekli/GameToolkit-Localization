// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace GameToolkit.Localization
{
	/// <summary>
	/// 
	/// </summary>
    public abstract class LocalizedAssetBehaviour : MonoBehaviour
    {
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        protected abstract bool IsAssetExist();

		/// <summary>
		/// 
		/// </summary>
        protected abstract void UpdateComponentValue();

        protected virtual void OnEnable()
        {
            if (IsAssetExist())
            {
                UpdateComponentValue();
                Localization.Instance.LocaleChanged += Localization_LocaleChanged;
            }
            else
            {
                enabled = false;
            }
        }

        protected virtual void OnDisable()
        {
            Localization.Instance.LocaleChanged -= Localization_LocaleChanged;
        }

        private void Localization_LocaleChanged(object sender, LocaleChangedEventArgs e)
        {
            if (IsAssetExist())
            {
                UpdateComponentValue();
            }
        }
    }
}
