using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;
    public StatSheetUI statSheetUI;

    [SerializeField] PlayerStats playerStats;
    private void Awake()
    {
        Instance = this;
    }
    public List<GameObject> gameObjectList = new List<GameObject>();

    Equipment[] currentEquipment;
    Inventory inventory;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(ItemSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.itemSlot;
        gameObjectList[slotIndex].GetComponent<Image>().sprite = newItem.itemSprite;
        gameObjectList[slotIndex].GetComponent<InventoryTooltip>().item = newItem;
        Equipment oldItem = Unequip(slotIndex);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        ApplyEquipmentStatChanges(newItem, true);
        statSheetUI.UpdateStatSheetText();
        currentEquipment[slotIndex] = newItem;
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, currentEquipment[slotIndex]);
            }

            Equipment oldItem = currentEquipment[slotIndex];
            ApplyEquipmentStatChanges(oldItem, false);
            statSheetUI.UpdateStatSheetText();
            inventory.AddItem(oldItem);
            currentEquipment[slotIndex] = null;
            return oldItem;
        }

        return null;
    }

    public void ApplyEquipmentStatChanges(Equipment equipment, bool addStats)
    {
        if (equipment == null || playerStats == null)
        {
            return;
        }

        List<Stat> allStats = new List<Stat>(equipment.prefixes);
        allStats.AddRange(equipment.suffixes);

        // Modify stats
        foreach (Stat stat in allStats)
        {
            Stat playerStat = playerStats.playerStats.Find(s => s.statName == stat.statName);
            if (playerStat != null)
            {
                playerStat.currentValue += addStats ? stat.currentValue : -stat.currentValue;
            }
        }

        // Modify armor
        Armor armor = equipment as Armor;
        playerStats.armor += addStats ? armor.armor : -armor.armor;


        playerStats.baseStatValue += addStats ? armor.baseStatValue : -armor.baseStatValue;


        // Modify stamina
        playerStats.stamina += addStats ? armor.stamina : -armor.stamina;
    }

}

