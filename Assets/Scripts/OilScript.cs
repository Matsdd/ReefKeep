using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilScript : MonoBehaviour
{
    public GameObject oil;
    public GameObject oilBit;
    private void Start()
    {
        oilBit = Instantiate(oil, new Vector3(0,0,0), new Quaternion(0,0,0,0));
        oilBit.transform.parent = this.transform;
        Debug.Log("nee");
    }
}
