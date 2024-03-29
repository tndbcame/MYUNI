using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;

public class GameController : MonoBehaviour
{
    public static int gameStatus = 0;
    public static bool notFirstTimeFlg = false;
    /*
     * エンドレスなら光るバナナをタップしたとき
     * デカバナナモードなら一体倒すごとに
     */
    public static int endlessTotalScore = 0;
    public static int dekabananaTotalScore = 0;


    void Start()
    {
        this.gameObject.GetComponent<GameStart>().enabled = true;
        GameOver.StartFinishAnimeflg = true;
    }

    void LateUpdate()
    {

        switch (gameStatus)
        {
            case 1:
                this.gameObject.GetComponent<EndlessGame>().enabled = true;
                break;

            case 2:
                this.gameObject.GetComponent<BossBattle>().enabled = true;
                break;

            case 3:
                this.gameObject.GetComponent<GameOver>().enabled = true;
                break;
            //一旦シーンをリセット
            case 4:
                gameStatus = 0;
                SceneManager.LoadScene("GameScreen");
                break;
        }

    }
}
