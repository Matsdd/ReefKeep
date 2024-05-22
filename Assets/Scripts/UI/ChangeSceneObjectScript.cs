using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeSceneObjectScript : MonoBehaviour
{
    public int scene;

    // Script to change scenes for objects
    void OnMouseDown()
    {
        // Load scene
        if (EventSystem.current.IsPointerOverGameObject()) return;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}