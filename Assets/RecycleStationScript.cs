using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecycleStationScript : MonoBehaviour
{
    public GameObject buildingCanvas;
    public GameObject buildingMenu;
    public GameObject upgradeConfirmMenu;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI moneyPerCleanText;
    public TextMeshProUGUI upgradeToLevelText;
    public TextMeshProUGUI upgradeCostText;
    public Button upgradeButton;
    public SpriteRenderer buildingSpriteRenderer;

    private void Start()
    {
        // Set the sprite after 1sec so the correct level can be loaded from PlayerPrefs
        Invoke(nameof(UpdateSprite), 1f);
    }

    // Open the menu when clicking on the building
    void OnMouseDown()
    {
        buildingCanvas.SetActive(true);
        buildingMenu.SetActive(true);
        upgradeConfirmMenu.SetActive(false);

        UpdateUI();
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
        UpdateSprite();

        // Close upgrade confirm menu
        upgradeConfirmMenu.SetActive(false);
    }

    // Updates all the text in the UI. Call this when an update happens
    public void UpdateUI()
    {
        levelText.text = "Level: " + VisitorCenterManager.instance.currentLevel;
        moneyPerCleanText.text = "Fishbucks per Trash: " + 10;
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

    public void UpdateSprite()
    {
        // Change the sprite based on the level
        string spriteName = "VISITORCENTER_" + VisitorCenterManager.instance.currentLevel;
        Sprite levelSprite = Resources.Load<Sprite>("Sprites/Buildings/" + spriteName);
        if (levelSprite != null)
        {
            buildingSpriteRenderer.sprite = levelSprite;
        }
        else
        {
            Debug.LogError("Sprite not found with name: " + spriteName);
        }
    }
}
