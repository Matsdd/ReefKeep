using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxEffect;

    private Transform camTransform;
    private float startPosX;

    void Start()
    {
        camTransform = Camera.main.transform;
        startPosX = transform.position.x;
    }

    void Update()
    {
        float distance = (camTransform.position.x - startPosX) * parallaxEffect;
        transform.position = new Vector3(startPosX + distance, transform.position.y, transform.position.z);
    }
}
