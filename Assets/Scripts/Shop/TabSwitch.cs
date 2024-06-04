using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class TabSwitch : MonoBehaviour
{
    public GameObject Nature;
    public GameObject Decor;
    public GameObject Fish;
    public GameObject Upgrade;
    public int currentLevel = 1;

    void Start()
    {
        // Get values from PlayerPrefs or set a default
        currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);
    }

    // Activate Nature Canvas and deactivate the others
    public void ToggleNature()
    {
        Debug.Log("ToggleNature");
        Nature.SetActive(true);
        Decor.SetActive(false);
        Fish.SetActive(false);
        Upgrade.SetActive(false);
        currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);
        return;
    }

    // Activate Decor Canvas and deactivate the others
    public void ToggleDecor()
    {
        if(currentLevel >= 2)
        { 
            Debug.Log("ToggleDecor");
            Nature.SetActive(false);
            Decor.SetActive(true);
            Fish.SetActive(false);
            Upgrade.SetActive(false);
            return;
        } else {
            Debug.Log("Level too low!");
            return;
        }
    }

    // Activate Fish Canvas and deactivate the others
    public void ToggleFish()
    {
        if (currentLevel >= 3)
        {
            Debug.Log("ToggleFish");
            Nature.SetActive(false);
            Decor.SetActive(false);
            Fish.SetActive(true);
            Upgrade.SetActive(false);
            return;
        } else {
            Debug.Log("Level too low!");
            return;
        }
    }
    public void ToggleUpgrade()
    {
        Debug.Log("ToggleUpgrade");
        Upgrade.SetActive(true);
        Nature.SetActive(false);
        Decor.SetActive(false);
        Fish.SetActive(false);
      
        return;
    }
}