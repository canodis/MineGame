using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;

    public int maxPoolSize;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, parent);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            pool.Enqueue(obj);
            return obj;
        }
        return null;
    }
}