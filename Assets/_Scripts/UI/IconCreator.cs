using System;
using UnityEngine;
using UnityEngine.UI;

public class IconCreator : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform iconTransform;
    [SerializeField] private float iconOffset;
    private GameObject prefabInstance;

    private bool isIconActive = false;

    public void CreateIconButton(Action ButtonClickFunction)
    {
        prefabInstance = Instantiate(iconPrefab, iconTransform.position, Quaternion.identity);
        prefabInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        prefabInstance.GetComponent<Button>().onClick.AddListener(() =>
        {
            ButtonClickFunction();
            RemoveIconButton();
        });
        isIconActive = true;
    }

    void Update()
    {
        if (isIconActive)
        {
            prefabInstance.transform.position = Camera.main.WorldToScreenPoint(iconTransform.position + new Vector3(0, iconOffset, 0));
        }
    }

    public void RemoveIconButton()
    {
        if (prefabInstance != null)
        {
            Destroy(prefabInstance);
        }
        isIconActive = false;
    }
}