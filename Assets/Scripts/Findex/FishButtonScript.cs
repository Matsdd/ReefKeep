using UnityEngine;
using UnityEngine.UI; 

public class FishButtonScript : MonoBehaviour
{


    public void SetFishName(string fishName)
    {
        Debug.Log("Setting fish name: " + fishName);

        // Give the button text
        Button fishButton = GetComponentInChildren<Button>();
        if (fishButton != null)
        {
            fishButton.GetComponentInChildren<Text>().text = fishName;
        }
        else
        {
            Debug.LogError("Button component not found.");
        }
    }

}
