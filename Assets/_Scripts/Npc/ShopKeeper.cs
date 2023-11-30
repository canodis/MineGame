using Unity.VisualScripting;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private ShopUICreator creator;

    void Start()
    {
        creator = GameObject.FindGameObjectWithTag("UICreator").GetComponent<ShopUICreator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            creator.OpenShop();
            creator.CreateShopItems();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            creator.CloseShop();
        }
    }
}