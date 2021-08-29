using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public enum CardType
    {
        Infantry,
        Cavalry,
        Artillery,
        Fortification
    }

    public string Name;
    public Sprite Logo;
    public int Attack;
    public int Armor;
    public int Power;
    public int TurnToAttack;
    public int TurnPassed;
    public bool CanAttack;
    public CardType Type;

    public bool IsAlive
    {
        get { return Power > 0; }
    }

    public Card(string name, string logoPath, int attack, int armor, int power, int turnToAttack, CardType type)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack; 
        Armor = armor;
        Power = power;
        TurnToAttack = turnToAttack;
        TurnPassed = 0;
        CanAttack = false;
        Type = type;
    }

    public void ChangeAttackState(bool isAttack)
    {
        if (isAttack)
        {
            CanAttack = false;
            TurnPassed = 0;
        }
        else if (Attack > 0)
        {
            TurnPassed++;

            if (TurnToAttack - TurnPassed <= 0)
            {
                CanAttack = true;
                TurnPassed = 0;
            }
        }
    }

    public void ChangePower(int dmg)
    {
        if (Armor - dmg >= 0)
        {
            Armor -= dmg;
        }
        else if (Armor - dmg < 0)
        {
            dmg -= Armor;
            Armor = 0;
            Power -= dmg;
        }        
    }
}

public static class CardManager 
{
    public static List<Card> AllCards = new List<Card>();
}



public class CardManagerSrp : MonoBehaviour
{
    public void Awake()
    {
        CardManager.AllCards.Add(new Card("Infantry", "Sprites/Cards/PolPih", 2, 3, 5, 1, Card.CardType.Infantry));
        CardManager.AllCards.Add(new Card("Cavalry", "Sprites/Cards/Cavalry", 6, 7, 9, 0, Card.CardType.Cavalry));
        CardManager.AllCards.Add(new Card("Artillery", "Sprites/Cards/Artil", 5, 1, 8, 2, Card.CardType.Artillery));
        CardManager.AllCards.Add(new Card("Hotyn", "Sprites/Cards/Hotyn", 0, 0, 10, 0, Card.CardType.Fortification));
    }

}
