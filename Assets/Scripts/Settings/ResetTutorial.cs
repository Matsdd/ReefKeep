using UnityEngine;

public class ResetTutorial : MonoBehaviour
{
    public void ResetTuto()
    {
        PlayerPrefs.SetInt("Tutorial", 0);
        PlayerPrefs.SetInt("TutorialU", 0);
        PlayerPrefs.SetInt("TutorialS", 0);
    }
}
