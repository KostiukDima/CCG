using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoSrp : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public Text Name;
    public GameObject HideObj;

    public void HideCardInfo(Card card)
    {
        SelfCard = card;
        HideObj.SetActive(true);
    }

    public void ShowCardInfo(Card card)
    {
        SelfCard = card;
        HideObj.SetActive(false);
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
    }

    private void Start()
    {
       // ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}
