using UnityEngine;

public class EquippableTool : MonoBehaviour
{
    public EquippableItemSO itemData;
    public bool active;
    public int itemLevel;
    public float damage;

    public void Upgrade()
    {
        itemLevel++;
        damage += itemData.ToolRank * itemLevel + 1;
    }

    public EquippableTool(string name, bool active, int level)
    {
        this.name = name;
        this.active = active;
        this.itemLevel = level;
        damage = itemData.ToolRank * itemLevel + 1;
    }
}