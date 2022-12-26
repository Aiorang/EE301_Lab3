using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject HPBar;
    public Text HPtext;
    private int MaxHP;

    public GameObject MPBar;
    public Text MPtext;
    private int MaxMP;

    public Text cash;

    public Text BulletLeft;

    public GameObject DashCD;

    private GameObject player;

    public GameObject prop1;
    public GameObject prop2;
    public GameObject prop3;
    public Text NumProp1;
    public Text NumProp2;
    public Text NumProp3;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MaxHP = player.GetComponent<playercontroller>().setMaxHP(0);
        MaxMP = player.GetComponent<playercontroller>().setMaxMP(0);

    }

    // Update is called once per frame
    void Update()
    {
        MaxHP = player.GetComponent<playercontroller>().setMaxHP(0);
        MaxMP = player.GetComponent<playercontroller>().setMaxMP(0);
        HP();
        MP();
        bullet();
        Dash();
        Bag();
        switchProps();
    }

    void HP()
    {
        HPBar.GetComponent<Image>().fillAmount = (float)player.transform.GetComponent<playercontroller>().getHP()/MaxHP;
        HPtext.text = string.Concat(player.GetComponent<playercontroller>().getHP(),"/",MaxHP);
    }

    void MP()
    {
        MPBar.GetComponent<Image>().fillAmount = (float)player.transform.GetComponent<playercontroller>().getMP() / MaxMP;
        MPtext.text = string.Concat(player.GetComponent<playercontroller>().getMP(), "/", MaxMP);
    }

    void bullet()
    {
        BulletLeft.text = string.Concat(":",player.GetComponent<playercontroller>().getBulletLeft(),"/6");
    }

    void Dash()
    {
        DashCD.GetComponent<Image>().fillAmount = player.GetComponent<playercontroller>().GetDashCD() / 2f;
    }
    void Bag()
    {
        int[] bag = player.GetComponent<playercontroller>().GetBag();
        cash.text = bag[0].ToString();
        NumProp1.text = bag[1].ToString();
        NumProp2.text = bag[2].ToString();
        NumProp3.text = bag[3].ToString();
    }

    public void switchProps()
    {
        int Counter = player.GetComponent<playercontroller>().switchProps();
        switch (Counter)
        {
            case 0:
                prop3.SetActive(false);
                break;
            case 1:
                prop1.SetActive(true);
                break;
            case 2:
                prop1.SetActive(false);
                prop2.SetActive(true);
                break;
            case 3:
                prop2.SetActive(false);
                prop3.SetActive(true);
                break;
        }
    }
}
