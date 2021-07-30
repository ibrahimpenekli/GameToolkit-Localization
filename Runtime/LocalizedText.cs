// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameToolkit.Localization
{
    [CreateAssetMenu(fileName = "LocalizedText", menuName = "GameToolkit/Localization/Text")]
    public class LocalizedText : LocalizedAsset<string>
    {
        [Serializable]
        private class TextLocaleItem : LocaleItem<string> { };

        [SerializeField]
        private TextLocaleItem[] m_LocaleItems = new TextLocaleItem[1];

        public override LocaleItemBase[] LocaleItems { get { return m_LocaleItems; } }
        
        /// <summary>
        /// Sets locale items in Editor or Playmode.
        /// </summary>
        /// 
        /// <example>
        /// This shows how to create <see cref="LocalizedText"/> at runtime.
        /// <code>
        /// var localizedText = ScriptableObject.CreateInstance&lt;LocalizedText&gt;();
        /// localizedText.SetLocaleItems(new[]
        /// {
        ///     new LocaleItem&lt;string&gt;(Language.English, "Hi! This text is created at runtime."),
        ///     new LocaleItem&lt;string&gt;(Language.Turkish, "Merhaba! Bu metin çalışma zamanı oluşturulmuştur.")
        /// });
        /// </code>
        /// </example>
        public void SetLocaleItems(IEnumerable<LocaleItem<string>> localeItems)
        {
            if (localeItems == null)
                throw new ArgumentNullException(nameof(localeItems));

            m_LocaleItems = localeItems.Select(x => new TextLocaleItem()
            {
                Language = x.Language,
                Value = x.Value
            }).ToArray();
        }
    }
}
