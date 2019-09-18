// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;

namespace GameToolkit.Localization.Editor
{
    /// <summary>
    /// Refreshes <see cref="LocalizationWindow"> if opened.
    /// </summary>
    public class LocaleAssetPostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
                                                   string[] movedAssets, string[] movedFromAssetPaths)
        {
            var localizationWindow = LocalizationWindow.Instance;
            if (localizationWindow)
            {
                localizationWindow.Refresh();
            }
        }
    }
}
