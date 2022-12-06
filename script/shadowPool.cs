using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class shadowPool : MonoBehaviour
{
    public static shadowPool instance;
    public GameObject shadowPrefab;
    public int shadowCount;
    private Queue<GameObject> availableObject = new Queue<GameObject> ();

    private void Awake()
    {
        instance = this;
    }

    public void FillPool()
    {
        for(int i = 0; i < shadowCount; i++)
        {
            
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);

        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive (false);

        availableObject.Enqueue (gameObject);
    }


    public GameObject GetFormPool()
    {
        if (availableObject.Count ==0)
        {
            FillPool();
        }
        
        var outShadow = availableObject.Dequeue();

        outShadow.SetActive(true);

        return outShadow;
        
    }
}
