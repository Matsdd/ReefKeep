using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class TabSwitch : MonoBehaviour
{
    public GameObject Plants;
    public GameObject Rocks;
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
    public void TogglePlants()
    {
        Debug.Log("TogglePlants");
        Plants.SetActive(true);
        Rocks.SetActive(false);
        Decor.SetActive(false);
        Fish.SetActive(false);
        Upgrade.SetActive(false);
        currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);
        return;
    }

    public void ToggleRocks()
    {
        Debug.Log("ToggleRocks");
        Plants.SetActive(false);
        Rocks.SetActive(true);
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
            Plants.SetActive(false);
            Rocks.SetActive(false);
            Decor.SetActive(true);
            Fish.SetActive(false);
            Upgrade.SetActive(false);
            return;
        } else {
            Debug.Log("Level too low!");
            GameManager.instance.ShowMessage("Shop level 2 needed to buy decor!", "Shop level 2 nodig om decoratie te kopen!");
            return;
        }
    }

    // Activate Fish Canvas and deactivate the others
    public void ToggleFish()
    {
        if (currentLevel >= 3)
        {
            Debug.Log("ToggleFish");
            Plants.SetActive(false);
            Rocks.SetActive(false);
            Decor.SetActive(false);
            Fish.SetActive(true);
            Upgrade.SetActive(false);
            return;
        } else {
            Debug.Log("Level too low!");
            GameManager.instance.ShowMessage("Shop level 3 needed to buy fish!", "Shop level 3 nodig om vissen te kopen!");
            return;
        }
    }
    public void ToggleUpgrade()
    {
        Debug.Log("ToggleUpgrade");
        Plants.SetActive(false);
        Rocks.SetActive(false);
        Decor.SetActive(false);
        Fish.SetActive(false);
        Upgrade.SetActive(true);
        return;
    }

}