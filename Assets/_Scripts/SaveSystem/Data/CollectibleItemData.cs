
[System.Serializable]
public class CollectableItemData
{
    public string Name;
    public int Quantity;

    public CollectableItemData(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}