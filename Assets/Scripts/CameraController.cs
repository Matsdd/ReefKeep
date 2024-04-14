using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

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
