using UnityEngine;


public class ClusterScript : MonoBehaviour
{
    public Canvas canvas;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Cluster"))
            {
                TrashUIScript.trashCluster = hit.transform.name;
                canvas.enabled = true;
            }
        }
    }
}
