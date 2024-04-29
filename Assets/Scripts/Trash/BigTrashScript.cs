using UnityEngine;

public class BigTrashScript : MonoBehaviour
{
    public GameObject canvas;
    public GameObject BigTrash1;
    public GameObject BigTrash2;

    private string trashCluster;

    private void Start()
    {
        // Get the state from PlayerPrefs (0=destroyed / 1=alive)
        int BigTrash1Alive = PlayerPrefs.GetInt("BigTrash1", 1);
        int BigTrash2Alive = PlayerPrefs.GetInt("BigTrash2", 1);

        // If the player destoryed the trash is a prev session, destroy the BigTrash object
        if (BigTrash1Alive == 0)
        {
            Destroy(BigTrash1);
            CameraController.minX = -19.6f;
        }

        if (BigTrash2Alive == 0)
        {
            Destroy(BigTrash2);
            CameraController.maxX = 19.6f;
        }
    }

    void Update()
    {
        // Check for clicks on the big trash and enable the canvas
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("BigTrash"))
            {
                // Set the name of the object for later use
                trashCluster = hit.collider.gameObject.name;
                canvas.SetActive(true);
            }
        }
    }

    // X button to close the menu
    public void OnClickX()
    {
        canvas.SetActive(false);
    }

    // Check button to perform purchase and destroy object
    public void OnClickV()
    {
        if (trashCluster != null)
        {
            // Try to remove 1000 money
            if (GameManager.instance.ChangeMoney(-1000))
            {
                // Destroy the object
                GameObject obj = GameObject.Find(trashCluster);
                Destroy(obj);

                // Check which trash was deleted, set the camera and save to PlayerPrefs
                if (trashCluster == "BigTrash1")
                {
                    CameraController.minX = -19.6f;
                    PlayerPrefs.SetInt("BigTrash1", 0);
                    PlayerPrefs.Save();
                }
                else if (trashCluster == "BigTrash2")
                {
                    CameraController.maxX = 19.6f;
                    PlayerPrefs.SetInt("BigTrash2", 0);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        canvas.SetActive(false);
    }
}
