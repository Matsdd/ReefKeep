using UnityEngine;

public class CloseGameButton : MonoBehaviour
{
    // This method will be called when the button is clicked
    public void CloseGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor, so we use this line to stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // This will quit the application
            Application.Quit();
#endif
    }
}
