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

    public Card(string name, string logoPath, int attack, int armor, int power)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack; 
        Armor = armor;
        Power = power;
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
        CardManager.AllCards.Add(new Card("Carabiniers", "Sprites/Cards/Carabiniers", 2, 3, 5));
        CardManager.AllCards.Add(new Card("Cavalry", "Sprites/Cards/Cavalry", 6, 7, 9));
        CardManager.AllCards.Add(new Card("Cuirassiers", "Sprites/Cards/Cuirassiers", 5, 1, 8));
        CardManager.AllCards.Add(new Card("Lancers", "Sprites/Cards/Lancers", 3, 4, 7));
    }

}
