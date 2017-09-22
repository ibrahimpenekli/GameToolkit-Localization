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
        private static Localization s_Instance;

        // Current language.
        private SystemLanguage m_CurrentLanguage = SystemLanguage.English;

        /// <summary>
        /// Gets and sets the current language.
        /// </summary>
        public SystemLanguage CurrentLanguage
        {
            get { return m_CurrentLanguage; }
            set
            {
                if (m_CurrentLanguage != value)
                {
                    var previousLanguage = m_CurrentLanguage;
                    m_CurrentLanguage = value;
                    OnLocaleChanged(new LocaleChangedEventArgs(previousLanguage, m_CurrentLanguage));
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
        public T[] FindAllLocalizedAssets<T>() where T : LocalizedAssetBase
        {
#if UNITY_EDITOR
            var guids = UnityEditor.AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            var assets = new T[guids.Length];
            for (int i = 0; i < guids.Length; ++i)
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
        public LocalizedAssetBase[] FindAllLocalizedAssets()
        {
            return FindAllLocalizedAssets<LocalizedAssetBase>();
        }

        /// <summary>
        /// Returns the <see href="https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes">iso-639-1</see> code for the 
        /// specified <paramref name="language"/>.
        /// </summary>
        /// <param name="language">Specified language.</param>
        /// <returns>Two-chararacters iso-639-1 code.</returns>
        public static string GetLanguageCode(SystemLanguage language)
        {
            switch (language)
            {
                case SystemLanguage.Afrikaans: return "af";
                case SystemLanguage.Arabic: return "ar";
                case SystemLanguage.Basque: return "eu";
                case SystemLanguage.Belarusian: return "be";
                case SystemLanguage.Bulgarian: return "bg";
                case SystemLanguage.Catalan: return "ca";
                case SystemLanguage.Chinese: return "zh";
                case SystemLanguage.Czech: return "cs";
                case SystemLanguage.Danish: return "da";
                case SystemLanguage.Dutch: return "nl";
                case SystemLanguage.English: return "en";
                case SystemLanguage.Estonian: return "et";
                case SystemLanguage.Faroese: return "fo";
                case SystemLanguage.Finnish: return "fi";
                case SystemLanguage.French: return "fr";
                case SystemLanguage.German: return "de";
                case SystemLanguage.Greek: return "el";
                case SystemLanguage.Hebrew: return "he";
                case SystemLanguage.Hungarian: return "hu";
                case SystemLanguage.Icelandic: return "is";
                case SystemLanguage.Indonesian: return "id";
                case SystemLanguage.Italian: return "it";
                case SystemLanguage.Japanese: return "ja";
                case SystemLanguage.Korean: return "ko";
                case SystemLanguage.Latvian: return "lv";
                case SystemLanguage.Lithuanian: return "lt";
                case SystemLanguage.Norwegian: return "no";
                case SystemLanguage.Polish: return "pl";
                case SystemLanguage.Portuguese: return "pt";
                case SystemLanguage.Romanian: return "ro";
                case SystemLanguage.Russian: return "ru";
                case SystemLanguage.SerboCroatian: return "hr";
                case SystemLanguage.Slovak: return "sk";
                case SystemLanguage.Slovenian: return "sl";
                case SystemLanguage.Spanish: return "es";
                case SystemLanguage.Swedish: return "sv";
                case SystemLanguage.Thai: return "th";
                case SystemLanguage.Turkish: return "tr";
                case SystemLanguage.Ukrainian: return "uk";
                case SystemLanguage.Vietnamese: return "vi";
                case SystemLanguage.ChineseSimplified: return "zh";
                case SystemLanguage.ChineseTraditional: return "zh";

                default:
                case SystemLanguage.Unknown: return "";
            }
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
        public SystemLanguage PreviousLanguage { get; private set; }

        /// <summary>
        /// Current language.
        /// </summary>
        public SystemLanguage CurrentLanguage { get; private set; }

        public LocaleChangedEventArgs(SystemLanguage previousLanguage, SystemLanguage currentLanguage)
        {
            PreviousLanguage = previousLanguage;
            CurrentLanguage = currentLanguage;
        }
    }
}
