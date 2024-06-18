using UnityEngine;
using UnityEngine.UI;

public class FishCardScript : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text likesText;
    [SerializeField] private Text dislikesText;

    private Fishinfo.FishData fishData;

    public void SetFishData(Fishinfo.FishData fishData)
    {
        this.fishData = fishData;
        //Set data on card in English or Dutch
        nameText.text = (PlayerPrefs.GetInt("LocaleID") == 0) ? fishData.name_en : fishData.name_nl;
        likesText.text = (PlayerPrefs.GetInt("LocaleID") == 0) ? "Likes:" : "Houdt van:";
        dislikesText.text = (PlayerPrefs.GetInt("LocaleID") == 0) ? "Dislikes:" : "Haat:";

        //Set the image on the card
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

        //Open the detail panel when pressing somewhere on the card
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
