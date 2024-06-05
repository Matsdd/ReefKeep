using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorManagerScript : MonoBehaviour
{
    public GameObject pers1;
    public GameObject pers2;
    private void FixedUpdate()
    {
        //spawn visitors depending on level
        if (PlayerPrefs.GetInt("VisitorCenterLevel") == 1 && PlayerPrefs.GetFloat("Tutorial") == 1)
        {
            int rand = Random.Range(0, 500);
            if (rand == 1)
            {
                Instantiate(pers1);
            }
            else if (rand == 2)
            {
                Instantiate(pers2);
            }
        }
        else if (PlayerPrefs.GetInt("VisitorCenterLevel") == 2 && PlayerPrefs.GetFloat("Tutorial") == 1)
        {
            int rand = Random.Range(0, 350);
            if (rand == 1)
            {
                Instantiate(pers1);
            }
            else if (rand == 2)
            {
                Instantiate(pers2);
            }
        }
        else if (PlayerPrefs.GetInt("VisitorCenterLevel") == 3 && PlayerPrefs.GetFloat("Tutorial") == 1)
        {
            int rand = Random.Range(0, 200);
            if (rand == 1)
            {
                Instantiate(pers1);
            }
            else if (rand == 2)
            {
                Instantiate(pers2);
            }
        }
    }
}
