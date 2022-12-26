using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    [Header("運動")]
    [SerializeField] private Rigidbody2D rb;
    public float speed;
    public float jumpforce;
    [SerializeField] private  Animator anim;
    public LayerMask ground;
    public Collider2D coll;
    public bool ReadyJump = true;
    public bool ReadyDoubleJump;
    [Header("收集物")]
    public int cherry;
    [Header("Data")]
    public int hp = 100;
    public int MaxHP ;
    public int mp ;
    public int MaxMP ;
    public int bulletLeft ;
    public float PhyDefense ;
    public float MagicDefense ;
    public int AttackPower ;
    public int MagicPower ;
    [Header("背包")]
    public int cash;
    public int bulletPack;
    public int MedKit;
    public int potion;
    public int propCounter;
    [Header("受傷")]
    private bool ishurt;
    [Header("打擊感")]
    public int combostep;
    public float interval = 2f;
    public float timer;
    public bool isAttack;
    public float attackspeed;
    public int pause ;
    public float shakeTime , strength;
    private float horizontalmove;
    [Header("衝刺")]
    public float dashTime;
    private float dashTimeLeft;
    private float lastdash;
    public float dashCD;
    public float dashSpeed;
    public bool isDash;
    [Header("射擊")]
    public Transform shotPoint;
    public GameObject bullet;
    public GameObject fireball;



    public float dir = 1;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //transform.position = gamemanager.instance.lastPosition;
        load();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ishurt)
        {
            Dash();
            if (isDash)
                return;
            fire();
            movement();
            Attact();
            UseProps();
        }
        SwitchAnim();

        deathcheck();
    }

    void Dash()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        if (isDash)
        {
            if (dashTimeLeft > 0)
            {
                rb.velocity = new Vector2(horizontalmove * dashSpeed, 0);

                dashTimeLeft -= Time.deltaTime;

                shadowPool.instance.GetFormPool();
            }

            if(dashTimeLeft <= 0)
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
                isDash = false;
            }
        }
    }

    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedir = Input.GetAxisRaw("Horizontal");

        if (!isAttack) { 
            if (horizontalmove != 0)
            {
                rb.velocity = new Vector2(horizontalmove * speed, rb.velocity.y);
                anim.SetFloat("running", Mathf.Abs(facedir));
            }
          }
        else
        {
            rb.velocity = new Vector2(transform.localScale.x * attackspeed * dir, rb.velocity.y);
        }

        if (dir * facedir < 0)
        {
            //transform.localScale = new Vector3(facedir, 1, 1);
            transform.Rotate(0f, 180f, 0f);
        }
        if (facedir != 0)
            dir = facedir;
        if (Input.GetButtonDown("Jump"))
        {
            

            if (ReadyJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
                ReadyJump = false;
                ReadyDoubleJump = true;
            }
            else if (ReadyDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
                ReadyDoubleJump = false;
            }
            
            
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Time.time >= (lastdash + dashCD))
            {
                ReadyToDash();
            }
        }
    }

    void Attact()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAttack =  true;
            combostep++;
            if (combostep > 3)
                combostep = 1;
            timer = interval;
            anim.SetTrigger("hit");
            anim.SetInteger("combo",combostep);
        }

        if (timer !=0)
        {
            timer -= Time.deltaTime;
            if (timer <=0)
            {
                timer = 0;
                combostep = 0;
            }
        }
    }
    void fire()
    {
        if (Input.GetKeyDown(KeyCode.K)&&bulletLeft>0)
        {
            Instantiate(bullet, shotPoint.position,shotPoint.rotation);
            anim.SetTrigger("shot");
            bulletLeft -= 1;
        }
        if (Input.GetKeyDown(KeyCode.U) && mp > 0)
        {
            Instantiate(fireball, shotPoint.position, shotPoint.rotation);
            anim.SetTrigger("fire");
            mp -= 10;
        }
    }
    public void Attackover()
    {
        isAttack = false;
    }

    void SwitchAnim()
    {
        anim.SetBool("idle", false);

        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
       
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling",false);
            anim.SetBool("idle", true);
            ReadyJump = true;
            ReadyDoubleJump = false;
        }
         if (Mathf.Abs(rb.velocity.x) < 0.3f)
        {
            ishurt = false;
            anim.SetBool("ishurt", false);
        }
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            cherry += 1;

            bulletReload();
        }
        if (collision.tag == "enemies")
        {
            collision.GetComponent<frog>().GetHit(60f,0f);
            attacksence.Instance.HitPause(pause);
            attacksence.Instance.CameraShake(shakeTime,strength);
        }
        if (collision.gameObject.tag == "checkpoint")
        {
            save();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    public void GetHit(GameObject enemy,float phyD,float magicD)
    {
        if (transform.position.x < enemy.transform.position.x)
        {
            rb.velocity = new Vector2(-10, rb.velocity.y);
        }
        if (transform.position.x >= enemy.transform.position.x)
        {
            rb.velocity = new Vector2(10, rb.velocity.y);
        }
        ishurt = true;
        anim.SetBool("ishurt", true);
        phyD = phyD - PhyDefense;
        magicD = magicD - MagicDefense;
        if (phyD < 0) phyD = 0;
        if (magicD < 0) magicD = 0;
        hp -= ((int)phyD + (int)magicD);
    }

    void ReadyToDash()
    {
        isDash = true;

        dashTimeLeft = dashTime;

        lastdash = Time.time;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
    }

    void death()
    {
        
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    void deathcheck()
    {
        if (hp <= 0)
        {
            anim.SetTrigger("death");
            Invoke("death", 0.45f);
        }
    }

    public int getHP()
    {
        return hp;
    }

    public int getMP()
    {
        return mp;
    }

    public int getBulletLeft()
    {
        return bulletLeft;
    }

    public void bulletReload()
    {
        bulletLeft = 6;
    }

    public int setMaxHP(int addmaxhp)
    {
        MaxHP += addmaxhp;
        return MaxHP;
    }

    public int setMaxMP(int addmaxmp)
    {
        MaxMP += addmaxmp;
        return MaxMP;
    }


    public float GetDashCD()
    {
        float CD = Time.time - (lastdash + dashCD);
        return CD;
    }

    #region PlayerPrefs

    void SaveByPlayerPrefs()
    {
        PlayerPrefs.SetInt("MaxHP",MaxHP);
        PlayerPrefs.SetInt("MaxMP", MaxMP);
        PlayerPrefs.SetInt("HP",hp);
        PlayerPrefs.SetInt("MP",mp);
        PlayerPrefs.SetInt("BulletLeft",bulletLeft);
        PlayerPrefs.SetFloat("PhyDefens", PhyDefense);
        PlayerPrefs.SetFloat("MagicDefense",MagicDefense);
        PlayerPrefs.SetInt("AttackPower", AttackPower);
        PlayerPrefs.SetInt("MagicPower", MagicPower);

        PlayerPrefs.SetInt("Cash",cash);
        PlayerPrefs.SetInt("BulletPack", bulletPack);
        PlayerPrefs.SetInt("MedKit", MedKit);
        PlayerPrefs.SetInt("Potion", potion);
        PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
    }

    void LoadFromPlayerPrefs()
    {
       MaxHP =  PlayerPrefs.GetInt("MaxHP",100);
       MaxMP =  PlayerPrefs.GetInt("MaxMP",100);
       hp =  PlayerPrefs.GetInt("HP",100);
       mp =  PlayerPrefs.GetInt("MP",100);
       bulletLeft =  PlayerPrefs.GetInt("BulletLeft",6);
       PhyDefense =  PlayerPrefs.GetFloat("PhyDefens", 50);
       MagicDefense =  PlayerPrefs.GetFloat("MagicDefense", 50);
       AttackPower =  PlayerPrefs.GetInt("AttackPower", 50);
       MagicPower =  PlayerPrefs.GetInt("MagicPower", 50);

       cash =  PlayerPrefs.GetInt("Cash");
       bulletPack =  PlayerPrefs.GetInt("BulletPack");
       MedKit = PlayerPrefs.GetInt("MedKit");
       potion = PlayerPrefs.GetInt("Potion");
    }

    #endregion

    public void save()
    {
        SaveByPlayerPrefs();
    }
    public void load()
    {
        LoadFromPlayerPrefs();
    }

    public void setAbility(int[] set)
    {
        int[] change = new int[6];
        for (int i = 0; i < 6; i++)
            change[i] = 0;

        change[set[0]] = 1;
        change[set[1]] = -1;


        MaxHP += change[0] * 10;
        MaxMP += change[1] * 10;
        PhyDefense += (float)change[2] * 5;
        MagicDefense += (float)change[3] * 5;
        AttackPower += change[4] * 5;
        MagicPower += change[5] * 5;

        if (MaxHP < hp) hp = MaxHP;
        if (MaxMP < mp) mp = MaxMP;

        save();
        load();
    }
    public int[] getAbility()
    {
        int[] ability = new int[6];
        ability[0] = MaxHP;
        ability[1] = MaxMP;
        ability[2] = (int)PhyDefense;
        ability[3] = (int)MagicDefense;
        ability[4] = AttackPower;
        ability[5] = MagicPower;
        return ability;
    }

    public int[] GetBag()
    {
        int[] bag = new int[4];

        bag[0] = cash;
        bag[1] = bulletPack;
        bag[2] = MedKit;
        bag[3] = potion;

        return bag;
    }

    public void setBag(int price ,int type)
    {
        cash -= price;
        switch (type) {
            case 1:
                bulletPack++;
                break;
            case 2:
                MedKit++;
                break;
            case 3:
                potion++;
                break;
            
        }

    }

    public int switchProps()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            propCounter++;
            if (propCounter > 3) propCounter = 0;
            
        }
        return propCounter;
    }

    public void UseProps()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            if (propCounter == 1 && bulletPack > 0)
            {
                bulletLeft = 6;
                bulletPack--;
            }
            if (propCounter == 2 && MedKit > 0)
            {
                hp += 10;
                MedKit--;
                if (hp >= MaxHP) hp = MaxHP;
            }
            if (propCounter == 3 && potion > 0)
            {
                mp += 10;
                potion--;
                if (mp >= MaxMP) mp = MaxMP;
            }


        }
    }
}
