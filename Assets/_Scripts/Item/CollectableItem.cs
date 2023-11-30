using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public CollectableItemSO itemData;
    public int quantity;

    public CollectableItem()
    {
        quantity = 0;
    }

    public void SetItemData(CollectableItemSO itemData)
    {
        this.itemData = itemData;
    }

    public void SetQuantity(int quantity)
    {
        this.quantity = quantity;
    }

    
}