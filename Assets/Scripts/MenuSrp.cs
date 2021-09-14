using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSrp : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
