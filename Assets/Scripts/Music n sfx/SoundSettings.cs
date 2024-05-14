using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    //geen flauw idee wtf hier allemaal gebeurt
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider soundSliderSfx;
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] AudioMixer sfxMixer;

    private void Start()
    {
        setVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100),0);
        setVolume(PlayerPrefs.GetFloat("SavedSfxVolume", 100),1);
    }

    public void setVolume(float _value, int sort)
    {
        if (_value < 1)
        {
            _value = .001f;
        }

        refreshSlider(_value,sort);
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

    public void refreshSlider(float _value, int sort)
    {
        if (sort == 0)
        {
            soundSlider.value = _value;
        }
        else
        {
            soundSliderSfx.value = _value;
        }
    }
}
