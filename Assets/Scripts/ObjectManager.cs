using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] buildableObjects;
    public GameObject placeButton;
    public GameObject cancelButton;

    private float objectSnapDistance = 0.5f;
    private float minX = -14f;
    private float maxX = 14f;

    private GameObject selectedObject;
    private bool isMovingObject = false;
    public static bool isHoldingObject = false;
    private Vector3 originalPosition;
    private SpriteRenderer selectedObjectRenderer;
    private bool allowMovement = false;

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
            } else {
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
        selectedObjectRenderer.color = Color.white;
        selectedObject = null;
        isMovingObject = false;

        // Hide move buttons
        placeButton.SetActive(false);
        cancelButton.SetActive(false);
    }
}
