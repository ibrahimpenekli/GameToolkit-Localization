// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEditor;

namespace GameToolkit.Localization
{
    [CustomEditor(typeof(LocalizedTexture))]
    public class LocalizedTextureEditor : LocalizedAssetEditor<Texture2D>
    {
    }
}
