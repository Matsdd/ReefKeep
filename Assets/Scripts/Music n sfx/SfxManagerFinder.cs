using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManagerFinder : MonoBehaviour
{
    private GameObject obj;
    private PlaySfxScript script;

    void Start()
    {
        //search for sfxPlayer object in game
        obj = GameObject.Find("sfxPlayer");
        if (obj != null)
        {
            //set script of sfxPlayer
            script = obj.GetComponent<PlaySfxScript>();
            if (script == null)
            {
                Debug.LogError("PlaySfxScript component not found on sfxManager object.");
            }
        }
        else
        {
            Debug.LogError("sfxManager object not found.");
        }
    }

    public void playSound(string soundName)
    {
        //playSound function for buttons with songName as parameter
        if (script != null)
        {
            script.playSfx(soundName);
        }
        else
        {
            Debug.LogError("PlaySfxScript is null. Cannot play sound.");
        }
    }
}