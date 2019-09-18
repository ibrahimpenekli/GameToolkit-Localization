// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace GameToolkit.Localization.Editor
{
    /// <summary>
    /// Refreshes <see cref="LocalizationWindow"/> if opened.
    /// </summary>
    public class LocaleAssetModificationProcessor : UnityEditor.AssetModificationProcessor
    {
        private static string[] OnWillSaveAssets(string[] paths)
        {
            var localizationWindow = LocalizationWindow.Instance;
            if (localizationWindow)
            {
                /// This will reset assets <see cref="AssetTreeViewItem.IsDirty"/> flag.
                localizationWindow.Refresh();
            }
            return paths;
        }
    }
}
