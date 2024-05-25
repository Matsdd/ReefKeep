using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LangSetter : MonoBehaviour
{
    // Set the langauge when the game starts
    void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleID", 0);
        StartCoroutine(SetLocale(ID));
    }

    // IEnumerator to actually switch the language
    private IEnumerator SetLocale(int LocaleID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[LocaleID];
    }
}
