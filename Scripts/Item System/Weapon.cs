using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Weapon : Equipment
{

    public Vector2 damageRange;
    public float attackSpeed;
    public DamageTypes damageType;

    public float GetDPS()
    {
        return (damageRange.x + damageRange.y) / 2 * attackSpeed;
    }

    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        string rarityColor = GetRarityColor(rarity);

        builder.Append("<size=1.2em><color=").Append(rarityColor).Append(">")
               .Append(itemSlot).Append("</color></size>").AppendLine();

        builder.Append("<size=0.8em>").Append(rarity).Append(" ");


        builder.Append("<size=0.8em>").Append(weaponType.weaponType).AppendLine();
        builder.AppendLine();
        builder.Append("Damage: ").Append(weaponType.damageRange.x).Append(" - ").Append(weaponType.damageRange.y).AppendLine();
        builder.Append("Attack Speed: ").Append(weaponType.attackSpeed).AppendLine();
        builder.Append("DPS: ").Append(weaponType.GetDPS()).AppendLine();


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