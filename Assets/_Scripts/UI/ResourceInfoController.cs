using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceInfoController : MonoBehaviour
{
    [SerializeField] private GameObject ResourceInfoPanel;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Durability;

    public float showTime;

    void Start()
    {
        ResourceInfoPanel.SetActive(false);
        InvokeRepeating("AutoDeactivator", 0f, 5);
    }

    private void AutoDeactivator()
    {
        if (showTime <= 0)
            HideResourceInfoPanel();
        showTime -= 5;
    }

    public void ShowResourceInfoPanel(Resource resource)
    {
        ResourceInfoPanel.SetActive(true);
        Name.text = resource.itemData.Name;
        Durability.text = "Durability : " + resource.getHealth();
        showTime = 5f;
    }

    public void UpdateResourceInfo(Resource resource)
    {
        Durability.text = "Durability : " + resource.getHealth();
        showTime = 5f;
    }

    public void HideResourceInfoPanel()
    {
        ResourceInfoPanel.SetActive(false);
    }

    private IEnumerator<WaitForSeconds> WaitAndDeactivate(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
