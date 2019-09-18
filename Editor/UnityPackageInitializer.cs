// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEditor;

namespace GameToolkit.Localization.Editor
{
    [InitializeOnLoad]
    public class UnityPackageInitializer
    {
        static UnityPackageInitializer()
        {
            EditorApplication.delayCall += () =>
            {
                // This will create settings if not exist.
                var localizationSettings = LocalizationSettings.Instance;
                if (!localizationSettings)
                {
                    Debug.LogWarning("LocalizationSettings not found. Please create manually.");
                }
            };
        }
    }
}