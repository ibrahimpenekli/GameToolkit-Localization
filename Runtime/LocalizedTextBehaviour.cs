// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameToolkit.Localization
{
    [AddComponentMenu(ComponentMenuRoot + "Localized Text")]
    public class LocalizedTextBehaviour : LocalizedGenericAssetBehaviour<LocalizedText, string>
    {
        [SerializeField]
        private string[] m_FormatArgs = new string[0];

        public string[] FormatArgs
        {
            get
            {
                return m_FormatArgs;
            }
            set
            {
                m_FormatArgs = value ?? new string[0];
                ForceUpdateComponentLocalization();
            }
        }

        protected override object GetLocalizedValue()
        {
            var value = (string)base.GetLocalizedValue();
            if (FormatArgs.Length > 0 && !string.IsNullOrEmpty(value))
            {
                return string.Format(value, FormatArgs.Cast<object>().ToArray());
            }
            return value;
        }

        private void Reset()
        {
            TrySetComponentAndPropertyIfNotSet<Text>("text");
            TrySetComponentAndPropertyIfNotSet<TextMesh>("text");
        }
    }
}