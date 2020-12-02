// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace GameToolkit.Localization.Utilities
{
    [Serializable]
    public class GoogleTranslateResponse
    {
        public string TranslatedText;
        public string DetectedSourceLanguage;

        public GoogleTranslateResponse()
        {
        }

        public GoogleTranslateResponse(string translatedText)
        {
            TranslatedText = translatedText;
        }
    }
}
