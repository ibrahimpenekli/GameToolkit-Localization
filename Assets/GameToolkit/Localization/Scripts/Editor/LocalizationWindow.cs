// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using GameToolkit.Localization.Utilities;

namespace GameToolkit.Localization.Editor
{
    public class LocalizationWindow : EditorWindow
    {
        private const string WindowName = "Localization Explorer";

        private static LocalizationWindow s_Instance;

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
        Rect BottomToolbarRect { get { return new Rect(20f, position.height - 22f, position.width - 40f, 20f); } }

        [MenuItem(LocalizationEditorHelper.LocalizationMenu + WindowName)]
        public static LocalizationWindow GetWindow()
        {
            s_Instance = GetWindow<LocalizationWindow>();
            s_Instance.titleContent = new GUIContent(WindowName);
            s_Instance.Focus();
            s_Instance.Repaint();
            return s_Instance;
        }

        public static LocalizationWindow Instance
        {
            get { return s_Instance; }
        }

        private void Awake()
        {
            m_Initialized = false;
        }

        private void OnProjectChange()
        {
            m_Initialized = false;
        }

        public void Refresh()
        {
            if (m_TreeView != null)
            {
                m_TreeView.Reload();
            }
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

            if (GUILayout.Button("Refresh", "miniButtonLeft"))
            {
                Refresh();
            }

            if (GUILayout.Button("Expand All", "miniButtonMid"))
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
                var assetTreeViewItem = selectedItem as AssetTreeViewItem;
                var localeTreeViewItem = selectedItem as LocaleTreeViewItem;

                if (assetTreeViewItem == null)
                {
                    assetTreeViewItem = ((LocaleTreeViewItem)selectedItem).Parent;
                }

                GUI.enabled = assetTreeViewItem != null && assetTreeViewItem.LocalizedAsset.ValueType == typeof(string);
                if (GUILayout.Button(new GUIContent("Translate By", "Translate missing locales."), "miniButton"))
                {
                    TranslateMissingLocales(assetTreeViewItem.LocalizedAsset);
                }

                // First element is already default.
                GUI.enabled = localeTreeViewItem != null;
                if (GUILayout.Button(new GUIContent("Make Default", "Make selected locale as default."), "miniButton"))
                {
                    MakeLocaleDefault(assetTreeViewItem.LocalizedAsset, localeTreeViewItem.LocaleItem);
                }

                GUI.enabled = assetTreeViewItem != null;
                if (GUILayout.Button(new GUIContent("+", "Adds locale for selected asset."), "miniButtonLeft"))
                {
                    AddLocale(assetTreeViewItem.LocalizedAsset);
                }

                GUI.enabled = localeTreeViewItem != null;
                if (GUILayout.Button(new GUIContent("-", "Removes selected locale."), "miniButtonRight"))
                {
                    RemoveLocale(assetTreeViewItem.LocalizedAsset, localeTreeViewItem.LocaleItem);
                }
                GUI.enabled = true;
            }
        }

        private GoogleTranslator m_Translator;

        private void TranslateMissingLocales(LocalizedAssetBase localizedAsset)
        {
            m_Translator = new GoogleTranslator(LocalizationSettings.Instance.GoogleAuthenticationFile);
            var localizedText = localizedAsset as LocalizedText;
            var options = new List<GUIContent>();
            foreach (var locale in localizedText.TypedLocaleItems)
            {
                if (!string.IsNullOrEmpty(locale.Value))
                {
                    options.Add(new GUIContent(locale.Language.ToString()));
                }
            }

            var mousePosition = Event.current.mousePosition;
            var popupPosition = new Rect(mousePosition.x, mousePosition.y, 0, 0);
            EditorUtility.DisplayCustomMenu(popupPosition, options.ToArray(), -1, TranslateSelected, localizedText);
        }

        private void TranslateSelected(object userData, string[] options, int selected)
        {
            var localizedText = userData as LocalizedText;
            var selectedLanguage = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), options[selected]);
            var selectedValue = localizedText.TypedLocaleItems.FirstOrDefault(x => x.Language == selectedLanguage).Value;

            foreach (var locale in localizedText.TypedLocaleItems)
            {
                if (string.IsNullOrEmpty(locale.Value))
                {
                    m_Translator.Translate(new GoogleTranslateRequest(selectedLanguage, locale.Language, selectedValue),
                        (TranslationCompletedEventArgs e) =>
                        {
                            locale.Value = e.Responses.FirstOrDefault().TranslatedText;
                            EditorUtility.SetDirty(localizedText);
                        },
                        (TranslationErrorEventArgs e) =>
                        {
                            Debug.LogError(e.Message);
                        }
                    );
                }
            }
        }

        private void MakeLocaleDefault(LocalizedAssetBase localizedAsset, LocaleItemBase localeItem)
        {
            var serializedObject = new SerializedObject(localizedAsset);
            serializedObject.Update();
            var elements = serializedObject.FindProperty(LocalizationEditorHelper.LocalizedElementsSerializedProperty);
            if (elements != null && elements.arraySize > 1)
            {
                var localeItemIndex = Array.IndexOf(localizedAsset.LocaleItems, localeItem);
                elements.MoveArrayElement(localeItemIndex, 0);
                serializedObject.ApplyModifiedProperties();
                m_TreeView.Reload();
            }
        }

        private void AddLocale(LocalizedAssetBase localizedAsset)
        {
            var serializedObject = new SerializedObject(localizedAsset);
            serializedObject.Update();
            var elements = serializedObject.FindProperty(LocalizationEditorHelper.LocalizedElementsSerializedProperty);
            if (elements != null)
            {
                elements.arraySize += 1;
                serializedObject.ApplyModifiedProperties();
                m_TreeView.Reload();
            }
        }

        private void RemoveLocale(LocalizedAssetBase localizedAsset, LocaleItemBase localeItem)
        {
            var serializedObject = new SerializedObject(localizedAsset);
            serializedObject.Update();
            var elements = serializedObject.FindProperty(LocalizationEditorHelper.LocalizedElementsSerializedProperty);
            if (elements != null && elements.arraySize > 1)
            {
                var localeItemIndex = Array.IndexOf(localizedAsset.LocaleItems, localeItem);
                elements.DeleteArrayElementAtIndex(localeItemIndex);
                serializedObject.ApplyModifiedProperties();
                m_TreeView.Reload();
            }
        }
    }
}
