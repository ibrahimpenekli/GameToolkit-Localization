using System;
using System.Linq;
using UnityEngine;
using NUnit.Framework;

namespace GameToolkit.Localization.Tests
{
    public class LocalizationTests
    {
        [Test]
        public void LocaleChanged_RaisedWithCorrectArgs()
        {
            var onLocaleChanged = new EventHandler<LocaleChangedEventArgs>(
                delegate(object sender, LocaleChangedEventArgs e)
                {
                    Assert.AreEqual(SystemLanguage.English, e.PreviousLanguage);
                    Assert.AreEqual(SystemLanguage.Turkish, e.CurrentLanguage);
                });

            Localization.Instance.CurrentLanguage = SystemLanguage.English;
            Localization.Instance.LocaleChanged += onLocaleChanged;
            Localization.Instance.CurrentLanguage = SystemLanguage.Turkish;
            Localization.Instance.LocaleChanged -= onLocaleChanged;
        }

        [Test]
        public void LocaleChanged_NotRaised()
        {
            var onLocaleChanged = new EventHandler<LocaleChangedEventArgs>(
                delegate(object sender, LocaleChangedEventArgs e)
                {
                    Assert.Fail("LocaleChanged event should not be raised when the same value is set.");
                });
            
            Localization.Instance.CurrentLanguage = SystemLanguage.English;
            Localization.Instance.LocaleChanged += onLocaleChanged;
            Localization.Instance.CurrentLanguage = SystemLanguage.English;
            Localization.Instance.LocaleChanged -= onLocaleChanged;
        }

        [Test]
        public void SetSystemLanguage_SetsCurrentAsSystemLanguage()
        {
            var systemLanguage = Application.systemLanguage;
            Localization.Instance.SetSystemLanguage();
            Assert.AreEqual(systemLanguage, Localization.Instance.CurrentLanguage);
        }

        [Test]
        public void SetDefaultLanguage_SetsCurrentAsDefaultLanguage()
        {
            var defaultLanguage = LocalizationSettings.Instance.AvailableLanguages.FirstOrDefault();
            Localization.Instance.SetDefaultLanguage();
            Assert.AreEqual(defaultLanguage, Localization.Instance.CurrentLanguage);
        }
    }
}