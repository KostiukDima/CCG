using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardDragSrp : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera MainCamera;
    Vector3 offset;
    public Transform defaultParrent, defaultTempCardParrent;
    GameObject tempCard;
    public bool IsDraggable;
    public GameManagerSrp GameManager;


    private void Awake()
    {
        MainCamera = Camera.allCameras[0];
        tempCard = GameObject.Find("TempCard");
        GameManager = FindObjectOfType<GameManagerSrp>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
        defaultParrent = transform.parent;
        defaultTempCardParrent = transform.parent;

        IsDraggable = defaultParrent.GetComponent<DropPlaceSrp>().Type == FieldType.PlayerHandLine && GameManager.IsPlayerTurn && !GameManager.PlayerMovedCardToField;

        if((defaultParrent.GetComponent<DropPlaceSrp>().Type == FieldType.PlayerFirstGameLine ||
           defaultParrent.GetComponent<DropPlaceSrp>().Type == FieldType.PlayerSecondGameLine) && GameManager.IsPlayerTurn)
        {
            IsDraggable = GetComponent<CardInfoSrp>().SelfCard.CanAttack;
        }

        if (!IsDraggable)
            return;

        if(GetComponent<CardInfoSrp>().SelfCard.CanAttack)
        GameManager.HighlighTargets(true, GetComponent<CardInfoSrp>());

        tempCard.transform.SetParent(defaultParrent);       
        tempCard.transform.SetSiblingIndex(transform.GetSiblingIndex());       

        transform.SetParent(defaultParrent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDraggable)
            return;

        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;

        if (tempCard.transform.parent != defaultTempCardParrent)
            tempCard.transform.SetParent(defaultTempCardParrent);
        
        if(defaultParrent.GetComponent<DropPlaceSrp>().Type == FieldType.PlayerHandLine)
            CheckPositions();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsDraggable)
            return;

        GameManager.HighlighTargets(false);

        transform.SetParent(defaultParrent);
        transform.SetSiblingIndex(tempCard.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        tempCard.transform.SetParent(GameObject.Find("Lines").transform);
        tempCard.transform.localPosition = new Vector3(2000, 0, 0);

        GameManager.RefreshGameScore();
    }

    void CheckPositions()
    {
        int newIndex = defaultTempCardParrent.childCount;

        for (int i = 0; i < defaultTempCardParrent.childCount; i++)
        {
            if(transform.position.x < defaultTempCardParrent.GetChild(i).position.x)
            {
                newIndex = i;

                if (tempCard.transform.GetSiblingIndex() < newIndex)
                    newIndex--;

                break;  
            }
        }

        tempCard.transform.SetSiblingIndex(newIndex);
    }

    public void MoveToField(Transform field)
    {
        transform.SetParent(GameObject.Find("Lines").transform);
        transform.DOMove(field.position, 0.5f);
    }

    public void MoveToTarget(Transform target)
    {
        StartCoroutine(MoveToTargetCor(target));
    }

    IEnumerator MoveToTargetCor(Transform target)
    {
        Vector3 pos = transform.position;
        Transform parent = transform.parent;
        int index = transform.GetSiblingIndex();

        transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = false;

        transform.SetParent(GameObject.Find("Lines").transform);

        transform.DOMove(target.position, 0.25f);

        yield return new WaitForSeconds(0.25f);

        transform.DOMove(pos, 0.25f);

        yield return new WaitForSeconds(0.25f);

        transform.SetParent(parent);
        transform.SetSiblingIndex(index);

        transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = true;

    }
}
