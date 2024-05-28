using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;

    // Opens panel (when pressing button stated in unity editor)
    public void OpenPanel()
    {
        if (Panel != null)
        {
           Panel.SetActive(true);
        }
    }
}
