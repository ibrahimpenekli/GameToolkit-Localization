// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace GameToolkit.Localization.Editor.Serialization
{
    public class CsvLocalizationSerialization : ILocalizationSerialization
    {
        private const string KeyColumn = "Key";
        
        public void Serialize(Stream stream)
        {
            var languages = LocalizationSettings.Instance.AvailableLanguages;
            var localizedTexts = Localization.FindAllLocalizedAssets<LocalizedText>();

            using (var writer = new StreamWriter(stream))
            {
                // Write key column.
                writer.Write(DoubleQuote(KeyColumn));

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
            var importLocation = LocalizationSettings.Instance.ImportLocation;
            var localizedTexts = Localization.FindAllLocalizedAssets<LocalizedText>();
            
            using (var reader = new StreamReader(stream))
            {
                var languages = ReadImportLanguages(reader);

                while (!reader.EndOfStream)
                {
                    var tokens = ReadNextTokens(reader);

                    if (tokens.Length != languages.Count + 1)
                    {
                        throw new IOException("Invalid row");
                    }

                    var key = tokens[0];
                    if (string.IsNullOrEmpty(key))
                    {
                        throw new IOException("Key field must not be empty");
                    }

                    var localizedText = localizedTexts.FirstOrDefault(x => x.name == key);
                    if (localizedText == null)
                    {
                        localizedText = ScriptableObject.CreateInstance<LocalizedText>();

                        var assetPath = Path.Combine(importLocation, string.Format("{0}.asset", key));
                        AssetDatabase.CreateAsset(localizedText, assetPath);
                        AssetDatabase.SaveAssets();
                    }
                    
                    // Read languages by ignoring first column (Key).
                    for (var i = 1; i < tokens.Length; i++)
                    {
                        LocalizedAssetEditor.AddOrUpdateLocale(localizedText, languages[i - 1], tokens[i]);
                    }
                    EditorUtility.SetDirty(localizedText);
                }
            }
            
            AssetDatabase.Refresh();
        }

        private string[] ReadNextTokens(StreamReader reader)
        {
            var line = "";

            do
            {
                if (line.Length != 0)
                {
                    line += "\n";
                }
                line += reader.ReadLine();
            } while (!reader.EndOfStream && line.Length > 0 && line[line.Length - 1] != '\"');

            if (line != null)
            {
                var tokens = Regex.Split(line, @""",""");
                if (tokens.Length > 0)
                {
                    var token = tokens[0];
                    
                    if (token.Length > 0 && token[0] == '\"')
                    {
                        token = token.Remove(0, 1);
                    }
                    
                    tokens[0] = token;
                }

                if (tokens.Length > 1)
                {
                    var token = tokens[tokens.Length - 1];
                    
                    if (token.Length > 0 && token[token.Length - 1] == '\"')
                    {
                        token = token.Remove(token.Length - 1, 1);
                    }

                    tokens[tokens.Length - 1] = token;
                }

                return tokens;
            }

            return new string[0];
        }

        private List<Language> ReadImportLanguages(StreamReader reader)
        {
            var availableLanguages = LocalizationSettings.Instance.AllLanguages;
            var importLanguages = new List<Language>();
            
            var columnTokens = ReadNextTokens(reader);
            if (columnTokens.Length == 0)
            {
                throw new IOException("Column size must be greater than zero");
            }
            
            // Read languages by ignoring first column (Key).
            for (var i = 1; i < columnTokens.Length; i++)
            {
                var token = columnTokens[i].Trim();
                if (token.Length == 0)
                {
                    throw new IOException("Invalid language code column");
                }

                // Read only language code.
                // Emit language name if exist.
                var tokens = token.Split(' ');
                if (tokens.Length > 0)
                {
                    token = tokens[0].Trim();
                }

                var language = availableLanguages.FirstOrDefault(x => x.Code == token);
                if (language == null)
                {
                    Debug.LogWarning("Language code (" + token + ") not exist in localization system.");
                }
                
                // Add null language as well to maintain order.
                importLanguages.Add(language);
            }

            return importLanguages;
        }
        
        private static string DoubleQuote(string s)
        {
            return string.Format("\"{0}\"", s);
        }
    }
}