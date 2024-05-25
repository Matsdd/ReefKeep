using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelectorScript : MonoBehaviour
{
    // Language Dropdown
    public TMP_Dropdown dropdown;

    private bool active = false;
   
    // Set the langauge when the game starts
    void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleID", 0);
        dropdown.value = ID;
        StartCoroutine(SetLocale(ID));
    }

    // Function to change language with the dropdown selector
    public void ChangeLocale()
    {
        if (!active)
        {
            int LocaleID = dropdown.value;
            StartCoroutine(SetLocale(LocaleID));
            Debug.Log(LocaleID);
        }
    }

    // IEnumerator to actually switch the language
    private IEnumerator SetLocale(int LocaleID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[LocaleID];
        PlayerPrefs.SetInt("LocaleID", LocaleID);
        active = false;
    }
}
