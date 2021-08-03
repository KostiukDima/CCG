using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game
{
    public List<Card> EnemyDeck;
    public List<Card> PlayerDeck;

    public Game()
    {
        EnemyDeck = GetDeckCard();
        PlayerDeck = GetDeckCard();
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
    public Transform EnemyFirstField;
    public Transform EnemySecondField;
    public Transform PlayerSecondField;
    public Transform PlayerFirstField;
    public GameObject cardPrefab;
    int Turn, TurnTime = 30;
    public Text TurnTimeTxt;
    public Button EndTurnBtn;

    public List<CardInfoSrp> EnemyHandCards = new List<CardInfoSrp>(); 
    public List<CardInfoSrp> PlayerHandCards = new List<CardInfoSrp>();
    public List<CardInfoSrp> EnemyFieldCards = new List<CardInfoSrp>();
    public List<CardInfoSrp> PlayerFieldCards = new List<CardInfoSrp>();

    public bool IsPlayerTurn
    {
        get { return Turn % 2 == 0; }
    }

    void Start()
    {
        Turn = 0;

        CurrentGame = new Game();

        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);

        StartCoroutine(TurnFunc());
    }

    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeTxt.text = TurnTime.ToString();

        if(IsPlayerTurn)
        {
            while (TurnTime-- > 0 )
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            while (TurnTime-- > 27)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }

            if (EnemyHandCards.Count > 0)
                EnemyTurn();
        }

        ChangeTurn();
    }

    void EnemyTurn()
    {
        int cardNumber = Random.Range(0, EnemyHandCards.Count);

        EnemyHandCards[cardNumber].ShowCardInfo(EnemyHandCards[cardNumber].SelfCard);

        int line = Random.Range(1, 3);

        if(line == 1)
        {
            EnemyHandCards[cardNumber].transform.SetParent(EnemyFirstField);
        }
        else
        {
            EnemyHandCards[cardNumber].transform.SetParent(EnemySecondField);
        }

        EnemyFieldCards.Add(EnemyHandCards[cardNumber]);
        EnemyHandCards.Remove(EnemyHandCards[cardNumber]);
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
        {
            cardObj.GetComponent<CardInfoSrp>().HideCardInfo(card);
            EnemyHandCards.Add(cardObj.GetComponent<CardInfoSrp>());
        }
        else
        {
            cardObj.GetComponent<CardInfoSrp>().ShowCardInfo(card);
            PlayerHandCards.Add(cardObj.GetComponent<CardInfoSrp>());
        }
        deck.RemoveAt(0);
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();

        Turn++;

        EndTurnBtn.interactable = IsPlayerTurn;

        StartCoroutine(TurnFunc());       
    }
}
