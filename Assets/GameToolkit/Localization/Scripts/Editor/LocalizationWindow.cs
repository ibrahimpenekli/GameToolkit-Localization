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
        private const float Padding = 12f;
        private static LocalizationWindow s_Instance;

        [NonSerialized]
        private bool m_Initialized;

        [SerializeField]
        private TreeViewState m_TreeViewState; // Serialized in the window layout file so it survives assembly reloading

        [SerializeField]
        private MultiColumnHeaderState m_MultiColumnHeaderState;

        private SearchField m_SearchField;
        private LocalizationTreeView m_TreeView;
        private GUIStyle m_BottomToolbarBackgroundStyle;
        private GUIStyle m_BottomToolbarStyle;

        private Rect ToolbarRect
        {
            get { return new Rect(Padding, 10, position.width - 2 * Padding, 20); }
        }

        private Rect BodyViewRect
        {
            get { return new Rect(Padding, 30, position.width - 2 * Padding, position.height - 58); }
        }

        private Rect BottomToolbarRect
        {
            get { return new Rect(Padding, position.height - 24, position.width - 2 * Padding, 20); }
        }

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
            OnContextMenu(rect);
        }

        private void SearchBarView(Rect rect)
        {
            m_TreeView.searchString = m_SearchField.OnGUI(rect, m_TreeView.searchString);
        }

        private void BottomToolbarView(Rect rect)
        {
            if (m_BottomToolbarBackgroundStyle == null)
            {
                m_BottomToolbarBackgroundStyle = new GUIStyle(GUI.skin.box);
                m_BottomToolbarBackgroundStyle.normal.background = MakeSingleColorTexture(2, 2, new Color(0.55f, 0.55f, 0.55f, 1f));
            }

            if (m_BottomToolbarStyle == null)
            {
                m_BottomToolbarStyle = new GUIStyle(EditorStyles.toolbar);
                var padding = m_BottomToolbarStyle.padding;
                padding.left = 0;
                padding.right = 0;
                m_BottomToolbarStyle.padding = padding;
            }

            // Toolbar background.
            GUI.Box(new Rect(rect.x, rect.y, rect.width, rect.height - 1), GUIContent.none, m_BottomToolbarBackgroundStyle);

            // Toolbar itself.
            GUILayout.BeginArea(new Rect(rect.x, rect.y + 1, rect.width - 1, rect.height));
            using (new EditorGUILayout.HorizontalScope(m_BottomToolbarStyle))
            {
                TreeViewControls();
                LocalizedAssetControls();
                GUILayout.FlexibleSpace();
                LocaleItemControls();
            }
            GUILayout.EndArea();
        }

        private static Texture2D MakeSingleColorTexture(int width, int height, Color color)
        {
            var pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; ++i)
            {
                pixels[i] = color;
            }

            var texture = new Texture2D(width, height);
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }

        private void TreeViewControls()
        {

            if (GUILayout.Button(IconOrText("TreeEditor.Refresh", "Refresh", "Refresh the window"), EditorStyles.toolbarButton))
            {
                Refresh();
            }
        }

        private void LocalizedAssetControls()
        {
            if (GUILayout.Button(new GUIContent("Create", "Create a new localized asset."), EditorStyles.toolbarDropDown))
            {
                var mousePosition = Event.current.mousePosition;
                CreateLocalizedAssetPopup(mousePosition);
            }

            var selectedItem = m_TreeView.GetSelectedItem() as AssetTreeViewItem;
            GUI.enabled = selectedItem != null;
            if (GUILayout.Button(new GUIContent("Rename", "Rename the selected localized asset."), EditorStyles.toolbarButton))
            {
                RenameLocalizedAsset(selectedItem);
            }

            if (GUILayout.Button(new GUIContent("Delete", "Delete the selected localized asset."), EditorStyles.toolbarButton))
            {
                DeleteLocalizedAsset(selectedItem);
            }
            GUI.enabled = true;
        }

        private void CreateLocalizedAssetPopup(Vector2 mousePosition)
        {
            var popupPosition = new Rect(mousePosition, Vector2.zero);
            EditorUtility.DisplayPopupMenu(popupPosition, "Assets/Create/GameToolkit/Localization/", null);
        }

        private void RevealLocalizedAsset(AssetTreeViewItem assetTreeViewItem)
        {
            Debug.Assert(assetTreeViewItem != null);
            EditorGUIUtility.PingObject(assetTreeViewItem.LocalizedAsset);
        }

        private void RenameLocalizedAsset(AssetTreeViewItem assetTreeViewItem)
        {
            Debug.Assert(assetTreeViewItem != null);
            m_TreeView.BeginRename(assetTreeViewItem);
        }

        private void DeleteLocalizedAsset(AssetTreeViewItem assetTreeViewItem)
        {
            Debug.Assert(assetTreeViewItem != null);
            var assetPath = AssetDatabase.GetAssetPath(assetTreeViewItem.LocalizedAsset.GetInstanceID());
            AssetDatabase.DeleteAsset(assetPath);
        }

        private void OnContextMenu(Rect rect)
        {
            var currentEvent = Event.current;
            var mousePosition = currentEvent.mousePosition;
            if (rect.Contains(mousePosition) && currentEvent.type == EventType.ContextClick)
            {
                AssetTreeViewItem assetTreeViewItem;
                LocaleTreeViewItem localeTreeViewItem;
                TryGetSelectedTreeViewItem(out assetTreeViewItem, out localeTreeViewItem);

                if (assetTreeViewItem != null && localeTreeViewItem != null)
                {
                    OnLocaleItemContextMenu(assetTreeViewItem, localeTreeViewItem);
                    currentEvent.Use();
                }
                else
                {
                    OnAssetItemContextMenu(assetTreeViewItem, mousePosition);
                    currentEvent.Use();
                }
            }
        }

        private void OnAssetItemContextMenu(AssetTreeViewItem assetTreeViewItem, Vector2 mousePosition)
        {
            string itemCreate = "Create";
            string itemReveal = "Reveal";
            string itemRename = "Rename";
            string itemDelete = "Delete";

            if (Event.current != null)
            {
                mousePosition = Event.current.mousePosition;
            }
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent(itemCreate), false, AssetItemContextMenu_Create, mousePosition);

            if (assetTreeViewItem == null)
            {
                menu.AddDisabledItem(new GUIContent(itemReveal));
                menu.AddDisabledItem(new GUIContent(itemRename));
                menu.AddDisabledItem(new GUIContent(itemDelete));
            }
            else
            {
                menu.AddItem(new GUIContent(itemReveal), false, AssetItemContextMenu_Select, assetTreeViewItem);
                menu.AddItem(new GUIContent(itemRename), false, AssetItemContextMenu_Rename);
                menu.AddItem(new GUIContent(itemDelete), false, AssetItemContextMenu_Delete);
            }
            menu.ShowAsContext();
        }

        private void AssetItemContextMenu_Select(object assetTreeViewItemObject)
        {
            var assetTreeViewItem = (AssetTreeViewItem)assetTreeViewItemObject;
            RevealLocalizedAsset(assetTreeViewItem);
        }

        private void AssetItemContextMenu_Create(object mousePosition)
        {
            CreateLocalizedAssetPopup((Vector2)mousePosition);
        }

        private void AssetItemContextMenu_Rename()
        {
            AssetTreeViewItem assetTreeViewItem;
            LocaleTreeViewItem localeTreeViewItem;
            TryGetSelectedTreeViewItem(out assetTreeViewItem, out localeTreeViewItem);
            RenameLocalizedAsset(assetTreeViewItem);
        }

        private void AssetItemContextMenu_Delete()
        {
            AssetTreeViewItem assetTreeViewItem;
            LocaleTreeViewItem localeTreeViewItem;
            TryGetSelectedTreeViewItem(out assetTreeViewItem, out localeTreeViewItem);
            DeleteLocalizedAsset(assetTreeViewItem);
        }

        private void OnLocaleItemContextMenu(AssetTreeViewItem assetTreeViewItem, LocaleTreeViewItem localeTreeViewItem)
        {
            Debug.Assert(assetTreeViewItem != null);
            Debug.Assert(localeTreeViewItem != null);
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Make default"), false, LocaleItemContextMenu_MakeDefault);
            menu.AddItem(new GUIContent("Remove"), false, LocaleItemContextMenu_Remove);
            menu.ShowAsContext();
        }

        private void LocaleItemContextMenu_MakeDefault()
        {
            AssetTreeViewItem assetTreeViewItem;
            LocaleTreeViewItem localeTreeViewItem;
            TryGetSelectedTreeViewItem(out assetTreeViewItem, out localeTreeViewItem);
            MakeLocaleDefault(assetTreeViewItem.LocalizedAsset, localeTreeViewItem.LocaleItem);
        }

        private void LocaleItemContextMenu_Remove()
        {
            AssetTreeViewItem assetTreeViewItem;
            LocaleTreeViewItem localeTreeViewItem;
            TryGetSelectedTreeViewItem(out assetTreeViewItem, out localeTreeViewItem);
            RemoveLocale(assetTreeViewItem.LocalizedAsset, localeTreeViewItem.LocaleItem);
        }

        private void LocaleItemControls()
        {
            AssetTreeViewItem assetTreeViewItem;
            LocaleTreeViewItem localeTreeViewItem;
            TryGetSelectedTreeViewItem(out assetTreeViewItem, out localeTreeViewItem);

            GUI.enabled = assetTreeViewItem != null && assetTreeViewItem.LocalizedAsset.ValueType == typeof(string);
            if (GUILayout.Button(new GUIContent("Translate By", "Translate missing locales."), EditorStyles.toolbarButton))
            {
                TranslateMissingLocales(assetTreeViewItem.LocalizedAsset);
            }

            // First element is already default.
            GUI.enabled = localeTreeViewItem != null;
            if (GUILayout.Button(new GUIContent("Make Default", "Make selected locale as default."), EditorStyles.toolbarButton))
            {
                MakeLocaleDefault(assetTreeViewItem.LocalizedAsset, localeTreeViewItem.LocaleItem);
            }

            GUI.enabled = assetTreeViewItem != null;
            if (GUILayout.Button(IconOrText("Toolbar Plus", "+", "Adds locale for selected asset."), EditorStyles.toolbarButton))
            {
                AddLocale(assetTreeViewItem.LocalizedAsset);
            }

            GUI.enabled = localeTreeViewItem != null;

            if (GUILayout.Button(IconOrText("Toolbar Minus", "-", "Removes selected locale."), EditorStyles.toolbarButton))
            {
                RemoveLocale(assetTreeViewItem.LocalizedAsset, localeTreeViewItem.LocaleItem);
            }
            GUI.enabled = true;
        }

        private void TryGetSelectedTreeViewItem(out AssetTreeViewItem assetTreeViewItem,
                                                out LocaleTreeViewItem localeTreeViewItem)
        {
            var selectedItem = m_TreeView.GetSelectedItem();
            assetTreeViewItem = selectedItem as AssetTreeViewItem;
            localeTreeViewItem = selectedItem as LocaleTreeViewItem;

            if (assetTreeViewItem == null && selectedItem != null)
            {
                assetTreeViewItem = ((LocaleTreeViewItem)selectedItem).Parent;
            }
        }

        private static GUIContent IconOrText(string iconName, string text, string tooltip)
        {
            var guiContent = EditorGUIUtility.IconContent(iconName);
            if (guiContent == null)
            {
                guiContent = new GUIContent(text);
            }
            guiContent.tooltip = tooltip;
            return guiContent;
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
                var defaultLanguage = localeItem.Language;
                var localeItemIndex = Array.IndexOf(localizedAsset.LocaleItems, localeItem);
                elements.MoveArrayElement(localeItemIndex, 0);
                serializedObject.ApplyModifiedProperties();
                m_TreeView.Reload();
                Debug.Log(localizedAsset.name + ":" + defaultLanguage + " was set as the default language.");
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
