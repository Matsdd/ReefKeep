using UnityEngine;
using UnityEngine.UI;

public class TabSwitch : MonoBehaviour
{
    public GameObject Plants;
    public GameObject Rocks;
    public GameObject Decor;
    public GameObject Fish;
    public GameObject Upgrade;

    public Image PlantButton;
    public Image RockButton;
    public Image DecorButton;
    public Image FishButton;

    void Start()
    {
        SetTabColor(1);
    }

    private void SetTabColor(int activeTab)
    {
        int currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);

        // Set the color of the active tab.
        if (activeTab == 1)
        {
            PlantButton.color = Color.cyan;
            RockButton.color = Color.white;
            DecorButton.color = Color.white;
            FishButton.color = Color.white;
        }
        else if (activeTab == 2)
        {
            PlantButton.color = Color.white;
            RockButton.color = Color.cyan;
            DecorButton.color = Color.white;
            FishButton.color = Color.white;
        }
        else if (activeTab == 3)
        {
            PlantButton.color = Color.white;
            RockButton.color = Color.white;
            DecorButton.color = Color.cyan;
            FishButton.color = Color.white;
        }
        else if (activeTab == 4)
        {
            PlantButton.color = Color.white;
            RockButton.color = Color.white;
            DecorButton.color = Color.white;
            FishButton.color = Color.cyan;
        }

        // Make the tabs grey
        if (currentLevel == 1)
        {
            RockButton.color = Color.grey;
            DecorButton.color = Color.grey;
            FishButton.color = Color.grey;
        }
        else if (currentLevel == 2)
        {
            DecorButton.color = Color.grey;
            FishButton.color = Color.grey;
        }
        else if (currentLevel == 3)
        {
            FishButton.color = Color.grey;
        }

    }
    public void TogglePlants()
    {
        int currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);

        Plants.SetActive(true);
        Rocks.SetActive(false);
        Decor.SetActive(false);
        Fish.SetActive(false);
        Upgrade.SetActive(false);

        SetTabColor(1);
    }

    public void ToggleRocks()
    {
        int currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);

        if (currentLevel >= 2)
        {
            Plants.SetActive(false);
            Rocks.SetActive(true);
            Decor.SetActive(false);
            Fish.SetActive(false);
            Upgrade.SetActive(false);

            SetTabColor(2);
        }
        else
        {
            GameManager.instance.ShowMessage("Shop level 2 needed!", "Shop level 2 nodig!");
        }

    }

    // Activate Decor Canvas and deactivate the others
    public void ToggleDecor()
    {
        int currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);

        if (currentLevel >= 3)
        {
            Plants.SetActive(false);
            Rocks.SetActive(false);
            Decor.SetActive(true);
            Fish.SetActive(false);
            Upgrade.SetActive(false);

            SetTabColor(3);
        }
        else
        {
            GameManager.instance.ShowMessage("Shop level 3 needed!", "Shop level 3 nodig!");
        }
    }

    // Activate Fish Canvas and deactivate the others
    public void ToggleFish()
    {
        int currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);

        if (currentLevel >= 4)
        {
            Plants.SetActive(false);
            Rocks.SetActive(false);
            Decor.SetActive(false);
            Fish.SetActive(true);
            Upgrade.SetActive(false);

            SetTabColor(4);
        }
        else
        {
            GameManager.instance.ShowMessage("Shop level 4 needed!", "Shop level 4 nodig!");
        }
    }

    public void ToggleUpgrade()
    {
        Plants.SetActive(false);
        Rocks.SetActive(false);
        Decor.SetActive(false);
        Fish.SetActive(false);
        Upgrade.SetActive(true);
    }

}