using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "Equipment", menuName = "Loot System/Equipment", order = 0)]
public class Equipment : Item
{
    public ItemRarity rarity;
    public List<Stat> prefixes;
    public List<Stat> suffixes;
    public ItemSlot itemSlot;

    public Equipment()
    {
        prefixes = new List<Stat>();
        suffixes = new List<Stat>();
    }

    public override void Use()
    {
        base.Use();
        EquipmentManager.Instance.Equip(this);
        RemoveFromInventory();
    }

    public override string GetTooltipInfoText()
    {
        return "";
    }


    public string GetRarityColor(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Normal:
                return "#FFFFFF";
            case ItemRarity.Magic:
                return "#3380FF";
            case ItemRarity.Rare:
                return "#FFF033";
            case ItemRarity.Epic:
                return "#D433FF";
            default:
                return "#FFFFFF";
        }
    }

    // Add any other properties you want items to have
}







