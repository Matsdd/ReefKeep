using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float minX = -10f;
    private float maxX = 10f;
    private float minY = -11.2f;
    private float maxY = 11.2f;

    private float zoomSpeed = 2.0f;
    private float minZoom = 1.0f;
    private float maxZoom = 10.0f;

    private float currentZoom = 5.0f;

    private Vector3 dragOrigin;
    private bool allowMovement = false;

    private void Start()
    {
        UpdateMaxPos();
    }

    void Update()
    {
        if (!ObjectManager.isHoldingObject) // Check if an object is not being moved
        {
            // Handle camera movement
            HandleCameraMovement();

            // Handle camera zoom
            HandleCameraZoom();
        }
    }

    public void UpdateMaxPos()
    {
        Debug.Log("UpdateMaxPos");

        // Get the state from PlayerPrefs (0=destroyed / 1=alive)
        int BigTrash1Alive = PlayerPrefs.GetInt("BigTrash1", 1);
        int BigTrash2Alive = PlayerPrefs.GetInt("BigTrash2", 1);

        // Update the max pos depending if BigTrash is alive
        if (BigTrash1Alive == 0)
        {
            minX = -19.6f;
        }

        if (BigTrash2Alive == 0)
        {
            maxX = 19.6f;
        }
    }
    private void HandleCameraMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            allowMovement = true;
        }

        if (Input.GetMouseButton(0) && allowMovement)
        {
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = transform.position + difference;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            transform.position = newPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            allowMovement = false;
        }
    }

    private void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        Camera.main.orthographicSize = currentZoom;
    }
}
