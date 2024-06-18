using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlyScript : MonoBehaviour
{
    private int side;
    private float timer = 0;
    private void Start()
    {
        side = Random.Range(0, 2);
        if (side == 0)
        {
            this.gameObject.transform.position = new Vector3(-10,Random.Range(-4,5),55);
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(10, Random.Range(-4, 5), 55);
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    private void FixedUpdate()
    {
        Vector3 vec = this.gameObject.transform.position;
        if (side == 0)
        {
            vec.x += 0.1f;
            this.gameObject.transform.position = vec;
            //destroy wanneer uit scherm
            if (vec.x > 10)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            vec.x -= 0.1f;
            this.gameObject.transform.position = vec;
            //destroy wanneer uit scherm
            if (vec.x < -10)
            {
                Destroy(this.gameObject);
            }
        }
        timer--;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (timer <= 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject.CompareTag("Bird"))
                {
                    timer = 60;
                    SfxManager.instance.playSfx("sBird");
                }
            }
        }
    }
}
