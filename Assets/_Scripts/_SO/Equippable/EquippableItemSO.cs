using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EquippableItem", menuName = "ScriptableObjects/EquippableItem", order = 1)]
public class EquippableItemSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField] public int[] UpgradePrices { get; private set; }

    [field: SerializeField] public int EvolvePrice { get; private set; }

    public int MaxLevel => UpgradePrices.Length;

    [field: SerializeField] public int ToolRank { get; private set; }

    [field: SerializeField] public Data.ToolType toolType { get; private set; }
}