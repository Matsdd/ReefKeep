using UnityEngine;

public class OilScript : MonoBehaviour
{
    public GameObject oil;
    public GameObject oilParent;

    float size;

    private void Start()
    {
        size = Mathf.Round(Random.Range(2, 4));
        for (int i = 0; i  < size; i++)
        {
            addChild();
        }
        
    }

    private void FixedUpdate()
    {
        if (this.transform.childCount > 0)
        {
            float duplicate = Mathf.Round(Random.Range(0, 400));
            size = 1;
            if (duplicate == 1)
            {
                addChild();
            }
        }
        else
        {
            Destroy(oilParent);
        }
    }

    public void addChild()
    {
        float oilx = Random.Range(size * -0.3f, size * 0.3f);
        float oily = Random.Range(size * -0.2f, size * 0.2f);

        GameObject oilBit = Instantiate(oil, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        oilBit.transform.parent = this.transform;

        oilBit.transform.position = new Vector3(oilx + this.transform.position.x, oily + this.transform.position.y, 0);
    }
}
