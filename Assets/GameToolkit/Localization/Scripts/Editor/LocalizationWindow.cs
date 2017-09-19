// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace GameToolkit.Localization.Editor
{
    public class LocalizationWindow : EditorWindow
    {
        private const string ParentMenu = "GameToolkit/Localization/";
        private const string WindowName = "Localization Explorer";

        [NonSerialized]
        private bool m_Initialized;

        [SerializeField]
        private TreeViewState m_TreeViewState; // Serialized in the window layout file so it survives assembly reloading

        [SerializeField]
        private MultiColumnHeaderState m_MultiColumnHeaderState;

        private SearchField m_SearchField;
        private LocalizationTreeView m_TreeView;

        Rect ToolbarRect { get { return new Rect(20f, 10f, position.width - 40f, 20f); } }
        Rect BodyViewRect { get { return new Rect(20, 30, position.width - 40, position.height - 60); } }
        Rect BottomToolbarRect { get { return new Rect(20f, position.height - 18f, position.width - 40f, 16f); } }

        [MenuItem(ParentMenu + WindowName)]
        public static LocalizationWindow GetWindow()
        {
            var window = GetWindow<LocalizationWindow>();
            window.titleContent = new GUIContent(WindowName);
            window.Focus();
            window.Repaint();
            return window;
        }

        private void Awake()
        {
            m_Initialized = false;
        }

        private void OnProjectChange()
        {
            m_Initialized = false;
        }

        private void InitializeIfNeeded()
        {
            if (!m_Initialized)
            {
                // Check if it already exists (deserialized from window layout file or scriptable object)
                if (m_TreeViewState == null)
                {
                    m_TreeViewState = new TreeViewState();
                }

                bool firstInit = m_MultiColumnHeaderState == null;
                var headerState = LocalizationTreeView.CreateDefaultMultiColumnHeaderState(BodyViewRect.width);
                if (MultiColumnHeaderState.CanOverwriteSerializedFields(m_MultiColumnHeaderState, headerState))
                {
                    MultiColumnHeaderState.OverwriteSerializedFields(m_MultiColumnHeaderState, headerState);
                }
                m_MultiColumnHeaderState = headerState;

                var multiColumnHeader = new MultiColumnHeader(headerState);
                if (firstInit)
                {
                    multiColumnHeader.ResizeToFit();
                }

                m_TreeView = new LocalizationTreeView(m_TreeViewState, multiColumnHeader);

                m_SearchField = new SearchField();
                m_SearchField.downOrUpArrowKeyPressed += m_TreeView.SetFocusAndEnsureSelectedItem;

                m_Initialized = true;
            }
        }

        private void OnGUI()
        {
            InitializeIfNeeded();

            SearchBarView(ToolbarRect);
            BodyView(BodyViewRect);
            BottomToolbarView(BottomToolbarRect);
        }

        private void BodyView(Rect rect)
        {
            m_TreeView.OnGUI(rect);
        }

        private void SearchBarView(Rect rect)
        {
            m_TreeView.searchString = m_SearchField.OnGUI(rect, m_TreeView.searchString);
        }

        private void BottomToolbarView(Rect rect)
        {
            GUILayout.BeginArea(rect);

            using (new EditorGUILayout.HorizontalScope())
            {
                TreeViewControls();
                LocalizedAssetControls();
                GUILayout.FlexibleSpace();
                LocaleItemControls();
            }

            GUILayout.EndArea();
        }

        private void TreeViewControls()
        {
            if (GUILayout.Button("Expand All", "miniButtonLeft"))
            {
                m_TreeView.ExpandAll();
            }

            if (GUILayout.Button("Collapse All", "miniButtonRight"))
            {
                m_TreeView.CollapseAll();
            }
        }

        private void LocalizedAssetControls()
        {
            if (GUILayout.Button(new GUIContent("Create", "Create a new localized asset."), "miniButtonLeft"))
            {
                var mousePosition = Event.current.mousePosition;
                var popupPosition = new Rect(mousePosition.x, mousePosition.y, 0, 0);
                EditorUtility.DisplayPopupMenu(popupPosition, "Assets/Create/GameToolkit/Localization/", null);
            }

            var selectedItem = m_TreeView.GetSelectedItem() as AssetTreeViewItem;
            GUI.enabled = selectedItem != null;

            if (GUILayout.Button(new GUIContent("Rename", "Rename the selected localized asset."), "miniButtonMid"))
            {
                m_TreeView.BeginRename(selectedItem);
            }
            if (GUILayout.Button(new GUIContent("Delete", "Delete the selected localized asset."), "miniButtonRight"))
            {
                var assetPath = AssetDatabase.GetAssetPath(selectedItem.LocalizedAsset.GetInstanceID());
                AssetDatabase.DeleteAsset(assetPath);
            }
            GUI.enabled = true;
        }

        private void LocaleItemControls()
        {
            var selectedItem = m_TreeView.GetSelectedItem();
            if (selectedItem != null)
            {
                var assetItem = selectedItem as AssetTreeViewItem;
                if (assetItem == null)
                {
                    assetItem = ((LocaleTreeViewItem)selectedItem).Parent;
                }

                GUI.enabled = assetItem != null;
                if (GUILayout.Button(new GUIContent("+", "Adds locale for selected asset."), "miniButtonLeft"))
                {
                    var serializedObject = new SerializedObject(assetItem.LocalizedAsset);
                    serializedObject.Update();
                    var elements = serializedObject.FindProperty("m_LocaleItems");
                    if (elements != null)
                    {
                        elements.arraySize += 1;
                        serializedObject.ApplyModifiedProperties();
                        m_TreeView.Reload();
                    }
                }

                var localeItem = selectedItem as LocaleTreeViewItem;
                GUI.enabled = localeItem != null;
                if (GUILayout.Button(new GUIContent("-", "Removes selected locale."), "miniButtonRight"))
                {
                    var serializedObject = new SerializedObject(assetItem.LocalizedAsset);
                    serializedObject.Update();
                    var elements = serializedObject.FindProperty("m_LocaleItems");
                    if (elements != null && elements.arraySize > 1)
                    {
                        var localizedAsset = assetItem.LocalizedAsset;
                        var localeItemIndex = Array.IndexOf(localizedAsset.LocaleItems, localeItem.LocaleItem);
                        elements.DeleteArrayElementAtIndex(localeItemIndex);
                        serializedObject.ApplyModifiedProperties();
                        m_TreeView.Reload();
                    }
                }
                GUI.enabled = true;
            }
        }
    }
}
