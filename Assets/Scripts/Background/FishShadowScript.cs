using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishShadowScript : MonoBehaviour
{
    public float fishSpeed = 0.1f;

    private void Start()
    {
        this.gameObject.transform.localScale = new Vector3(-2, 2, 1);
    }

    private void FixedUpdate()
    {
        this.gameObject.transform.position += new Vector3(fishSpeed,0,0);

        if (this.gameObject.transform.position.x > 30)
        {
            Destroy(this.gameObject);
        }
    }
}
