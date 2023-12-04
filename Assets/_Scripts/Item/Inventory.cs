using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IDataPersistance
{
    [SerializeField] private CollectableItemSO[] collectableItemSOs;
    public List<CollectableItem> CollectableItems = new List<CollectableItem>();
    public List<EquippableTool> EquippableTools = new List<EquippableTool>();

    public EquippableTool EquippedTool;
    public int Money;

    private LeftInfoPanel leftInfoPanel;

    void Start()
    {
        leftInfoPanel = GameObject.FindGameObjectWithTag("LeftInfoPanel").GetComponent<LeftInfoPanel>();
    }

    public void addItem(CollectableItemSO data, int quantity)
    {
        bool found = false;

        foreach (CollectableItem item in CollectableItems)
        {
            if (item.itemData.Name == data.Name)
            {
                item.quantity += quantity;
                found = true;
                quantity = item.quantity;
                break;
            }
        }
        if (!found)
        {
            CollectableItem newItem = gameObject.AddComponent<CollectableItem>();
            newItem.SetItemData(data);
            newItem.SetQuantity(quantity);
            CollectableItems.Add(newItem);
        }
        leftInfoPanel.addResourceInfoPanel(data, quantity);
    }

    public CollectableItem getItem(CollectableItemSO data)
    {
        foreach (CollectableItem item in CollectableItems)
        {
            if (item.itemData.Name == data.Name)
            {
                return item;
            }
        }
        return null;
    }

    public EquippableTool getTool(EquippableItemSO data)
    {
        foreach (EquippableTool item in EquippableTools)
        {
            if (item.itemData.Name == data.Name)
            {
                return item;
            }
        }
        return null;
    }

    public void EquipTool(EquippableTool tool)
    {
        EquippedTool = tool;
    }

    public void RemoveItem(CollectableItemSO itemData, int v)
    {
        foreach (CollectableItem item in CollectableItems)
        {
            if (item.itemData.Name == itemData.Name)
            {
                item.quantity -= v;
                if (item.quantity <= 0)
                {
                    CollectableItems.Remove(item);
                }
                break;
            }
        }
    }

    public void SetActiveEquippedTool(EquippableTool tool)
    {
        tool.gameObject.SetActive(true);
    }


    #region SaveSystem

    public void LoadData(GameData data)
    {
        Money = data.Money;
        GetEquippableItems(data);
        GetCollectableItems(data);
    }

    public void SaveData(ref GameData data)
    {
        data.Money = Money;
        data.ActiveToolName = EquippedTool.itemData.Name;
        data.SetEquippablesData(EquippableTools);
        data.SetCollectableItemDatas(CollectableItems);
    }

    private void GetCollectableItems(GameData data)
    {
        CollectableItems.Clear();
        List<CollectableItemData> collectableItemDatas = data.GetCollectableItemDatas();
        foreach (CollectableItemData itemData in collectableItemDatas)
        {
            foreach (CollectableItemSO so in collectableItemSOs)
            {
                if (so.Name == itemData.Name)
                {
                    CollectableItem item = gameObject.AddComponent<CollectableItem>();
                    item.SetItemData(so);
                    item.SetQuantity(itemData.Quantity);
                    CollectableItems.Add(item);
                    break;
                }
            }
        }
    }

    private void GetEquippableItems(GameData data)
    {
        List<EquippableItemData> equippableItemDatas = data.GetEquippableDatas();
        foreach (EquippableItemData itemData in equippableItemDatas)
        {
            foreach (EquippableTool item in EquippableTools)
            {
                if (item.itemData.Name == itemData.Name)
                {
                    item.active = itemData.Active;
                    item.itemLevel = itemData.Level;
                    break;
                }
            }
        }
        foreach (EquippableTool item in EquippableTools)
        {
            if (item.active)
            {
                EquippedTool = item;
                SetActiveEquippedTool(item);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    #endregion
}