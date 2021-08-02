using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragSrp : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera MainCamera;
    Vector3 offset;
    public Transform defaultParrent, defaultTempCardParrent;
    GameObject tempCard;

    private void Awake()
    {
        MainCamera = Camera.allCameras[0];
        tempCard = GameObject.Find("TempCard");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
        defaultParrent = transform.parent;
        defaultTempCardParrent = transform.parent;

        tempCard.transform.SetParent(defaultParrent);       
        tempCard.transform.SetSiblingIndex(transform.GetSiblingIndex());       

        transform.SetParent(defaultParrent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;

        if (tempCard.transform.parent != defaultTempCardParrent)
            tempCard.transform.SetParent(defaultTempCardParrent);

        CheckPositions();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(defaultParrent);
        transform.SetSiblingIndex(tempCard.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        tempCard.transform.SetParent(GameObject.Find("Lines").transform);
        tempCard.transform.localPosition = new Vector3(2000, 0, 0);
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
}
