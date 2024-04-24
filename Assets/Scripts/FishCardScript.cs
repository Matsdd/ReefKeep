using UnityEngine;
using UnityEngine.UI;

public class FishCardScript : MonoBehaviour
{
    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Image fishImage;

    public void SetFishData(Fishdex_card_script.FishData fishData) // Corrected class name to match FishData
    {
        // Set the properties of the card based on the fish data
        nameText.text = fishData.name;
        // Add code to set other properties (likes, dislikes, etc.) if needed
        fishImage.sprite = fishData.image;
    }
}
