using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoSrp : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public Text Name;

    public void ShowCardInfo(Card card)
    {
        SelfCard = card;
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
    }

    private void Start()
    {
        ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}
