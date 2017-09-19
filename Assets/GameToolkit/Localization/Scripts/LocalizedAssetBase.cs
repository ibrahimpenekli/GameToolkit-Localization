// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace GameToolkit.Localization
{
	/// <summary>
	/// 
	/// </summary>
    public abstract class LocalizedAssetBase : ScriptableObject
    {
        /// <summary>
        /// Gets the defined locale items of the localized asset.
        /// </summary>
        public abstract LocaleItemBase[] LocaleItems { get; }

        /// <summary>
        /// Gets the value type.
        /// </summary>
        public abstract Type ValueType { get; }
    }
}
