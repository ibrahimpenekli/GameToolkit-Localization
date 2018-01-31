// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace GameToolkit.Localization.Editor
{
    public class LocalizationTreeView : TreeView
    {
        private const float RowHeight = 20f;
        private const float ToggleWidth = 18f;
        private const int FirstElementId = 1;

        // TreeView columns.
        private enum Columns
        {
            Type,
            Name,
            Language,
            Value
        }

        private int m_ElementId = FirstElementId;

        public LocalizationTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state, multiColumnHeader)
        {
            rowHeight = RowHeight;
            columnIndexForTreeFoldouts = 1;
            showAlternatingRowBackgrounds = true;
            showBorder = true;

            // Center foldout in the row since we also center content. See RowGUI.
            customFoldoutYOffset = (RowHeight - EditorGUIUtility.singleLineHeight) * 0.5f;
            multiColumnHeader.canSort = false;
            Reload();
        }

        protected override TreeViewItem BuildRoot()
        {
            m_ElementId = FirstElementId;

            // This section illustrates that IDs should be unique. The root item is required to 
            // have a depth of -1, and the rest of the items increment from that.
            var root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };

            var localizedAssets = Localization.Instance.FindAllLocalizedAssets();
            var allItems = new List<TreeViewItem>();

            // Add localized assets.
            foreach (var localizedAsset in localizedAssets)
            {
                var assetItem = new AssetTreeViewItem(0, localizedAsset);
                allItems.Add(assetItem);

                // Add locale items.
                var localItems = localizedAsset.LocaleItems;
                for (int i = 0; i < localItems.Length; i++)
                {
                    allItems.Add(new LocaleTreeViewItem(m_ElementId++, 1, localItems[i], assetItem));
                }
            }

            // Utility method that initializes the TreeViewItem.children and .parent for all items.
            SetupParentsAndChildrenFromDepths(root, allItems);
            return root;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            for (int i = 0; i < args.GetNumVisibleColumns(); ++i)
            {
                CellGUI(args.GetCellRect(i), args.item, (Columns)args.GetColumn(i), ref args);
            }
        }

        void CellGUI(Rect cellRect, TreeViewItem item, Columns column, ref RowGUIArgs args)
        {
            // Center cell rect vertically (makes it easier to place controls, icons etc in the cells).
            CenterRectUsingSingleLineHeight(ref cellRect);
            switch (column)
            {
                case Columns.Type:
                    {
                        var treeViewItem = item as AssetTreeViewItem;
                        if (treeViewItem != null)
                        {
                            Texture2D icon;
                            var valueType = treeViewItem.LocalizedAsset.ValueType;
                            if (valueType == typeof(string))
                            {
                                icon = EditorGUIUtility.FindTexture("TextAsset Icon");
                            }
                            else
                            {
                                icon = EditorGUIUtility.FindTexture(valueType.Name + " Icon");
                                if (!icon)
                                {
                                    icon = EditorGUIUtility.FindTexture("DefaultAsset Icon");
                                }
                            }
                            if (icon)
                            {
                                GUI.DrawTexture(cellRect, icon, ScaleMode.ScaleToFit);
                            }
                        }
                    }
                    break;
                case Columns.Name:
                    {
                        // Default icon and label
                        args.rowRect = cellRect;
                        base.RowGUI(args);
                    }
                    break;
                case Columns.Language:
                    {
                        var localeItem = item as LocaleTreeViewItem;
                        if (localeItem != null)
                        {
                            localeItem.LocaleItem.Language = (SystemLanguage)EditorGUI.EnumPopup(cellRect, localeItem.LocaleItem.Language);
                        }
                    }
                    break;
                case Columns.Value:
                    {
                        var treeViewItem = item as LocaleTreeViewItem;
                        if (treeViewItem != null)
                        {
                            var localeItem = treeViewItem.LocaleItem;
                            var valueType = treeViewItem.Parent.LocalizedAsset.ValueType;

                            EditorGUI.BeginChangeCheck();
                            if (valueType.IsSubclassOf(typeof(UnityEngine.Object)))
                            {
                                localeItem.ObjectValue = EditorGUI.ObjectField(cellRect, (UnityEngine.Object)localeItem.ObjectValue, localeItem.ObjectValue.GetType(), false);
                            }
                            else if (valueType == typeof(string))
                            {
                                localeItem.ObjectValue = EditorGUI.TextArea(cellRect, (string)localeItem.ObjectValue);
                            }
                            else
                            {
                                EditorGUI.LabelField(cellRect, valueType + " value type not supported.");
                            }
                            if (EditorGUI.EndChangeCheck())
                            {
                                treeViewItem.Parent.IsDirty = true;
                                EditorUtility.SetDirty(treeViewItem.Parent.LocalizedAsset);
                            }

                        }
                    }
                    break;
            }
        }

        protected override bool CanRename(TreeViewItem item)
        {
            if (item is AssetTreeViewItem)
            {
                // Only allow rename if we can show the rename overlay with a certain width (label might be clipped
                // by other columns).
                var renameRect = GetRenameRect(treeViewRect, 0, item);
                return renameRect.width > 30;
            }
            return false;
        }

        protected override void RenameEnded(RenameEndedArgs args)
        {
            // Set the backend name and reload the tree to reflect the new model
            if (args.acceptedRename)
            {
                var item = FindItem(args.itemID, rootItem) as AssetTreeViewItem;
                if (item != null)
                {
                    var assetPath = AssetDatabase.GetAssetPath(item.LocalizedAsset.GetInstanceID());
                    AssetDatabase.RenameAsset(assetPath, args.newName);
                    AssetDatabase.SaveAssets();
                    Reload();
                }
            }
        }

        public TreeViewItem GetSelectedItem()
        {
            var selection = GetSelection();
            if (selection.Count > 0)
            {
                return FindItem(selection[0], rootItem);
            }
            return null;
        }

        protected override Rect GetRenameRect(Rect rowRect, int row, TreeViewItem item)
        {
            var cellRect = GetCellRectForTreeFoldouts(rowRect);
            CenterRectUsingSingleLineHeight(ref cellRect);
            return base.GetRenameRect(cellRect, row, item);
        }

        public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(float treeViewWidth)
        {
            var typeColumnWidth = 30;
            var nameColumnWidth = 150;
            var languageColumnWidth = 110;
            var valueColumnWidth = treeViewWidth - typeColumnWidth - nameColumnWidth - languageColumnWidth;
            var columns = new[]
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(EditorGUIUtility.FindTexture("FilterByType"), "Localized asset type."),
                    contextMenuText = "Type",
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = typeColumnWidth,
                    minWidth = 30,
                    maxWidth = 60,
                    autoResize = false,
                    allowToggleVisibility = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Name", "Localized asset name."),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = nameColumnWidth,
                    minWidth = 60,
                    autoResize = false,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Language", "Locale language."),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = languageColumnWidth,
                    minWidth = 35,
                    autoResize = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Value", "Locale value."),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Left,
                    width = valueColumnWidth,
                    minWidth = 60,
                    autoResize = true
                }
            };

            Assert.AreEqual(columns.Length, Enum.GetValues(typeof(Columns)).Length,
                            "Number of columns should match number of enum values: You probably forgot to update one of them.");

            var state = new MultiColumnHeaderState(columns);
            return state;
        }
    }
}
