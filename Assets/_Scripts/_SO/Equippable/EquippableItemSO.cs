using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EquippableItem", menuName = "ScriptableObjects/EquippableItem", order = 1)]
public class EquippableItemSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField] public int UpgradePrice { get; private set; }

    [field: SerializeField] public int ToolLevel { get; private set; }

    [field: SerializeField] public Data.ToolType toolType { get; private set; }
}