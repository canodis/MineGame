using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftInfoPanel : MonoBehaviour
{
    private ObjectPool infoPanelPool;

    void Start()
    {
        infoPanelPool = GetComponent<ObjectPool>();
    }
    public void addTextInfoPanel(string text)
    {
        GameObject infoPanel = infoPanelPool.GetObject();
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = text;
        StartCoroutine(WaitAndDeactivate(infoPanel, 5f));
    }

    
    public void addResourceInfoPanel(CollectableItemSO data, int quantity)
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

    public void addUpgradeInfoPanel(EquippableTool data)
    {
        GameObject infoPanel = infoPanelPool.GetObject();
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Upgraded: " 
            + data.itemData.Name + " +" + data.itemLevel;
        StartCoroutine(WaitAndDeactivate(infoPanel, 5f));
    }

    public void addEvolveInfoPanel(EquippableTool data)
    {
        GameObject infoPanel = infoPanelPool.GetObject();
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Evolved: " 
            + data.itemData.Name + " +" + data.itemLevel;
        StartCoroutine(WaitAndDeactivate(infoPanel, 5f));
    }

    private IEnumerator<WaitForSeconds> WaitAndDeactivate(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}