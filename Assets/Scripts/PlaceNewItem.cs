using UnityEngine;

public class PlaceNewItem : MonoBehaviour
{
    public GameObject itemPrefab; // Reference to the prefab of the item you want to place

    public void PlaceItem()
    {
        if (GameManager.instance.ChangeMoney(-10))
        {
            // Instantiate a new instance of the item prefab
            GameObject newItem = Instantiate(itemPrefab);

            // Set the position of the new item in the scene
            newItem.transform.position = new Vector3(0f, -9.2f + newItem.GetComponent<SpriteRenderer>().bounds.extents.y, 0f); // Set the desired position
        }
        else
        {
            Debug.Log("Not enough Money!");
        }
    }
}
