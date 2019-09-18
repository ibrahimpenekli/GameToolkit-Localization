using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameToolkit.Localization;

[RequireComponent(typeof(Dropdown))]
public class LocalizationDropdown : MonoBehaviour
{
    [SerializeField]
    private LocalizedText m_Languages;

    private void Start()
    {
        // Write language names as native.
        var options = LocalizationSettings.Instance.AvailableLanguages.Select(language =>
        {
            return m_Languages.GetLocaleValue(language);
        }).ToList();

        var dropdown = GetComponent<Dropdown>();
        dropdown.AddOptions(options);
        dropdown.onValueChanged.AddListener(delegate
        {
            Localization.Instance.CurrentLanguage = LocalizationSettings.Instance.AvailableLanguages[dropdown.value];
        });
        dropdown.value = LocalizationSettings.Instance.AvailableLanguages.IndexOf(Localization.Instance.CurrentLanguage);
    }
}
