using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    PlayerHandLine,
    PlayerFirstGameLine,
    PlayerSecondGameLine,
    EnemySecondGameLine,
    EnemyFirstGameLine,
    EnemyHandLine,
}

public class DropPlaceSrp : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public FieldType Type;
    public void OnDrop(PointerEventData eventData)
    {
        if (Type != FieldType.PlayerFirstGameLine &&
            Type != FieldType.PlayerSecondGameLine)
            return;

        CardDragSrp card = eventData.pointerDrag.GetComponent<CardDragSrp>();

        if (card.GameManager.PlayerMovedCardToField)
            return;

        if (card.defaultParrent.GetComponent<DropPlaceSrp>().Type == FieldType.PlayerFirstGameLine ||
            card.defaultParrent.GetComponent<DropPlaceSrp>().Type == FieldType.PlayerSecondGameLine)
            return;

        if (card && card.GameManager.IsPlayerTurn)
        {
            card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoSrp>());
            card.GameManager.PlayerFieldCards.Add(card.GetComponent<CardInfoSrp>());
            card.GameManager.PlayerMovedCardToField = true;
            card.GameManager.ButtonEndTurnTxt.text = "????????? ???";
            card.defaultParrent = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null ||
            Type == FieldType.EnemyHandLine ||
            Type == FieldType.EnemyFirstGameLine ||
            Type == FieldType.EnemySecondGameLine ||
            Type == FieldType.PlayerHandLine)
            return;

        CardDragSrp card = eventData.pointerDrag.GetComponent<CardDragSrp>();

        if (card)
        {
            if (card.defaultParrent.GetComponent<DropPlaceSrp>().Type == FieldType.PlayerFirstGameLine ||
              card.defaultParrent.GetComponent<DropPlaceSrp>().Type == FieldType.PlayerSecondGameLine)
                return;

            card.defaultTempCardParrent = transform;
        }
        
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
