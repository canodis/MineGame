using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class Inventory : MonoBehaviour
{
    public List<CollectableItem> CollectableItems = new List<CollectableItem>();
    public List<EquippableTool> EquippableTools = new List<EquippableTool>();
    public EquippableTool EquippedTool;
    public ObjectPool infoPanelPool;
    public int Money;

    void Start()
    {
        EquippedTool = EquippableTools[0];
        foreach (EquippableTool item in EquippableTools)
        {
            item.active = false;
        }
        EquippedTool.active = true;
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
        addResourceInfoPanel(data, quantity);
    }

    private void addResourceInfoPanel(CollectableItemSO data, int quantity)
    {
        GameObject infoPanel = infoPanelPool.GetObject();
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = data.name + " (" + quantity + ")";
        StartCoroutine(WaitAndDeactivate(infoPanel, 5f));
    }

    public void addMoneyInfoPanel(int amount)
    {
        GameObject infoPanel = infoPanelPool.GetObject();
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Gained: " + amount;
        StartCoroutine(WaitAndDeactivate(infoPanel, 5f));
    }

    public void addUpgradeInfoPanel(EquippableItemSO data)
    {
        GameObject infoPanel = infoPanelPool.GetObject();
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Upgraded: " + data.Name;
        StartCoroutine(WaitAndDeactivate(infoPanel, 5f));
    }

    public void addTextInfoPanel(string text)
    {
        GameObject infoPanel = infoPanelPool.GetObject();
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = text;
        StartCoroutine(WaitAndDeactivate(infoPanel, 5f));
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
        foreach (EquippableTool item in EquippableTools)
        {
            if (item.itemData.Name == tool.itemData.Name)
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    } 

    private IEnumerator<WaitForSeconds> WaitAndDeactivate(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}