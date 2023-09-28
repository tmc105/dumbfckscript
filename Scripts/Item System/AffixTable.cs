using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AffixTable", menuName = "Loot System/AffixTable", order = 3)]
public class AffixTable : ScriptableObject
{
    // This list will hold the affixes available in the affix table
    // You need to add affixes to this list using the Unity editor, specifying the name, type (prefix/suffix),
    // and the tiers with their respective stats and values
    public List<Stat> addedDamagePrefixes;
    public List<Stat> penetrationPrefixes;
    public List<Stat> damageOverTimePrefixes;
    public List<Stat> baseStatSuffixes;
    public List<Stat> prefixes;
    public List<Stat> suffixes;
}



