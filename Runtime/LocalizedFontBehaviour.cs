﻿// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.UI;

namespace GameToolkit.Localization
{
    [AddComponentMenu(ComponentMenuRoot + "Localized Font")]
    public class LocalizedFontBehaviour : LocalizedGenericAssetBehaviour<LocalizedFont, Font>
    {
        private void Reset()
        {
            TrySetComponentAndPropertyIfNotSet<Text>("font");
            TrySetComponentAndPropertyIfNotSet<TextMesh>("font");
        }
    }
}
