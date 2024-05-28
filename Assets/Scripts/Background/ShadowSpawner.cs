using UnityEngine;

public class ShadowSpawner : MonoBehaviour
{
    private float spawnTimer;
    private float fishPosY;

    public GameObject fish1;
    public GameObject fish2;
    public GameObject fish3;
    public GameObject fish4;
    public GameObject fish5;

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
            //1-9 bass
            //10-15 sardine
            //16-17 opah
            //18 tresher 19 basking
            int rfish = Random.Range(0, 20);
            if (rfish < 10)
            {
                Instantiate(fish1, new Vector3(-30, fishPosY, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (rfish < 16)
            {
                Instantiate(fish5, new Vector3(-30, fishPosY, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (rfish == 16 || rfish == 17)
            {
                Instantiate(fish2, new Vector3(-30, fishPosY, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (rfish == 18)
            {
                Instantiate(fish3, new Vector3(-30, fishPosY, 0), new Quaternion(0, 0, 0, 0));
            }
            else if (rfish == 19)
            {
                Instantiate(fish4, new Vector3(-30, fishPosY, 0), new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
