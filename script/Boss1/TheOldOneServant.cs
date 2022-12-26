using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheOldOneServant : MonoBehaviour
{
    public GameObject notes;
    public GameObject egg;
    public GameObject tentacles;
    //public GameObject laser;
    public GameObject platform;

    public int HP;
    public int speed;
    public int PhyDefense;
    public int MagicDefense;
    public int AttackPower;
    public int MagicPower;

    public Rigidbody2D rb;
    public Collider2D coll;
    public LayerMask groundLayer;

    private Color NomarlColor;
    private Color HurtColor = new Color(0.5f, 0.5f, 1, 1);
    private SpriteRenderer thisSprite;

    public bool isAttack;
    public bool isBeaten;
    public bool TouchGround;

    public GameObject player;

    public float ground;

    //public int stage = 1;

    public Animator anim;

    public float BeatenTime;
    public float BeatenLeftTime;
    int BeatenCounter;

    public float noteCD;
    public float noteLeftTime;

    public float AttackCD;
    public float AttackCDLeftTime;

    public float platformDuratione;
    public float platformDurationeLeft;

    public GameObject ReturnLocation;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        NomarlColor = thisSprite.color;
    }


    private void Update()
    {
        MonsterAI();
        PlatformState();
    }

    public void MonsterAI()
    {
        //if (HP <= 70) stage = 2;
        if (isBeaten)
        {
            BeatenLeftTime = BeatenTime;
            //isBeaten = false;
        }
        else if (BeatenLeftTime > 0) Beaten();
        else stage1();
        //else if (stage == 1) stage1();
        //else if (stage == 2) stage2();
    }

    public void Beaten()
    {
        while (!TouchGround)
        {
            rb.velocity = new Vector2(0, -10);
            if (coll.IsTouchingLayers(groundLayer))
            {
                TouchGround = true;
                break;
            }
        }
        BeatenLeftTime -= Time.deltaTime;
        if(BeatenLeftTime <= 0)
        {
            isBeaten = false;
            this.transform.position = ReturnLocation.transform.position;
            BeatenCounter = 0;
        }
    }

    public void stage1()
    {
        if(isAttack == false)
        {
            //move

            //attack
            AttackCDLeftTime -= Time.deltaTime;
            if (AttackCDLeftTime <= 0)
            {
                if(BeatenCounter  == 0)
                {
                    PlayTheFlute(3);
                }
                else if (BeatenCounter == 1)
                {
                    TentacleBinding();
                }else if(BeatenCounter == 2)
                {
                    LayingEgg();
                }else if (BeatenCounter == 3)
                {
                    showPlatform();
                }
                    BeatenCounter++;
            }
            if(BeatenCounter == 4)
            {
                //isBeaten = true;
                BeatenCounter = 0;
            }
        }
    }
    public void stage2()
    {
        //move
        if (isAttack == false)
        {

        }
        //attack
    }
    public void attackOver()
    {
        isAttack = false;
        AttackCDLeftTime = AttackCD;
    } 


    public void GetHit(int phyD,int magicD)
    {
        anim.SetBool("ishurt", true);
        phyD = phyD - PhyDefense;
        magicD = magicD - MagicDefense;
        if (phyD < 0) phyD = 0;
        if (magicD < 0) magicD = 0;
        HP -= ((int)phyD + (int)magicD);
        thisSprite.color = HurtColor;
    }

    public void LayingEgg()
    {
        isAttack = true;
        Instantiate(egg, transform.position, transform.rotation);
        attackOver();
    }

    public void PlayTheFlute(int num)
    {
        int left = num;
        while (left > 0)
        {
            if (noteLeftTime > 0)
            {
                noteLeftTime -= Time.deltaTime;
            }
            else
            {
                Instantiate(notes, transform.position, transform.rotation);
                noteLeftTime = noteCD;
                left--;
            }
        }
        attackOver();
    }

    public int LaunchNote ()
    {
        if (noteLeftTime > 0)
        {
            noteLeftTime -= Time.deltaTime;
            return 0;
        }
        else
        {
            Instantiate(notes, transform.position, transform.rotation);
            noteLeftTime = noteCD;
            return 1;
        }
    }

    public void TentacleBinding()
    {
        isAttack = true;
        Vector3 pos1 = new Vector3(player.transform.position.x+1, ground, 0);
        Vector3 pos2 = new Vector3(player.transform.position.x-1, ground, 0);
        Instantiate(tentacles, pos1, player.transform.rotation);
        Instantiate(tentacles, pos2, player.transform.rotation);
        attackOver();
    }

    public void showPlatform()
    {
        platform.SetActive(true);
        platformDurationeLeft = platformDuratione;
        //while (platformDurationeLeft > 0)
        //{
        //    platformDurationeLeft -= Time.deltaTime;
        //}
        //platform.SetActive(false);
    }
    public void PlatformState()
    {
        if (platformDurationeLeft > 0)
        {
            platformDurationeLeft -= Time.deltaTime;
        }
        else platform.SetActive(false);
    }


}
