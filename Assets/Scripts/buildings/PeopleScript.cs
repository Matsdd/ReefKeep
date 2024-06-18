using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleScript : MonoBehaviour
{
    private int state = 1;
    private int side;
    public float moveSpeed;
    private float timer = 0;

    private void Start()
    {
        //0=spawnt rechts 1 is spawnt linkts
        side = Random.Range(0, 2);

        if (side == 0)
        {
            this.gameObject.transform.position = new Vector3(-9.2f,2.5f,50);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(9.2f, 0.9f, 50);
        }

        moveSpeed = Random.Range(15, 25)*0.001f;
    }
    private void FixedUpdate()
    {
        //vec is positie
        Vector3 vec = this.gameObject.transform.position;
        if (side == 0)
        {
            if (state == 1)
            {
                //past vec aan en daarna positie daarna check of positie goed is om te draaien
                vec.x += moveSpeed;
                this.gameObject.transform.position = vec;

                if (vec.x >= -1.45f)
                {
                    state = 2;
                }
            }
            else if (state == 2)
            {
                vec.y -= moveSpeed;
                this.gameObject.transform.position = vec;

                if (vec.y <= 0.8f)
                {
                    state = 3;
                }
            }
            else
            {
                vec.x += moveSpeed;
                this.gameObject.transform.position = vec;

                if (vec.x > 9.5f)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            if (state == 1)
            {
                //past vec aan en daarna positie daarna check of positie goed is om te draaien
                vec.x -= moveSpeed;
                this.gameObject.transform.position = vec;

                if (vec.x <= -1.45f)
                {
                    state = 2;
                }
            }
            else if (state == 2)
            {
                vec.y += moveSpeed;
                this.gameObject.transform.position = vec;

                if (vec.y >= 2.5f)
                {
                    state = 3;
                }
            }
            else
            {
                vec.x -= moveSpeed;
                this.gameObject.transform.position = vec;

                if (vec.x < -9.5f)
                {
                    Destroy(this.gameObject);
                }
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

                if (hit.collider != null && hit.collider.gameObject.CompareTag("People"))
                {
                    timer = 60;
                    SfxManager.instance.playSfx("sVisitor");
                }
            }
        }
    }
}
