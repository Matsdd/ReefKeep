using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class FishSpawnScript : MonoBehaviour
{
    [Serializable]
    public class FishType
    {
        public GameObject prefab;
        public int spawnWeight;
        public int maxCount; // Maximum number of this fish type allowed
        public List<SpawnCondition> spawnConditions; // List of conditions for spawning
    }

    [Serializable]
    public class SpawnCondition
    {
        public string objectType; // Type of object (e.g., fish type, underwater object)
        public int requiredCount; // Required count of the object type to allow spawning
    }

    public float spawnRate = 1;
    private float timer = 0;

    public float leftBorder = -30;
    public float rightBorder = 30;
    public float maxSpawnHeight = 13;
    public float minSpawnHeight = -13;

    public FishType[] fishTypes;

    private List<FishObject> fishList = new();
    private List<UnderwaterObject> underwaterObjectList = new();
    private string fishFilePath;
    private string underwaterObjectFilePath;

    private Dictionary<string, int> fishCounts = new(); // Dictionary to keep track of fish counts
    private Dictionary<string, int> underwaterObjectCounts = new(); // Dictionary to keep track of underwater object counts

    private void Start()
    {
        fishFilePath = Application.persistentDataPath + "/fishInEcosystem.json";
        underwaterObjectFilePath = Application.persistentDataPath + "/underwaterObjects.json";
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        LoadEcosystemData();
    }

    private void Update()
    {
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
        GameObject chosenFish = ChooseRandomFish();
        if (chosenFish == null)
        {
            Debug.Log("No fish available to spawn due to type limits or conditions.");
            return;
        }

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

        // Update fish count
        if (fishCounts.ContainsKey(chosenFish.name))
        {
            fishCounts[chosenFish.name]++;
        }
        else
        {
            fishCounts[chosenFish.name] = 1;
        }

        SaveFishData();
    }

    public void SpawnFishByName(string fishName)
    {

        Debug.Log("SpawnFishByName is accessed");
        // Load the fish prefab from Resources
        GameObject fishPrefab = Resources.Load<GameObject>("Prefabs/Fishies/" + fishName);

        if (fishPrefab == null)
        {
            Debug.LogError("Fish prefab not found: " + fishName);
            return;
        }

        // Generate a random spawn position within the defined borders
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

        // Add the spawned fish to the list
        fishList.Add(new FishObject
        {
            name = fishPrefab.name,
            xposition = spawnPosition.x,
            yposition = spawnPosition.y,
            instanceID = newFish.GetInstanceID()
        });

        // Update fish count
        if (fishCounts.ContainsKey(fishPrefab.name))
        {
            fishCounts[fishPrefab.name]++;
        }
        else
        {
            fishCounts[fishPrefab.name] = 1;
        }

        // Save the updated fish data
        SaveFishData();
    }


    private GameObject ChooseRandomFish()
    {
        int totalWeight = 0;
        foreach (FishType fish in fishTypes)
        {
            if (CanSpawnFish(fish) && (!fishCounts.ContainsKey(fish.prefab.name) || fishCounts[fish.prefab.name] < fish.maxCount))
            {
                totalWeight += fish.spawnWeight;
            }
        }

        if (totalWeight == 0)
        {
            return null; // No fish can be spawned due to conditions or max counts
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        foreach (FishType fish in fishTypes)
        {
            if (CanSpawnFish(fish) && (!fishCounts.ContainsKey(fish.prefab.name) || fishCounts[fish.prefab.name] < fish.maxCount))
            {
                cumulativeWeight += fish.spawnWeight;
                if (randomValue < cumulativeWeight)
                {
                    return fish.prefab;
                }
            }
        }

        // This should never happen if weights are set up correctly
        return null;
    }

    private bool CanSpawnFish(FishType fish)
    {
        foreach (SpawnCondition condition in fish.spawnConditions)
        {
            if (condition.objectType.StartsWith("Fish:"))
            {
                string fishTypeName = condition.objectType.Substring(5);
                if (!fishCounts.ContainsKey(fishTypeName) || fishCounts[fishTypeName] < condition.requiredCount)
                {
                    return false;
                }
            }
            else
            {
                if (!underwaterObjectCounts.ContainsKey(condition.objectType) || underwaterObjectCounts[condition.objectType] < condition.requiredCount)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void LoadEcosystemData()
    {
        LoadFishData();
        LoadUnderwaterObjectData();
    }

    private void LoadFishData()
    {
        if (File.Exists(fishFilePath))
        {
            string jsonData = File.ReadAllText(fishFilePath);
            Debug.Log("Loaded JSON Data: " + jsonData);

            try
            {
                FishInEcosystemData data = JsonUtility.FromJson<FishInEcosystemData>(jsonData);
                if (data != null && data.fishObjects != null)
                {
                    fishList.Clear(); // Clear the current list
                    fishCounts.Clear(); // Clear the fish counts

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

                            // Update fish count
                            if (fishCounts.ContainsKey(fish.name))
                            {
                                fishCounts[fish.name]++;
                            }
                            else
                            {
                                fishCounts[fish.name] = 1;
                            }

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
            Debug.LogError("File not found: " + fishFilePath);
        }
    }

    private void LoadUnderwaterObjectData()
    {
        if (File.Exists(underwaterObjectFilePath))
        {
            string jsonData = File.ReadAllText(underwaterObjectFilePath);
            Debug.Log("Loaded JSON Data: " + jsonData);

            try
            {
                UnderwaterObjectsData data = JsonUtility.FromJson<UnderwaterObjectsData>(jsonData);
                if (data != null && data.ecosystemObjects != null)
                {
                    underwaterObjectList.Clear(); // Clear the current list
                    underwaterObjectCounts.Clear(); // Clear the underwater object counts

                    foreach (UnderwaterObject obj in data.ecosystemObjects)
                    {
                        Debug.Log("Loading underwater object: " + obj.objectType);

                        // Add the object to the list
                        underwaterObjectList.Add(obj);

                        // Update object count
                        if (underwaterObjectCounts.ContainsKey(obj.objectType))
                        {
                            underwaterObjectCounts[obj.objectType]++;
                        }
                        else
                        {
                            underwaterObjectCounts[obj.objectType] = 1;
                        }
                    }
                }
                else
                {
                    Debug.LogError("Deserialized data is null or ecosystemObjects list is null.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error deserializing JSON data: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("File not found: " + underwaterObjectFilePath);
        }
    }

    public void AddUnderwaterObject(string objectType, float xCoordinate)
    {
        UnderwaterObject newObj = new UnderwaterObject { objectType = objectType, xCoordinate = xCoordinate };
        underwaterObjectList.Add(newObj);

        // Update object count
        if (underwaterObjectCounts.ContainsKey(objectType))
        {
            underwaterObjectCounts[objectType]++;
        }
        else
        {
            underwaterObjectCounts[objectType] = 1;
        }

        SaveUnderwaterObjectData();
    }

    private void SaveFishData()
    {
        string jsonData = JsonUtility.ToJson(new FishInEcosystemData { fishObjects = fishList });
        File.WriteAllText(fishFilePath, jsonData);
    }

    private void SaveUnderwaterObjectData()
    {
        string jsonData = JsonUtility.ToJson(new UnderwaterObjectsData { ecosystemObjects = underwaterObjectList });
        File.WriteAllText(underwaterObjectFilePath, jsonData);
    }

    private void RemoveFishFromList(GameObject fishToRemove)
    {
        int fishInstanceID = fishToRemove.GetInstanceID();
        FishObject fishObjectToRemove = fishList.Find(fish => fish.instanceID == fishInstanceID);
        if (fishObjectToRemove != null)
        {
            fishList.Remove(fishObjectToRemove);

            // Update fish count
            if (fishCounts.ContainsKey(fishObjectToRemove.name))
            {
                fishCounts[fishObjectToRemove.name]--;
                if (fishCounts[fishObjectToRemove.name] <= 0)
                {
                    fishCounts.Remove(fishObjectToRemove.name);
                }
            }

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
        LoadEcosystemData();
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

[Serializable]
public class UnderwaterObjectsData
{
    public List<UnderwaterObject> ecosystemObjects = new List<UnderwaterObject>();
}

[Serializable]
public class UnderwaterObject
{
    public string objectType;
    public float xCoordinate;
}
