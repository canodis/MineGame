

[System.Serializable]
public class EquippableItemData
{
    public string Name;
    public bool Active;
    public int Level;

    public EquippableItemData(string name, bool active, int level)
    {
        Name = name;
        Active = active;
        Level = level;
    }
}