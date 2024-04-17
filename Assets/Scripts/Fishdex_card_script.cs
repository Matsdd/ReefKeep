using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this directive to access UI components

public class Fishdex_card_script : MonoBehaviour
{
    [System.Serializable]
    public class FishData // Corrected class name to match FishData
    {
        public string name;
        public Sprite image;
    }

    [SerializeField]
    private GameObject cardPrefabTemplate;

    [SerializeField]
    private FishData[] fishDataArray; // Corrected class name to match FishData

    [SerializeField]
    private int cardsPerRow;

    [SerializeField]
    private float cardWidth;

    [SerializeField]
    private float cardHeight;

    [SerializeField]
    private float cardSpacing;

    public FishButtonScript fishButtonScript;

    public FishImageScript fishImageScript;



    void Start()
    {
        if (fishDataArray == null)
        {
            Debug.LogError("fishDataArray is not assigned.");
            return;
        }



        for (int i = 0; i < fishDataArray.Length; i++)
        {
            InstantiateCard(fishDataArray[i], i);
        }
    }

    void InstantiateCard(FishData fishData, int index) // Corrected class name to match FishData
    {
        GameObject cardPrefab = Instantiate(cardPrefabTemplate, transform); // Line 45
        FishCardScript cardScript = cardPrefab.GetComponent<FishCardScript>();
        cardScript.SetFishData(fishData);

        int row = index / cardsPerRow;
        int col = index % cardsPerRow;

        float xOffset = col * (cardWidth + cardSpacing) -295;
        float yOffset = -row * (cardHeight + cardSpacing) +90;

        cardPrefab.transform.localPosition = new Vector3(xOffset, yOffset, 0f);
        Debug.Log("Fish Name:" + fishData.name);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
