// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace GameToolkit.Localization
{
    public interface ILocalizable
    {
        /// <summary>
        /// Gets or sets the language of the locale.
        /// </summary>
        SystemLanguage Language { get; set; }
    }
}