using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoSrp : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public Text Name;
    public Text Attack;
    public Text Armor;
    public Text Power;
    public GameObject HideObj;
    public GameObject CanAttackObj;
    public GameObject AttackTargetObj;

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
        Attack.text = card.Attack.ToString();
        if (card.Attack == 0)
            Attack.gameObject.SetActive(false);
        Armor.text = card.Armor.ToString();
        if (card.Armor == 0)
            Armor.gameObject.SetActive(false);
        Power.text = card.Power.ToString();
    }

    private void Start()
    {
       
    }

    public void ShowCanAttack()
    {
        CanAttackObj.SetActive(true);
    }
    public void HideCanAttack()
    {
        if(CanAttackObj)
            CanAttackObj.SetActive(false);
    }

    public void RefreshData()
    {
        Armor.text = SelfCard.Armor.ToString();
        if (SelfCard.Armor == 0)
            Armor.gameObject.SetActive(false);
        Power.text = SelfCard.Power.ToString();
    }

    public void HighlightAsTarget (bool highlight)
    {
        AttackTargetObj.SetActive(highlight);
    }
}
