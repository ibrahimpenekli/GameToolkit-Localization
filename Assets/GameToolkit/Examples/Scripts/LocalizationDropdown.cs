using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameToolkit.Localization;

[RequireComponent(typeof(Dropdown))]
public class LocalizationDropdown : MonoBehaviour
{
    private void Start()
    {
        var options = LocalizationSettings.Instance.AvailableLanguages.Select(x => x.ToString()).ToList();
        var dropdown = GetComponent<Dropdown>();
        dropdown.AddOptions(options);
        dropdown.onValueChanged.AddListener(delegate
        {
            Localization.Instance.CurrentLanguage = LocalizationSettings.Instance.AvailableLanguages[dropdown.value];
        });
        dropdown.value = LocalizationSettings.Instance.AvailableLanguages.IndexOf(Localization.Instance.CurrentLanguage);
    }
}
