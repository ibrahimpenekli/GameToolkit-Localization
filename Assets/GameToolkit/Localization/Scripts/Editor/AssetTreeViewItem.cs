// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor.IMGUI.Controls;

namespace GameToolkit.Localization.Editor
{
    class AssetTreeViewItem : TreeViewItem
    {
        public LocalizedAssetBase LocalizedAsset { get; private set; }

        public AssetTreeViewItem(int depth, LocalizedAssetBase data) : base(data.GetInstanceID(), depth, data.name)
        {
            LocalizedAsset = data;
        }
    }
}
