using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private Text YourScore;

    void Start()
    {
        YourScore.text = string.Format("あなたのスコア\n{0}", GameController.totalScore);

        //現在のスコア
        int scoreNow = GameController.totalScore;

        int record = PlayerPrefs.GetInt("HiScore", 0);

        if (scoreNow > record)
            PlayerPrefs.SetInt("HiScore", scoreNow);

        GameController.totalScore = 0;
    }

    public void onReStart()
    {
        SceneManager.LoadScene("GameScreen");
    }

    public void onToMainMenu()
    {
        MainMenu.GameMode = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
