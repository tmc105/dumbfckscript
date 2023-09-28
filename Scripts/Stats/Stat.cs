using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Loot System/Stat", order = 1)]
public class Stat : ScriptableObject
{
    public string statName;
    public string statUnit;
    public List<Tier> tiers;
    public int currentValue;
    public float weight;

    // Add any other properties you want the stat to have
}

[System.Serializable]
public class Tier
{
    public int minValue;
    public int maxValue;
}




