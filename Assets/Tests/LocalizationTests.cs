using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace GameToolkit.Localization.Tests
{
    public class LocalizationTests
    {
        [UnityTest]
        public IEnumerator LocaleChanged_RaisedWithCorrectArgs()
        {
            var onLocaleChanged = new EventHandler<LocaleChangedEventArgs>(
                delegate(object sender, LocaleChangedEventArgs e)
                {
                    Assert.AreEqual(Language.English, e.PreviousLanguage);
                    Assert.AreEqual(Language.Turkish, e.CurrentLanguage);
                });

            Localization.Instance.CurrentLanguage = Language.English;
            Localization.Instance.LocaleChanged += onLocaleChanged;
            Localization.Instance.CurrentLanguage = Language.Turkish;
            Localization.Instance.LocaleChanged -= onLocaleChanged;
            yield break;
        }
        
        [UnityTest]
        public IEnumerator LocaleChanged_ShouldRaised()
        {
            var isEventRaised = false;
            var onLocaleChanged = new EventHandler<LocaleChangedEventArgs>((sender, e) =>
            {
                isEventRaised = true;
                
                Assert.AreEqual(Language.English, e.PreviousLanguage, "PreviousLanguage arg is wrong");
                Assert.AreEqual(Language.Turkish, e.CurrentLanguage, "CurrentLanguage arg is wrong");
            });

            Localization.Instance.CurrentLanguage = Language.English;
            Localization.Instance.LocaleChanged += onLocaleChanged;
            Localization.Instance.CurrentLanguage = Language.Turkish;
            Localization.Instance.LocaleChanged -= onLocaleChanged;
            
            Assert.True(isEventRaised, "LocaleChangedEventArgs is not raised");
            yield break;
        }

        [UnityTest]
        public IEnumerator LocaleChanged_ShouldNotRaised()
        {
            var onLocaleChanged = new EventHandler<LocaleChangedEventArgs>((sender, e) =>
            {
                Assert.Fail("LocaleChanged event should not be raised when the same value is set." +
                            " Old Value: " + e.PreviousLanguage + " New Value: " + e.CurrentLanguage);
            });

            Localization.Instance.CurrentLanguage = Language.English;
            Localization.Instance.LocaleChanged += onLocaleChanged;
            Localization.Instance.CurrentLanguage = Language.English;
            Localization.Instance.LocaleChanged -= onLocaleChanged;
            yield break;
        }

        [UnityTest]
        public IEnumerator SetSystemLanguage_SetsCurrentAsSystemLanguage()
        {
            var systemLanguage = Application.systemLanguage;
            Localization.Instance.SetSystemLanguage();
            Assert.AreEqual((Language) systemLanguage, Localization.Instance.CurrentLanguage);
            yield break;
        }

        [UnityTest]
        public IEnumerator SetDefaultLanguage_SetsCurrentAsDefaultLanguage()
        {
            var defaultLanguage = LocalizationSettings.Instance.AvailableLanguages.FirstOrDefault();
            Localization.Instance.SetDefaultLanguage();
            Assert.AreEqual(defaultLanguage, Localization.Instance.CurrentLanguage);
            yield break;
        }
        
        [UnityTest]
        public IEnumerator Language_CheckBuiltinLanguages()
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
            yield break;
        }
        
        [UnityTest]
        public IEnumerator Language_LanguageToSystemLanguage()
        {
            var systemLanguages = ((SystemLanguage[]) Enum.GetValues(typeof(SystemLanguage)))
                .Distinct().OrderBy(x => (int) x).ToArray();

            for (var i = 0; i < systemLanguages.Length; i++)
            {
                var language = (Language) systemLanguages[i];
                var systemLanguage = (SystemLanguage) language;
                
                Assert.AreEqual(systemLanguages[i], systemLanguage, systemLanguages[i] + " not converted correctly");
            }
            yield break;
        }
        
    }
}