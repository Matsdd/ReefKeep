using UnityEngine;

public class TrashScript : MonoBehaviour
{
    public GameObject trash;
    GameObject obj;

    float speed = 2f;
    float height = 0.01f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                obj = hit.transform.gameObject;
            }

            if (hit.collider != null && hit.collider.gameObject.tag == "Trash" && obj != null)
            {
                Destroy(obj);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
}
