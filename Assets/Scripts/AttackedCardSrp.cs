using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCardSrp : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (!GetComponent<CardDragSrp>().GameManager.IsPlayerTurn)
            return;

        CardInfoSrp card = eventData.pointerDrag.GetComponent<CardInfoSrp>();
        
        if(card && card.SelfCard.CanAttack && 
            transform.parent == GetComponent<CardDragSrp>().GameManager.EnemyFirstField ||
            transform.parent == GetComponent<CardDragSrp>().GameManager.EnemySecondField)
        {
            card.SelfCard.ChangeAttackState(true);
            card.HideCanAttack();
            GetComponent<CardDragSrp>().GameManager.CardsFight(card, GetComponent<CardInfoSrp>());
        }
    }    
}
