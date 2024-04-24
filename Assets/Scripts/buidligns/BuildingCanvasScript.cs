using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCanvasScript : MonoBehaviour
{
    //0,yes,1,no
    public int buttonSort;

    public Canvas canvas;

    public void onClick()
    {
        if (buttonSort == 1)
        {
            canvas.enabled = false;
        }

        if (buttonSort == 0)
        {
            //check if enough cash
            //build building
            //disable canvas
            //destroy sign
        }
    }
}
