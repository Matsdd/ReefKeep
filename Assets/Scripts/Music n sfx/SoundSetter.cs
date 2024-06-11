using UnityEngine;
using UnityEngine.Audio;

public class SoundSetter : MonoBehaviour
{
    public AudioMixer masterMixer;
    public AudioMixer sfxMixer;

    private void Start()
    {
        // Get values from PlayerPrefs
        float masterVolume = PlayerPrefs.GetFloat("SavedMasterVolume", 100);
        float sfxVolume = PlayerPrefs.GetFloat("SavedSfxVolume", 100);

        // Set the volume
        setVolume(masterVolume, 0);
        setVolume(sfxVolume, 1);
    }

    // Set the volume, sort 0: Master | sort 1: SFX
    public void setVolume(float _value, int sort)
    {
        if (_value < 1)
        {
            _value = .001f;
        }

        if (sort == 0)
        {
            masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
        }
        else
        {
            sfxMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
        }
    }
}
