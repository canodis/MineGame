using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemCreator : MonoBehaviour
{
    private Inventory inventory;
    public GameObject UpgradeMainPanel;
    public GameObject UpgradeItemList;
    public GameObject UpgradePanel;
    public GameObject ItemImage;
    public Image UpgradeItemImage;
    public Button UpgradeButton;
    public TextMeshProUGUI UpgradeItemName;
    public TextMeshProUGUI UpgradeItemDescription;
    public TextMeshProUGUI UpgradeButtonText;
    
    private UIHelper uIHelper;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        uIHelper = GameObject.FindGameObjectWithTag("UIHelper").GetComponent<UIHelper>();
        UpgradePanel.SetActive(false);
        UpgradeMainPanel.SetActive(false);
    }

    public void CreateItemList()
    {
        ClearList();
        foreach (EquippableTool item in inventory.EquippableTools)
        {
            if (item.active == false)
                continue;
            GameObject playerItem = Instantiate(ItemImage, UpgradeItemList.transform);
            playerItem.GetComponent<Image>().sprite = item.itemData.Sprite;
            playerItem.GetComponent<UpgradeItem>().itemData = item.itemData;
            playerItem.GetComponentInChildren<TextMeshProUGUI>().text = item.itemData.Name;
        }
    }

    public void OpenUpgradePanel(EquippableItemSO item)
    {
        UpgradePanel.SetActive(true);
        UpgradeItemImage.sprite = item.Sprite;
        UpgradeItemName.text = item.Name;
        UpgradeItemDescription.text = "Upgrade Price : " + item.UpgradePrice;
        UpgradeButton.onClick.RemoveAllListeners();
        UpgradeButton.onClick.AddListener(() => UpgradeItem(item));
        if (inventory.Money >= item.UpgradePrice)
            UpgradeButtonText.color = Color.green;
        else
            UpgradeButtonText.color = Color.red;
        UpgradeButtonText.text = "Upgrade";
    }

    public void OpenUpgradeMenu()
    {
        UpgradeMainPanel.SetActive(true);
        CreateItemList();
    
        uIHelper.DeactiveInGamePanel();
    }

    public void CloseUpgradeMenu()
    {
        UpgradeMainPanel.SetActive(false);
        UpgradePanel.SetActive(false);

        uIHelper.ActiveInGamePanel();
    }

    public void CloseUpgradePanel()
    {
        UpgradePanel.SetActive(false);
    }

    private void ClearList()
    {
        foreach (Transform child in UpgradeItemList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpgradeItem(EquippableItemSO item)
    {
        if (inventory.Money < item.UpgradePrice)
        {
            inventory.addTextInfoPanel("Not enough money");
            return;
        }
        inventory.Money -= item.UpgradePrice;
        foreach (EquippableTool tool in inventory.EquippableTools)
        {
            if (inventory.EquippedTool.itemData.toolType != tool.itemData.toolType)
                continue;
            if (inventory.EquippedTool.itemData.ToolLevel == tool.itemData.ToolLevel - 1)
            {
                inventory.EquippedTool.active = false;
                inventory.EquippedTool = tool;
                tool.active = true;
                inventory.SetActiveEquippedTool(tool);
                CreateItemList();
                UpgradePanel.SetActive(false);
                inventory.addUpgradeInfoPanel(item);
                break;
            }
        }

    }
}
