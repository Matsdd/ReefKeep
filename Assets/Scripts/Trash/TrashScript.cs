using UnityEngine;

public class TrashScript : MonoBehaviour
{
    public bool sideMovement = false;
    private float speed = 2f;
    private float height = 0.01f;
    private float sideSpeed = 0;

    private void FixedUpdate()
    {
        //Sideways & vertical movement
        Vector3 pos = transform.position;
        float newX = pos.x;
        if (sideMovement)
        {
            if (pos.x < 19 && pos.x > -19)
            {
                if (sideSpeed == 0)
                {
                    sideSpeed = Mathf.Round(Random.Range(-1.4f, 1.4f));
                }
                newX = pos.x + (sideSpeed * 0.001f);
            }
        }

        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        transform.position = new Vector3(newX, newY, pos.z);

    }
}
