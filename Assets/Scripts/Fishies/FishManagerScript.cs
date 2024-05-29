using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class FishData
{
    public List<SingleFishData> dataOfFishes;
}

public class SingleFishData
{
    public string fishType;
    public float xpos;
    public float ypos;
}

public class FishManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
