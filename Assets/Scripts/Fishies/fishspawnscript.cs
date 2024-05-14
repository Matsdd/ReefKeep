using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class FishObject
{
    public string name;
    public string xcordinate;
    public string ycordinate;
}

[Serializable]
public class FishInEcosystemData
{
    public List<FishObject> fishObjects;
}

public class fishspawnscript : MonoBehaviour
{

    public float spawnrate = 1;
    private float timer = 0;
    private float randomstartpos = 0;
    private float randomheightpos = 0;

    public float leftborder = -30;
    public float rightborder = 30;

    public float maxSpawnHeight = 13;
    public float minSpawnHeight = -13;

    private GameObject chosenFish;
    private int totalSpawnrange;

    public GameObject fish1;
    public int fish1SpawnWeight = 1;
    private int spawn1Range;

    public GameObject fish2;
    public int fish2SpawnWeight = 1;
    private int spawn2Range;


    // Start is called before the first frame update
    void Start()
    {
        spawn1Range = 0 + fish1SpawnWeight;
        spawn2Range = spawn1Range + fish2SpawnWeight;

        totalSpawnrange = spawn2Range;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnrate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            FishChooser();
            randomstartpos = UnityEngine.Random.value;
            randomheightpos = (UnityEngine.Random.value * 26) - 13;

            if (randomstartpos < 0.5)
            {
                Instantiate(chosenFish, new Vector3(leftborder, randomheightpos, 0), transform.rotation);
            }
            else
            {
                Instantiate(chosenFish, new Vector3(rightborder, randomheightpos, 0), transform.rotation);
            }

            timer = 0;
        }
    }

    void FishChooser()
    {
        float randomValue = UnityEngine.Random.Range(0, totalSpawnrange);

        if (randomValue < spawn1Range)
        {
            chosenFish = fish1;
        }
        else if (randomValue < spawn2Range)
        {
            chosenFish = fish2;
        }
        else if (randomValue < totalSpawnrange)
        {
            chosenFish = fish2;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting object has a specific tag (optional)
        if (other.CompareTag("Fish"))
        {
            Debug.Log("hooray!!: " + other.gameObject.GetInstanceID());
            // Destroy the exiting object
            Destroy(other.gameObject);

        }
        else
        {
            Debug.Log("collider has wrong tag");
        }
    }
}
