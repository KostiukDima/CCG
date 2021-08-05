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
        Armor.text = card.Armor.ToString();
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
        CanAttackObj.SetActive(false);
    }

    public void RefreshData()
    {
        Armor.text = SelfCard.Armor.ToString();
        Power.text = SelfCard.Power.ToString();
    }
}
