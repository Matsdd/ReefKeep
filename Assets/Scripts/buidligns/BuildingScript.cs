using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingScript : MonoBehaviour
{
    public GameObject self;
    public Canvas canvas;

    private void OnMouseDown()
    {
        if (!canvas.enabled)
        {
            canvas.enabled = true;
        }
    }
}
