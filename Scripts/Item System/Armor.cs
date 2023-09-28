using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Armor : Equipment
{
    public int armor;
    public BaseStats baseStat;
    public int baseStatValue;
    public int stamina;
    public ItemMaterial material; // Renamed from itemMaterial for consistency

    public override string GetTooltipInfoText()
    {
        string rarityColor = GetRarityColor(rarity);

        StringBuilder builder = new StringBuilder()
            .AppendFormat("<size=1.2em><color={0}>{1}</color></size>\n", rarityColor, itemSlot)
            .AppendFormat("<size=0.8em>{0} {1}\n\n", rarity, material)
            .AppendFormat("Armor: {0}\n\n", armor)
            .AppendFormat("{0}: {1}\n", baseStat, baseStatValue)
            .AppendFormat("Stamina: {0}\n\n<color=#009DF5>", stamina);

        foreach (Stat prefix in prefixes)
        {
            if (prefix.currentValue > 0)
                builder.AppendFormat("{0}: {1}{2}\n", prefix.statName, prefix.currentValue, prefix.statUnit);
        }

        foreach (Stat suffix in suffixes)
        {
            if (suffix.currentValue > 0)
                builder.AppendFormat("{0}: {1}{2}\n", suffix.statName, suffix.currentValue, suffix.statUnit);
        }

        builder.Append("</color></size>");

        return builder.ToString();
    }
}
