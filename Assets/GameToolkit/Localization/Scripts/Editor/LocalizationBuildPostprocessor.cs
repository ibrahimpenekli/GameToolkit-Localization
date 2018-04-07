// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace GameToolkit.Localization.Editor
{
    public class LocalizationBuildPostprocessor
    {
        private const string InfoPlistFile = "Info.plist";

        [PostProcessBuild(9999)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
        {
#if UNITY_IOS || UNITY_EDITOR
            if (buildTarget == BuildTarget.iOS)
            {
                // Continue if any localization info exists.
                var localizations = GetLocalizations();
                if (localizations.Count == 0)
                {
                    return;
                }

                var infoPList = Path.Combine(pathToBuiltProject, InfoPlistFile);
                if (!File.Exists(infoPList))
                {
                    Debug.LogError("Could not add localizations to Info.plist file: Info.plist not exist.");
                    return;
                }

                var xmlDocument = new XmlDocument();
                xmlDocument.Load(infoPList);

                var plistDictionary = xmlDocument.SelectSingleNode("plist/dict");
                if (plistDictionary == null)
                {
                    Debug.LogError("Could not add localizations to Info.plist file.");
                    return;
                }

                var localizationsKey = xmlDocument.CreateElement("key");
                localizationsKey.InnerText = "CFBundleLocalizations";
                plistDictionary.AppendChild(localizationsKey);

                // Add localizations.
                var localizationsArray = xmlDocument.CreateElement("array");
                foreach (string locale in localizations)
                {
                    var localizationElement = xmlDocument.CreateElement("string");
                    localizationElement.InnerText = locale;
                    localizationsArray.AppendChild(localizationElement);
                    Debug.Log("[LocalizationBuildPostprocessor] Localization added: " + locale);
                }

                plistDictionary.AppendChild(localizationsArray);
                xmlDocument.Save(infoPList);
            }
#endif
        }

        private static List<string> GetLocalizations()
        {
            var localizations = new List<string>();
            var localizationSettings = LocalizationSettings.Instance;
            if (localizationSettings)
            {
                foreach (var language in localizationSettings.AvailableLanguages)
                {
                    localizations.Add(Localization.GetLanguageCode(language));
                }
            }
            return localizations;
        }
    }
}
