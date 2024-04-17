using UnityEngine;

public class TrashScript : MonoBehaviour
{
    public GameObject oil;
    public GameObject trash;
    GameObject obj;

    float speed = 2f;
    float height = 0.01f;
    public int sort = 0;
    float side = 0;

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
        //Set movement oil
        Vector3 pos = transform.position;
        float newX = pos.x;
        if (sort == 1)
        {
            if (side == 0)
            {
                side = Mathf.Round(Random.Range(-1.4f, 1.4f));
            }

            newX = pos.x + (side * 0.001f);
        }


        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        transform.position = new Vector3(newX, newY, pos.z);

    }
}
