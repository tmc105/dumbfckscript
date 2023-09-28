using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{

    public string itemName;
    public Sprite itemSprite;
    public string itemDescription;
    public int itemLevel;


    // Add any other properties you want items to have
    public virtual void Use()
    {
        // Use the item
        // Something might happen
    }

    public virtual void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }

    public abstract string GetTooltipInfoText();

}

