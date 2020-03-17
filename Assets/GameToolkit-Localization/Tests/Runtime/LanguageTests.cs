using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace GameToolkit.Localization.Tests
{
    [TestFixture]
    public class LanguageTestFixture
    {
        [Test]
        public void CheckIfMissingBuiltinLanguages()
        {
            var systemLanguages = ((SystemLanguage[]) Enum.GetValues(typeof(SystemLanguage)))
                .Distinct().OrderBy(x => (int) x).ToArray();

            for (var i = 0; i < systemLanguages.Length; i++)
            {
                if (Language.BuiltinLanguages.All(x => x.Name != systemLanguages[i].ToString()))
                {
                    Assert.Fail("Missing SystemLanguage: " + systemLanguages[i]);
                }
            }
        }
        
        [Test]
        public void LanguageAndSystemLanguageConversion()
        {
            var systemLanguages = ((SystemLanguage[]) Enum.GetValues(typeof(SystemLanguage)))
                .Distinct().OrderBy(x => (int) x).ToArray();

            for (var i = 0; i < systemLanguages.Length; i++)
            {
                var language = (Language) systemLanguages[i];
                var systemLanguage = (SystemLanguage) language;
                
                Assert.AreEqual(systemLanguages[i], systemLanguage, systemLanguages[i] + " not converted correctly");
            }
        }
    }
}