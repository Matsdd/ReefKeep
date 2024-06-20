using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class ActiveChecker : MonoBehaviour
{
    public int currentLevel = 1;

    public Image DecorButton;

    public Sprite ActiveSprite;
    public Sprite InactiveSprite;

    // Start is called before the first frame update
    void Start()
    {
        // Get values from PlayerPrefs or set a default
        currentLevel = PlayerPrefs.GetInt("ShopLevel", 1);
    }

    // Update is called once per frame
    void Update()
    {

            if (currentLevel >= 2)
            {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }

        Debug.Log("hi");
        FishCheck();
    }

    public void FishCheck()
    {
        if (currentLevel >= 2)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
