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
        spawnTimer--;

        if (spawnTimer <= 0)
        {
            spawnTimer = Random.Range(100,120);
            fishPosY = Random.Range(-13,11);

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
