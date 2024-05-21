using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using System;

public class FishSpawnScript : MonoBehaviour
{
    public float spawnRate = 1;
    private float timer = 0;

    public float leftBorder = -30;
    public float rightBorder = 30;
    public float maxSpawnHeight = 13;
    public float minSpawnHeight = -13;

    public GameObject fish1;
    public int fish1SpawnWeight = 1;

    public GameObject fish2;
    public int fish2SpawnWeight = 1;

    private List<FishObject> fishList = new();
    private string filePath;

    private const int populationLimit = 20;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/fishInEcosystem.json";
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        LoadFishData();
    }

    private void Update()
    {
        if (fishList.Count >= populationLimit)
        {
            return; // Stop spawning if the population limit is reached
        }

        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnFish();
            timer = 0;
        }
    }

    private void SpawnFish()
    {
        if (fishList.Count >= populationLimit)
        {
            Debug.Log("Population limit reached. No more fish will be spawned.");
            return;
        }

        GameObject chosenFish = UnityEngine.Random.Range(0, fish1SpawnWeight + fish2SpawnWeight) < fish1SpawnWeight ? fish1 : fish2;
        float randomStartPos = UnityEngine.Random.Range(0f, 1f);
        float randomHeightPos = UnityEngine.Random.Range(minSpawnHeight, maxSpawnHeight);
        Vector3 spawnPosition = randomStartPos < 0.5f ? new Vector3(leftBorder, randomHeightPos, 0) : new Vector3(rightBorder, randomHeightPos, 0);
        GameObject newFish = Instantiate(chosenFish, spawnPosition, Quaternion.identity);

        // Add spawned fish to the list with instance ID
        fishList.Add(new FishObject
        {
            name = chosenFish.name,
            xposition = spawnPosition.x,
            yposition = spawnPosition.y,
            instanceID = newFish.GetInstanceID()
        });
        SaveFishData();
    }

    private void SaveFishData()
    {
        string jsonData = JsonUtility.ToJson(new FishInEcosystemData { fishObjects = fishList });
        File.WriteAllText(filePath, jsonData);
    }

    private void LoadFishData()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            Debug.Log("Loaded JSON Data: " + jsonData);

            try
            {
                FishInEcosystemData data = JsonUtility.FromJson<FishInEcosystemData>(jsonData);
                if (data != null && data.fishObjects != null)
                {
                    fishList.Clear(); // Clear the current list

                    foreach (FishObject fish in data.fishObjects)
                    {
                        Debug.Log("Loading fish: " + fish.name);
                        GameObject fishPrefab = Resources.Load<GameObject>("Prefabs/Fishies/" + fish.name);

                        if (fishPrefab != null)
                        {
                            Vector3 spawnPosition = new Vector3(fish.xposition, fish.yposition, 0);
                            GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

                            // Add the newly spawned fish to the list with a new instance ID
                            fishList.Add(new FishObject
                            {
                                name = fish.name,
                                xposition = spawnPosition.x,
                                yposition = spawnPosition.y,
                                instanceID = newFish.GetInstanceID()
                            });

                            Debug.Log("Spawned fish: " + fish.name + " at position: " + spawnPosition);
                        }
                        else
                        {
                            Debug.LogError("Could not load prefab for fish: " + fish.name);
                        }
                    }

                    SaveFishData(); // Save the updated list with new instance IDs
                }
                else
                {
                    Debug.LogError("Deserialized data is null or fishObjects list is null.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error deserializing JSON data: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }

    private void RemoveFishFromList(GameObject fishToRemove)
    {
        int fishInstanceID = fishToRemove.GetInstanceID();
        FishObject fishObjectToRemove = fishList.Find(fish => fish.instanceID == fishInstanceID);
        if (fishObjectToRemove != null)
        {
            fishList.Remove(fishObjectToRemove);
            SaveFishData();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            RemoveFishFromList(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadFishData();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        SaveFishData();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}

[Serializable]
public class FishInEcosystemData
{
    public List<FishObject> fishObjects = new List<FishObject>();
}

[Serializable]
public class FishObject
{
    public string name;
    public float xposition;
    public float yposition;
    public int instanceID;
}
