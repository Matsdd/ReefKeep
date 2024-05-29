using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishswimscript : MonoBehaviour
{
    public float swimspeed = 5;
    private Boolean startlocation = false;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 0)
        {
            startlocation = true;
        } else
        {
            startlocation = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startlocation)
        {
            transform.position = transform.position + (Vector3.right * swimspeed) * Time.deltaTime;
        } else
        {
            transform.position = transform.position + (Vector3.left * swimspeed) * Time.deltaTime;
        }
        
        if(transform.position.x < -40 || transform.position.x > 40)
        {
            Destroy(gameObject);
            Debug.Log("fish destryed by fish script");
        }
    }

}
