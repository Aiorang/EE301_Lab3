using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class frog : MonoBehaviour
{
    public int movespeed;
    public GameObject frogobj;
    public int hp = 100;
    public float speed;
    public Vector2 dir;
    private bool isHit;
    private AnimatorStateInfo info;
    private Animator anima;
    [SerializeField] private Rigidbody2D rb;
    public GameObject DropThing;
    [Header("Data")]
    public float PhyDefense = 50;
    public float MagicDefense = 10;
    public int AttackPower = 20;
    public int MagicPower = 0;

    private GameObject player;

    public float hatred;
    public float attackrange;

    public float attackCD =1;
    public float leftTime;
    public bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anima =  transform.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        info = anima.GetCurrentAnimatorStateInfo(0);
        if (isHit)
        {
            rb.velocity = dir * speed;
            if (info.normalizedTime >= .6f)
            {
                isHit = false;
            }
        }
        death();
        MonsterAI();
    }

    //public void GertHit(Vector2 direction)
    //{
    //    transform.localScale = new Vector3(-direction.x,1,1);
    //    rb.velocity = new Vector2(-10, rb.velocity.y);
    //    isHit = true;
    //    this.dir = direction;
    //    anima.SetTrigger("hurt");
    //    hp -= 10;
    //}
    public void GetHit( float phyD, float magicD)
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(-50, rb.velocity.y);
        }
        if (transform.position.x >= player.transform.position.x)
        {
            rb.velocity = new Vector2(50, rb.velocity.y);
        }
        //rb.velocity = new Vector2(-10, rb.velocity.y);
        anima.SetTrigger("hurt");
        phyD = phyD - PhyDefense;
        magicD = magicD - MagicDefense;
        if (phyD < 0) phyD = 0;
        if (magicD < 0) magicD = 0;
        hp -= ((int)phyD + (int)magicD);
    }

    void death()
    {
        if (hp <= 0)
        {
            Destroy(frogobj);
            //Instantiate(DropThing, transform.position, transform.rotation);
        }
    }

    void MonsterAI()
    {
        float dx, dy;
        dx = player.transform.position.x - transform.position.x;
        dy = player.transform.position.y - transform.position.y;
        double d = Math.Pow(dx, 2) + Math.Pow(dy, 2);
        d = Math.Pow(d, 0.5f);
        if (d <= hatred&&d>=attackCD)
        {
            rb.velocity = new Vector2(dx*movespeed / Math.Abs(dx), dy * movespeed / Math.Abs(dy)); 
        }
        if (d <= attackrange)
        {
            if(leftTime <= 0) {
                anima.SetTrigger("attack");
                isAttack = true;
                leftTime = attackCD;
            }
            if (leftTime > 0)
            {
                leftTime -= Time.deltaTime;
            }
            
        }
    }
    public void Attackover()
    {
        isAttack = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&&isAttack == true)
        {
                player.GetComponent<playercontroller>().GetHit(frogobj,60f,0f);
        }
    }
}
