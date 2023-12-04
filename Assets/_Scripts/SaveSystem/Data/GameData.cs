using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Money;
    public Vector3 PlayerPosition;
    public string ActiveToolName;

    public List<EquippableItemData> EquippableItems;
    public List<SpawnerMineData> SpawnerMineData;
    public List<CollectableItemData> CollectableItems;

    public GameData()
    {
        Money = 0;
        PlayerPosition = new Vector3(-35, 0, -25);
        ActiveToolName = "Wooden Pickaxe";
        EquippableItems = new List<EquippableItemData>();
        SpawnerMineData = new List<SpawnerMineData>();
        CollectableItems = new List<CollectableItemData>();
    }

    public void SetEquippablesData(List<EquippableTool> itemList)
    {
        EquippableItems.Clear();
        foreach (var item in itemList)
        {
            EquippableItems.Add(new EquippableItemData(item.itemData.Name, item.active, item.itemLevel));
        }
    }

    public void SetSpawnerMineData(string spawnerName, List<GameObject> spawnedMines)
    {
        SpawnerMineData.Clear();
        foreach (var mine in spawnedMines)
        {
            SpawnerMineData.Add(new SpawnerMineData(spawnerName, mine.GetComponent<Resource>().itemData.Name,
                mine.gameObject.transform.position,
                mine.GetComponent<Resource>().getHealthInt()));
        }
    }

    public void SetCollectableItemDatas(List<CollectableItem> items)
    {
        CollectableItems.Clear();
        foreach (var item in items)
        {
            CollectableItems.Add(new CollectableItemData(item.itemData.Name, item.quantity));
        }
    }

    public List<CollectableItemData> GetCollectableItemDatas()
    {
        return CollectableItems;
    }

    public List<EquippableItemData> GetEquippableDatas()
    {
        return EquippableItems;
    }

    public List<SpawnerMineData> GetSpawnerMineDatas()
    {
        return SpawnerMineData;
    }


}

