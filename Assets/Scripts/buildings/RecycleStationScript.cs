using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecycleStationScript : MonoBehaviour
{
    public int currentLevel = 1;

    // First array int is unused because level starts at 1
    public readonly int[] moneyPerTrash = { 0, 10, 20, 30 };
    public readonly int[] upgradeCost = { 0, 100, 200, 300 };

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
        // Get values from PlayerPrefs or set a default
        currentLevel = PlayerPrefs.GetInt("RecycleStationLevel", 1);

        // Set the sprite after 1sec so the correct level can be loaded from PlayerPrefs
        Invoke(nameof(UpdateSprite), 0.1f);
    }

    // Open the menu when clicking on the building
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // Prevent from being clicked on through open UI
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
        UpdateSprite();

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

    public void UpdateSprite()
    {
        // Change the sprite based on the level
        string spriteName = "RECYCLER_" + currentLevel;
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
