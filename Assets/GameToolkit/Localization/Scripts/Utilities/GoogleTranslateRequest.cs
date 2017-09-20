// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace GameToolkit.Localization.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class GoogleTranslateRequest
    {
        public SystemLanguage Source;
        public SystemLanguage Target;
        public string Text;

        public GoogleTranslateRequest()
        {
        }

        public GoogleTranslateRequest(SystemLanguage source, SystemLanguage target, string text)
        {
            Source = source;
            Target = target;
            Text = text;
        }
    }
}
