using TMPro;
using UnityEngine;

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

    void Update()
    {
        UpdateUI();
    }

    // Open the menu when clicking on the building
    void OnMouseDown()
    {
        buildingCanvas.SetActive(true);
        buildingMenu.SetActive(true);
        upgradeConfirmMenu.SetActive(false);
    }

    // Close all menu's when clicking on Close button
    public void CloseMenus()
    {
        buildingCanvas.SetActive(false);
        buildingMenu.SetActive(false);
        upgradeConfirmMenu.SetActive(false);
    }

    public void ToggleUpgradeMenu()
    {
        upgradeConfirmMenu.SetActive(!upgradeConfirmMenu.activeSelf);
    }


    public void ConfirmUpgrade()
    {
        // Upgrade building
        VisitorCenterManager.instance.UpgradeLevel();

        // Close upgrade confirm menu
        upgradeConfirmMenu.SetActive(false);
    }

    public void UpdateUI()
    {
        levelText.text = "Level: " + VisitorCenterManager.instance.currentLevel;
        moneyPerSecText.text = "Fishbucks/s = " + VisitorCenterManager.instance.CalcMoneyPerSec();
        storedMoneyText.text = VisitorCenterManager.instance.storedMoney.ToString();
        moneyCapText.text = VisitorCenterManager.instance.maxMoney[VisitorCenterManager.instance.currentLevel].ToString();
        upgradeToLevelText.text = "Upgrade to level " + (VisitorCenterManager.instance.currentLevel + 1) + "?";
        upgradeCostText.text = VisitorCenterManager.instance.upgradeCost[VisitorCenterManager.instance.currentLevel].ToString();
    }
}
