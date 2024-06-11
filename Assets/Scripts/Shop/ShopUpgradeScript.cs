using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgradeScript : MonoBehaviour
{
    public int currentLevel = 1;
    public Button upgradeButton;
    public readonly int[] upgradeCost = { 0, 500, 2000 };

    public GameObject upgradeConfirmMenu;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradeToLevelText;
    public TextMeshProUGUI upgradeCostText;

    // Start is called before the first frame update
    void Start()
    {
        // Get values from PlayerPrefs or set a default
        currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);
        UpdateUI();
    }
    public void ConfirmUpgrade()
    {
        // Upgrade building, Max level is 3
        if (currentLevel <= 2)
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
        if (currentLevel < 3)
        {
            levelText.text = "Level: " + currentLevel;
            upgradeToLevelText.text = "Upgrade to level " + (currentLevel + 1) + "?";
            upgradeCostText.text = "This will unlock a new page of the shop and cost " + upgradeCost[currentLevel].ToString() + " Fishbucks.";
        } else {
            levelText.text = "Level: " + currentLevel;
            upgradeToLevelText.text = "Max level";
            upgradeCostText.text = "Max level";
            upgradeButton.gameObject.SetActive(false);
        }
    }
}
