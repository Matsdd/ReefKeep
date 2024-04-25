using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public int scene;

    void OnMouseDown()
    {
        // Load scene
        if (EventSystem.current.IsPointerOverGameObject()) return;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}