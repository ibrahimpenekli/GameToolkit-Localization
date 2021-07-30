using GameToolkit.Localization;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeLocalizedText : MonoBehaviour
{
    public Text label;

    private LocalizedText m_LocalizedText;
    
    private void Awake()
    {
        m_LocalizedText = ScriptableObject.CreateInstance<LocalizedText>();
        m_LocalizedText.SetLocaleItems(new[]
        {
            new LocaleItem<string>(Language.English, "Hi! This text is created at runtime."),
            new LocaleItem<string>(Language.Turkish, "Merhaba! Bu metin çalışma zamanı oluşturulmuştur.")
        });

        label.text = m_LocalizedText.Value;
    }

    private void OnDestroy()
    {
        Destroy(m_LocalizedText);
    }

    private void OnEnable()
    {
        Localization.Instance.LocaleChanged += Localization_OnLocaleChanged;
    }

    private void OnDisable()
    {
        Localization.Instance.LocaleChanged -= Localization_OnLocaleChanged;
    }
    
    private void Localization_OnLocaleChanged(object sender, LocaleChangedEventArgs e)
    {
        label.text = m_LocalizedText.Value;
    }
}
