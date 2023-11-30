using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeItem : MonoBehaviour, IPointerClickHandler
{
    public EquippableItemSO itemData;
    private UpgradeItemCreator creator;

    void Start()
    {
        creator = GameObject.FindGameObjectWithTag("UpgradeCreator").GetComponent<UpgradeItemCreator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        creator.OpenUpgradePanel(itemData);
    }
}