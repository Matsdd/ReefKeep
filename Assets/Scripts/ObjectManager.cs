using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] buildableObjects;
    public float objectSnapDistance = 0.5f;
    public GameObject placeButton;
    public GameObject cancelButton;

    private GameObject selectedObject;
    private bool isMovingObject = false;
    public static bool isHoldingObject = false;
    private Vector3 originalPosition;
    private SpriteRenderer selectedObjectRenderer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast to determine if an object is clicked
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!isMovingObject && hit.collider != null && hit.collider.CompareTag("BuildableObject"))
            {
                StartMovingObject(hit.collider.gameObject);
            }

            if (isMovingObject && hit.collider != null && hit.collider.CompareTag("BuildableObject"))
            {
                isHoldingObject = true;
                MoveSelectedObject();
                Debug.Log("True");
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
        newPosition.y = -9.2f; // Place the object at the bottom of the game area
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
