using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgradeScript : MonoBehaviour
{
    public int currentLevel = 1;
    public Button upgradeButton;
    public readonly int[] upgradeCost = { 0, 500, 2000, 5000 };

    public GameObject upgradeConfirmMenu;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradeToLevelText;
    public TextMeshProUGUI upgradeCostText;

    void Start()
    {
        // Get values from PlayerPrefs or set a default
        currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);
        UpdateUI();
    }

    public void ConfirmUpgrade()
    {
        // Upgrade building, Max level is 4
        if (currentLevel <= 4)
        {
            // Try to buy the upgrade, cancel if not enough money
            if (GameManager.instance.ChangeMoney(-upgradeCost[currentLevel]))
            {
                currentLevel++;
                PlayerPrefs.SetInt("ShopLevel", currentLevel);
                PlayerPrefs.Save();
            }
            else
            {
                GameManager.instance.ShowMessage("Not enough money!", "Niet genoeg geld!");
            }
        }

        UpdateUI();
        upgradeConfirmMenu.SetActive(false);
    }

    // Updates all the text in the UI. Call this when an update happens
    public void UpdateUI()
    {
        if (currentLevel < 4)
        {
            if (PlayerPrefs.GetInt("LocaleID", 0) == 0)
            {
                upgradeToLevelText.text = "Upgrade to level " + (currentLevel + 1) + "?";
            }
            else
            {
                upgradeToLevelText.text = "Upgrade naar level " + (currentLevel + 1) + "?";
            }
            levelText.text = "Level: " + currentLevel;
            upgradeCostText.text = upgradeCost[currentLevel].ToString();
        }
        else
        {
            levelText.text = "Level: " + currentLevel;
            upgradeToLevelText.text = "Max level";
            upgradeCostText.text = "Max level";
            upgradeButton.gameObject.SetActive(false);
        }
    }
}
