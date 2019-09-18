// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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
                if (value == null)
                {
                    m_FormatArgs = new string[0];
                }
                else
                {
                    m_FormatArgs = value;
                }
                UpdateComponentValue();
            }
        }

        protected override object GetLocalizedValue()
        {
            var value = (string)base.GetLocalizedValue();
            if (FormatArgs.Length > 0 && !string.IsNullOrEmpty(value))
            {
                return string.Format(value, FormatArgs);
            }
            return value;
        }

        private void Reset()
        {
            m_Component = GetComponent<Text>();
            if (m_Component)
            {
                m_Property = "text";
            }
        }
    }
}