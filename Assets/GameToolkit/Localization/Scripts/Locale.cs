// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace GameToolkit.Localization
{
    public interface ILocale { }

    /// <summary>
    /// Keeps the both asset and the corresponding language.
    /// </summary>
    public class Locale<T> : ILocale
    {
        [SerializeField, Tooltip("Locale language.")]
        private SystemLanguage m_Language = SystemLanguage.English;
        public SystemLanguage Language
        {
            get { return m_Language; }
            set { m_Language = value; }
        }

        [SerializeField, Tooltip("Locale value.")]
        private T m_Value;
        public T Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public Locale()
        {
            // Intentionally empty.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <param name="val"></param>
        public Locale(SystemLanguage language, T val)
        {
            m_Language = language;
            m_Value = val;
        }
    }
}
