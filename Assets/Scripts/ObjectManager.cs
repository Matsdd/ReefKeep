using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class EcosystemData
{
    public List<EcosystemObject> ecosystemObjects;
}


[Serializable]
public class EcosystemObject
{
    public string objectType;
    public float xCoordinate;
}


public class ObjectManager : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public GameObject placeButton;
    public GameObject cancelButton;

    private readonly float objectSnapDistance = 0.5f;
    private readonly float minX = -25f;
    private readonly float maxX = 25f;
    private readonly float objectY = -15.9f;

    public GameObject selectedObject;
    private bool isMovingObject = false;
    public static bool isHoldingObject = false;
    private bool allowMovement = false;
    private Vector3 originalPosition;
    private SpriteRenderer selectedObjectRenderer;

    private List<EcosystemObject> placedObjectsList = new(); // List to store placed objects

    void Start()
    {
        LoadEcosystem();
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

            // If there is no object being moved, start moving the object
            if (!isMovingObject && hit.collider != null && hit.collider.CompareTag("BuildableObject"))
            {
                StartMovingObject(hit.collider.gameObject);
            }

            // If the current object is being clicked, allow movement
            if (Input.GetMouseButtonDown(0) && isMovingObject && hit.collider != null && hit.collider.gameObject == selectedObject)
            {
                allowMovement = true;
            }

            // If the object is allowed to move, move it
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

    // Function to create rock (for testing, will be moved to shopmenu later)
    public void CreateRock()
    {
        CreateNewObject("Rock", 0);
    }

    // Function to create a new type of objet at a location
    public void CreateNewObject(string objectName, float xCoordinate)
    {
        GameObject prefab = Array.Find(objectPrefabs, item => item.name == objectName);
        if (prefab != null)
        {
            GameObject newItem = Instantiate(prefab);
            newItem.transform.position = new Vector3(xCoordinate, objectY + newItem.GetComponent<SpriteRenderer>().bounds.extents.y, 0f);

            // Add the placed object to the list
            AddObjectToList(objectName, xCoordinate);
        }
        else
        {
            Debug.LogError("Item prefab not found for name: " + objectName);
        }
    }

    // Get everything ready for moving objects
    private void StartMovingObject(GameObject obj)
    {
        // Set selectedObject
        selectedObject = obj;
        isMovingObject = true;
        originalPosition = selectedObject.transform.position;
        selectedObjectRenderer = selectedObject.GetComponent<SpriteRenderer>();
        selectedObjectRenderer.color = Color.green;

        // Show move buttons
        placeButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    // Actually move the selected object
    private void MoveSelectedObject()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Snap the object to the grid
        Vector3 newPosition = mousePosition;
        newPosition.x = Mathf.Round(newPosition.x / objectSnapDistance) * objectSnapDistance;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = objectY + selectedObject.GetComponent<SpriteRenderer>().bounds.extents.y;
        selectedObject.transform.position = newPosition;
    }

    // Cancel movement and place the object back in the origional position
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

    // Confirm movement of the object into the new position
    public void ConfirmMovingObject()
    {
        // Restore the original color and clear the selected object
        if (selectedObject != null)
        {
            // Edit the existing list
            float xCoordinate = selectedObject.transform.position.x;
            EditObjectInList(xCoordinate);

            selectedObjectRenderer.color = Color.white;
            selectedObject = null;
            isMovingObject = false;
        }

        placeButton.SetActive(false);
        cancelButton.SetActive(false);
    }

    // Function to place a new object in the list
    private void AddObjectToList(string objectType, float xCoordinate)
    {
        EcosystemObject newObject = new EcosystemObject();
        newObject.objectType = objectType;
        newObject.xCoordinate = xCoordinate;
        placedObjectsList.Add(newObject);
        SaveEcosystem();
    }

    // Function to edit an item with the old X value
    private void EditObjectInList(float xCoordinate)
    {
        // Find the index of the object with the same type and coordinates
        int existingIndex = placedObjectsList.FindIndex(obj => Mathf.Approximately(obj.xCoordinate, originalPosition.x));

        // Check if the object with the same type and coordinates exists
        if (existingIndex != -1)
        {
            // Update the x coordinate of the existing object
            placedObjectsList[existingIndex].xCoordinate = xCoordinate;
            // Save the updated ecosystem
            SaveEcosystem();
        }

        else
        {
            Debug.LogError("Error confirming object into list!" + existingIndex);
        }
    }

    private void SaveEcosystem()
    {
        // Create an instance of EcosystemData and assign the placedObjectsList to its ecosystemObjects field
        EcosystemData data = new()
        {
            ecosystemObjects = placedObjectsList
        };

        // Serialize the data to JSON
        string json = JsonUtility.ToJson(data);

        // Write the JSON string to the file
        string filePath = Application.persistentDataPath + "/ecosystem.json";
        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log("Ecosystem Saved. File path: " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving ecosystem: " + e.Message);
        }
    }

    private void LoadEcosystem()
    {
        // Read the JSON string from the file
        string filePath = Application.persistentDataPath + "/ecosystem.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON string to EcosystemData
            EcosystemData data = JsonUtility.FromJson<EcosystemData>(json);

            // Assign the list of EcosystemObject from EcosystemData to placedObjectsList
            placedObjectsList = data.ecosystemObjects;

            // Loop through the list and create objects
            foreach (var obj in placedObjectsList)
            {
                GameObject prefab = Array.Find(objectPrefabs, item => item.name == obj.objectType);
                if (prefab != null)
                {
                    GameObject newItem = Instantiate(prefab);
                    newItem.transform.position = new Vector3(obj.xCoordinate, objectY + newItem.GetComponent<SpriteRenderer>().bounds.extents.y, 0f);
                }
                else
                {
                    Debug.LogError("Item prefab not found for name: " + obj.objectType);
                }
            }
        }
        else
        {
            Debug.LogWarning("Ecosystem file not found: " + filePath);
        }
    }
}