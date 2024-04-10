using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuArrowScript : MonoBehaviour
{
    public GameObject UI;
    public bool folded = false;
    public float foldSide;
    public float foldTimer = 0;

    public void onClick()
    {
        if (foldSide == 1)
        {
            foldTimer = 1200;
        }
        
        if (foldSide == -1)
        {
            foldTimer = -1200;
        }
    }

    private void FixedUpdate()
    {
        if (foldTimer > 0)
        {
            UI.transform.position += new Vector3(25f, 0, 0);
            foldTimer -= 25f;
        }
        else if (foldTimer < 0)
        {
            UI.transform.position += new Vector3(-25f, 0, 0);
            foldTimer += 25f;
        }
    }
}
