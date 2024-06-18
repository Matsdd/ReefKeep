using UnityEngine;
using UnityEngine.UI;

public class FishCardScript : MonoBehaviour
{
    // State the name and image of the fish in the Unity Editor
    [SerializeField] private Text nameText;
    private Fishinfo.FishData fishData;

    // Set the fish Data on the cards
    public void SetFishData(Fishinfo.FishData fishData)
    {
        this.fishData = fishData;

        int localeID = PlayerPrefs.GetInt("LocaleID", 0);
        if (localeID == 0) // English
        {
            nameText.text = fishData.name_en;
        }
        else // Dutch
        {
            nameText.text = fishData.name_nl;
        }

        FishImageScript imageScript = GetComponentInChildren<FishImageScript>();
        if (imageScript != null)
        {
            Sprite sprite = Resources.Load<Sprite>(fishData.spritePath);
            if (sprite != null)
            {
                imageScript.SetFishSprite(sprite);
            }
            else
            {
                Debug.LogError("Failed to load sprite for fish: " + fishData.name_nl);
            }
        }
        else
        {
            Debug.LogError("FishImageScript component not found on card prefab.");
        }

        Button button = GetComponentInChildren<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => DetailPanelController.Instance.ShowDetails(fishData));
        }
        else
        {
            Debug.LogError("Button component not found.");
        }
    }
}
