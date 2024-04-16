using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float minX = -19.6f;
    private float maxX = 19.6f;
    private float minY = -11.2f;
    private float maxY = 11.2f;

    private Vector3 dragOrigin;
    private bool allowMovement = false;

    void Update()
    {
        if (!ObjectManager.isHoldingObject) // Check if an object is not being moved
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
    }
}
