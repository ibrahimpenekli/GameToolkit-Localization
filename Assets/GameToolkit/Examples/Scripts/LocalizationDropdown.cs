using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameToolkit.Localization;

[RequireComponent(typeof(Dropdown))]
public class LocalizationDropdown : MonoBehaviour
{
	private Array m_Values;

    private void Start()
    {
		m_Values = Enum.GetValues(typeof(SystemLanguage));

		var dropdown = GetComponent<Dropdown>();
		dropdown.AddOptions(new List<string>(Enum.GetNames(typeof(SystemLanguage))));
		dropdown.onValueChanged.AddListener(delegate
		{
			Localization.Instance.CurrentLanguage = (SystemLanguage) m_Values.GetValue(dropdown.value);
		});
		dropdown.value = (int) Localization.Instance.CurrentLanguage;
    }
}
