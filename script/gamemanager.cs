using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance;

    public Vector2 lastPosition;

    public GameObject frog;

    public GameObject level;

    public gamemanager player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Instantiate(player, lastPosition);
        reloadlevel(level);
    }

    public void reloadlevel(GameObject level)
    {
        
        foreach (Transform child in level.transform)
        {
            Instantiate(frog, child.position, child.rotation);
        }

    }

}
