using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enterthelevel : MonoBehaviour
{
    public GameObject button;
    public bool ReadyInto;
    public int sceneTarget;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (ReadyInto && Input.GetKeyDown(KeyCode.F))
        {
            player.GetComponent<playercontroller>().save();
            SceneManager.LoadScene(sceneTarget);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        button.SetActive(true);
        ReadyInto = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        button.SetActive(false);
        ReadyInto = false;
    }
    
}
