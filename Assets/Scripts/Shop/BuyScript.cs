using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyScript : MonoBehaviour
{
    public string itemName;
    public int cost;

    public void BuyObject()
    {
        // Try to buy the object, cancel if not enough money
        if (GameManager.instance.ChangeMoney(-cost))
        {
            PlayerPrefs.SetString("BoughtObject", itemName);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Underwater", LoadSceneMode.Single);
        }
        else
        {
            GameManager.instance.ShowMessage("Not enough money!", "Niet genoeg geld!");
        }
    }

    public void BuyFish()
    {
        // Try to buy the fish, cancel if not enough money
        if (GameManager.instance.ChangeMoney(-cost))
        {
            PlayerPrefs.SetString("BoughtFish", itemName);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Underwater", LoadSceneMode.Single);
        }
        else
        {
            GameManager.instance.ShowMessage("Not enough money!", "Niet genoeg geld!");
        }
    }
}
