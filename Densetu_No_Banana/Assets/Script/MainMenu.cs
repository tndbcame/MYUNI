using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using KanKikuchi.AudioManager;

public class MainMenu : MonoBehaviour
{
    //バナナラベルオブジェクト
    [SerializeField] private GameObject bananaLabel;
    //変更用バナナラベルテクスチャ
    [SerializeField] private Sprite bananaLabel2;
    //アニメーション定義
    [SerializeField] private Animator Shrink;
    [SerializeField] private Animator Expand;

    [SerializeField] private Text username;

    private Image __bananaLabel;
    private Sprite bananaLabel1;
    private Text bananaLabelText;

    /*
     * 0:エンドレス
     * 1:デカバナナ
     */
    public static int GameMode = 0;

    //こいつがonのときだけゲームを切り替えられるよ
    private bool swichGameflg = true;
    private void Start()
    {
        Expand.SetTrigger("startExpand");
        StartCoroutine(StartToMainmenu(2f));
        username.text = PlayerPrefs.GetString("UserName", "");
    }

    //ゲームモードの切り替え
    public void onSwichGameMode()
    {
        SEManager.Instance.Play(SEPath.KOUKAON1);
        if (swichGameflg)
        {
            if (GameMode == 0)
            {
                __bananaLabel.sprite = bananaLabel2;
                bananaLabelText.text = "でかばなな";
                GameMode = 1;
            }
            else if (GameMode == 1)
            {
                __bananaLabel.sprite = bananaLabel1;
                bananaLabelText.text = "えんどれす";
                GameMode = 0;
            }
        }

    }

    //ゲームスタート
    public void onStartEndlessGame()
    {
        SEManager.Instance.Play(SEPath.KOUKAON1);
        swichGameflg = false;
        if (GameMode == 0)
        {
            StartCoroutine(StartEndlessGame());
        }
        else if (GameMode == 1)
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

    IEnumerator StartToMainmenu(float f)
    {
        yield return new WaitForSeconds(f);
        //第二引数で音量調整も簡単にできる
        BGMManager.Instance.Play(BGMPath.BGM1);
        __bananaLabel = bananaLabel.GetComponent<Image>();
        bananaLabel1 = __bananaLabel.sprite;
        bananaLabelText = bananaLabel.transform.GetChild(0).GetComponent<Text>();
    }
}
