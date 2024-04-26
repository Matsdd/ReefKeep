using UnityEngine;

public class TrashScript : MonoBehaviour
{
    public bool isCluster = false;
    public bool sideMovement = false;
    private float speed = 2f;
    private float height = 0.01f;
    private float sideSpeed = 0;
    public bool clusterSide = false;

    public static int trashCashMultiplier = 1;

    public Canvas canvas;
    public GameObject trashCluster;

    // Dit is misschien minder efficient omdat dit per trash word uitgevoerd, maar boeie voor nu.
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Trash"))
            {
                if (hit.transform.name == "TrashCollection1" || hit.transform.name == "TrashCollection2")
                {

                    TrashUIScript.trashCluster = hit.transform.name;
                    canvas.enabled = true;

                }
                else
                {
                    Destroy(hit.transform.gameObject);
                    GameManager.instance.ChangeMoney(10 * trashCashMultiplier);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //Sideways & vertical movement*
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
