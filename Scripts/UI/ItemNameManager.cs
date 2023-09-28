using UnityEngine;
using TMPro;

public class ItemNameManager : MonoBehaviour
{
    public static ItemNameManager Instance;
    public TooltipUI popupCanvas;

    [SerializeField] private GameObject itemNamePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject CreateItemNameText(Transform itemTransform, Item item)
    {
        GameObject itemNameObject = Instantiate(itemNamePrefab, transform);
        itemNameObject.GetComponent<InventoryTooltip>().tooltipPopup = popupCanvas;
        itemNameObject.GetComponent<InventoryTooltip>().item = item;
        return itemNameObject;
    }


}
