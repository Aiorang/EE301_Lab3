using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    public float activeTime;
    public float activeStar;

    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;

        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;

        activeStar = Time.time;
    }
    void Update()
    {
        alpha *= alphaMultiplier;

        color = new Color(0.5f,0.5f,1,alpha);

        thisSprite.color = color;

        if (Time.time >=activeStar + activeTime)
        {
            shadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
