using UnityEngine;

public class BigTrashScript : MonoBehaviour
{
    public GameObject canvas;

    private string trashCluster;

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
                }
                else if (trashCluster == "BigTrash2")
                {
                    CameraController.maxX = 19.6f;
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
