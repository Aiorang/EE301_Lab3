using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMap : MonoBehaviour
{
    public GameObject AbilityMapPlane;
    public GameObject player;
    private int[] set =  new int[2];
    private int[] ability = new int[6];
    public Text scale;

    [Header("Bar")]
    public GameObject MaxHPBar;
    public GameObject MaxMPBar;
    public GameObject PhyDefenseBar;
    public GameObject MagicDefenseBar;
    public GameObject AttackPowerBar;
    public GameObject MagicPowerBar;
    [Header("Text")]
    public Text MaxHP;
    public Text MaxMP;
    public Text PhyDefense;
    public Text MagicDefense;
    public Text AttackPower;
    public Text MagicPower;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ability = player.GetComponent<playercontroller>().getAbility();
        reselect();
    }

    private void Update()
    {
        ability = player.GetComponent<playercontroller>().getAbility();
        updateBar();
    }

    private void updateBar()
    {
        MaxHPBar.GetComponent<Image>().fillAmount = (float)ability[0] / 200;
        MaxHP.text = string.Concat(ability[0]);
        MaxMPBar.GetComponent<Image>().fillAmount = (float)ability[1] / 200;
        MaxMP.text = string.Concat(ability[1]);
        PhyDefenseBar.GetComponent<Image>().fillAmount = (float)ability[2] / 100;
        PhyDefense.text = string.Concat(ability[2]);
        MagicDefenseBar.GetComponent<Image>().fillAmount = (float)ability[3] / 100;
        MagicDefense.text = string.Concat(ability[3]);
        AttackPowerBar.GetComponent<Image>().fillAmount = (float)ability[4] / 100;
        AttackPower.text = string.Concat(ability[4]);
        MagicPowerBar.GetComponent<Image>().fillAmount = (float)ability[5] / 100;
        MagicPower.text = string.Concat(ability[5]);
    }

    public void open()
    {
        AbilityMapPlane.SetActive(true);
    }
    public void close()
    {
        AbilityMapPlane.SetActive(false);
    }

    public void choice0()
    {
        setControl(0, "MaxHP");
    }
    public void choice1()
    {
        setControl(1, "MaxMP");
    }
    public void choice2()
    {
        setControl(2,"PhyDefense");
    }
    public void choice3()
    {
        setControl(3, "MagicDefense");
    }
    public void choice4()
    {
        setControl(4, "PhyPower");
    }
    public void choice5()
    {
        setControl(5, " MagicPower");
    }

    public void setControl(int i,string s)
    {
        string sca = scale.text;
        if(set[0] == -1) {
            set[0] = i;
            scale.text = string.Concat(s,sca);
        }
        else
        {
            if(i != set[0]&&set[1] == -1)
            {
                set[1] = i;
                scale.text = string.Concat(sca,s);
            }
        }
    }

    public void decision()
    {
        player.GetComponent<playercontroller>().setAbility(set);
        reselect();
    }

    public void reselect()
    {
        set[0] = -1;
        set[1] = -1;
        scale.text = "------------------";
    }
}
