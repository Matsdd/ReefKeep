using UnityEngine;
using System;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public GameObject placeButton;
    public GameObject cancelButton;

    private float objectSnapDistance = 0.5f;
    private float minX = -14f;
    private float maxX = 14f;
    private float objectY = -15.9f;

    private GameObject selectedObject;
    private bool isMovingObject = false;
    public static bool isHoldingObject = false;
    private bool allowMovement = false;
    private Vector3 originalPosition;
    private SpriteRenderer selectedObjectRenderer;

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

    public void CreateObject(string objectName)
    {
        // Find the corresponding item prefab by name
        GameObject prefab = Array.Find(objectPrefabs, item => item.name == objectName);
        if (prefab != null)
        {
            // Instantiate the item prefab
            GameObject newItem = Instantiate(prefab);

            // Set the position of the new item in the scene
            newItem.transform.position = new Vector3(0f, objectY + newItem.GetComponent<SpriteRenderer>().bounds.extents.y, 0f); // Set the desired position
        }
        else
        {
            Debug.LogError("Item prefab not found for name: " + objectName);
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
        newPosition.y = objectY + selectedObject.GetComponent<SpriteRenderer>().bounds.extents.y;
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
