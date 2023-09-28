using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class LootGenerator : MonoBehaviour
{
    public LootTable lootTable;
    public List<Sprite> icons = new List<Sprite>();
    public List<Weapon> baseWeapons;
    public List<Armor> baseArmors;
    public List<Jewellery> baseJewelleries;
    public AffixTable jewelleryAffixTable, armorAffixTable, trinketAffixTable, weaponAffixTable;
    int maxLevel = 50;
    int numTiers = 7;



    #region Dictionaries

    private Dictionary<ItemRarity, int> rarityToAffixes = new Dictionary<ItemRarity, int>
    {
    { ItemRarity.Normal, 0 },
    { ItemRarity.Magic, 1 },
    { ItemRarity.Rare, 2 },
    { ItemRarity.Epic, 3 }
    };
    #endregion


    private void Awake()
    {


    }

    public List<Equipment> GenerateLoot(int enemyLevel, EnemyRarity enemyRarity)
    {
        int dropCount = GetDropCount(enemyRarity);

        List<Equipment> loot = new List<Equipment>();

        for (int i = 0; i < dropCount; i++)
        {
            float randomValue = UnityEngine.Random.Range(0, lootTable.totalDropRate);
            float currentDropRate = 0;
            Equipment newItem = null;

            foreach (var item in lootTable.items)
            {
                currentDropRate += item.dropRate;
                if (randomValue <= currentDropRate)
                {
                    newItem = ChooseCategory();
                    newItem.itemLevel = enemyLevel;
                    newItem.itemName = newItem.itemSlot.ToString();
                    // RollBaseItem(newItem);
                    // GetIcon(newItem);
                    // RollBaseStats(newItem);
                    // RollRarity(newItem, enemyRarity);
                    // int prefixAmount = rarityToAffixes[newItem.rarity];
                    // AffixTable chosenAffixTable = ChooseAffixTable(newItem);
                    // AddAffixes(newItem, prefixAmount, prefixAmount, chosenAffixTable);
                    // break;
                }
            }

            if (newItem != null)
            {
                loot.Add(newItem);
            }
        }

        return loot;
    }

    private Equipment ChooseCategory()
    {
        float roll = UnityEngine.Random.Range(0f, 1f);  // Generate a random float between 0 and 1

        Equipment newEquipment = null;
        ItemSlot[] allSlots = (ItemSlot[])System.Enum.GetValues(typeof(ItemSlot));


        if (roll < 0.5f)  // 50% chance
        {
            newEquipment = ScriptableObject.CreateInstance<Weapon>();
            newEquipment.itemSlot = allSlots[UnityEngine.Random.Range(8, 10)];
            // Set properties for Weapon...
        }
        else if (roll < 0.9f)  // 40% chance
        {
            newEquipment = ScriptableObject.CreateInstance<Armor>();
            newEquipment.itemSlot = allSlots[UnityEngine.Random.Range(0, 7)];
            // Set properties for Armor...
        }
        else  // 10% chance
        {
            newEquipment = ScriptableObject.CreateInstance<Jewellery>();
            newEquipment.itemSlot = allSlots[UnityEngine.Random.Range(10, 13)];
            // Set properties for Jewellery...
        }

        return newEquipment;
    }

    // private void RollBaseItem(Equipment item)
    // {
    //     if (item.itemSlot == ItemSlot.MainHand)
    //     {
    //         item.weaponType = availableWeaponTypes[UnityEngine.Random.Range(0, availableWeaponTypes.Count)];
    //     }
    //     else if (item.itemSlot != ItemSlot.OffHand && item.itemSlot != ItemSlot.Ring && item.itemSlot != ItemSlot.Amulet && item.itemSlot != ItemSlot.Trinket)
    //     {
    //         Array values = Enum.GetValues(typeof(ItemMaterial));
    //         item.material = (ItemMaterial)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    //     }
    // }

    // private void GetIcon(Equipment item)
    // {
    //     int index = (int)item.itemSlot;

    //     if (index >= 0 && index < icons.Count)
    //     {
    //         item.itemSprite = icons[index];
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Unhandled item slot: " + item.itemSlot);
    //     }
    // }

    // private void RollBaseStats(Equipment item)
    // {

    //     if (!item.isWeapon && item.itemSlot != ItemSlot.OffHand && item.itemSlot != ItemSlot.Ring && item.itemSlot != ItemSlot.Amulet && item.itemSlot != ItemSlot.Trinket)
    //     {
    //         item.armor = (UnityEngine.Random.Range(4, 9) * (1 + item.itemLevel / 6));
    //         switch (item.material)
    //         {
    //             case ItemMaterial.Cloth:
    //                 item.armor = item.armor * 1;
    //                 break;
    //             case ItemMaterial.Leather:
    //                 item.armor = (int)(item.armor * 1.4f);
    //                 break;
    //             case ItemMaterial.Mail:
    //                 item.armor = (int)(item.armor * 1.8f);
    //                 break;
    //             case ItemMaterial.Plate:
    //                 item.armor = (int)(item.armor * 2.4f);
    //                 break;
    //             default:
    //                 break;
    //         }

    //         item.baseStat = (BaseStats)UnityEngine.Random.Range(0, BaseStats.GetValues(typeof(BaseStats)).Length);
    //         item.baseStatValue = (int)(UnityEngine.Random.Range(3f, 6f) * (1 + item.itemLevel / 12f));
    //         item.stamina = (int)(UnityEngine.Random.Range(3f, 6f) * (1 + item.itemLevel / 12f));
    //     }

    //     if (item.itemSlot == ItemSlot.OffHand)
    //     {
    //         item.armor = (UnityEngine.Random.Range(1, 11) * (1 + item.itemLevel / 6)) * 4;
    //     }

    // }

    // private void RollRarity(Equipment item, EnemyRarity enemyRarity)
    // {
    //     List<float> weights;

    //     switch (enemyRarity)
    //     {
    //         case EnemyRarity.Normal:
    //             weights = new List<float> { 90, 5, 1, .01f }; // Adjust these values as needed for normal enemies
    //             break;
    //         case EnemyRarity.Elite:
    //             weights = new List<float> { 40, 40, 15, 0 }; // Adjust these values as needed for elite enemies
    //             break;
    //         case EnemyRarity.Rare:
    //             weights = new List<float> { 0, 40, 95, .5f }; // Adjust these values as needed for rare enemies
    //             break;
    //         case EnemyRarity.Boss:
    //             weights = new List<float> { 0, 0, 95, 5 }; // Adjust these values as needed for boss enemies
    //             break;
    //         default:
    //             weights = new List<float> { 90, 5, 1, .01f }; // Default values, you can adjust them as needed
    //             break;
    //     }

    //     item.rarity = GetRandomRarity(weights);
    // }


    // private ItemRarity GetRandomRarity(List<float> weights)
    // {
    //     float totalWeight = 0;
    //     foreach (float weight in weights)
    //     {
    //         totalWeight += weight;
    //     }

    //     float randomValue = UnityEngine.Random.Range(0, totalWeight);
    //     float currentWeight = 0;

    //     for (int i = 0; i < weights.Count; i++)
    //     {
    //         currentWeight += weights[i];
    //         if (randomValue < currentWeight)
    //         {
    //             return (ItemRarity)i;
    //         }
    //     }

    //     return (ItemRarity)(weights.Count - 1); // Default to the last rarity in the list
    // }


    // private AffixTable ChooseAffixTable(Equipment item)
    // {
    //     switch (item.itemSlot)
    //     {
    //         case ItemSlot.MainHand:
    //         case ItemSlot.OffHand:
    //             return weaponAffixTable;
    //         case ItemSlot.Helmet:
    //         case ItemSlot.Shoulders:
    //         case ItemSlot.Chest:
    //         case ItemSlot.Gloves:
    //         case ItemSlot.Legs:
    //         case ItemSlot.Boots:
    //         case ItemSlot.Belt:
    //             return armorAffixTable;
    //         case ItemSlot.Ring:
    //         case ItemSlot.Amulet:
    //         case ItemSlot.Trinket:
    //             return jewelleryAffixTable;
    //         default:
    //             return null;
    //     }
    // }

    // private int CalculateTier(Equipment item)
    // {
    //     // Determine the minimum and maximum tier that can be rolled
    //     int minTier = Mathf.Max(1, item.itemLevel / (maxLevel / numTiers) - 1);
    //     int maxTier;

    //     if (item.itemLevel >= maxLevel)
    //     {
    //         maxTier = numTiers;
    //     }
    //     else
    //     {
    //         maxTier = Mathf.Min(item.itemLevel / (maxLevel / numTiers) + 1, numTiers - 1);
    //     }

    //     // Roll a random tier within the determined range
    //     int tier = UnityEngine.Random.Range(minTier, maxTier + 1);

    //     return tier;
    // }


    // private void AddAffixes(Equipment item, int prefixes, int suffixes, AffixTable affixTable)
    // {
    //     if (item.rarity == ItemRarity.Normal || item == null)
    //     {
    //         return;
    //     }

    //     // Add one random prefix from each group to the base prefixes list

    //     int randomAddedDamageIndex = UnityEngine.Random.Range(0, affixTable.addedDamagePrefixes.Count);
    //     int randomDamageOverTimeIndex = UnityEngine.Random.Range(0, affixTable.damageOverTimePrefixes.Count);
    //     int randomPenetrationIndex = UnityEngine.Random.Range(0, affixTable.penetrationPrefixes.Count);

    //     int randomBaseStatIndex = UnityEngine.Random.Range(0, affixTable.baseStatSuffixes.Count);

    //     List<Stat> combinedPrefixes = new List<Stat>(affixTable.prefixes);
    //     if (affixTable.addedDamagePrefixes.Count > 0 && affixTable.damageOverTimePrefixes.Count > 0)
    //     {
    //         combinedPrefixes.Add(affixTable.addedDamagePrefixes[randomAddedDamageIndex]);
    //         combinedPrefixes.Add(affixTable.damageOverTimePrefixes[randomDamageOverTimeIndex]);
    //     }
    //     List<Stat> combinedSuffixes = new List<Stat>(affixTable.suffixes);
    //     combinedSuffixes.Add(affixTable.baseStatSuffixes[randomBaseStatIndex]);
    //     combinedPrefixes.Add(affixTable.penetrationPrefixes[randomPenetrationIndex]);

    //     HashSet<int> usedPrefixIndices = new HashSet<int>();
    //     for (int i = 0; i < prefixes; i++)
    //     {
    //         int index;
    //         do
    //         {
    //             index = UnityEngine.Random.Range(0, combinedPrefixes.Count);
    //         } while (usedPrefixIndices.Contains(index));
    //         usedPrefixIndices.Add(index);

    //         Stat stat = combinedPrefixes[index];
    //         int tier = CalculateTier(item);
    //         Stat newStat = ScriptableObject.CreateInstance<Stat>();
    //         newStat = stat;
    //         newStat.currentValue = UnityEngine.Random.Range(stat.tiers[tier].minValue, stat.tiers[tier].maxValue + 1);
    //         item.prefixes.Add(newStat);
    //     }

    //     HashSet<int> usedSuffixIndices = new HashSet<int>();
    //     for (int i = 0; i < suffixes; i++)
    //     {
    //         int index;
    //         do
    //         {
    //             index = UnityEngine.Random.Range(0, combinedSuffixes.Count);
    //         } while (usedSuffixIndices.Contains(index));
    //         usedSuffixIndices.Add(index);

    //         Stat stat = combinedSuffixes[index];
    //         int tier = CalculateTier(item);
    //         Stat newStat = ScriptableObject.CreateInstance<Stat>();
    //         newStat = stat;
    //         newStat.currentValue = UnityEngine.Random.Range(stat.tiers[tier].minValue, stat.tiers[tier].maxValue + 1);
    //         item.suffixes.Add(newStat);
    //     }
    // }



    private int GetDropCount(EnemyRarity enemyRarity)
    {
        int dropCount;
        float randomValue = UnityEngine.Random.Range(0f, 1f);

        switch (enemyRarity)
        {
            case EnemyRarity.Normal:
                dropCount = randomValue < 0.05f ? 1 : 0; // 5% chance to drop 1 item
                break;
            case EnemyRarity.Elite:
                dropCount = randomValue < 0.5f ? 1 : 0; // 50% chance to drop 1 item
                break;
            case EnemyRarity.Rare:
                dropCount = 1; // Always drop 1 item
                break;
            case EnemyRarity.Boss:
                dropCount = 1 + (randomValue < 0.5f ? 1 : 0); // Drop 1 item and 50% chance for an extra item
                break;
            default:
                dropCount = 0;
                break;
        }

        return dropCount;
    }




}

