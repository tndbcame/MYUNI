using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using KanKikuchi.AudioManager;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private Text YourScore;

    void Start()
    {
        BGMManager.Instance.FadeOut();

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
        SEManager.Instance.Play(SEPath.KOUKAON1);
        SceneManager.LoadScene("GameScreen");
    }

    public void onToMainMenu()
    {
        SEManager.Instance.Play(SEPath.KOUKAON1);
        MainMenu.GameMode = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
