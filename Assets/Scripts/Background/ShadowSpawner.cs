using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawner : MonoBehaviour
{
    private float spawnTimer;
    private float fishPosY;

    public GameObject fish1;
    public GameObject fish2;
    public GameObject fish3;

    private void FixedUpdate()
    {
        //als spawntimer onder nul is, spawn vis
        spawnTimer--;

        if (spawnTimer <= 0)
        {
            //reset timer
            spawnTimer = Random.Range(100,120);
            //zet fish positie
            fishPosY = Random.Range(-13,11);

            //determine welke vis, grote vis, kleine kans, kleine vis, grote kans
            int rfish = Random.Range(0, 9);
            if (rfish < 6)
            {
                Instantiate(fish1, new Vector3(-30, fishPosY, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (rfish == 6 || rfish == 7)
            {
                Instantiate(fish2, new Vector3(-30, fishPosY, 0), new Quaternion(0, 0, 0, 0));
            }
            else
            {
                Instantiate(fish3, new Vector3(-30, fishPosY, 0), new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
