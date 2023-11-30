using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour , IPointerClickHandler
{
    public CollectableItemSO itemData;
    private ShopUICreator creator;

    void Start()
    {
        creator = GameObject.FindGameObjectWithTag("UICreator").GetComponent<ShopUICreator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            creator.CreateItemInfoPanel(itemData, eventData.position);
        }
    }
}   