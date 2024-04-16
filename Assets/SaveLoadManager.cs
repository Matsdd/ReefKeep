using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static void SaveEcosystem(List<EcosystemObject> ecosystemObjects)
    {
        string json = JsonUtility.ToJson(ecosystemObjects);
        File.WriteAllText(Application.persistentDataPath + "/ecosystem.json", json);
        Debug.Log("Ecosystem Saved:" + json);
    }

    public static List<EcosystemObject> LoadEcosystem()
    {
        string path = Application.persistentDataPath + "/ecosystem.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("Ecosystem Loaded:" + json);
            return JsonUtility.FromJson<List<EcosystemObject>>(json);
        }
        else
        {
            Debug.LogError("No saved ecosystem found.");
            return new List<EcosystemObject>();
        }
    }
}
