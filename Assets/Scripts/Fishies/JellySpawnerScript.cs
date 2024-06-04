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
        int rand = Random.Range(0, 5000);
        if (rand == 1 && totalJellies <= 5)
        {
            //spawn jellyfish 1 or 2
            rand = Random.Range(0, 2);
            //determine location it spawns
            Vector3 newPos = new Vector3(Random.Range(PlayerPrefs.GetInt("BigTrash1") == 1 ? -14 : -24, PlayerPrefs.GetInt("BigTrash2") == 1 ? 14 : 24),Random.Range(-12,12), 1);
            if (rand == 1)
            {
                Instantiate(jelly2,newPos,new Quaternion(0,0,0,0));
            }
            else
            {
                Instantiate(jelly1, newPos, new Quaternion(0, 0, 0, 0));
            }
            //set total of jellyfih
            totalJellies++;
        }
    }
}
