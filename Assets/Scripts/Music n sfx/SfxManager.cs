using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;
    public AudioSource audioSource;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent from being destroyed when loading new scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

        if (audioClip != null)
        {
            // Add the audioClip to the source and play the SFX
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioClip not found: " + soundName);
        }
    }
}
