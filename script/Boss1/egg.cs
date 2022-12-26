using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg : MonoBehaviour
{
    public float LncubationTime;
    public float leftTime;
    public int hp;

    public GameObject frog;
    public Transform MonsterPool;


    // Start is called before the first frame update
    void Start()
    {
        leftTime = LncubationTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
        }
        if (leftTime <= 0) {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y+1, 0);
            GameObject Newfrog =  Instantiate(frog,pos, transform.rotation);
            Newfrog.transform.SetParent(MonsterPool);
            Destroy(this.gameObject);
        }
    }




}
