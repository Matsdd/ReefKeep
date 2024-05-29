using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TabSwitch : MonoBehaviour
{
    public GameObject Nature;
    public GameObject Decor;
    public GameObject Fish;

    // Activate Nature Canvas and deactivate the others
    public void ToggleNature()
    {
        Debug.Log("ToggleNature");
        Nature.SetActive(true);
        Decor.SetActive(false);
        Fish.SetActive(false);
        return;
    }

    // Activate Decor Canvas and deactivate the others
    public void ToggleDecor()
    {
        Debug.Log("ToggleDecor");
        Nature.SetActive(false);
        Decor.SetActive(true);
        Fish.SetActive(false);
        return;
    }

    // Activate Fish Canvas and deactivate the others
    public void ToggleFish()
    {
        Debug.Log("ToggleFish");
        Nature.SetActive(false);
        Decor.SetActive(false);
        Fish.SetActive(true);
        return;
    }
}