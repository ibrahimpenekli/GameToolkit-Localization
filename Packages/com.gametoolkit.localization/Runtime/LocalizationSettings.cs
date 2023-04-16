// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameToolkit.Localization
{
    [HelpURL(Localization.HelpUrl + "/getting-started#3-localization-settings")]
    [CreateAssetMenu(fileName = "LocalizationSettings", menuName = "GameToolkit/Localization/Localization Settings",
        order = 9999)]
    public sealed class LocalizationSettings : ScriptableObject, ISerializationCallbackReceiver
    {
        internal const string ConfigName = "com.gametoolkit.localization.settings";
        private const string AssetName = "LocalizationSettings";
        private static LocalizationSettings s_Instance = null;

        [SerializeField, HideInInspector]
        private int m_Version = 0;

        // Keep it for migration.
        [SerializeField]
        private List<SystemLanguage> m_AvailableLanguages = new List<SystemLanguage>(1)
        {
            SystemLanguage.English
        };

        [SerializeField, Tooltip("Enabled languages for the application.")]
        private List<Language> m_AvailableLanguages2 = new List<Language>(1)
        {
            Language.English
        };

        [SerializeField, Tooltip("Imported text assets location.")]
        private string m_ImportLocation = "Assets";

        [Tooltip("Google Cloud authentication file.")]
        public TextAsset GoogleAuthenticationFile;

        /// <summary>
        /// Settings version related to the Localization system.
        /// </summary>
        internal int Version
        {
            get { return m_Version; }
            set { m_Version = value; }
        }

        /// <summary>
        /// Gets import location of external localization files..
        /// </summary>
        internal string ImportLocation
        {
            get { return m_ImportLocation; }
        }

        /// <summary>
        /// Gets the localization settings instance.
        /// </summary>
        public static LocalizationSettings Instance
        {
            get { return GetOrCreateSettings(); }
        }

        /// <summary>
        /// Returns the singleton of the LocalizationSettings but does not create a default one if no active settings are found.
        /// </summary>
        /// <returns></returns>
        public static LocalizationSettings GetInstanceOrNull()
        {
            if (s_Instance != null)
                return s_Instance;

            LocalizationSettings settings = null;
#if UNITY_EDITOR
            settings = ActiveSettings;

            // Try to load existing settings from older package version.
            if (settings == null)
            {
                settings = Resources.Load<LocalizationSettings>(AssetName);
                
                if (settings != null)
                {
                    ActiveSettings = settings;
                }
            }
#else
            settings = FindObjectOfType<LocalizationSettings>();
#endif
            return settings;
        }

        public static LocalizationSettings GetOrCreateSettings()
        {
            var settings = GetInstanceOrNull();
            if (settings == null)
            {
                Debug.LogWarning("Could not find localization settings. Default will be used.");

                settings = CreateInstance<LocalizationSettings>();
                settings.name = "Default Localization Settings";

#if UNITY_EDITOR
                // Saving during Awake() will crash Unity, delay saving until next editor frame.
                if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    UnityEditor.EditorApplication.delayCall += () => SaveAndSetActive(settings);
                }
                else
                {
                    SaveAndSetActive(settings);
                }
#endif
            }

            return settings;
        }
        
#if UNITY_EDITOR
        internal static LocalizationSettings ActiveSettings
        {
            get
            {
                UnityEditor.EditorBuildSettings.TryGetConfigObject(ConfigName, out LocalizationSettings settings);
                return settings;
            }
            set
            {
                if (value == null)
                {
                    UnityEditor.EditorBuildSettings.RemoveConfigObject(ConfigName);
                }
                else
                {
                    UnityEditor.EditorBuildSettings.AddConfigObject(ConfigName, value, true);
                }
            }
        }
        #endif

        /// <summary>
        /// Enabled languages for the application.
        /// </summary>
        public List<Language> AvailableLanguages
        {
            get { return m_AvailableLanguages2; }
        }

        /// <summary>
        /// Gets all languages that contains built-in and custom languages.
        /// </summary>
        public List<Language> AllLanguages
        {
            get
            {
                var languages = new List<Language>();
                languages.AddRange(Language.BuiltinLanguages);
                languages.AddRange(AvailableLanguages.Where(x => x.Custom));
                return languages;
            }
        }

#if UNITY_EDITOR
        private static void SaveAndSetActive(LocalizationSettings settings)
        {
            var assetPath = $"Assets/Resources/{settings.name}.asset";
            var directoryName = Path.GetDirectoryName(assetPath);
            
            if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            var uniqueAssetPath = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(assetPath);
            UnityEditor.AssetDatabase.CreateAsset(settings, uniqueAssetPath);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            
            ActiveSettings = settings;
            Debug.Log(AssetName + " has been created: " + assetPath);
        }
#endif
        
        public void OnBeforeSerialize()
        {
            // Intentionally empty.
        }

        public void OnAfterDeserialize()
        {
            // Migration from SystemLanguage to Language.
            if (m_AvailableLanguages.Any())
            {
                foreach (var availableLanguage in m_AvailableLanguages)
                {
                    m_AvailableLanguages2.Add(availableLanguage);
                }

                m_AvailableLanguages.Clear();
            }
        }
    }
}