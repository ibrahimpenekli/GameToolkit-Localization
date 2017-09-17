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
            Debug.Log("LocalizationWindow: Awake");
            m_Initialized = false;
        }

        private void OnProjectChange()
        {
            Debug.Log("LocalizationWindow: OnProjectChange");
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
					MultiColumnHeaderState.OverwriteSerializedFields(m_MultiColumnHeaderState, headerState);
				m_MultiColumnHeaderState = headerState;
				
				var multiColumnHeader = new MultiColumnHeader(headerState);
				if (firstInit)
				{
					multiColumnHeader.ResizeToFit();
				}
					

				//var treeModel = new TreeModel<MyTreeElement>(GetData());
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
            m_SearchField.OnGUI(rect, "");
        }

        private void BottomToolbarView(Rect rect)
        {
            GUILayout.BeginArea(rect);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Expand All", "miniButtonLeft"))
                {
                    m_TreeView.ExpandAll();
                }

                if (GUILayout.Button("Collapse All", "miniButtonRight"))
                {
                    m_TreeView.CollapseAll();
                }

                GUILayout.FlexibleSpace();
            }

            GUILayout.EndArea();
        }
    }
}
