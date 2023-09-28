using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TooltipUI tooltipPopup;
    public Item item;
    public bool isMouseOver = false;


    // private void Update()
    // {
    //     if (EventSystem.current.IsPointerOverGameObject() && PointerOverThisGameObject())
    //     {
    //         tooltipPopup.DisplayInfo(item);

    //     }
    //     else
    //     {
    //         tooltipPopup.HideInfo();
    //     }
    // }

    // private bool PointerOverThisGameObject()
    // {
    //     PointerEventData pointerData = new PointerEventData(EventSystem.current)
    //     {
    //         position = Input.mousePosition
    //     };

    //     List<RaycastResult> results = new List<RaycastResult>();
    //     EventSystem.current.RaycastAll(pointerData, results);

    //     return results.Exists(result => result.gameObject == gameObject);
    // }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipPopup != null)
        {
            isMouseOver = true;
            tooltipPopup.DisplayInfo(item);
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipPopup != null)
        {
            isMouseOver = false;
            tooltipPopup.HideInfo();
        }
    }

}

