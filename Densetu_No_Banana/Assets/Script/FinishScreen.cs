using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using KanKikuchi.AudioManager;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private Text YourScore;

    [SerializeField] private GameObject monkey;
    [SerializeField] private Sprite monkey1;
    [SerializeField] private Sprite monkey2;
    [SerializeField] private Sprite monkey3;
    [SerializeField] private Sprite monkey4;
    [SerializeField] private Sprite monkey5;

    private SpriteRenderer __monkey;

    void Start()
    {
        BGMManager.Instance.FadeOut();
        int scoreNow;
        int record;
        string HiScore;
        __monkey = monkey.transform.GetComponent<SpriteRenderer>();

        //おサルの画像を設定する
        switch (TreasureChest.FinishScreenMonkeyImage)
        {
            case 1:
                __monkey.sprite = monkey1;
                break;
            case 2:
                __monkey.sprite = monkey2;
                break;
            case 3:
                __monkey.sprite = monkey3;
                break;
            case 4:
                __monkey.sprite = monkey4;
                break;
            case 5:
                __monkey.sprite = monkey5;
                break;
        }


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
