using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class PickupItem : Interactable
{
    public Item item;
    public Equipment equipment;
    public GameObject itemNameText;
    private Camera mainCamera;
    [SerializeField] private float nameplateOffset = 2f;

    private void Start()
    {
        itemNameText = ItemNameManager.Instance.CreateItemNameText(transform, item);
        mainCamera = Camera.main;
        equipment = item as Equipment;
        itemNameText.GetComponentInChildren<TextMeshProUGUI>().text = "<color=" + equipment.GetRarityColor(equipment.rarity) + ">" + item.itemName + "</color>";


        EventTrigger eventTrigger = itemNameText.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { Interact(); });
        eventTrigger.triggers.Add(entry);
    }

    private void Update()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position + Vector3.up * nameplateOffset);
        itemNameText.transform.position = screenPos;

    }

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        bool wasPickedUp = Inventory.instance.AddItem(item);

        if (wasPickedUp)
        {
            gameObject.GetComponent<InventoryTooltip>().tooltipPopup.HideInfo();
            Destroy(itemNameText.gameObject);
            Destroy(gameObject);
        }
    }
}



