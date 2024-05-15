using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySfxScript : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        //set audiosource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            //Add audiosource if it doesnt exists yet
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void playSfx(string soundName)
    {
        //Load audioclip
        AudioClip audioClip = Resources.Load<AudioClip>("Sound/sfx/" + soundName);

        //is audioclip loaded?
        if (audioClip != null)
        {
            // Wijs de geladen audioclip toe aan de AudioSource en speel deze af
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioClip not found: " + soundName);
        }
    }
}