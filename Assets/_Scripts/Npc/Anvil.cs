using UnityEngine;

public class Anvil : MonoBehaviour
{
    public UpgradeItemCreator creator;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            creator.OpenUpgradeMenu();
            creator.CreateItemList();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            creator.CloseUpgradeMenu();
        }
    }
}