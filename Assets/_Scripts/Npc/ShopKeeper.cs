using Unity.VisualScripting;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private ShopUICreator creator;
    private IconCreator iconCreator;

    void Start()
    {
        creator = GameObject.FindGameObjectWithTag("UICreator").GetComponent<ShopUICreator>();
        iconCreator = GetComponent<IconCreator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            iconCreator.CreateIconButton(ButtonClickFunction);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            creator.CloseShop();
            iconCreator.RemoveIconButton();
        }
    }

    private void ButtonClickFunction()
    {
        creator.OpenShop();
    }
}