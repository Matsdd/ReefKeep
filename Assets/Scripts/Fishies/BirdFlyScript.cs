using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlyScript : MonoBehaviour
{
    private int side;
    private void Start()
    {
        side = Random.Range(0, 2);
        if (side == 0)
        {
            this.gameObject.transform.position = new Vector3(-10,Random.Range(-4,5),45);
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(10, Random.Range(-4, 5), 45);
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    private void FixedUpdate()
    {
        Vector3 vec = this.gameObject.transform.position;
        if (side == 0)
        {
            vec.x += 0.1f;
            this.gameObject.transform.position = vec;
        }
        else
        {
            vec.x -= 0.1f;
            this.gameObject.transform.position = vec;
        }
    }
}
