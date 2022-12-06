using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void openShop() {
        this.gameObject.SetActive(true);
    }
    public void closeShop()
    {
        this.gameObject.SetActive(false);
        player.GetComponent<playercontroller>().save();
    }

    public void buy1()
    {
        buy(1);
    }
    public void buy2()
    {
        buy(2);
    }
    public void buy3()
    {
        buy(3);
    }

    public void buy(int type)
    {
        int price = 10;
        player.GetComponent<playercontroller>().setBag(price,type);
    }
}
