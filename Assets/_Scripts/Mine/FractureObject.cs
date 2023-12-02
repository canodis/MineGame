using UnityEngine;

public class FractureObject : MonoBehaviour
{
    public GameObject fracturedObject;
    public float minFractureForce = 50;
    public float maxFractureForce = 50;
    public float fractureRadius = 10;

    private GameObject fractObj;

    public void Fracture()
    {
        if (fracturedObject != null)
        {
            fractObj = Instantiate(fracturedObject, transform.position, transform.rotation);
            foreach (Transform child in fractObj.transform)
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(minFractureForce, maxFractureForce), transform.position, fractureRadius);
            Destroy(fractObj, 5f);
        }
    }
}

