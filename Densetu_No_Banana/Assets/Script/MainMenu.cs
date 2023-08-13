using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //バナナラベルオブジェクト
    [SerializeField] private GameObject bananaLabel;
    //変更用バナナラベルテクスチャ
    [SerializeField] private Sprite bananaLabel2;
    //アニメーション定義
    [SerializeField] private Animator Shrink;

    private Image __bananaLabel;
    private Sprite bananaLabel1;
    private Text bananaLabelText;

    /*
     * 0:エンドレス
     * 1:デカバナナ
     */
    public static int GameMode = 0;
    private void Start()
    {
        __bananaLabel = bananaLabel.GetComponent<Image>();
        bananaLabel1 = __bananaLabel.sprite;
        bananaLabelText = bananaLabel.transform.GetChild(0).GetComponent<Text>();
    }

    //ゲームモードの切り替え
    public void onSwichGameMode()
    {
        if (GameMode == 0)
        {
            __bananaLabel.sprite = bananaLabel2;
            bananaLabelText.text = "でかばなな"; 
            GameMode = 1;
        }
        else if(GameMode == 1)
        {
            __bananaLabel.sprite = bananaLabel1;
            bananaLabelText.text = "えんどれす";
            GameMode = 0;
        }
    }

    //ゲームスタート
    public void onStartEndlessGame()
    {
        if(GameMode == 0)
        {
            StartCoroutine(StartEndlessGame());
        }
        else if(GameMode == 1)
        {
            StartCoroutine(StartBigBananaGame());
        }
    }
    //エンドレス
    IEnumerator StartEndlessGame()
    {
        Shrink.SetTrigger("Shrink");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameScreen");
    }
    //デカバナナ
    IEnumerator StartBigBananaGame()
    {
        Shrink.SetTrigger("Shrink");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameScreen");
    }
}
