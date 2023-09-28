using UnityEngine;

[CreateAssetMenu(fileName = "WeaponType", menuName = "Items/WeaponType", order = 1)]
public class WeaponType : ScriptableObject
{
    public WeaponTypes weaponType;
    public Vector2 damageRange; // The min and max damage values
    public float attackSpeed;

    public float GetDPS()
    {
        return (damageRange.x + damageRange.y) / 2 * attackSpeed;
    }
}