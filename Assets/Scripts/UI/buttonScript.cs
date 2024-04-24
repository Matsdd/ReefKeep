using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public int scene;


    void OnMouseDown()
    {
        // Load scene
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}