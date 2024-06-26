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
        public int maxCount;
        public List<SpawnCondition> spawnConditions;
    }

    [Serializable]
    public class SpawnCondition
    {
        public string objectType;
        public int requiredCount;
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

    private Dictionary<string, int> fishCounts = new();
    private Dictionary<string, int> underwaterObjectCounts = new();

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
        //Debug.Log("Attempting to spawn fish...");
        GameObject chosenFish = ChooseRandomFish();
        if (chosenFish == null)
        {
            //Debug.Log("No fish available to spawn due to type limits or conditions.");
            return;
        }

        float randomStartPos = UnityEngine.Random.Range(0f, 1f);
        float randomHeightPos = UnityEngine.Random.Range(minSpawnHeight, maxSpawnHeight);
        Vector3 spawnPosition = randomStartPos < 0.5f ? new Vector3(leftBorder, randomHeightPos, 0) : new Vector3(rightBorder, randomHeightPos, 0);
        GameObject newFish = Instantiate(chosenFish, spawnPosition, Quaternion.identity);

        fishList.Add(new FishObject
        {
            name = chosenFish.name,
            xposition = spawnPosition.x,
            yposition = spawnPosition.y,
            instanceID = newFish.GetInstanceID()
        });

        if (fishCounts.ContainsKey(chosenFish.name))
        {
            fishCounts[chosenFish.name]++;
        }
        else
        {
            fishCounts[chosenFish.name] = 1;
        }

        //Debug.Log("Spawned fish: " + chosenFish.name + " at position: " + spawnPosition);
        SaveFishData();
    }

    public void SpawnFishByName(string fishName)
    {
        //Debug.Log("SpawnFishByName is accessed");
        GameObject fishPrefab = Resources.Load<GameObject>("Prefabs/Fishies/ML/" + fishName);

        if (fishPrefab == null)
        {
            //Debug.LogError("Fish prefab not found: " + fishName);
            return;
        }

        Vector3 spawnPosition = new Vector3(0, 0, 0);
        GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

        fishList.Add(new FishObject
        {
            name = fishPrefab.name,
            xposition = spawnPosition.x,
            yposition = spawnPosition.y,
            instanceID = newFish.GetInstanceID()
        });

        if (fishCounts.ContainsKey(fishPrefab.name))
        {
            fishCounts[fishPrefab.name]++;
        }
        else
        {
            fishCounts[fishPrefab.name] = 1;
        }

        SaveFishData();
    }

    private GameObject ChooseRandomFish()
    {
        //Debug.Log("Choosing random fish...");
        int totalWeight = 0;
        foreach (FishType fish in fishTypes)
        {
            bool canSpawn = CanSpawnFish(fish);
            bool underMaxCount = !fishCounts.ContainsKey(fish.prefab.name) || fishCounts[fish.prefab.name] < fish.maxCount;

            if (canSpawn && underMaxCount)
            {
                totalWeight += fish.spawnWeight;
                //Debug.Log($"Fish: {fish.prefab.name}, Weight: {fish.spawnWeight}, TotalWeight: {totalWeight}");
            }
            else
            {
                //Debug.Log($"Cannot spawn fish: {fish.prefab.name}, CanSpawn: {canSpawn}, UnderMaxCount: {underMaxCount}");
            }
        }

        if (totalWeight == 0)
        {
            //Debug.Log("Total weight is 0, no fish can be spawned.");
            return null;
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        foreach (FishType fish in fishTypes)
        {
            bool canSpawn = CanSpawnFish(fish);
            bool underMaxCount = !fishCounts.ContainsKey(fish.prefab.name) || fishCounts[fish.prefab.name] < fish.maxCount;

            if (canSpawn && underMaxCount)
            {
                cumulativeWeight += fish.spawnWeight;
                if (randomValue < cumulativeWeight)
                {
                    Debug.Log("Selected fish: " + fish.prefab.name);
                    return fish.prefab;
                }
            }
        }

        //Debug.LogWarning("Failed to select a fish. This should not happen if weights are set correctly.");
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
                    //Debug.Log($"Condition not met for fish type: {fishTypeName}. Required: {condition.requiredCount}, Current: {(fishCounts.ContainsKey(fishTypeName) ? fishCounts[fishTypeName] : 0)}");
                    return false;
                }
            }
            else
            {
                if (!underwaterObjectCounts.ContainsKey(condition.objectType) || underwaterObjectCounts[condition.objectType] < condition.requiredCount)
                {
                    //Debug.Log($"Condition not met for underwater object type: {condition.objectType}. Required: {condition.requiredCount}, Current: {(underwaterObjectCounts.ContainsKey(condition.objectType) ? underwaterObjectCounts[condition.objectType] : 0)}");
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
            //Debug.Log("Loaded JSON Data: " + jsonData);

            try
            {
                FishInEcosystemData data = JsonUtility.FromJson<FishInEcosystemData>(jsonData);
                if (data != null && data.fishObjects != null)
                {
                    fishList.Clear();
                    fishCounts.Clear();

                    foreach (FishObject fish in data.fishObjects)
                    {
                        //Debug.Log("Loading fish: " + fish.name);
                        GameObject fishPrefab = Resources.Load<GameObject>("Prefabs/Fishies/ML/" + fish.name);

                        if (fishPrefab != null)
                        {
                            Vector3 spawnPosition = new Vector3(fish.xposition, fish.yposition, 0);
                            GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

                            fishList.Add(new FishObject
                            {
                                name = fish.name,
                                xposition = spawnPosition.x,
                                yposition = spawnPosition.y,
                                instanceID = newFish.GetInstanceID()
                            });

                            if (fishCounts.ContainsKey(fish.name))
                            {
                                fishCounts[fish.name]++;
                            }
                            else
                            {
                                fishCounts[fish.name] = 1;
                            }

                            //Debug.Log("Spawned fish: " + fish.name + " at position: " + spawnPosition);
                        }
                        else
                        {
                            Debug.LogError("Could not load prefab for fish: " + fish.name);
                        }
                    }

                    SaveFishData();
                }
                else
                {
                    Debug.LogError("Deserialized data is null or fishObjects list is null.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error Deserializing JSON data: " + e.Message);
            }
        }
        else
        {
            //Debug.LogError("File not found: " + fishFilePath);
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
                    underwaterObjectList.Clear();
                    underwaterObjectCounts.Clear();

                    foreach (UnderwaterObject obj in data.ecosystemObjects)
                    {
                        Debug.Log("Loading underwater object: " + obj.objectType);

                        underwaterObjectList.Add(obj);

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

    public void FishLeft()
    {
        // Decrease fish count for the fish that left
        if (fishList.Count > 0)
        {
            FishObject lastFish = fishList[fishList.Count - 1];
            if (fishCounts.ContainsKey(lastFish.name))
            {
                fishCounts[lastFish.name]--;
                if (fishCounts[lastFish.name] <= 0)
                {
                    fishCounts.Remove(lastFish.name);
                }
            }
            fishList.RemoveAt(fishList.Count - 1);
        }

        SaveFishData();
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
    public float zCoordinate;
    public bool flipped;
}