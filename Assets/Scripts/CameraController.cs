using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float minX = -28.6f;
    private float maxX = 28.6f;
    private float minY = -16.1f;
    private float maxY = 16.1f;

    private float zoomSpeed = 3.0f;
    private float minZoom = 2.0f;
    private float maxZoom = 10.0f;

    private float currentZoom = 5.0f;

    private Vector3 dragOrigin;
    private bool allowMovement = false;

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

    // Code for camera movement
    private void HandleCameraMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            allowMovement = true;
        }

        if (Input.GetMouseButton(0) && allowMovement)
        {
            // Calculate the difference and set the new positon
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = transform.position + difference;

            // Get camera zoom details
            float cameraHeight = Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * Camera.main.aspect;

            // Clamp the position to the border + camera zoom
            newPosition.x = Mathf.Clamp(newPosition.x, minX + cameraWidth, maxX - cameraWidth);
            newPosition.y = Mathf.Clamp(newPosition.y, minY + cameraHeight, maxY - cameraHeight);

            transform.position = newPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            allowMovement = false;
        }
    }

    // Code for zoom, probably not working on mobile yet.
    private void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        Camera.main.orthographicSize = currentZoom;

        // Update the max pos depending on the zoom level
        Vector3 newPosition = transform.position;
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        newPosition.x = Mathf.Clamp(newPosition.x, minX + cameraWidth, maxX - cameraWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, minY + cameraHeight, maxY - cameraHeight);

        transform.position = newPosition;
    }
}
