using System.Linq;
using GameToolkit.Localization;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Localization/Localization Prefs")]
public class LocalizationPrefs : MonoBehaviour
{
    [SerializeField, Tooltip("PlayerPrefs key to keep language preference.")]
    private string m_PrefKey = "GameLanguage";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadLanguage();
        Localization.Instance.LocaleChanged += Localization_OnLocaleChanged;
    }

    private void OnDestroy()
    {
        Localization.Instance.LocaleChanged -= Localization_OnLocaleChanged;
    }

    private void Localization_OnLocaleChanged(object sender, LocaleChangedEventArgs e)
    {
        SaveLanguage();
    }

    private void LoadLanguage()
    {
        // Set previously saved language if available.
        var savedLanguage = GetSavedLanguage();
        if (savedLanguage != Language.Unknown)
        {
            Localization.Instance.CurrentLanguage = savedLanguage;
        }
    }

    private void SaveLanguage()
    {
        PlayerPrefs.SetString(m_PrefKey, Localization.Instance.CurrentLanguage.Code);
        PlayerPrefs.Save();
    }

    private Language GetSavedLanguage()
    {
        if (PlayerPrefs.HasKey(m_PrefKey))
        {
            var languageCode = PlayerPrefs.GetString(m_PrefKey, "");
            var language = 
                LocalizationSettings.Instance.AvailableLanguages.FirstOrDefault(x => x.Code == languageCode);
            if (language != null)
            {
                return language;
            }
        }

        return Language.Unknown;
    }
}