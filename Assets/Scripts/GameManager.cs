using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int money = 0;
    public TextMeshProUGUI moneyText;

    private int moneyPerSecond = 1; // Amount of money gained per minute

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent GameManager from being destroyed when loading new scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load player money from PlayerPrefs
        LoadPlayerMoney();
        UpdateMoneyUI();

        // Calculate offline income
        AddOfflineIncome();

        // Start the income coroutine
        InvokeRepeating(nameof(AddMoneyPerSecond), 1f, 1f);
    }

    private void AddMoneyPerSecond()
    {
        ChangeMoney(moneyPerSecond);
    }

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
                ChangeMoney(offlineIncome);
            }
        }
        else
        {
            Debug.LogError("AddOfflineIncome FAIL!");
        }
    }

    public bool ChangeMoney(int amount)
    {
        // Check if the player has enough money to perform the transaction
        if (money + amount >= 0)
        {
            money += amount;
            SavePlayerMoney();
            UpdateMoneyUI();
            return true; // Transaction successful
        }
        else
        {
            return false; // Not enough money
        }
    }

    // Shop code explaination:
    //
    // if (GameManager.instance.ChangeMoney(-itemPrice))
    // {
    // Player had enough money, perform purchase.
    // }
    // else
    // {
    // Player didn't have enough money, show message to player.
    // }


    private void SavePlayerMoney()
    {
        PlayerPrefs.SetInt("PlayerMoney", money);
    }

    private void LoadPlayerMoney()
    {
        money = PlayerPrefs.GetInt("PlayerMoney", 0);
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money.ToString();
        }
    }

    private void OnApplicationQuit()
    {
        // Save the current real-world time when the application quits
        long ticks = System.DateTime.UtcNow.Ticks;
        PlayerPrefs.SetString("LastPlayTimeTicks", ticks.ToString());
        SavePlayerMoney();
    }
}
