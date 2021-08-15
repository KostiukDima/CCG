using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMatchWindowSpr : MonoBehaviour
{
    public Text EndMatchPlayerMatchScoreTxt;
    public Text EndMatchEnemyMatchScoreTxt;

    public Text EndMatchPlayerPowerFirstScoreTxt;
    public Text EndMatchPlayerPowerSecondScoreTxt;
    public Text EndMatchPlayerPowerThirdScoreTxt;

    public Text EndMatchEnemyPowerFirstScoreTxt;
    public Text EndMatchEnemyPowerSecondScoreTxt;
    public Text EndMatchEnemyPowerThirdScoreTxt;

    public Text EndMatchTxt;

    public GameManagerSrp GameManager;
    private void Awake()
    {
        GameManager = FindObjectOfType<GameManagerSrp>();
    }

    public void OpenWindow(MatchScore score)
    {
        transform.gameObject.SetActive(true);

        OpenEndMatchWindow(score);
    }

    public void OpenEndMatchWindow(MatchScore score)
    {

        EndMatchPlayerMatchScoreTxt.text = score.PlayerScore.ToString();
        EndMatchEnemyMatchScoreTxt.text = score.EnemyScore.ToString();

        for (int i = 0; i < 3; i++)
        {
            if(i == 0)
            {
                EndMatchPlayerPowerFirstScoreTxt.text = score.GameScores[i].PlayerPower.ToString();
                EndMatchEnemyPowerFirstScoreTxt.text = score.GameScores[i].EnemyPower.ToString();
            }
            else if (i == 1)
            {
                EndMatchPlayerPowerSecondScoreTxt.text = score.GameScores[i].PlayerPower.ToString();
                EndMatchEnemyPowerSecondScoreTxt.text = score.GameScores[i].EnemyPower.ToString();

            }
            else if (i == 2)
            {
                EndMatchPlayerPowerThirdScoreTxt.text = score.GameScores[i].PlayerPower.ToString();
                EndMatchEnemyPowerThirdScoreTxt.text = score.GameScores[i].EnemyPower.ToString();
            }
        }
        
        if (score.EnemyScore > score.PlayerScore)
        {
            EndMatchTxt.text = "Поразка";
        }
        else if (score.EnemyScore < score.PlayerScore)
        {
            EndMatchTxt.text = "Перемога";
        }
        else
        {
            EndMatchTxt.text = "Нічия";
        }
    }
}
