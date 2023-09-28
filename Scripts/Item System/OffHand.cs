using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class OffHand : Equipment
{

    public OffHandType offHandType;
    public int armor;

    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        string rarityColor = GetRarityColor(rarity);

        builder.Append("<size=1.2em><color=").Append(rarityColor).Append(">")
               .Append(itemSlot).Append("</color></size>").AppendLine();

        builder.Append("<size=0.8em>").Append(rarity).Append(" ");
        if (offHandType == OffHandType.Shield)
        {
            builder.Append("Armor: ").Append(armor).AppendLine();
            builder.AppendLine();
        }

        builder.AppendLine();
        builder.Append("<color=#009DF5>");
        foreach (Stat prefix in prefixes)
        {
            if (prefix.currentValue > 0)
                builder.AppendLine(prefix.statName + ": " + prefix.currentValue + prefix.statUnit);
        }

        foreach (Stat suffix in suffixes)
        {
            if (suffix.currentValue > 0)
                builder.AppendLine(suffix.statName + ": " + suffix.currentValue + suffix.statUnit);
        }
        builder.Append("</color></size>");

        return builder.ToString();
    }
}