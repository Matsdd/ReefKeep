using UnityEngine;
using UnityEngine.UI;

public class DetailPanelController : MonoBehaviour
{
    public static DetailPanelController Instance;

    [SerializeField] private GameObject detailPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Image fishImage;
    [SerializeField] private Text factText;
    [SerializeField] private Text likesText;
    [SerializeField] private Text dislikesText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Hide the detail panel initially
        detailPanel.SetActive(false);

        // Add listener to the close button
        closeButton.onClick.AddListener(HideDetails);
    }

    public void ShowDetails(Fishinfo.FishData data)
    {
        Debug.Log("Showing details for: " + data.name);
        nameText.text = data.name;
        factText.text = data.fact;
        likesText.text = data.likes;
        dislikesText.text = data.dislikes;

        // Load and set the fish sprite
        Sprite sprite = Resources.Load<Sprite>(data.spritePath);
        if (sprite != null)
        {
            fishImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("Failed to load sprite for fish: " + data.name);
        }

        // Show the detail panel
        detailPanel.SetActive(true);
    }

    public void HideDetails()
    {
        detailPanel.SetActive(false);
    }
}
