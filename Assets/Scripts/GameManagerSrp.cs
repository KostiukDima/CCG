using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game
{
    public List<Card> EnemyDeck;
    public List<Card> PlayerDeck;
    public List<Card> EnemyHand;
    public List<Card> PlayerHand;
    public List<Card> EnemyField;
    public List<Card> PlayerField;

    public Game()
    {
        EnemyDeck = GetDeckCard();
        PlayerDeck = GetDeckCard();

        EnemyHand = new List<Card>();
        PlayerHand = new List<Card>();
        
        EnemyField = new List<Card>();
        PlayerField = new List<Card>();
    }

    List<Card> GetDeckCard()
    {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < 15; i++)
        {
            cards.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
        }

        return cards;
    }
}

public class GameManagerSrp : MonoBehaviour
{
    public Game CurrentGame;
    public Transform EnemyHand;
    public Transform PlayerHand;
    public GameObject cardPrefab;


    void Start()
    {
        CurrentGame = new Game();

        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
    }

    void GiveHandCards (List<Card> deck, Transform hand)
    {
        for (int i = 0; i < 5; i++)
        {
            GiveCardToHand(deck, hand);
        }
    }

    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;

        Card card = deck[0];

        GameObject cardObj = Instantiate(cardPrefab, hand, false);

        if (hand == EnemyHand)
            cardObj.GetComponent<CardInfoSrp>().HideCardInfo(card);
        else
            cardObj.GetComponent<CardInfoSrp>().ShowCardInfo(card);

        deck.RemoveAt(0);
    }
}
