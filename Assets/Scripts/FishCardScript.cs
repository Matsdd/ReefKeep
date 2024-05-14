using UnityEngine;
using UnityEngine.UI;

public class FishCardScript : MonoBehaviour
{
    [SerializeField] private Text nameText;

    public void SetFishData(Fishinfo.FishData fishData)
    {
        // Set the name text
        nameText.text = fishData.name;

        // Get the FishImageScript component from the "Fish type Image" child object
        FishImageScript imageScript = GetComponentInChildren<FishImageScript>();

        if (imageScript != null)
        {
            // Load sprite based on the path and set it using FishImageScript
            Sprite sprite = Resources.Load<Sprite>(fishData.spritePath);
            if (sprite != null)
            {
                imageScript.SetFishSprite(sprite);
            }
            else
            {
                Debug.LogError("Failed to load sprite for fish: " + fishData.name);
                Debug.LogError("Sprite path: " + fishData.spritePath);
            }
        }
        else
        {
            Debug.LogError("FishImageScript component not found on card prefab.");
        }
    }
}
