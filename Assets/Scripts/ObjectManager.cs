using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

// Define a custom class to represent the saved data for each placed item
[Serializable]
public class PlacedObjectData
{
    public int id;
    public string objectName;
    public Vector3 position;

    public PlacedObjectData(int id, string objectName, Vector3 position)
    {
        this.id = id;
        this.objectName = objectName;
        this.position = position;
    }
}

public class ObjectManager : MonoBehaviour
{
    // List to store the placed item data
    private List<PlacedObjectData> placedObjects = new List<PlacedObjectData>();
    private int nextObjectId = 1;

    public GameObject[] objectPrefabs;
    public GameObject placeButton;
    public GameObject cancelButton;

    private float objectSnapDistance = 0.5f;
    private float minX = -14f;
    private float maxX = 14f;

    private GameObject selectedObject;
    private bool isMovingObject = false;
    public static bool isHoldingObject = false;
    private bool allowMovement = false;
    private Vector3 originalPosition;
    private SpriteRenderer selectedObjectRenderer;

    void Start()
    {
        LoadPlacedObjects();
    }

    public void SavePlacedObject(string objectName, Vector3 position)
    {
        int id = GetOrCreateObjectId(objectName);
        PlacedObjectData data = placedObjects.Find(obj => obj.id == id);
        if (data != null)
        {
            data.position = position;
        }
        else
        {
            placedObjects.Add(new PlacedObjectData(id, objectName, position));
        }
        PlayerPrefs.SetString("PlacedObjects", JsonUtility.ToJson(placedObjects));
        PlayerPrefs.Save();
    }

    public void LoadPlacedObjects()
    {
        string json = PlayerPrefs.GetString("PlacedObjects", "");
        Debug.Log("Loaded PlacedObjects JSON: " + json);

        if (!string.IsNullOrEmpty(json))
        {
            placedObjects = JsonUtility.FromJson<List<PlacedObjectData>>(json);
            nextObjectId = placedObjects.Count > 0 ? placedObjects.Max(obj => obj.id) + 1 : 1;

            foreach (PlacedObjectData objectData in placedObjects)
            {
                // Find prefab by ID or name, instead of name
                GameObject prefab = objectPrefabs.FirstOrDefault(obj => obj.name == objectData.objectName);
                if (prefab != null)
                {
                    GameObject newObj = Instantiate(prefab, objectData.position, Quaternion.identity);
                    Debug.Log("Instantiated object: " + newObj.name);
                }
                else
                {
                    Debug.LogError("Object prefab not found for name: " + objectData.objectName);
                }
            }
        }
    }

    private int GetOrCreateObjectId(string objectName)
    {
        PlacedObjectData data = placedObjects.Find(obj => obj.objectName == objectName);
        if (data != null)
        {
            return data.id;
        }
        else
        {
            int newId = nextObjectId++;
            return newId;
        }
    }

    public void CreateObject(string objectName)
    {
        // Find the corresponding item prefab by name
        GameObject prefab = Array.Find(objectPrefabs, item => item.name == objectName);
        if (prefab != null)
        {
            // Instantiate the item prefab
            GameObject newItem = Instantiate(prefab);

            // Set the position of the new item in the scene
            newItem.transform.position = new Vector3(0f, -9.2f + newItem.GetComponent<SpriteRenderer>().bounds.extents.y, 0f); // Set the desired position

            // Save the position of the newly placed item
            SavePlacedObject(objectName, newItem.transform.position);
            Debug.Log(objectName);
        }
        else
        {
            Debug.LogError("Item prefab not found for name: " + objectName);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            allowMovement = false;
        }

        if (Input.GetMouseButton(0))
        {
            // Raycast to determine if an object is clicked
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!isMovingObject && hit.collider != null && hit.collider.CompareTag("BuildableObject"))
            {
                StartMovingObject(hit.collider.gameObject);
            }

            if (Input.GetMouseButtonDown(0) && isMovingObject && hit.collider != null && hit.collider.CompareTag("BuildableObject"))
            {
                allowMovement = true;
            }

            if (allowMovement)
            {
                isHoldingObject = true;
                MoveSelectedObject();
            }
            else
            {
                isHoldingObject = false;
            }
        }
    }

    void StartMovingObject(GameObject obj)
    {
        selectedObject = obj;
        isMovingObject = true;
        originalPosition = selectedObject.transform.position;
        selectedObjectRenderer = selectedObject.GetComponent<SpriteRenderer>();
        selectedObjectRenderer.color = Color.green;

        // Show move buttons
        placeButton.SetActive(true);
        cancelButton.SetActive(true);

    }

    void MoveSelectedObject()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z-coordinate is zero

        // Snap the object to the grid
        Vector3 newPosition = mousePosition;
        newPosition.x = Mathf.Round(newPosition.x / objectSnapDistance) * objectSnapDistance;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = -9.2f + selectedObject.GetComponent<SpriteRenderer>().bounds.extents.y;
        selectedObject.transform.position = newPosition;
    }

    public void CancelMovingObject()
    {
        selectedObject.transform.position = originalPosition;
        selectedObjectRenderer.color = Color.white;
        selectedObject = null;
        isMovingObject = false;

        // Hide move buttons
        placeButton.SetActive(false);
        cancelButton.SetActive(false);
    }

    public void ConfirmMovingObject()
    {
        // Save object
        if (selectedObject != null)
        {
            string objectName = selectedObject.name.Replace("(Clone)", "").Trim();
            SavePlacedObject(objectName, selectedObject.transform.position);
            Debug.Log("Saved object: " + objectName);
        }

        // Restore the original color and clear the selected object
        if (selectedObject != null)
        {
            selectedObjectRenderer.color = Color.white;
            selectedObject = null;
            isMovingObject = false;
        }

        // Hide move buttons
        placeButton.SetActive(false);
        cancelButton.SetActive(false);
    }

}
