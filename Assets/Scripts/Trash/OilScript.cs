using UnityEngine;

public class OilScript : MonoBehaviour
{
    public GameObject oil;
    public GameObject oilParent;

    // Spawn some children oil
    private void Start()
    {
        for (int i = 0; i < Mathf.Round(Random.Range(2, 4)); i++)
        {
            AddChild();
        }
    }

    private void FixedUpdate()
    {
        // Check if there are still oil children
        if (this.transform.childCount > 0)
        {
            // Randomly spawn a new oil child
            float duplicate = Mathf.Round(Random.Range(0, 400));
            if (duplicate == 1)
            {
                AddChild();
            }
        }
        else
        {
            Destroy(oilParent);
            GameManager.instance.ChangeMoney(10 * TrashManagerScript.trashCashMultiplier);
        }
    }

    public void AddChild()
    {
        // Set random position
        float oilx = Random.Range(1f, 4f);
        float oily = Random.Range(1f, 4f);

        // Spawn the oil, set parent, set position
        GameObject oilBit = Instantiate(oil, new Vector3(0, 0, 0), Quaternion.identity);
        oilBit.transform.parent = this.transform;
        oilBit.transform.position = new Vector3(oilx + this.transform.position.x, oily + this.transform.position.y, 0);
    }
}