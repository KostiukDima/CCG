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

public class GameScore
{
    public int PlayerPower;
    public int EnemyPower;
}

public class MatchScore
{
    public List<GameScore> GameScores;
    public int PlayerScore;
    public int EnemyScore;
    public MatchScore()
    {
        GameScores = new List<GameScore>();
        EnemyScore = 0;
        PlayerScore = 0;
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
    public MatchScore MatchScore;
    public List<CardInfoSrp> EnemyHandCards = new List<CardInfoSrp>();
    public List<CardInfoSrp> PlayerHandCards = new List<CardInfoSrp>();
    public List<CardInfoSrp> EnemyFieldCards = new List<CardInfoSrp>();
    public List<CardInfoSrp> PlayerFieldCards = new List<CardInfoSrp>();

    public Text PlayerPowerScoreTxt;
    public Text PlayerMatchScoreTxt;
    public Text EnemyPowerScoreTxt;
    public Text EnemyMatchScoreTxt;

    public bool PlayerPassed;
    public bool EnemyPassed;
    public GameObject EnemyPassedObj;
    public GameObject PlayerPassedObj;

    int numberRound;

    public bool IsPlayerTurn
    {
        get { return Turn % 2 == 0; }
    }

    void Start()
    {
        StartGame();
    }

    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeTxt.text = TurnTime.ToString();

        foreach (var item in PlayerFieldCards)
        {
            item.HideCanAttack();
        }

        if (IsPlayerTurn)
        {
            if (!PlayerPassed)
            {
                foreach (var item in PlayerFieldCards)
                {
                    item.SelfCard.ChangeAttackState(false);
                    if (item.SelfCard.CanAttack)
                    {
                        item.ShowCanAttack();
                    }
                }

                while (TurnTime-- > 0)
                {
                    TurnTimeTxt.text = TurnTime.ToString();
                    yield return new WaitForSeconds(1);
                }
            }
        }
        else if(!EnemyPassed)
        {
            foreach (var item in EnemyFieldCards)
            {
                item.SelfCard.ChangeAttackState(false);
            }

            while (TurnTime-- > 27)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }

            EnemyTurn();

            if(EnemyHandCards.Count == 0)
            {
                EnemyPassed = true;
                EnemyPassedObj.SetActive(true);
            }

            CheckForResult();
        }

