using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int money = 0;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI messageText;

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
        money = PlayerPrefs.GetInt("PlayerMoney", 69);
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = money.ToString();
        }
    }

    // Function to show the player a message
    public void ShowMessage(string message)
    {
        // Set and show the message text
        messageText.text = message;

        // Make sure the alpla is 1
        Color textColor = messageText.color;
        textColor.a = 1f;
        messageText.color = textColor;

        messageText.enabled = true;

        StartCoroutine(HideMessage());
    }

    // Function to hide the message
    private IEnumerator HideMessage()
    {
        // Wait for 2 seconds before starting the fade-out effect
        yield return new WaitForSeconds(1.5f);

        // Fade away the text color gradually
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.05f);

            // Calculate the new alpha value
            float newAlpha = messageText.color.a - 0.05f;

            // Set the new alpha value for the text color
            Color textColor = messageText.color;
            textColor.a = newAlpha;
            messageText.color = textColor;
        }

        // Disable the text
        messageText.enabled = false;
    }
}
