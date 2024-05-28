using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUpgradeScript : MonoBehaviour
{
    public int currentLevel = 1;
    public Button upgradeButton;
    public readonly int[] upgradeCost = { 0, 500, 2000 };

    public GameObject upgradeConfirmMenu;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI moneyPerCleanText;
    public TextMeshProUGUI upgradeToLevelText;
    public TextMeshProUGUI upgradeCostText;

    // Start is called before the first frame update
    void Start()
    {
        // Get values from PlayerPrefs or set a default
        currentLevel = PlayerPrefs.GetInt("RecycleStationLevel", 1);

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
                PlayerPrefs.SetInt("RecycleStationLevel", currentLevel);
                PlayerPrefs.Save();
            }
            else
            {
                GameManager.instance.ShowMessage("Not enough money!");
            }
        }

        // Update UI
        UpdateUI();

        // Close upgrade confirm menu
        upgradeConfirmMenu.SetActive(false);
    }

    // Updates all the text in the UI. Call this when an update happens
    public void UpdateUI()
    {
        levelText.text = "Level: " + currentLevel;
        moneyPerCleanText.text = "Fishbucks per Trash: " + (currentLevel * 10);
        upgradeToLevelText.text = "Upgrade to level " + (currentLevel + 1) + "?";
        upgradeCostText.text = upgradeCost[currentLevel].ToString();

        if (currentLevel >= 3)
        {
            upgradeButton.gameObject.SetActive(false);
        }
        else
        {
            upgradeButton.gameObject.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
