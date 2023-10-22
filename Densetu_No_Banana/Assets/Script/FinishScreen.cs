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
        int scoreNow;
        int record;
        string HiScore;

        if (MainMenu.GameMode == 0)
        {
            YourScore.text = string.Format("あなたのスコア\n{0}", GameController.endlessTotalScore);

            //現在のスコア
            HiScore = "EndlessHiscore";

            scoreNow = GameController.endlessTotalScore;
        }
        else
        {
            YourScore.text = string.Format("あなたのスコア\n{0}", GameController.dekabananaTotalScore);

            //現在のスコア
            HiScore = "DekabananaHiscore";

            scoreNow = GameController.dekabananaTotalScore;
        }

        record = PlayerPrefs.GetInt(HiScore, 0);

        if (scoreNow > record)
            PlayerPrefs.SetInt(HiScore, scoreNow);

        //スコアの初期化
        GameController.endlessTotalScore = 0;
        GameController.dekabananaTotalScore = 0;


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
