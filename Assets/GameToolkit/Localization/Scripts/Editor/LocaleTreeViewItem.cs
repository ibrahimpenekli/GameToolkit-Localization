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