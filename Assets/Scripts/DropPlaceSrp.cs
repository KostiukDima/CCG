using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlaceSrp : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardDragSrp card = eventData.pointerDrag.GetComponent<CardDragSrp>();

        if(card)
            card.defaultParrent = transform;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        CardDragSrp card = eventData.pointerDrag.GetComponent<CardDragSrp>();

        if (card)
            card.defaultTempCardParrent = transform;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        CardDragSrp card = eventData.pointerDrag.GetComponent<CardDragSrp>();

        if (card && card.defaultTempCardParrent == transform)
            card.defaultTempCardParrent = card.defaultParrent;
    }
}
