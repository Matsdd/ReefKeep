using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
    public GameObject deleteButton;
    public GameObject beachButton;
    public GameObject delConfirmMenu;

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
    private float holdTimer = 0;

    private List<EcosystemObject> placedObjectsList = new(); // List to store placed objects

    // Reference to FishSpawnScript
    public FishSpawnScript fishSpawnScript;

    void Start()
    {
        // Assuming FishSpawnScript is attached to the same GameObject
        fishSpawnScript = GetComponent<FishSpawnScript>();
        LoadEcosystem();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            allowMovement = false;
            holdTimer = 0;
        }

        if (Input.GetMouseButton(0))
        {
            // Raycast to determine if an object is clicked
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // If there is no object being moved, start moving the object
            if (!isMovingObject && hit.collider != null && hit.collider.CompareTag("BuildableObject"))
            {
                // If held for 1 second
                holdTimer += Time.deltaTime;
                if (holdTimer >= 0.3f)
                {
                    StartMovingObject(hit.collider.gameObject);
                    holdTimer = 0;
                }
            }

            // If the current object is being clicked, allow movement
            if (Input.GetMouseButton(0) && isMovingObject && hit.collider != null && hit.collider.gameObject == selectedObject)
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

    // Function to create a new type of object at a location
    public void CreateNewObject(string objectName)
    {
        GameObject prefab = Array.Find(objectPrefabs, item => item.name == objectName);
        if (prefab != null)
        {
            GameObject newItem = Instantiate(prefab);
            newItem.transform.position = new Vector3(0f, objectY + newItem.GetComponent<SpriteRenderer>().bounds.extents.y, 0f);

            // Add the placed object to the list
            AddObjectToList(objectName, 69420f);

            StartMovingNewObject(newItem);
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
        selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, -1);

        // Show move buttons
        placeButton.SetActive(true);
        cancelButton.SetActive(true);
        deleteButton.SetActive(true);
        beachButton.SetActive(false);
    }

    private void StartMovingNewObject(GameObject obj)
    {
        // Set selectedObject
        selectedObject = obj;
        isMovingObject = true;
        originalPosition = new Vector3(69420f, -15.9f, 0f);
        selectedObjectRenderer = selectedObject.GetComponent<SpriteRenderer>();
        selectedObjectRenderer.color = Color.red;
        selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, -1);

        // Show move buttons
        placeButton.SetActive(true);
        cancelButton.SetActive(false);
        deleteButton.SetActive(true);
        beachButton.SetActive(false);
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
        newPosition.z = -1;

        // Check for collisions with objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector3(newPosition.x, objectY, newPosition.z), 0.01f);

        // Filter colliders by tag
        colliders = colliders.Where(c => c.CompareTag("BuildableObject")).ToArray();

        // Check if there are more than one colliders
        if (colliders.Length > 1)
        {
            selectedObjectRenderer.color = Color.red;
        }
        else
        {
            selectedObjectRenderer.color = Color.green;
        }

        selectedObject.transform.position = newPosition;
    }

    // Cancel movement and place the object back in the original position
    public void CancelMovingObject()
    {
        selectedObject.transform.position = originalPosition;
        selectedObjectRenderer.color = Color.white;
        selectedObject = null;
        isMovingObject = false;

        // Hide move buttons
        placeButton.SetActive(false);
        cancelButton.SetActive(false);
        deleteButton.SetActive(false);
        beachButton.SetActive(true);
    }

    // Confirm movement of the object into the new position
    public void ConfirmMovingObject()
    {
        // Restore the original color and clear the selected object
        if (selectedObject != null)
        {
            // Check if the object is red (because it's colliding)
            if (selectedObjectRenderer.color == Color.red)
            {
                GameManager.instance.ShowMessage("Object is colliding!");
                return;
            }

            // Edit the existing list
            EditObjectInList(selectedObject.transform.position.x);

            selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, 0);
            selectedObjectRenderer.color = Color.white;

            selectedObject = null;
            isMovingObject = false;
        }

        placeButton.SetActive(false);
        cancelButton.SetActive(false);
        deleteButton.SetActive(false);
        beachButton.SetActive(true);
    }

    // Function to place a new object in the list
    private void AddObjectToList(string objectType, float xCoordinate)
    {
        EcosystemObject newObject = new EcosystemObject
        {
            objectType = objectType,
            xCoordinate = xCoordinate
        };
        placedObjectsList.Add(newObject);
        SaveEcosystem();
    }

    // Function to edit an item with the old X value
    private void EditObjectInList(float xCoordinateNew)
    {
        // Find the index of the object with the same type and coordinates
        int existingIndex = placedObjectsList.FindIndex(obj => Mathf.Approximately(obj.xCoordinate, originalPosition.x));

        // Check if the object with the same coordinates exists
        if (existingIndex != -1)
        {
            // Update the x coordinate of the existing object
            placedObjectsList[existingIndex].xCoordinate = xCoordinateNew;
            SaveEcosystem();
        }
        else
        {
            Debug.LogError("Error confirming object into list!" + existingIndex);
        }
    }

    // Function to delete an item with the old X value
    public void DeleteObjectFromList()
    {
        // Find the index of the object with the same type and coordinates
        int existingIndex = placedObjectsList.FindIndex(obj => Mathf.Approximately(obj.xCoordinate, originalPosition.x));

        // Check if the object with the same coordinates exists
        if (existingIndex != -1)
        {
            // Delete the object from the scene
            if (selectedObject != null)
            {
                Destroy(selectedObject);
                isMovingObject = false;

                // Hide move buttons
                placeButton.SetActive(false);
                cancelButton.SetActive(false);
                deleteButton.SetActive(false);
                beachButton.SetActive(true);
                delConfirmMenu.SetActive(false);
            }
            else
            {
                Debug.LogError("No selected object to delete.");
            }

            // Delete the object from the list
            placedObjectsList.RemoveAt(existingIndex);
            SaveEcosystem();
        }
        else
        {
            Debug.LogError("Error confirming object into list!" + existingIndex);
        }
    }

    public void OpenDeleteConfirm()
    {
        delConfirmMenu.SetActive(true);
    }

    public void CloseDeleteConfirm()
    {
        delConfirmMenu.SetActive(false);
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
        string filePath = Application.persistentDataPath + "/underwaterObjects.json";
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
        string filePath = Application.persistentDataPath + "/underwaterObjects.json";
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
