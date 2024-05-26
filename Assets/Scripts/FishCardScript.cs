using UnityEngine;
using UnityEngine.UI;

public class FishCardScript : MonoBehaviour
{
    [SerializeField] private Text nameText;
    private Fishinfo.FishData fishData;

    public void SetFishData(Fishinfo.FishData fishData)
    {
        this.fishData = fishData;
        nameText.text = fishData.name;

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
                Debug.LogError("Failed to load sprite for fish: " + fishData.name);
            }
        }
        else
        {
            Debug.LogError("FishImageScript component not found on card prefab.");
        }

        Button button = GetComponentInChildren<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners(); // Clear any previous listeners
            button.onClick.AddListener(() => DetailPanelController.Instance.ShowDetails(fishData));
        }
        else
        {
            Debug.LogError("Button component not found.");
        }
    }
}
