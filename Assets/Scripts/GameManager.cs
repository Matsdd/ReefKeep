using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int playerMoney = 0;
    public TextMeshProUGUI moneyText;

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
    }

    public bool ChangeMoney(int amount)
    {
        // Check if the player has enough money to perform the transaction
        if (playerMoney + amount >= 0)
        {
            playerMoney += amount;
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
        PlayerPrefs.SetInt("PlayerMoney", playerMoney);
    }

    private void LoadPlayerMoney()
    {
        playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + playerMoney.ToString();
        }
    }
}
