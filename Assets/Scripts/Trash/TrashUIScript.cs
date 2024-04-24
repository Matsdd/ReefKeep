using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashUIScript : MonoBehaviour
{
    //0,yes,1,no
    public int buttonSort = 0;
    public float destroytimer = 0;

    public Canvas canvas;
    public TMP_Text canvasTxt;
    public static GameObject trashCluster;

    public void onClick()
    {
        if (buttonSort == 1)
        {
            canvas.enabled = false;
        }else if (buttonSort == 0)
        {
            if (GameManager.instance.ChangeMoney(-1000)) {
                // -- check if enough money
                if (trashCluster != null)
                {
                    Destroy(trashCluster);
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
