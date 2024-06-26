using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FishFlee
{
    public class FishPreferencesLoader : MonoBehaviour
    {
        private string FishPreferencesPath = string.Empty;
        private string FishInEcosystemPath = string.Empty;
        private string UnderwaterObjectsPath = string.Empty;

        private FishFleePreferences fishFleePreferences;
        private FishInEcosystem fishInEcosystem;
        private UnderwaterObjects underwaterObjects;

        public string fishName;
        private FishControl fishControl;
        private FishSpawnScript fishSpawnScript;

        void Start()
        {
            fishSpawnScript = FindFirstObjectByType<FishSpawnScript>();
            if (fishSpawnScript == null)
            {
                Debug.LogError("FishSpawnScript not found in the scene!");
            }

            FishPreferencesPath = Path.Combine(Application.streamingAssetsPath, "EcoPrefs.json");
            FishInEcosystemPath = Application.persistentDataPath + "/fishInEcosystem.json";
            UnderwaterObjectsPath = Application.persistentDataPath + "/underwaterObjects.json";

            fishControl = GetComponent<FishControl>();

            LoadFishPreferences();
            LoadFishInEcosystem();
            LoadUnderwaterObjects();
            CheckFishLikes();
        }

        private void LoadFishPreferences()
        {
            if (File.Exists(FishPreferencesPath))
            {
                string jsonText = File.ReadAllText(FishPreferencesPath);
                fishFleePreferences = JsonUtility.FromJson<FishFleePreferences>(jsonText);
                if (fishFleePreferences == null)
                {
                    Debug.LogError($"Failed to parse FishFleePreferences from JSON at {FishPreferencesPath}");
                }
            }
            else
            {
                Debug.LogError($"JSON file not found at {FishPreferencesPath}");
            }
        }

        private void LoadFishInEcosystem()
        {
            if (File.Exists(FishInEcosystemPath))
            {
                string jsonText = File.ReadAllText(FishInEcosystemPath);
                fishInEcosystem = JsonUtility.FromJson<FishInEcosystem>(jsonText);
                if (fishInEcosystem == null)
                {
                    Debug.LogError($"Failed to parse FishInEcosystem from JSON at {FishInEcosystemPath}");
                }
            }
            else
            {
                Debug.LogError($"JSON file not found at {FishInEcosystemPath}");
            }
        }

        private void LoadUnderwaterObjects()
        {
            if (File.Exists(UnderwaterObjectsPath))
            {
                string jsonText = File.ReadAllText(UnderwaterObjectsPath);
                underwaterObjects = JsonUtility.FromJson<UnderwaterObjects>(jsonText);
                if (underwaterObjects == null)
                {
                    Debug.LogError($"Failed to parse UnderwaterObjects from JSON at {UnderwaterObjectsPath}");
                }
            }
            else
            {
                Debug.LogError($"JSON file not found at {UnderwaterObjectsPath}");
            }
        }

        private void CheckFishLikes()
        {
            var fishPref = fishFleePreferences?.fishPreferences?.Find(f => f.name == fishName);
            if (fishPref == null)
            {
                Debug.LogError($"Fish preferences not found for fish: {fishName}");
                return;
            }

            string[] likes = fishPref.likes.Split(new[] { ", " }, System.StringSplitOptions.None);
            HashSet<string> likeSet = new HashSet<string>(likes);

            // Check for liked fish
            if (fishInEcosystem?.fishObjects != null)
            {
                foreach (var fish in fishInEcosystem.fishObjects)
                {
                    if (likeSet.Contains(fish.name))
                    {
                        likeSet.Remove(fish.name);
                    }
                }
            }
            else
            {
                Debug.LogWarning("fishInEcosystem or its fishObjects list is null.");
            }

            // Check for liked ecosystem objects
            if (underwaterObjects?.ecosystemObjects != null)
            {
                foreach (var obj in underwaterObjects.ecosystemObjects)
                {
                    if (likeSet.Contains(obj.objectType))
                    {
                        likeSet.Remove(obj.objectType);
                    }
                }
            }
            else
            {
                Debug.LogWarning("underwaterObjects or its ecosystemObjects list is null.");
            }

            // If all likes are missing, call the flee function
            if (likeSet.Count == likes.Length)
            {
                Debug.Log($"Fish {fishName} does not like the ecosystem. Missing likes: {string.Join(", ", likeSet)}");
                CallFleeFunction();
            }
            else
            {
                Debug.Log($"Fish {fishName} likes all the objects and fish in the ecosystem.");
            }
        }

        private void CallFleeFunction()
        {
            if (fishControl != null)
            {
                fishControl.Flee();

                // Remove fish object from fishInEcosystem
                if (fishInEcosystem != null && fishInEcosystem.fishObjects != null)
                {
                    int instanceIDToRemove = gameObject.GetInstanceID();
                    fishInEcosystem.RemoveFishObject(instanceIDToRemove);

                    // Save updated fishInEcosystem to file
                    SaveFishInEcosystem();

                    // Notify FishSpawnScript that a fish has left
                    if (fishSpawnScript != null)
                    {
                        fishSpawnScript.FishLeft();
                    }
                }
                else
                {
                    Debug.LogWarning("FishInEcosystem or its fishObjects list is null.");
                }
            }
            else
            {
                Debug.LogError("FishControl component not found.");
            }
        }

        private void SaveFishInEcosystem()
        {
            if (fishInEcosystem != null)
            {
                string jsonText = JsonUtility.ToJson(fishInEcosystem);
                File.WriteAllText(FishInEcosystemPath, jsonText);
                Debug.Log($"Updated fishInEcosystem saved to {FishInEcosystemPath}");
            }
            else
            {
                Debug.LogError("fishInEcosystem is null. Cannot save.");
            }
        }

        public FishFleePreferences GetFishPreferences()
        {
            return fishFleePreferences;
        }
    }

    [System.Serializable]
    public class FishPreference
    {
        public string name;
        public string likes;
        public string dislikes;
    }

    [System.Serializable]
    public class FishFleePreferences
    {
        public List<FishPreference> fishPreferences;
    }

    [System.Serializable]
    public class FishObject
    {
        public string name;
        public float xposition;
        public float yposition;
        public int instanceID;
    }

    [System.Serializable]
    public class FishInEcosystem
    {
        public List<FishObject> fishObjects;

        public void RemoveFishObject(int instanceID)
        {
            if (fishObjects != null)
            {
                fishObjects.RemoveAll(fish => fish.instanceID == instanceID);
            }
        }
    }

    [System.Serializable]
    public class EcosystemObject
    {
        public string objectType;
        public float xCoordinate;
        public float zCoordinate;
        public bool flipped;
    }

    [System.Serializable]
    public class UnderwaterObjects
    {
        public List<EcosystemObject> ecosystemObjects;
    }
}
