using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyScript : MonoBehaviour
{
    public string itemName;
    public int cost;

    private SceneHandler sceneHandler;

    private void Start()
    {
        sceneHandler = FindFirstObjectByType<SceneHandler>();
    }

    public void BuyObject()
    {
        // Try to buy the object, cancel if not enough money
        if (GameManager.instance.ChangeMoney(-cost))
        {
            PlayerPrefs.SetString("BoughtObject", itemName);
            PlayerPrefs.Save();

            if (sceneHandler != null)
            {
                sceneHandler.ChangeScene(2);
            }
            else
            {
                Debug.LogError("SceneHandler not found in the scene!");
            }
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

            if (sceneHandler != null)
            {
                sceneHandler.ChangeScene(2);
            }
            else
            {
                Debug.LogError("SceneHandler not found in the scene!");
            }
        }
        else
        {
            GameManager.instance.ShowMessage("Not enough money!", "Niet genoeg geld!");
        }
    }
}
