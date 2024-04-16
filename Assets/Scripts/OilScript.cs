using UnityEngine;

public class OilScript : MonoBehaviour
{
    public GameObject oil;

    private void Start()
    {
        float size = Mathf.Round(Random.Range(2, 8));
        for (int i = 0; i  < size; i++)
        {

            float oilx = Random.Range(size * -0.3f, size * 0.3f);
            float oily = Random.Range(size * -0.2f, size * 0.2f);

            GameObject oilBit = Instantiate(oil, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            oilBit.transform.parent = this.transform;

            oilBit.transform.position = new Vector3(oilx, oily, 0);
        }
        
    }
}
