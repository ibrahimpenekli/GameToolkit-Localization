// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameToolkit.Localization.Utilities
{
    public class GoogleTranslator
    {
        private const string RequestUrlFormat = "https://translation.googleapis.com/language/translate/v2?key={0}";
        private const string RequestKeyInputText = "q";
        private const string RequestKeySourceLanguage = "source";
        private const string RequestKeyTargetLanguage = "target";

        /// <summary>
        /// Gets or sets the google cloud API key.
        /// </summary>
        /// <seealso cref="https://cloud.google.com/docs/authentication/api-keys"/>
        public TextAsset AuthenticationFile { get; set; }

        public GoogleTranslator(TextAsset authFile)
        {
            AuthenticationFile = authFile;
        }

        public IEnumerator Translate(GoogleTranslateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (!AuthenticationFile)
            {
                throw new ArgumentNullException("AuthenticationFile", "Auth file not attached.");
            }

            var form = new WWWForm();
            form.AddField(RequestKeyInputText, request.Text);
            form.AddField(RequestKeySourceLanguage, Localization.GetLanguageCode(request.Source));
            form.AddField(RequestKeyTargetLanguage, Localization.GetLanguageCode(request.Target));

            var url = string.Format(RequestUrlFormat, AuthenticationFile.text);
            WWW www = new WWW(url, form);
            yield return www;

            if (www.error == null)
            {
                var response = JsonUtility.FromJson<JsonResponse>(www.text);
                if (response != null && response.data != null && response.data.translations != null &&
                    response.data.translations.Length > 0)
                {
                    var requests = new GoogleTranslateRequest[] { request };

                    var translateResponse = new GoogleTranslateResponse();
                    translateResponse.TranslatedText = response.data.translations[0].translatedText;
                    var responses = new GoogleTranslateResponse[] { translateResponse };

                    OnTranslationCompleted(new TranslationCompletedEventArgs(requests, responses));
                }
                else
                {
                    OnTranslationError(new TranslationErrorEventArgs("Response data could not be read."));
                }
            }
            else
            {
                OnTranslationError(new TranslationErrorEventArgs(www.error));
            }
        }

        // TODO: Implement.
        public IEnumerator Translate(IEnumerable<GoogleTranslateRequest> requests)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Raises the <see cref="TranslationError"/> event.
        /// </summary>
        /// <param name="e"><see cref="TranslationErrorEventArgs"/></param>
        private void OnTranslationError(TranslationErrorEventArgs e)
        {
            var handler = TranslationError;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="TranslationCompleted"/> event.
        /// </summary>
        /// <param name="e"><see cref="TranslationCompletedEventArgs"/></param>
        private void OnTranslationCompleted(TranslationCompletedEventArgs e)
        {
            var handler = TranslationCompleted;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }

        [Serializable]
        private class JsonResponse
        {
            public JsonData data;
        }

        [Serializable]
        private class JsonData
        {
            public JsonTranslation[] translations;
        }

        [Serializable]
        private class JsonTranslation
        {
            public string translatedText;
            public string detectedSourceLanguage;
        }

        /// <summary>
        /// Raised when translation requests performed. 
        /// </summary>
        public event EventHandler<TranslationCompletedEventArgs> TranslationCompleted;

        /// <summary>
        /// Raised when translation error ocurred.
        /// </summary>
        public event EventHandler<TranslationErrorEventArgs> TranslationError;
    }

    /// <summary>
    /// Provides the requests and translation responses.
    /// </summary>
    public class TranslationCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Translate requests.
        /// </summary>
        public GoogleTranslateRequest[] Requests { get; private set; }

        /// <summary>
        /// Translate responses.
        /// </summary>
        public GoogleTranslateResponse[] Responses { get; private set; }

        public TranslationCompletedEventArgs(GoogleTranslateRequest[] requests,
                                              GoogleTranslateResponse[] responses)
        {
            Debug.Assert(requests != null);
            Debug.Assert(responses != null);
            Requests = requests;
            Responses = responses;
        }
    }

    /// <summary>
    /// Provides detailed information upon translation errors.
    /// </summary>
    public class TranslationErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; private set; }

        public TranslationErrorEventArgs(string message)
        {
            Message = message;
        }
    }
}
