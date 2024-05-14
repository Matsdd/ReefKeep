using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFillScript : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Image fishImage;

    public void SetFishData(Fishinfo.FishData fishData)
    {
        // Set the properties of the card based on the fish data
        nameText.text = fishData.name;

        // Load sprite based on the path
        Sprite sprite = Resources.Load<Sprite>(fishData.spritePath);
        if (sprite != null)
        {
            fishImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("Failed to load sprite for fish: " + fishData.name);
        }
    }
}
