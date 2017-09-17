// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor.IMGUI.Controls;

namespace GameToolkit.Localization.Editor
{
    class LocaleTreeViewItem : TreeViewItem
    {
        public LocaleItemBase LocaleItem { get; private set; }

        public LocaleTreeViewItem(int id, int depth, LocaleItemBase localeItem) : base(id, depth, "")
        {
            LocaleItem = localeItem;
        }
    }
}