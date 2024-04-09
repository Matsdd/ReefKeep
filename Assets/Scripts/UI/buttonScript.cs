using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public int scene;

    private void Start()
    {

    }

    private void Update()
    {

    }
    void OnMouseDown()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void onClick()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}