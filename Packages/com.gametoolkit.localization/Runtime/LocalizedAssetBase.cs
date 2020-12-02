// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace GameToolkit.Localization
{
    /// <summary>
    /// Base class for all localized asset types.
    /// </summary>
    [HelpURL(Localization.HelpUrl)]
    public abstract class LocalizedAssetBase : ScriptableObject
    {
        /// <summary>
        /// Gets the read-only locale items.
        /// </summary>
        public abstract LocaleItemBase[] LocaleItems { get; }

        /// <summary>
        /// Gets the value type.
        /// </summary>
        public abstract Type ValueType { get; }
    }
}