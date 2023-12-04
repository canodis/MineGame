using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemCreator : MonoBehaviour
{
    [SerializeField] private IconCreator iconCreator;
    [SerializeField] private TextMeshProUGUI MoneyText;
    public GameObject UpgradeMainPanel;
    public GameObject UpgradeItemList;
    public GameObject UpgradePanel;
    public GameObject ItemImage;
    public Image UpgradeItemImage;
    public Button UpgradeButton;
    public TextMeshProUGUI UpgradeItemName;
    public TextMeshProUGUI UpgradeItemDescription;
    public TextMeshProUGUI UpgradeButtonText;
    
    private Inventory inventory;
    private UIHelper uIHelper;
    private LeftInfoPanel leftInfoPanel;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        uIHelper = GameObject.FindGameObjectWithTag("UIHelper").GetComponent<UIHelper>();
        leftInfoPanel = GameObject.FindGameObjectWithTag("LeftInfoPanel").GetComponent<LeftInfoPanel>();
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
            playerItem.GetComponentInChildren<TextMeshProUGUI>().text = item.itemData.Name
                + " +" + item.itemLevel.ToString();
        }
    }

    public void OpenUpgradePanel(EquippableItemSO item)
    {
        EquippableTool tool = inventory.getTool(item);
        UpgradePanel.SetActive(true);
        UpgradeItemImage.sprite = item.Sprite;
        UpgradeItemName.text = item.Name + " +" + tool.itemLevel.ToString();
        UpgradeButton.onClick.RemoveAllListeners();
        if (tool.itemLevel == item.MaxLevel)
        {
            UpgradeButton.onClick.AddListener(() => EvolveItem(item));
            if (inventory.Money >= item.EvolvePrice)
                UpgradeButtonText.color = Color.green;
            else
                UpgradeButtonText.color = Color.red;
            UpgradeButtonText.text = "Evolve";
            UpgradeItemDescription.text = "Evolve Price : " + item.EvolvePrice;
        }
        else
        {
            UpgradeButton.onClick.AddListener(() => UpgradeItem(tool));
            if (inventory.Money >= item.UpgradePrices[tool.itemLevel])
                UpgradeButtonText.color = Color.green;
            else
                UpgradeButtonText.color = Color.red;
            UpgradeButtonText.text = "Upgrade to +" + (tool.itemLevel + 1).ToString();
            UpgradeItemDescription.text = "Upgrade Price : " + item.UpgradePrices[tool.itemLevel];
        }
    }

    public void OpenUpgradeMenu()
    {
        UpgradeMainPanel.SetActive(true);
        CreateItemList();
        UpdateMoneyText();
        uIHelper.DeactiveInGamePanel();
    }

    public void CloseUpgradeMenu(bool isIconActive)
    {
        UpgradeMainPanel.SetActive(false);
        UpgradePanel.SetActive(false);

        uIHelper.ActiveInGamePanel();
        if (isIconActive)
            iconCreator.CreateIconButton(() => OpenUpgradeMenu());

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

    public void EvolveItem(EquippableItemSO item)
    {
        if (inventory.Money < item.EvolvePrice)
        {
            leftInfoPanel.addTextInfoPanel("Not enough money");
            return;
        }
        inventory.Money -= item.EvolvePrice;
        UpdateMoneyText();
        foreach (EquippableTool tool in inventory.EquippableTools)
        {
            if (inventory.EquippedTool.itemData.toolType != tool.itemData.toolType)
                continue;
            if (inventory.EquippedTool.itemData.ToolRank == tool.itemData.ToolRank - 1)
            {
                inventory.EquippedTool.active = false;
                inventory.EquippedTool = tool;
                tool.active = true;
                inventory.SetActiveEquippedTool(tool);
                CreateItemList();
                UpgradePanel.SetActive(false);
                leftInfoPanel.addUpgradeInfoPanel(tool);
                break;
            }
        }
    }
    public void UpgradeItem(EquippableTool tool)
    {
        if (inventory.Money < tool.itemData.UpgradePrices[tool.itemLevel])
        {
            leftInfoPanel.addTextInfoPanel("Not enough money");
            return;
        }
        inventory.Money -= tool.itemData.UpgradePrices[tool.itemLevel];
        UpdateMoneyText();
        tool.Upgrade();
        CreateItemList();
        OpenUpgradePanel(tool.itemData);
        leftInfoPanel.addUpgradeInfoPanel(tool);
    }

    private void UpdateMoneyText()
    {
        MoneyText.text = "Money : " + inventory.Money.ToString();
    }
}
