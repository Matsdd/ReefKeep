using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    public GameObject trash;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.tag == "Trash")
            {
                Destroy(trash);
            }
        }
    }

    float speed = 2f;
    float height = 0.2f;

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
}
