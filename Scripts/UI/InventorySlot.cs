using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public Image icon;
    public InventoryTooltip inventoryTooltip;
    public Button removeButton;

    private void Awake()
    {
        inventoryTooltip.item = item;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        inventoryTooltip.item = newItem;
        icon.sprite = item.itemSprite;
        icon.enabled = true;
        removeButton.interactable = true;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItem(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

}
