using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    //geen flauw idee wtf hier allemaal gebeurt
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;

    private void Start()
    {
        setVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void setVolume(float _value)
    {
        if (_value < 1)
        {
            _value = .001f;
        }

        refreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
    }

    public void setVolumeFromSlider()
    {
        setVolume(soundSlider.value);
    }

    public void refreshSlider(float _value)
    {
        soundSlider.value = _value;
    }
}
