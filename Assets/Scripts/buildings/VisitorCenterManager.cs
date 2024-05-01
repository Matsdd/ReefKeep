using UnityEngine;

public class VisitorCenterManager : MonoBehaviour
{
    public static VisitorCenterManager instance;

    public int storedMoney = 0;
    public int currentLevel = 1;

    private float moneyPerSecond = 0f;
    private float moneyAccumulator = 0f;

    // First array int is unused because level starts at 1
    public readonly int[] maxMoney = { 500, 1000, 2000, 3000 };
    public readonly int[] upgradeCost = { 500, 1000, 2000, 3000 };

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent from being destroyed when loading new scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Get values from PlayerPrefs or set a default
        currentLevel = PlayerPrefs.GetInt("VisitorCenterLevel", 1);
        storedMoney = PlayerPrefs.GetInt("StoredMoney", 0);

        // Calculate offline income
        AddOfflineIncome();

        // Start the income coroutine
        InvokeRepeating(nameof(AddMoneyPerSecond), 1f, 1f);
    }

    public void UpgradeLevel()
    {
        // Max level is 3
        if (currentLevel <= 2)
        {
            // Try to buy the upgrade, cancel if not enough money
            if (GameManager.instance.ChangeMoney(-upgradeCost[currentLevel]))
            {
                currentLevel++;
                PlayerPrefs.SetInt("VisitorCenterLevel", currentLevel);
                PlayerPrefs.Save();
            }
            else
            {
                GameManager.instance.ShowMessage("Not enough money!");
            }
        }
    }

    // Changed the stored money inside the building
    public void ChangeStoredMoney(int amount)
    {
        // Calculate the new stored money value
        int newStoredMoney = storedMoney + amount;

        // Cap the stored money at the maximum value if it exceeds it
        newStoredMoney = Mathf.Min(newStoredMoney, maxMoney[currentLevel]);

        // Update the stored money value
        storedMoney = newStoredMoney;
        PlayerPrefs.SetInt("StoredMoney", storedMoney);
        PlayerPrefs.Save();
    }

    // Calculate the amount of money that needs to be add every second, can be decimal.
    public float CalcMoneyPerSec()
    {
        // ADD CALCULATION BASED ON FISH HAPPINESS AND DIFFERENT SPECIES COUNT!!!!!!!!!!!!!!!!!!!!!!!!!!
        float moneyPerSec;
        moneyPerSec = currentLevel / 2f;
        moneyPerSecond = moneyPerSec;
        return moneyPerSec;
    }

    // Add money to the building every second
    private void AddMoneyPerSecond()
    {
        // Make sure to set the new moneyPerSecond
        CalcMoneyPerSec();

        // Add the integral part of moneyPerSecond to stored money
        ChangeStoredMoney(Mathf.FloorToInt(moneyPerSecond));

        // Accumulate the fractional part
        moneyAccumulator += moneyPerSecond - Mathf.Floor(moneyPerSecond);

        // If the accumulator is greater than or equal to 1, add one unit of money and subtract 1 from the accumulator
        if (moneyAccumulator >= 1f)
        {
            ChangeStoredMoney(1);
            moneyAccumulator -= 1f;
        }
    }

    // Add the money in the building to actual money and reset it to 0
    public void ClaimMoney()
    {
        if (storedMoney > 0)
        {
            GameManager.instance.ChangeMoney(storedMoney);
            ChangeStoredMoney(-storedMoney);
        }
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
            int offlineIncome = Mathf.FloorToInt(timeDifferenceSeconds * CalcMoneyPerSec());

            // Add the offline income to the player's money
            if (offlineIncome > 0)
            {
                ChangeStoredMoney(offlineIncome);
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
