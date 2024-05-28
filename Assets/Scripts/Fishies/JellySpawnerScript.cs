using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellySpawnerScript : MonoBehaviour
{
    private static int totalJellies = 0;

    public GameObject jelly1;
    public GameObject jelly2;

    private void FixedUpdate()
    {
        //spawnt jellyfish?
        int rand = Random.Range(0, 100);
        if (rand == 1)
        {
            //spawn jellyfish 1 or 2
            rand = Random.Range(0, 2);
            if (rand == 1)
            {
                Instantiate(jelly2);
            }
            else
            {
                Instantiate(jelly1);
            }
            //set total of jellyfih
            totalJellies++;
        }
    }
}
