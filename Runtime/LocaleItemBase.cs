// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace GameToolkit.Localization
{
    [Serializable]
    public abstract class LocaleItemBase : ISerializationCallbackReceiver
    {
        // For backward compability.
        [SerializeField]
        private int m_Language = -1;

        [SerializeField]
        private Language m_Language2 = Language.English;
        
        /// <summary>
        /// Gets or sets the language of the locale.
        /// </summary>
        public Language Language
        {
            get { return m_Language2; }
            set { m_Language2 = value; }
        }

        /// <summary>
        /// Gets the value of the locale.
        /// </summary>
        public abstract object ObjectValue { get; set; }

        public void OnBeforeSerialize()
        {
            // Intentionally empty.
        }

        public void OnAfterDeserialize()
        {
            if (m_Language >= 0)
            {
                m_Language2 = (SystemLanguage) m_Language;
                m_Language = -1;
            }
        }
    }
}