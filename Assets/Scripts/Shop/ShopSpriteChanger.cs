using UnityEngine;

public class ShopSpriteChanger : MonoBehaviour
{
    private int currentLevel = 1;
    public SpriteRenderer buildingSpriteRenderer;

    private void Start()
    {
        // Get values from PlayerPrefs or set a default
        currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);

        // Set the sprite after 1sec so the correct level can be loaded from PlayerPrefs
        Invoke(nameof(UpdateSprite), 0.1f);
    }

    public void UpdateSprite()
    {
        // Change the sprite based on the level
        string spriteName = "SHOP_" + currentLevel;
        Sprite levelSprite = Resources.Load<Sprite>("Sprites/Buildings/" + spriteName);
        if (levelSprite != null)
        {
            buildingSpriteRenderer.sprite = levelSprite;
        }
        else
        {
            Debug.LogError("Sprite not found with name: " + spriteName);
        }
    }
}
