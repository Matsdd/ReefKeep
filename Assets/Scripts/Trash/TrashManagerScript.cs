using UnityEngine;

public class TrashManagerScript : MonoBehaviour
{
    public GameObject trashS;
    public GameObject trashB;
    public GameObject oil;

    public static int trashCashMultiplier = 1;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // If clicked on trash, destory is and give money
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Trash"))
            {
                Destroy(hit.transform.gameObject);
                GameManager.instance.ChangeMoney(10 * trashCashMultiplier);
            }

            // If clicked on oil, destory it and give no money to prevent infinite money with spread
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Oil"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }

    // Randomly spawn new trash
    private void FixedUpdate()
    {
        // Generate a random number, make bigger to make trash more rare
        float randomNum = Mathf.Round(Random.Range(0, 1500));
        if (randomNum == 1)
        {
            float randomX = Random.Range(-10, 10);
            float randomY = Random.Range(-12, 12);

            // Generate a random number to choose trash type
            randomNum = Mathf.Round(Random.Range(0, 5));
            if (randomNum >= 4)
            {
                // Big trash
                Instantiate(trashB, new Vector3(randomX, randomY, 0), Quaternion.identity);
            }
            else if (randomNum == 3)
            {
                // Oil
                Instantiate(oil, new Vector3(randomX, randomY, 0), Quaternion.identity);
            }
            else
            {
                // Small Trash
                Instantiate(trashS, new Vector3(randomX, randomY, 0), Quaternion.identity);
            }
        }
    }

}
