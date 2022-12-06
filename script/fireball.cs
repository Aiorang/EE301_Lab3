using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public float firespeed;
    public Collider2D coll;
    public Rigidbody2D firerb;
    public Animator fireanim;

    // Start is called before the first frame update
    void Start()
    {
        firerb.velocity = transform.right * firespeed;
        Invoke("fireboom", 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemies")
        {

            Invoke("fireboom", 0.1f);
            collision.GetComponent<frog>().GetHit(60f, 0f);



        }

    }
    public void fireboom()
    {
        firerb.velocity = new Vector2(0, 0);
        fireanim.SetTrigger("point");
        Invoke("destoryfireball", 0.2f);
    }

    void destoryfireball()
    {
        Destroy(gameObject);
    }

}
