using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public int damage;
    public DamageTypes damageType;
    public GameObject abilityPrefab;
    public float cooldown;
    public float resourceCost;
}
