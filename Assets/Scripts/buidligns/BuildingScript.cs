using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingScript : MonoBehaviour
{
    public string assignedBuilding;
    public int buildingCost;

    public GameObject self;
    public Canvas canvas;
    public TMP_Text CanvasTxt;

    private void OnMouseDown()
    {
        if (!canvas.enabled)
        {
            canvas.enabled = true;
            CanvasTxt.text = "Would you like to build a "+assignedBuilding+" for "+buildingCost+" fishbucks?";
        }
    }
}
