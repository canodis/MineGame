using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItem", menuName = "ScriptableObjects/CollectableItem", order = 1)]
public class CollectableItemSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField] public int Worth { get; private set; }

    [field: SerializeField] public int Rarity { get; private set; }
}