using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace GameToolkit.Localization.Tests
{
    [TestFixture]
    public class LocalizationTestFixture
    {
        [Test]
        public void SetSystemLanguage()
        {
            var systemLanguage = Application.systemLanguage;
            Localization.Instance.SetSystemLanguage();
            Assert.AreEqual((Language) systemLanguage, Localization.Instance.CurrentLanguage);
        }

        [Test]
        public void SetDefaultLanguage()
        {
            var defaultLanguage = LocalizationSettings.Instance.AvailableLanguages.FirstOrDefault();
            Localization.Instance.SetDefaultLanguage();
            Assert.AreEqual(defaultLanguage, Localization.Instance.CurrentLanguage);
        }
        
        [Test]
        public void LocaleChangedEvent_RaisedWithCorrectArgs()
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
        }
        
        [Test]
        public void LocaleChangedEvent_ShouldRaised()
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
        }

        [Test]
        public void LocaleChangedEvent_ShouldNotRaised()
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
        }
    }
}