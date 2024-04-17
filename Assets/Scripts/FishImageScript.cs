using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishImageScript : MonoBehaviour
{
    public void SetFishSprite(Sprite fishSprite)
    {
        Debug.Log("GameObject name: " + gameObject.name);

        // Set the text of the Button dynamically
        Image fishImage = GetComponent<Image>();
        if (fishImage != null)
        {
            fishImage.sprite = fishSprite;
        }
        else
        {
            Debug.LogError("Image component not found.");
        }
    }
}
