using UnityEngine;

public class VisitorCenterScript : MonoBehaviour
{

    private int moneyPerSecond = 1; // Amount of money gained per minute

    void Start()
    {
        // Calculate offline income
        AddOfflineIncome();

        // Start the income coroutine
        InvokeRepeating(nameof(AddMoneyPerSecond), 1f, 1f);
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
