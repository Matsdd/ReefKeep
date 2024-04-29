using UnityEngine;

public class BigTrashScript : MonoBehaviour
{
    public GameObject canvas;
    public GameObject BigTrash1;
    public GameObject BigTrash2;

    private int BigTrash1Alive = 1;
    private int BigTrash2Alive = 1;

    private string trashCluster;

    private void Start()
    {
        BigTrash1Alive = PlayerPrefs.GetInt("BigTrash1", 1);
        BigTrash2Alive = PlayerPrefs.GetInt("BigTrash2", 1);

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
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("BigTrash"))
            {
                trashCluster = hit.collider.gameObject.name;
                canvas.SetActive(true);
            }
        }
    }

    public void onClickX()
    {
        canvas.SetActive(false);
    }

    public void onClickV()
    {
        if (trashCluster != null)
        {
            if (GameManager.instance.ChangeMoney(-1000))
            {
                GameObject obj = GameObject.Find(trashCluster);
                Destroy(obj);

                if (trashCluster == "BigTrash1")
                {
                    CameraController.minX = -19.6f;
                    PlayerPrefs.SetInt("BigTrash1", 0);
                }
                else if (trashCluster == "BigTrash2")
                {
                    CameraController.maxX = 19.6f;
                    PlayerPrefs.SetInt("BigTrash2", 0);
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
