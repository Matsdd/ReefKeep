using TMPro;
using UnityEngine;

public class VisitorCenterScript : MonoBehaviour
{
    public GameObject buildingCanvas;
    public GameObject buildingMenu;
    public GameObject upgradeConfirmMenu;
    public TextMeshProUGUI levelText;

    private int currentLevel = 1;

    private int moneyPerSecond = 1;


    void Start()
    {
        // Calculate offline income
        AddOfflineIncome();

        // Load the saved level from PlayerPrefs
        currentLevel = PlayerPrefs.GetInt("VisitorCenterLevel", 1);
        UpdateUi();

        // Start the income coroutine
        InvokeRepeating(nameof(AddMoneyPerSecond), 1f, 1f);
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

    public void OpenUpgradeMenu()
    {
        upgradeConfirmMenu.SetActive(true);
    }

    public void ConfirmUpgrade()
    {
        // Upgrade building
        currentLevel++;
        UpdateUi();
        SaveLevel();

        // Close upgrade confirm menu
        upgradeConfirmMenu.SetActive(false);
    }

    private void UpdateUi()
    {
        levelText.text = "Level: " + currentLevel;
    }

    private void SaveLevel()
    {
        // Save current level to PlayerPrefs
        PlayerPrefs.SetInt("VisitorCenterLevel", currentLevel);
        PlayerPrefs.Save();
    }

    private void AddMoneyPerSecond()
    {
        GameManager.instance.ChangeMoney(moneyPerSecond);
    }

    // WARNING. TICKS CAN BE CHEATED BY CHANGING DEVICE DATE/TIME. CREATE A CHECK!!!
    private void AddOfflineIncome()
    {
        // Get the timestamp when the player last played
        if (long.TryParse(PlayerPrefs.GetString("LastPlayTimeTicks"), out long lastPlayTicks))
        {
            // Get the current time
            long currentTicks = System.DateTime.UtcNow.Ticks;

            // Calculate the time difference in ticks
            long timeDifferenceTicks = currentTicks - lastPlayTicks;

            // Convert ticks to seconds
            float timeDifferenceSeconds = timeDifferenceTicks / (float)System.TimeSpan.TicksPerSecond;

            // Calculate the offline income
            int offlineIncome = Mathf.FloorToInt(timeDifferenceSeconds * moneyPerSecond);

            // Add the offline income to the player's money
            if (offlineIncome > 0)
            {
                GameManager.instance.ChangeMoney(offlineIncome);
            }
        }
        else
        {
            Debug.LogError("AddOfflineIncome FAIL!");
        }
    }

    private void OnApplicationQuit()
    {
        // Save the current real-world time when the application quits
        // WARNING. TICKS CAN BE CHEATED BY CHANGING DEVICE DATE/TIME. CREATE A CHECK!!!
        long ticks = System.DateTime.UtcNow.Ticks;
        PlayerPrefs.SetString("LastPlayTimeTicks", ticks.ToString());
    }
}
