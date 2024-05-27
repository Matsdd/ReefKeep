using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishShadowScript : MonoBehaviour
{
    public float fishSpeed = 0.1f;
    private int side;

    private void Start()
    {
        //bepaal kant om op te zwemmen
        side = Random.Range(0, 2) == 0 ? -1 : 1;
        //vis goed kant op zwemmen en goede kant spawnen
        this.gameObject.transform.localScale = new Vector3(-2 * side, 2, 1);
        Vector3 newPos = this.gameObject.transform.position;
        newPos.x *= side;
        this.gameObject.transform.position = newPos;
    }

    private void FixedUpdate()
    {
        //vis doet zwem
        this.gameObject.transform.position += new Vector3(fishSpeed * side,0,0);

        //als vis uit scherm, vis dood
        if (this.gameObject.transform.position.x > 30)
        {
            Destroy(this.gameObject);
        }
        else if (this.gameObject.transform.position.x < -30)
        {
            Destroy(this.gameObject);
        }
    }
}
