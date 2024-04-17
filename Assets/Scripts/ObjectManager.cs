using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

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
    private readonly float minX = -14f;
    private readonly float maxX = 14f;
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
        // Load the ecosystem when the game starts
        // LoadEcosystem();

        AddObjectToList("Rock", 0);
        Debug.Log(placedObjectsList);
        SaveEcosystem();
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
            string objectType = selectedObject.name;
            float xCoordinate = selectedObject.transform.position.x;

            int existingIndex = placedObjectsList.FindIndex(obj => obj.objectType == objectType && obj.xCoordinate == originalPosition.x);
            if (existingIndex != -1)
            {
                placedObjectsList[existingIndex].xCoordinate = xCoordinate;
            }
            else
            {
                Debug.LogError("Error confirming object into list!" + existingIndex);
            }

            selectedObjectRenderer.color = Color.white;
            selectedObject = null;
            isMovingObject = false;
        }

        placeButton.SetActive(false);
        cancelButton.SetActive(false);

        SaveEcosystem();
    }

    // Function to place a new object in the list
    private void AddObjectToList(string objectType, float xCoordinate)
    {
        EcosystemObject newObject = new EcosystemObject();
        newObject.objectType = objectType;
        newObject.xCoordinate = xCoordinate;
        placedObjectsList.Add(newObject);
    }

    // Save the list into JSON
    private void SaveEcosystem()
    {
        SaveLoadManager.SaveEcosystem(placedObjectsList);
    }

    private void LoadEcosystem()
    {
        // Fill the list from the JSON
        placedObjectsList = SaveLoadManager.LoadEcosystem();

        // Loop through the list and create a new object for each
        foreach (var obj in placedObjectsList)
        {
            GameObject prefab = Array.Find(objectPrefabs, item => item.name == obj.objectType);
            if (prefab != null)
            {
                CreateNewObject(obj.objectType, obj.xCoordinate);
            }
            else
            {
                Debug.LogError("Item prefab not found for name: " + obj.objectType);
            }
        }
    }
}