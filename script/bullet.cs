using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public Collider2D coll;
    public Rigidbody2D rb;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Invoke("boom", 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemies")
        {

            boom();
            collision.GetComponent<frog>().GetHit(60f,0f);

        }

    }
    public void boom()
    {
        rb.velocity =  new Vector2(1,0);
        anim.SetTrigger("point");
        Invoke("destoryBullet", 0.2f);
    }

    void destoryBullet()
    {
        Destroy(gameObject);
    }
}
