using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Fishinfo : MonoBehaviour
{
    public GameObject fishCardPrefab;
    public Transform cardsParent;

    [System.Serializable]
    public class FishData
    {
        public string name;
        public string spritePath;
        public string fact;
        public string likes;
        public string dislikes;
    }

    void Start()
    {
        string jsonFilePath = Application.dataPath + "/fishData.json";
        string jsonData = File.ReadAllText(jsonFilePath);
        jsonData = "{\"fishes\":" + jsonData + "}";
        Fishes fishes = JsonUtility.FromJson<Fishes>(jsonData);

        int cardIndex = 0;
        float cardOffsetX = 410f; 
        float cardOffsetY = -510f; 
        int cardsPerRow = 4;
        float initialX = -cardOffsetX * (cardsPerRow - 1) / 2f;
         
        foreach (FishData fish in fishes.fishes)
        {   
            int row = cardIndex / cardsPerRow;
            int column = cardIndex % cardsPerRow;

            float xPos = initialX + cardOffsetX * column;
            float yPos = cardOffsetY * row;

            GameObject card = Instantiate(fishCardPrefab, cardsParent);
            card.transform.localPosition = new Vector3(xPos, yPos, 0f);

            // Set fish data on the card
            FishCardScript cardScript = card.GetComponent<FishCardScript>();
            if (cardScript != null)
            {
                cardScript.SetFishData(fish);
            }
            else
            {
                Debug.LogError("FishCardScript component not found on card prefab.");
            }

            cardIndex++;
        }
    }

    [System.Serializable]
    public class Fishes
    {
        public List<FishData> fishes;
    }
}
