using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacle : MonoBehaviour
{
    public float DestoryTime;
    public float leftTime;

    public Animator anima;
    // Start is called before the first frame update
    void Start()
    {
        leftTime = DestoryTime;
        anima.SetBool("isGrowing", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(leftTime > 0)
        {
            leftTime -= Time.deltaTime;
        }
        if(leftTime <= 0)
        {
            anima.SetBool("isShrinkage", true);
        }
    }

    public void GrowOver()
    {
        anima.SetBool("isGrowing", false);
    }

    public void ShrinkageOver()
    {
        Destroy(this.gameObject);
    }
}
