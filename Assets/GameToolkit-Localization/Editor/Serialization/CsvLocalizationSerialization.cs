// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.IO;

namespace GameToolkit.Localization.Editor.Serialization
{
    public class CsvLocalizationSerialization : ILocalizationSerialization
    {
        public void Serialize(Stream stream)
        {
            var languages = LocalizationSettings.Instance.AvailableLanguages;
            var localizedTexts = Localization.FindAllLocalizedAssets<LocalizedText>();

            using (var writer = new StreamWriter(stream))
            {
                // Write key column.
                writer.Write(DoubleQuote("Key"));

                if (languages.Count > 0)
                {
                    writer.Write(",");    
                }
                
                // Write used language columns.
                for (var i = 0; i < languages.Count; i++)
                {
                    writer.Write(DoubleQuote("{0} ({1})"), languages[i].Code, languages[i].Name);

                    if (i != languages.Count - 1)
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine();
                
                // Write localized assets.
                foreach (var localizedText in localizedTexts)
                {
                    // Write key.
                    writer.Write(DoubleQuote(localizedText.name));
                    
                    if (languages.Count > 0)
                    {
                        writer.Write(",");    
                    }
                    
                    for (var i = 0; i < languages.Count; i++)
                    {
                        string value;
                        if (!localizedText.TryGetLocaleValue(languages[i], out value))
                        {
                            value = "";
                        }
                        
                        writer.Write(DoubleQuote(value));

                        if (i != languages.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    
                    writer.WriteLine();
                }
            }
        }

        public void Deserialize(Stream stream)
        {
        }
        
        private static string DoubleQuote(string s)
        {
            return string.Format("\"{0}\"", s);
        }
    }
}