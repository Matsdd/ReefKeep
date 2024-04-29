using UnityEngine;

public class TrashManagerScript : MonoBehaviour
{
    //Hoe groter hoe kleiner de kans dat trash spawnt
    public int trashSpawnrate = 1200;

    public GameObject trashS;
    public GameObject trashB;
    public GameObject oil;

    public static int trashCashMultiplier = 1;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Trash"))
            {
                Destroy(hit.transform.gameObject);
                GameManager.instance.ChangeMoney(10 * trashCashMultiplier);
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Oil"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        float randomNum = Mathf.Round(Random.Range(0, trashSpawnrate));
        if (randomNum == 1)
        {
            float randomX = Random.Range(-10, 10);
            float randomY = Random.Range(-12, 12);
            Quaternion quat = new Quaternion(0, 0, 0, 0);

            randomNum = Mathf.Round(Random.Range(0, 5));
            if (randomNum >= 4)
            {
                //groot trash
                Debug.Log("Groot");
                Instantiate(trashB, new Vector3(randomX, randomY, 0), quat);
            }
            else if (randomNum == 3)
            {
                //oil
                Debug.Log("Oil");
                Instantiate(oil, new Vector3(randomX, randomY, 0), quat);
            }
            else
            {
                //kleine trash
                Debug.Log("Klein");
                Instantiate(trashS, new Vector3(randomX, randomY, 0), quat);
            }
        }
    }
}
