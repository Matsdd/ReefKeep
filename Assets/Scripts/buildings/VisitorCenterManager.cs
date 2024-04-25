using UnityEngine;

public class VisitorCenterManager : MonoBehaviour
{
    public static VisitorCenterManager instance;

    public int storedMoney = 0;
    public int currentLevel = 1;

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
        currentLevel = PlayerPrefs.GetInt("VisitorCenterLevel", 1);
        storedMoney = PlayerPrefs.GetInt("StoredMoney", 0);

        // Calculate offline income
        AddOfflineIncome();

        // Start the income coroutine
        InvokeRepeating(nameof(AddMoneyPerSecond), 1f, 1f);
    }

    public void UpgradeLevel()
    {
        if (currentLevel <= 2)
        {
            if (GameManager.instance.ChangeMoney(-upgradeCost[currentLevel]))
            {
                currentLevel++;
                PlayerPrefs.SetInt("VisitorCenterLevel", currentLevel);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Not enough money!");
            }
        }
    }

    public void ChangeStoredMoney(int amount)
    {
        if ((storedMoney + amount) <= maxMoney[currentLevel])
        {
            storedMoney += amount;
            PlayerPrefs.SetInt("StoredMoney", storedMoney);
            PlayerPrefs.Save();
        }
    }

    public int CalcMoneyPerSec()
    {
        int moneyPerSec;
        moneyPerSec = currentLevel;
        return moneyPerSec;
    }

    private void AddMoneyPerSecond()
    {
        ChangeStoredMoney(CalcMoneyPerSec());
    }

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
