using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack;
    public int Armor;
    public int Power;
    public int TurnToAttack;
    public int TurnPassed;
    public bool CanAttack;

    public bool IsAlive
    {
        get { return Power > 0; }
    }

    public Card(string name, string logoPath, int attack, int armor, int power, int turnToAttack)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack; 
        Armor = armor;
        Power = power;
        TurnToAttack = turnToAttack;
        TurnPassed = 0;
        CanAttack = false;
    }

    public void ChangeAttackState(bool isAttack)
    {
        if (isAttack)
        {
            CanAttack = false;
            TurnPassed = 0;
        }
        else
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
        CardManager.AllCards.Add(new Card("Carabiniers", "Sprites/Cards/Carabiniers", 2, 3, 5, 1));
        CardManager.AllCards.Add(new Card("Cavalry", "Sprites/Cards/Cavalry", 6, 7, 9, 0));
        CardManager.AllCards.Add(new Card("Cuirassiers", "Sprites/Cards/Cuirassiers", 5, 1, 8, 2));
        CardManager.AllCards.Add(new Card("Lancers", "Sprites/Cards/Lancers", 3, 4, 7, 1));
    }

}
