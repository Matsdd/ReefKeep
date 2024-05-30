using UnityEngine;

public class BuyChecker : MonoBehaviour
{
    // Reference to the ObjectManager script
    private ObjectManager objectManager;
    public FishSpawnScript fishSpawnScript; // Reference to the FishSpawnScript

    void Start()
    {
        // Set the objectManager
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();

        // Find and set the FishSpawnScript
        fishSpawnScript = GameObject.Find("FishSpawner").GetComponent<FishSpawnScript>();

        // Check if the objectManager reference is found
        if (objectManager == null)
        {
            Debug.LogError("ObjectManager component not found!");
            return;
        }

        // Check if the fishSpawnScript reference is found
        if (fishSpawnScript == null)
        {
            Debug.LogError("FishSpawnScript component not found!");
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
            // Use the new method in FishSpawnScript to spawn the fish
            fishSpawnScript.SpawnFishByName(BoughtFish);
            PlayerPrefs.SetString("BoughtFish", "null");
            PlayerPrefs.Save();
        }
    }
}
