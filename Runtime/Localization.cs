// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using UnityEngine;

namespace GameToolkit.Localization
{
    /// <summary>
    /// Localization manager.
    /// </summary>
    public sealed class Localization
    {
        internal const string HelpUrl = "https://hibrahimpenekli.gitbook.io/gametoolkit-localization";
        
        private static Localization s_Instance;

        // Current language.
        private Language m_CurrentLanguage = Language.English;

        /// <summary>
        /// Gets and sets the current language.
        /// </summary>
        public Language CurrentLanguage
        {
            get { return m_CurrentLanguage; }
            set
            {
                if (m_CurrentLanguage != value)
                {
                    var oldValue = m_CurrentLanguage;
                    var newValue = value;
                    m_CurrentLanguage = newValue;
                    OnLocaleChanged(new LocaleChangedEventArgs(oldValue, newValue));
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="Localization"/> instance.
        /// </summary>
        public static Localization Instance
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    Debug.LogError("Localization instance is only available when application is playing.");
                    return null;
                }
#endif
                if (s_Instance == null)
                {
                    s_Instance = new Localization();
                    s_Instance.SetDefaultLanguage();
                }

                return s_Instance;
            }
        }

        /// <summary>
        /// Sets the <see cref="CurrentLanguage"/> as <see cref="Application.systemLanguage"/>.
        /// </summary>
        public void SetSystemLanguage()
        {
            CurrentLanguage = Application.systemLanguage;
        }

        /// <summary>
        /// Sets the <see cref="CurrentLanguage"/> to default language defined in <see cref="LocalizationSettings"/>.
        /// </summary>
        public void SetDefaultLanguage()
        {
            CurrentLanguage = LocalizationSettings.Instance.AvailableLanguages.FirstOrDefault();
        }

        /// <summary>
        /// Finds all localized assets with type given. Finds all assets in the project if in Editor; otherwise,
        /// finds only that loaded in memory.
        /// </summary>
        /// <returns>Array of specified localized assets.</returns>
        public static T[] FindAllLocalizedAssets<T>() where T : LocalizedAssetBase
        {
#if UNITY_EDITOR
            var guids = UnityEditor.AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            var assets = new T[guids.Length];
            for (var i = 0; i < guids.Length; ++i)
            {
                var assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
                assets[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(assetPath);
                Debug.Assert(assets[i]);
            }

            return assets;
#else
            return Resources.FindObjectsOfTypeAll<T>();
#endif
        }

        /// <summary>
        /// Finds all localized assets.
        /// </summary>
        /// <seealso cref="FindAllLocalizedAssets{T}"/>
        /// <returns>Array of localized assets.</returns>
        public static LocalizedAssetBase[] FindAllLocalizedAssets()
        {
            return FindAllLocalizedAssets<LocalizedAssetBase>();
        }

        /// <summary>
        /// Returns the <see href="https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes">iso-639-1</see> code for the 
        /// specified <paramref name="language"/>.
        /// </summary>
        /// <param name="language">Specified language.</param>
        /// <returns>ISO-639-1 code.</returns>
        [Obsolete("Use Language.Code property.")]
        public static string GetLanguageCode(Language language)
        {
            return language.Code;
        }

        /// <summary>
        /// Raises the <see cref="LocaleChanged"/> event.
        /// </summary>
        /// <param name="e"><see cref="LocaleChangedEventArgs"/></param>
        private void OnLocaleChanged(LocaleChangedEventArgs e)
        {
            var handler = LocaleChanged;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raised when <see cref="CurrentLanguage"/> has been changed. 
        /// </summary>
        public event EventHandler<LocaleChangedEventArgs> LocaleChanged;
    }

    /// <summary>
    /// Provides the previous language and the new language for the  locale changed event.
    /// </summary>
    public class LocaleChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Previous language.
        /// </summary>
        public Language PreviousLanguage { get; private set; }

        /// <summary>
        /// Current language.
        /// </summary>
        public Language CurrentLanguage { get; private set; }

        public LocaleChangedEventArgs(Language previousLanguage, Language currentLanguage)
        {
            PreviousLanguage = previousLanguage;
            CurrentLanguage = currentLanguage;
        }
    }
}