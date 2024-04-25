using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisitorCenterUIScript : MonoBehaviour
{
    public GameObject buildingCanvas;
    public GameObject buildingMenu;
    public GameObject upgradeConfirmMenu;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI moneyPerSecText;
    public TextMeshProUGUI storedMoneyText;
    public TextMeshProUGUI moneyCapText;
    public TextMeshProUGUI upgradeToLevelText;
    public TextMeshProUGUI upgradeCostText;
    public Button upgradeButton;

    // Open the menu when clicking on the building
    void OnMouseDown()
    {
        buildingCanvas.SetActive(true);
        buildingMenu.SetActive(true);
        upgradeConfirmMenu.SetActive(false);

        // Start repeating UpdateUI to show the amount of money
        InvokeRepeating(nameof(UpdateUI), 0f, 1f);
    }

    // Close all menu's when clicking on Close button
    public void CloseMenus()
    {
        buildingCanvas.SetActive(false);
        buildingMenu.SetActive(false);
        upgradeConfirmMenu.SetActive(false);

        // Stop repeatingly updating the UI when the menu is closed
        CancelInvoke(nameof(UpdateUI));
    }

    // Toggle the upgrade menu
    public void ToggleUpgradeMenu()
    {
        upgradeConfirmMenu.SetActive(!upgradeConfirmMenu.activeSelf);
        UpdateUI();
    }

    // Actually upgrade the building
    public void ConfirmUpgrade()
    {
        // Upgrade building
        VisitorCenterManager.instance.UpgradeLevel();
        UpdateUI();

        // Close upgrade confirm menu
        upgradeConfirmMenu.SetActive(false);
    }

    // Updates all the text in the UI. Call this when an update happens
    public void UpdateUI()
    {
        levelText.text = "Level: " + VisitorCenterManager.instance.currentLevel;
        moneyPerSecText.text = "Fishbucks /s: " + VisitorCenterManager.instance.CalcMoneyPerSec();
        storedMoneyText.text = VisitorCenterManager.instance.storedMoney.ToString();
        moneyCapText.text = VisitorCenterManager.instance.maxMoney[VisitorCenterManager.instance.currentLevel].ToString();
        upgradeToLevelText.text = "Upgrade to level " + (VisitorCenterManager.instance.currentLevel + 1) + "?";
        upgradeCostText.text = VisitorCenterManager.instance.upgradeCost[VisitorCenterManager.instance.currentLevel].ToString();

        if (VisitorCenterManager.instance.currentLevel >= 3)
        {
            upgradeButton.gameObject.SetActive(false);
        }
        else
        {
            upgradeButton.gameObject.SetActive(true);
        }

    }
}
