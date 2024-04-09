using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuArrowScript : MonoBehaviour
{
    public GameObject UI;
    public bool folded = false;

    public void onClick()
    {
        if (folded)
        {
            UI.transform.position -= new Vector3(100, 0, 0);
            folded = false;
            Debug.Log("Kaas");
        }
        else
        {
            UI.transform.position += new Vector3(100, 0, 0);
            folded = true;
            Debug.Log("Kaas");
        }
    }
}
