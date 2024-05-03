using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButtonScript : MonoBehaviour
{
    public int scene;

    // Script to change scenes for buttons
    public void ChangeScene()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
