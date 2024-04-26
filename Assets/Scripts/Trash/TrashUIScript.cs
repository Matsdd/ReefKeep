using UnityEngine;
using TMPro;

public class TrashUIScript : MonoBehaviour
{
    //0,yes,1,no
    public int buttonSort = 0;
    public float destroytimer = 0;
    public bool clusterSide;

    public Canvas canvas;
    public TMP_Text canvasTxt;
    public static string trashCluster;

    public void onClick()
    {
        if (buttonSort == 1)
        {
            canvas.enabled = false;
        }

        else if (buttonSort == 0)
        {
            if (GameManager.instance.ChangeMoney(-1000))
            {
                if (trashCluster != null)
                {
                    GameObject obj = GameObject.Find(trashCluster);
                    Destroy(obj);

                    //edit camera movement
                    //slechte manier (:
                    if (trashCluster == "TrashCollection1")
                    {
                        CameraController.maxX = 19.6f;
                    }
                    else if (trashCluster == "TrashCollection2")
                    {
                        CameraController.minX = -19.6f;
                    }
                }
                canvas.enabled = false;
            }
            else
            {
                canvasTxt.text = "Not enough money!?";
                destroytimer = 60;
            }
        }
    }

    private void FixedUpdate()
    {
        if (destroytimer > 0)
        {
            destroytimer--;
            if (destroytimer == 0)
            {
                canvas.enabled = false;
                canvasTxt.text = "Would you like to destroy this cluster for 1000 Fishbucks?";
            }
        }
    }
}
