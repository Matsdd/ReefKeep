using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuArrowScript : MonoBehaviour
{
    public GameObject UI;
    public bool folded = false;
    public float foldTimer = 0;

    public void onClick()
    {
        if (folded && foldTimer == 0)
        {
            foldTimer = 250;
            folded = false;
        }
        
        if (!folded && foldTimer == 0)
        {
            foldTimer = -250;
            folded = true;
        }
    }

    private void FixedUpdate()
    {
        if (foldTimer > 0)
        {
            UI.transform.position += new Vector3(12.5f, 0, 0);
            foldTimer -= 12.5f;
        }
        else if (foldTimer < 0)
        {
            UI.transform.position += new Vector3(-12.5f, 0, 0);
            foldTimer += 12.5f;
        }
    }
}
