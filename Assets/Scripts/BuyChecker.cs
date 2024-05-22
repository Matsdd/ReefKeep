using UnityEngine;

public class BuyChecker : MonoBehaviour
{
    // Reference to the ObjectManager script
    private ObjectManager objectManager;

    void Start()
    {
        // Set the objectManager
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();

        // Add fishspawner later

        // Check if the objectManager reference is found
        if (objectManager == null)
        {
            Debug.LogError("ObjectManager component not found!");
            return;
        }

        string BoughtObject = PlayerPrefs.GetString("BoughtObject", "null");
        string BoughtFish = PlayerPrefs.GetString("BoughtFish", "null");

        if (BoughtObject != "null")
        {
            objectManager.CreateNewObject(BoughtObject);
            PlayerPrefs.SetString("BoughtObject", "null");
            PlayerPrefs.Save();
        }

        if (BoughtFish != "null")
        {
            // Change this later to the fish spawning code!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            objectManager.CreateNewObject(BoughtFish);
            PlayerPrefs.SetString("BoughtFish", "null");
            PlayerPrefs.Save();
        }
    }
}
