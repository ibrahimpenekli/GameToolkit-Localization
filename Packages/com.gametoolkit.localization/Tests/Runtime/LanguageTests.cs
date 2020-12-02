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

            foreach (var systemLanguage in systemLanguages)
            {
                if (Language.BuiltinLanguages.All(x => x.Name != systemLanguage.ToString()))
                {
                    Assert.Fail("Missing SystemLanguage: " + systemLanguage);
                }
            }
        }
        
        [Test]
        public void LanguageAndSystemLanguageConversion()
        {
            var systemLanguages = ((SystemLanguage[]) Enum.GetValues(typeof(SystemLanguage)))
                .Distinct().OrderBy(x => (int) x).ToArray();

            foreach (var systemLanguage in systemLanguages)
            {
                var language = (Language) systemLanguage;
                var systemLanguage2 = (SystemLanguage) language;
                
                Assert.AreEqual(systemLanguage , systemLanguage2, systemLanguage + " not converted correctly");
            }
            
            // Custom language to SystemLanguage.
            Assert.That(SystemLanguage.Unknown == (SystemLanguage) new Language("Hindi", "hi"), 
                "Casting from custom language to SystemLanguage should be Unknown");
        }
        
        [Test]
        public void LanguageAndSystemLanguageComparison()
        {
            Assert.That(SystemLanguage.Turkish == Language.Turkish, "SystemLanguage == Language comparison failed");
            Assert.That(SystemLanguage.Turkish != Language.English, "SystemLanguage != Language comparison failed");
            
            Assert.That(Language.Turkish == SystemLanguage.Turkish, "Language == SystemLanguage comparison failed");
            Assert.That(Language.English != SystemLanguage.Turkish, "Language != SystemLanguage comparison failed");

            Assert.That(Language.Turkish.Equals(SystemLanguage.Turkish), "Language.Equals(SystemLanguage) comparison failed");
            Assert.That(!Language.English.Equals(SystemLanguage.Turkish), "!Language.Equals(SystemLanguage) comparison failed");
        }
    }
}