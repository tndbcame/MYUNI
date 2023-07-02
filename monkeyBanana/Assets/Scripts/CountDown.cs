using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{
    [SerializeField] private int timeLimit;
    [SerializeField] private Text timerText;
    private float time;
    private bool _isStart = true;

    void Update()
    {
        if(_isStart == true)
        {
            //フレーム毎の経過時間をtime変数に追加
            time += Time.deltaTime;
            //time変数をint型にし制限時間から引いた数をint型のlimit変数に代入
            int countDownTimer = timeLimit - (int)time;

            if(countDownTimer <= 0)
            {
                timerText.text = "スタート！";
                if(countDownTimer <= -1)
                {
                    _isStart = false;
                    game_start();
                }
            }
            else
            {
                //
                timerText.text = countDownTimer.ToString("D1");
            }

        }

    }

    private void game_start()
    {
        if (GameController.gameflg == 1)
        {
            SceneManager.LoadScene("GhanSwich");
        }
        if(GameController.gameflg == 2)
        {
            //TODOここの処理を追加する
            //SceneManager.LoadScene("");
        }
    }
}
