using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider soundSlider;
    public Slider soundSliderSfx;
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

        // set the slider
        soundSlider.value = masterVolume;
        soundSliderSfx.value = sfxVolume;
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

            PlayerPrefs.SetFloat("SavedMasterVolume", _value);
            masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
        }
        else
        {
            PlayerPrefs.SetFloat("SavedSfxVolume", _value);
            sfxMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
        }
    }

    // Runs when slider is changed
    public void setVolumeFromSlider(int sort)
    {
        if (sort == 0)
        {
            setVolume(soundSlider.value,sort);
        }
        else
        {
            setVolume(soundSliderSfx.value,sort);
        }
    }
}
