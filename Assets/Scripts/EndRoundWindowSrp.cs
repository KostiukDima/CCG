using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRoundWindowSrp : MonoBehaviour
{
    public Text EndRoundPlayerPowerScoreTxt;
    public Text EndRoundPlayerMatchScoreTxt;
    public Text EndRoundEnemyPowerScoreTxt;
    public Text EndRoundEnemyMatchScoreTxt;
    public Text EndRoundTxt;
    public GameManagerSrp GameManager;
    private void Awake()
    {       
        GameManager = FindObjectOfType<GameManagerSrp>();
    }

    public void OpenWindow(MatchScore score)
    {
        transform.gameObject.SetActive(true);

        StartCoroutine(OpenEndRoundWindow(score));
    }

    IEnumerator OpenEndRoundWindow(MatchScore score)
    {

        EndRoundPlayerMatchScoreTxt.text = score.PlayerScore.ToString();
        EndRoundEnemyMatchScoreTxt.text = score.EnemyScore.ToString();
        EndRoundPlayerPowerScoreTxt.text = score.GameScores[GameManager.numberRound].PlayerPower.ToString();
        EndRoundEnemyPowerScoreTxt.text = score.GameScores[GameManager.numberRound].EnemyPower.ToString(); 

        if(score.GameScores[GameManager.numberRound].EnemyPower > score.GameScores[GameManager.numberRound].PlayerPower)
        {
            EndRoundTxt.text = "Ви програли раунд";
        }
        else if (score.GameScores[GameManager.numberRound].EnemyPower < score.GameScores[GameManager.numberRound].PlayerPower)
        {
            EndRoundTxt.text = "Ви виграли раунд";
        }
        else 
        {
            EndRoundTxt.text = "Нічия";
        }

        yield return new WaitForSeconds(3);

        StopAllCoroutines();
        transform.gameObject.SetActive(false);
        GameManager.StartNewRound();

    }
}
