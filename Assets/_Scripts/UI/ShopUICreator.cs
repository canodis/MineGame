using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUICreator : MonoBehaviour
{
    private Inventory inventory;
    public GameObject ShopMainPanel;
    public GameObject ShopPanel;
    public GameObject ItemImage;
    public GameObject ItemInfoPanel;
    public GameObject ItemInfoPanelButtons;
    public GameObject ItemInfoPanelText;
    public GameObject ItemInfoPanelImage;
    public GameObject SellButton;
    public TextMeshProUGUI MoneyText;
    
    private UIHelper uIHelper;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        uIHelper = GameObject.FindGameObjectWithTag("UIHelper").GetComponent<UIHelper>();
        ShopMainPanel.SetActive(false);
    }

    public void CreateShopItems()
    {
        ClearShopItems();
        UpdateMoneyText();
        foreach (CollectableItem item in inventory.CollectableItems)
        {
            GameObject shopItem = Instantiate(ItemImage, ShopPanel.transform);
            shopItem.GetComponent<Image>().sprite = item.itemData.Sprite;
            shopItem.GetComponent<ShopItem>().itemData = item.itemData;
        }
    }

    public void OpenShop()
    {
        ShopMainPanel.SetActive(true);
        CreateShopItems();
        UpdateMoneyText();

        uIHelper.DeactiveInGamePanel();
    }

    public void CloseShop()
    {
        ItemInfoPanel.SetActive(false);
        ShopMainPanel.SetActive(false);

        uIHelper.ActiveInGamePanel();
    }

    public void CreateItemInfoPanel(CollectableItemSO itemData, Vector2 position)
    {
        ClearItemInfoPanel();
        CollectableItem item = inventory.getItem(itemData);
        ItemInfoPanel.SetActive(true);
        ItemInfoPanel.transform.position = position;
        ItemInfoPanelImage.GetComponent<Image>().sprite = item.itemData.Sprite;
        ItemInfoPanelText.GetComponent<TextMeshProUGUI>().text = item.itemData.Name + "\n(" + item.quantity + ")";
        CreateItemInfoPanelButtons(item);
    }

    private void CreateItemInfoPanelButtons(CollectableItem item)
    {
        if (item.quantity >= 1)
        {
            GameObject button = Instantiate(SellButton, ItemInfoPanelButtons.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Sell\n1x";
            button.GetComponent<Button>().onClick.AddListener(() => SellItem(item, 1));
        }
        if (item.quantity >= 10)
        {
            GameObject button = Instantiate(SellButton, ItemInfoPanelButtons.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Sell\n10x";
            button.GetComponent<Button>().onClick.AddListener(() => SellItem(item, 10));
        }
        if (item.quantity >= 100)
        {
            GameObject button = Instantiate(SellButton, ItemInfoPanelButtons.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Sell\n100x";
            button.GetComponent<Button>().onClick.AddListener(() => SellItem(item, 100));
        }
        GameObject button2 = Instantiate(SellButton, ItemInfoPanelButtons.transform);
        button2.GetComponentInChildren<TextMeshProUGUI>().text = "Sell\n" + item.quantity + "x";
        button2.GetComponent<Button>().onClick.AddListener(() => SellItem(item, item.quantity));
    }

    private void SellItem(CollectableItem item, int v)
    {
        inventory.Money += item.itemData.Worth * v;
        inventory.addMoneyInfoPanel(item.itemData.Worth * v);
        inventory.RemoveItem(item.itemData, v);

        ClearItemInfoPanel();
        if (item.quantity > 0)
            CreateItemInfoPanel(item.itemData, ItemInfoPanel.transform.position);
        else
        {
            ItemInfoPanel.SetActive(false);
            CreateShopItems();
        }
        UpdateMoneyText();
    }

    private void ClearItemInfoPanel()
    {
        foreach (Transform child in ItemInfoPanelButtons.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ClearShopItems()
    {
        foreach (Transform child in ShopPanel.transform)
        {
            if (child.gameObject.CompareTag("ShopItem"))
                Destroy(child.gameObject);
        }
    }

    public void UpdateMoneyText()
    {
        MoneyText.text = "Money: " + inventory.Money;
    }
}