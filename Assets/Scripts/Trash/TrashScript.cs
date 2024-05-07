using UnityEngine;

public class TrashScript : MonoBehaviour
{
    public bool sideMovement = false;
    private float speed = 2f;
    private float height = 0.01f;
    private float sideSpeed = 0;

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        float newX = pos.x;

        if (sideMovement)
        {
            // Bounds to keep the trash inside the map
            if (pos.x < 19 && pos.x > -19)
            {
                // If there is no sideSpeed, generate a new number
                if (sideSpeed == 0)
                {
                    sideSpeed = Mathf.Round(Random.Range(-1.4f, 1.4f));
                }
                // Set a new Xpos for drift
                newX = pos.x + (sideSpeed * 0.001f);
            }
        }

        // Set a new Y for vertical movement
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;

        // Move the object with the new X & Y
        transform.position = new Vector3(newX, newY, pos.z);

    }
}
