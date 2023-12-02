using UnityEngine;

public class FracturedObjectsPool : MonoBehaviour
{
    // [SerializeField] GameObject[] fracturedObjects;
    // [SerializeField] List<ObjectPool> pools;

    // void Start()
    // {
    //     foreach (GameObject obj in fracturedObjects)
    //     {
    //         ObjectPool pool = gameObject.AddComponent<ObjectPool>();
    //         pool.InitializePool(obj.GetComponent<FractureObject>().fracturedObject, transform, 5);
    //         pools.Add(pool);
    //     }
    // }

    // public GameObject GetPooledObject(string objName)
    // {
    //     foreach (ObjectPool pool in pools)
    //     {
    //         GameObject obj = pool.GetDeactiveObject();
    //         if (objName == obj.GetComponent<FracturedObjectSO>().Name)
    //         {
    //             obj.SetActive(true);
    //             return obj;
    //         }
    //     }
    //     return null;
    // }
}