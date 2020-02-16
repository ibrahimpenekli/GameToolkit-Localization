// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace GameToolkit.Localization
{
    [Serializable]
    public class Language : IEquatable<Language>
    {
        public static readonly Language Afrikaans = new Language("Afrikaans", "af");
        public static readonly Language Arabic = new Language("Arabic", "ar");
        public static readonly Language Basque = new Language("Basque", "eu");
        public static readonly Language Belarusian = new Language("Belarusian", "be");
        public static readonly Language Bulgarian = new Language("Bulgarian", "bg");
        public static readonly Language Catalan = new Language("Catalan", "ca");
        public static readonly Language Chinese = new Language("Chinese", "zh");
        public static readonly Language Czech = new Language("Czech", "cs");
        public static readonly Language Danish = new Language("Danish", "da");
        public static readonly Language Dutch = new Language("Dutch", "nl");
        public static readonly Language English = new Language("English", "en");
        public static readonly Language Estonian = new Language("Estonian", "et");
        public static readonly Language Faroese = new Language("Faroese", "fo");
        public static readonly Language Finnish = new Language("Finnish", "fi");
        public static readonly Language French = new Language("French", "fr");
        public static readonly Language German = new Language("German", "de");
        public static readonly Language Greek = new Language("Greek", "el");
        public static readonly Language Hebrew = new Language("Hebrew", "he");
        public static readonly Language Hungarian = new Language("Hungarian", "hu");
        public static readonly Language Icelandic = new Language("Icelandic", "is");
        public static readonly Language Indonesian = new Language("Indonesian", "id");
        public static readonly Language Italian = new Language("Italian", "it");
        public static readonly Language Japanese = new Language("Japanese", "ja");
        public static readonly Language Korean = new Language("Korean", "ko");
        public static readonly Language Latvian = new Language("Latvian", "lv");
        public static readonly Language Lithuanian = new Language("Lithuanian", "lt");
        public static readonly Language Norwegian = new Language("Norwegian", "no");
        public static readonly Language Polish = new Language("Polish", "pl");
        public static readonly Language Portuguese = new Language("Portuguese", "pt");
        public static readonly Language Romanian = new Language("Romanian", "ro");
        public static readonly Language Russian = new Language("Russian", "ru");
        public static readonly Language SerboCroatian = new Language("SerboCroatian", "hr");
        public static readonly Language Slovak = new Language("Slovak", "sk");
        public static readonly Language Slovenian = new Language("Slovenian", "sl");
        public static readonly Language Spanish = new Language("Spanish", "es");
        public static readonly Language Swedish = new Language("Swedish", "sv");
        public static readonly Language Thai = new Language("Thai", "th");
        public static readonly Language Turkish = new Language("Turkish", "tr");
        public static readonly Language Ukrainian = new Language("Ukrainian", "uk");
        public static readonly Language Vietnamese = new Language("Vietnamese", "vi");
        public static readonly Language ChineseSimplified = new Language("ChineseSimplified", "zh-Hans");
        public static readonly Language ChineseTraditional = new Language("ChineseTraditional", "zh-Hant");
        public static readonly Language Unknown = new Language("Unknown", "");
        
        [SerializeField]
        private string m_Name;
        
        [SerializeField]
        private string m_Code;
        
        /// <summary>
        /// Language name.
        /// </summary>
        public string Name
        {
            get { return m_Name; }
        }

        /// <summary>
        /// Language code.
        /// </summary>
        public string Code
        {
            get { return m_Code; }
        }

        public Language(string name, string code)
        {
            m_Name = name ?? "";
            m_Code = code ?? "";
        }

        public bool Equals(Language other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Code == other.Code;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Language) obj);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public static bool operator ==(Language left, Language right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Language left, Language right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Name;
        }
        
        public static implicit operator Language(SystemLanguage systemLanguage)
        {
            switch (systemLanguage)
            {
                case SystemLanguage.Afrikaans: return Afrikaans;
                case SystemLanguage.Arabic: return Arabic;
                case SystemLanguage.Basque: return Basque;
                case SystemLanguage.Belarusian: return Belarusian;
                case SystemLanguage.Bulgarian: return Bulgarian;
                case SystemLanguage.Catalan: return Catalan;
                case SystemLanguage.Chinese: return Chinese;
                case SystemLanguage.Czech: return Czech;
                case SystemLanguage.Danish: return Danish;
                case SystemLanguage.Dutch: return Dutch;
                case SystemLanguage.English: return English;
                case SystemLanguage.Estonian: return Estonian;
                case SystemLanguage.Faroese: return Faroese;
                case SystemLanguage.Finnish: return Finnish;
                case SystemLanguage.French: return French;
                case SystemLanguage.German: return German;
                case SystemLanguage.Greek: return Greek;
                case SystemLanguage.Hebrew: return Hebrew;
                case SystemLanguage.Hungarian: return Hungarian;
                case SystemLanguage.Icelandic: return Icelandic;
                case SystemLanguage.Indonesian: return Indonesian;
                case SystemLanguage.Italian: return Italian;
                case SystemLanguage.Japanese: return Japanese;
                case SystemLanguage.Korean: return Korean;
                case SystemLanguage.Latvian: return Latvian;
                case SystemLanguage.Lithuanian: return Lithuanian;
                case SystemLanguage.Norwegian: return Norwegian;
                case SystemLanguage.Polish: return Polish;
                case SystemLanguage.Portuguese: return Portuguese;
                case SystemLanguage.Romanian: return Romanian;
                case SystemLanguage.Russian: return Russian;
                case SystemLanguage.SerboCroatian: return SerboCroatian;
                case SystemLanguage.Slovak: return Slovak;
                case SystemLanguage.Slovenian: return Slovenian;
                case SystemLanguage.Spanish: return Spanish;
                case SystemLanguage.Swedish: return Swedish;
                case SystemLanguage.Thai: return Thai;
                case SystemLanguage.Turkish: return Turkish;
                case SystemLanguage.Ukrainian: return Ukrainian;
                case SystemLanguage.Vietnamese: return Vietnamese;
                case SystemLanguage.ChineseSimplified: return ChineseSimplified;
                case SystemLanguage.ChineseTraditional: return ChineseTraditional;

                default:
                case SystemLanguage.Unknown: return null;
            }
        }
        
        public static implicit operator SystemLanguage(Language language)
        {
            if (Afrikaans == language) return SystemLanguage.Afrikaans;
            if (Arabic == language) return SystemLanguage.Arabic;
            if (Basque == language) return SystemLanguage.Basque;
            if (Belarusian == language) return SystemLanguage.Belarusian;
            if (Bulgarian == language) return SystemLanguage.Bulgarian;
            if (Catalan == language) return SystemLanguage.Catalan;
            if (Chinese == language) return SystemLanguage.Chinese;
            if (Czech == language) return SystemLanguage.Czech;
            if (Danish == language) return SystemLanguage.Danish;
            if (Dutch == language) return SystemLanguage.Dutch;
            if (English == language) return SystemLanguage.English;
            if (Estonian == language) return SystemLanguage.Estonian;
            if (Faroese == language) return SystemLanguage.Faroese;
            if (Finnish == language) return SystemLanguage.Finnish;
            if (French == language) return SystemLanguage.French;
            if (German == language) return SystemLanguage.German;
            if (Greek == language) return SystemLanguage.Greek;
            if (Hebrew == language) return SystemLanguage.Hebrew;
            if (Hungarian == language) return SystemLanguage.Hungarian;
            if (Icelandic == language) return SystemLanguage.Icelandic;
            if (Indonesian == language) return SystemLanguage.Indonesian;
            if (Italian == language) return SystemLanguage.Italian;
            if (Japanese == language) return SystemLanguage.Japanese;
            if (Korean == language) return SystemLanguage.Korean;
            if (Latvian == language) return SystemLanguage.Latvian;
            if (Lithuanian == language) return SystemLanguage.Lithuanian;
            if (Norwegian == language) return SystemLanguage.Norwegian;
            if (Polish == language) return SystemLanguage.Polish;
            if (Portuguese == language) return SystemLanguage.Portuguese;
            if (Romanian == language) return SystemLanguage.Romanian;
            if (Russian == language) return SystemLanguage.Russian;
            if (SerboCroatian == language) return SystemLanguage.SerboCroatian;
            if (Slovak == language) return SystemLanguage.Slovak;
            if (Slovenian == language) return SystemLanguage.Slovenian;
            if (Spanish == language) return SystemLanguage.Spanish;
            if (Swedish == language) return SystemLanguage.Swedish;
            if (Thai == language) return SystemLanguage.Thai;
            if (Turkish == language) return SystemLanguage.Turkish;
            if (Ukrainian == language) return SystemLanguage.Ukrainian;
            if (Vietnamese == language) return SystemLanguage.Vietnamese;
            if (ChineseSimplified == language) return SystemLanguage.ChineseSimplified;
            if (ChineseTraditional == language) return SystemLanguage.ChineseTraditional;

            return SystemLanguage.Unknown;
        }
    }
}