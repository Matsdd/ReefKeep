using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyFish : MonoBehaviour
{
    public GameObject fishToSpawn; // Assign the fish object to spawn in the Inspector
    public int aquariumSceneIndex; // Index of the scene where you want to spawn the fish

    public void SpawnFish()
    {
        // Check if the scene index is valid
        if (aquariumSceneIndex >= 0 && aquariumSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("dit kan ik wel");
            // Load the target scene asynchronously by index
            SceneManager.LoadSceneAsync(aquariumSceneIndex, LoadSceneMode.Additive).completed += OnSceneLoaded;
        }
        else
        {
            Debug.LogError("Invalid scene index: " + aquariumSceneIndex);
        }
    }

    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        // Check if the scene is loaded
        if (asyncOperation.isDone)
        {
            Debug.Log("kan je dit sukel");
            // Get the GameObject of the object manager script in the loaded scene
            GameObject objectManagerGO = SceneManager.GetSceneByBuildIndex(aquariumSceneIndex).GetRootGameObjects()[0];
            ObjectManager objectManager = objectManagerGO.GetComponent<ObjectManager>();
            Debug.Log("dit kan je zeker niet");

            // Call the CreateNewObject function from the ObjectManager script
            if (objectManager != null)
            {
                Debug.Log("hij werk hiet wel hoor !");
                objectManager.CreateNewObject("fishToSpawn", -10f);
            }
            else
            {
                Debug.Log("hier gaat het dus fout");
                Debug.LogError("ObjectManager not found in the loaded scene");
            }

            // Unload the previous scene (assuming it's the current active scene)
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
        else
        {
            Debug.LogError("Failed to load scene with index: " + aquariumSceneIndex);
        }
    }
}