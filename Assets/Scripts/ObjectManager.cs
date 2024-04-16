using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
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

    private float objectSnapDistance = 0.5f;
    private float minX = -14f;
    private float maxX = 14f;
    private float objectY = -15.9f;

    public GameObject selectedObject;
    private bool isMovingObject = false;
    public static bool isHoldingObject = false;
    private bool allowMovement = false;
    private Vector3 originalPosition;
    private SpriteRenderer selectedObjectRenderer;

    private List<EcosystemObject> placedObjects = new List<EcosystemObject>(); // List to store placed objects

    void Start()
    {
        // Load the ecosystem when the game starts
        //LoadEcosystem();
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

    public void CreateObject(string objectName)
    {
        GameObject prefab = Array.Find(objectPrefabs, item => item.name == objectName);
        if (prefab != null)
        {
            GameObject newItem = Instantiate(prefab);
            newItem.transform.position = new Vector3(0f, objectY + newItem.GetComponent<SpriteRenderer>().bounds.extents.y, 0f);

            // Add the placed object to the list
            //AddPlacedObject(objectName, newItem.transform.position.x);
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
        mousePosition.z = 0;

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
            string objectType = selectedObject.name; // Assuming object name is unique
            float xCoordinate = selectedObject.transform.position.x;

            int existingIndex = placedObjects.FindIndex(obj => obj.objectType == objectType);
            if (existingIndex != -1)
            {
                placedObjects[existingIndex].xCoordinate = xCoordinate;
            }
            else
            {
                //AddPlacedObject(objectType, xCoordinate);
            }

            selectedObjectRenderer.color = Color.white;
            selectedObject = null;
            isMovingObject = false;
        }

        placeButton.SetActive(false);
        cancelButton.SetActive(false);

        // Save the updated ecosystem
        //SaveEcosystem();
    }

    //    void addplacedobject(string objecttype, float xcoordinate)
    //    {
    //        ecosystemobject newobject = new ecosystemobject();
    //        newobject.objecttype = objecttype;
    //        newobject.xcoordinate = xcoordinate;
    //        placedobjects.add(newobject);
    //    }

    //    void saveecosystem()
    //    {
    //        debug.log("list: " + placedobjects);
    //        saveloadmanager.saveecosystem(placedobjects);
    //    }

    //    void loadecosystem()
    //    {
    //        placedobjects = saveloadmanager.loadecosystem();
    //        foreach (var obj in placedobjects)
    //        {
    //            gameobject prefab = array.find(objectprefabs, item => item.name == obj.objecttype);
    //            if (prefab != null)
    //            {
    //                gameobject newitem = instantiate(prefab);
    //                newitem.transform.position = new vector3(obj.xcoordinate, objecty + newitem.getcomponent<spriterenderer>().bounds.extents.y, 0f);
    //            }
    //            else
    //            {
    //                debug.logerror("item prefab not found for name: " + obj.objecttype);
    //            }
    //        }
    //    }
}
