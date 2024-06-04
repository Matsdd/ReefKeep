using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] RectTransform transObj;
    private int newScene = 0;

    private void Start()
    {
        transObj.gameObject.SetActive(true);

        LeanTween.scale(transObj, new Vector3(1, 1, 1), 0);
        LeanTween.scale(transObj, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            transObj.gameObject.SetActive(false);
        });
    }

    public void ChangeScene(int scene)
    {
        transObj.gameObject.SetActive(true);

        LeanTween.scale(transObj, Vector3.zero, 0f);
        LeanTween.scale(transObj, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            newScene = scene;
            LoadScene();

            // Little pause before loading the next scene
            // Invoke("LoadScene", 0.1f);
        });
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(newScene, LoadSceneMode.Single);
    }
}