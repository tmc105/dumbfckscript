using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Loot System/LootTable", order = 2)]
public class LootTable : ScriptableObject
{
    // This list will hold the items available in the loot table, along with their drop rates
    // You need to add items to this list using the Unity editor, specifying the drop rate for each item
    public List<ItemDrop> items;

    // The total drop rate is calculated as the sum of the individual drop rates of all items
    [HideInInspector]
    public float totalDropRate;

    private void OnValidate()
    {
        UpdateTotalDropRate();
    }

    private void UpdateTotalDropRate()
    {
        totalDropRate = 0;
        foreach (var itemDrop in items)
        {
            totalDropRate += itemDrop.dropRate;
        }
    }
}

[System.Serializable]
public class ItemDrop
{
    public Item item;
    public float dropRate;
}

