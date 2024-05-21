using UnityEngine;

public class SfxPlayScript : MonoBehaviour
{
    public void playSound(string sound)
    {
        SfxManager.instance.playSfx(sound);
    }
}