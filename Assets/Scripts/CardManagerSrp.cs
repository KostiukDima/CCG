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
        CardManager.AllCards.Add(new Card("Мушкетери", "Sprites/Cards/Musketeer", 2, 3, 5, 1, Card.CardType.Infantry));
        CardManager.AllCards.Add(new Card("Єгері", "Sprites/Cards/Jager", 2, 3, 5, 1, Card.CardType.Infantry));
        CardManager.AllCards.Add(new Card("Гренадери", "Sprites/Cards/Grenadier", 2, 3, 5, 1, Card.CardType.Infantry));
        CardManager.AllCards.Add(new Card("Аркебузири", "Sprites/Cards/Arquebus", 2, 3, 5, 1, Card.CardType.Infantry));
        CardManager.AllCards.Add(new Card("Кірасири", "Sprites/Cards/Cuirassiers", 6, 7, 9, 0, Card.CardType.Cavalry));
        CardManager.AllCards.Add(new Card("Драгуни", "Sprites/Cards/Dragons", 6, 7, 9, 0, Card.CardType.Cavalry));
        CardManager.AllCards.Add(new Card("Карабінери", "Sprites/Cards/Carabiniers", 6, 7, 9, 0, Card.CardType.Cavalry));
        CardManager.AllCards.Add(new Card("Кавалеристи", "Sprites/Cards/Cavalry", 6, 7, 9, 0, Card.CardType.Cavalry));
        CardManager.AllCards.Add(new Card("Бомбарда", "Sprites/Cards/Bombarda", 5, 1, 8, 2, Card.CardType.Artillery));
        CardManager.AllCards.Add(new Card("Канона", "Sprites/Cards/Canona", 5, 1, 8, 2, Card.CardType.Artillery));
        CardManager.AllCards.Add(new Card("Гаубиця", "Sprites/Cards/Artil", 5, 1, 8, 2, Card.CardType.Artillery));
        CardManager.AllCards.Add(new Card("Мортира", "Sprites/Cards/Mortira", 5, 1, 8, 2, Card.CardType.Artillery));
        CardManager.AllCards.Add(new Card("Ліхтенштайн", "Sprites/Cards/Lichtenstein", 0, 0, 10, 0, Card.CardType.Fortification));
        CardManager.AllCards.Add(new Card("Марксбург", "Sprites/Cards/Marksburg", 0, 0, 10, 0, Card.CardType.Fortification));
        CardManager.AllCards.Add(new Card("Фалькенберг", "Sprites/Cards/Falkenberg", 0, 0, 10, 0, Card.CardType.Fortification));
        CardManager.AllCards.Add(new Card("Шлайнінґ", "Sprites/Cards/Karlstein", 0, 0, 10, 0, Card.CardType.Fortification));
        CardManager.AllCards.Add(new Card("Шильон", "Sprites/Cards/Schlaining", 0, 0, 10, 0, Card.CardType.Fortification));
        CardManager.AllCards.Add(new Card("Карлштейн", "Sprites/Cards/Chillon", 0, 0, 10, 0, Card.CardType.Fortification));
    }

}
