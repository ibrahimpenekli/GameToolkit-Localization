﻿// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.UI;

namespace GameToolkit.Localization
{
    [AddComponentMenu(ComponentMenuRoot + "Localized Texture")]
    public class LocalizedTextureBehaviour : LocalizedGenericAssetBehaviour<LocalizedTexture, Texture>
    {
        private void Reset()
        {
            TrySetComponentAndPropertyIfNotSet<RawImage>("texture");
        }
    }
}
