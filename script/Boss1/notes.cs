using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class notes : MonoBehaviour
{
    public GameObject player;

    public int flymode;
    public int movespeed;
    public Rigidbody2D rb;

    public float leftTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        leftTime -= Time.deltaTime;
        if(leftTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void FindPlayer()
    {
        float dx, dy;
        dx = player.transform.position.x - transform.position.x;
        dy = player.transform.position.y - transform.position.y;
        double d = Math.Pow(dx, 2) + Math.Pow(dy, 2);
        d = Math.Pow(d, 0.5f);
            rb.velocity = new Vector2(dx * movespeed / Math.Abs(dx), dy * movespeed / Math.Abs(dy));
    }
}
