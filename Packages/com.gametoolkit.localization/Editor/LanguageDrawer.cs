// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;
using UnityEngine;

namespace GameToolkit.Localization.Editor
{
    [CustomPropertyDrawer(typeof(Language))]
    public class LanguageDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            LanguageEditorUtility.LanguageField(position, property, label);
        }
    }
}