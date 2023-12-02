using System;
using UnityEngine;
using UnityEngine.UI;

public class Anvil : MonoBehaviour
{
    public UpgradeItemCreator creator;
    private IconCreator iconCreator;

    void Start()
    {
        iconCreator = GetComponent<IconCreator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            iconCreator.CreateIconButton(ButtonClickFunction);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            creator.CloseUpgradeMenu();
            iconCreator.RemoveIconButton();
        }
    }
    private void ButtonClickFunction()
    {
        creator.OpenUpgradeMenu();
    }
}