        ChangeTurn();
    }

    void EnemyTurn()
    {
        if (EnemyHandCards.Count > 0)
        {
            int cardNumber = Random.Range(0, EnemyHandCards.Count);

            EnemyHandCards[cardNumber].ShowCardInfo(EnemyHandCards[cardNumber].SelfCard);

            int line = Random.Range(1, 3);

            if (line == 1)
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
        foreach (var attackingCard in EnemyFieldCards.FindAll(c => c.SelfCard.CanAttack))
        {
            if (PlayerFieldCards.Count == 0)
                return;

            CardInfoSrp enemyCard = PlayerFieldCards[Random.Range(0, PlayerFieldCards.Count)];

            attackingCard.SelfCard.ChangeAttackState(true);
            CardsFight(attackingCard, enemyCard);
        }

        RefreshGameScore();
    }

    void GiveHandCards(List<Card> deck, Transform hand)
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
            cardObj.GetComponent<AttackedCardSrp>().enabled = false;
        }
        deck.RemoveAt(0);
    }

    public void EndTurn()
    {
        if(PlayerHandCards.Count == 0)
        {
            PlayerPassed = true;
            PlayerPassedObj.SetActive(true);
        }

        ChangeTurn();
    }


    public void ChangeTurn()
    {
        StopAllCoroutines();

        Turn++;

        EndTurnBtn.interactable = IsPlayerTurn && !PlayerPassed;

        StartCoroutine(TurnFunc());
    }

    public void CardsFight(CardInfoSrp attackingCard, CardInfoSrp attackedCard)
    {
        attackedCard.SelfCard.ChangePower(attackingCard.SelfCard.Attack);

        if (!attackedCard.SelfCard.IsAlive)
            DestroyCard(attackedCard);
        else
            attackedCard.RefreshData();

        RefreshGameScore();
    }

    public void DestroyCard(CardInfoSrp card)
    {
        card.GetComponent<CardDragSrp>().OnEndDrag(null);

        if (EnemyFieldCards.Exists(c => c == card))
        {
            EnemyFieldCards.Remove(card);
        }

        if (PlayerFieldCards.Exists(c => c == card))
        {
            PlayerFieldCards.Remove(card);
        }

        Destroy(card.gameObject);
    }

    public void RefreshGameScore()
    {
        int playerPower = GetGamePower(PlayerFieldCards);
        int enemyPower = GetGamePower(EnemyFieldCards);

        MatchScore.GameScores[numberRound].EnemyPower = enemyPower;
        MatchScore.GameScores[numberRound].PlayerPower = playerPower;

        PlayerPowerScoreTxt.text = playerPower.ToString();
        EnemyPowerScoreTxt.text = enemyPower.ToString();
    }

    public int GetGamePower(List<CardInfoSrp> cardList)
    {
        int power = 0;

        foreach (var item in cardList)
        {
            power += item.SelfCard.Power;
        }

        return power;
    }

    public void CheckForResult()
    {
        if (PlayerPassed && EnemyPassed)
        {
            StopAllCoroutines();

            RefreshRoundResult();

            if (MatchScore.GameScores.Count == 3)
            {
                EndGame();
            }
            else if (MatchScore.GameScores.Count == 2)
            {
                if (MatchScore.EnemyScore == MatchScore.PlayerScore)
                { 
                    StartNewRound();
                }
                else
                {
                    EndGame();
                }
            }
            else
            {
                StartNewRound();
            }
        }
    }

    public void StartNewRound()
    {
        Turn = 0;
        numberRound++;
        MatchScore.GameScores.Add(new GameScore());
        ClearFieldInfo();

        EndTurnBtn.interactable = true;

        EnemyPassedObj.SetActive(false);
        PlayerPassedObj.SetActive(false);

        PlayerPassed = false;
        EnemyPassed = false;

        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);

        StartCoroutine(TurnFunc());
    }

    public void EndGame()
    {
        ClearFieldInfo();
    }

    public void StartGame()
    {
        Turn = 0;
        numberRound = 0;
        CurrentGame = new Game();

        MatchScore = new MatchScore();
        MatchScore.GameScores.Add(new GameScore());        

        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);

        StartCoroutine(TurnFunc());
    }

    public void ClearFieldInfo()
    {
        StopAllCoroutines();

        foreach (var item in PlayerHandCards)
            Destroy(item.gameObject);
        foreach (var item in PlayerFieldCards)
            Destroy(item.gameObject);
        foreach (var item in EnemyHandCards)
            Destroy(item.gameObject);
        foreach (var item in EnemyFieldCards)
            Destroy(item.gameObject);

        PlayerFieldCards.Clear();
        PlayerHandCards.Clear();
        EnemyFieldCards.Clear();
        EnemyHandCards.Clear();

        EnemyPowerScoreTxt.text = "0";
        PlayerPowerScoreTxt.text = "0";
    }
    public void RefreshRoundResult()
    {
        if(MatchScore.GameScores[numberRound].EnemyPower > MatchScore.GameScores[numberRound].PlayerPower)
        {
            MatchScore.EnemyScore++;
            EnemyMatchScoreTxt.text = MatchScore.EnemyScore.ToString();
        }
        else if (MatchScore.GameScores[numberRound].EnemyPower < MatchScore.GameScores[numberRound].PlayerPower)
        {
            MatchScore.PlayerScore++;
            PlayerMatchScoreTxt.text = MatchScore.PlayerScore.ToString();
        }
        else
        {
            MatchScore.EnemyScore++;
            EnemyMatchScoreTxt.text = MatchScore.EnemyScore.ToString();
            MatchScore.PlayerScore++;
            PlayerMatchScoreTxt.text = MatchScore.PlayerScore.ToString();
        }
    }
}
