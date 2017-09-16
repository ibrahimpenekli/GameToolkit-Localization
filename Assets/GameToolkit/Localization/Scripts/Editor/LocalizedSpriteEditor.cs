// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace GameToolkit.Localization
{
    [CustomEditor(typeof(LocalizedSprite))]
    public class LocalizedSpriteEditor : LocalizedAssetEditor<Sprite>
    {
    }
}
