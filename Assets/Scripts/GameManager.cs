using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int money = 0;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI incomeText;
    public Image incomeBackground;
    private Coroutine hideMessageCoroutine;
    private Coroutine hideIncomeCoroutine;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

            // Add money SFX
            SfxManager.instance.playSfx("sCoins");

            // Set the text to show the amount with + or - sign
            incomeText.text = (amount > 0 ? "+" : "") + amount.ToString();

            // Set the text color based on income or expense
            Color textColor = incomeText.color;
            textColor.a = 1f;
            textColor = amount > 0 ? Color.green : Color.red;
            incomeText.color = textColor;

            // Ensure the background is fully opaque
            Color backgroundColor = incomeBackground.color;
            backgroundColor.a = 0.5f;
            incomeBackground.color = backgroundColor;

            incomeText.enabled = true;
            incomeBackground.enabled = true;

            // Stop any ongoing HideIncome coroutine
            if (hideIncomeCoroutine != null)
            {
                StopCoroutine(hideIncomeCoroutine);
            }

            // Start a new HideIncome coroutine
            hideIncomeCoroutine = StartCoroutine(HideIncome());

            SavePlayerMoney();
            UpdateMoneyUI();
            return true; // Transaction successful
        }
        else
        {
            return false; // Not enough money
        }
    }

    // Function to hide the income message
    private IEnumerator HideIncome()
    {
        // Wait for 2 seconds before starting the fade-out effect
        yield return new WaitForSeconds(1.5f);

        // Fade away the text color gradually
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.05f);

            // Calculate the new alpha value
            float newTextAlpha = incomeText.color.a - 0.05f;
            float newBackAlpha = incomeBackground.color.a - 0.05f;

            // Set the new alpha value for the text color
            Color textColor = incomeText.color;
            textColor.a = newTextAlpha;
            incomeText.color = textColor;

            // Set the new alpha value for the background color
            Color backgroundColor = incomeBackground.color;
            backgroundColor.a = newBackAlpha;
            incomeBackground.color = backgroundColor;
        }

        // Disable the text
        incomeText.enabled = false;
        incomeBackground.enabled = false;
        hideIncomeCoroutine = null;
    }

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
    public void ShowMessage(string en, string nl)
    {
        // Set and show the message text
        if (PlayerPrefs.GetInt("LocaleID") == 0)
        {
            messageText.text = en;
        }
        else
        {
            messageText.text = nl;
        }

        // Make sure the alpha is 1
        Color textColor = messageText.color;
        textColor.a = 1f;
        messageText.color = textColor;

        messageText.enabled = true;

        // Stop any ongoing HideMessage coroutine
        if (hideMessageCoroutine != null)
        {
            StopCoroutine(hideMessageCoroutine);
        }

        // Start a new HideMessage coroutine
        hideMessageCoroutine = StartCoroutine(HideMessage());
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
        hideMessageCoroutine = null;
    }
}
