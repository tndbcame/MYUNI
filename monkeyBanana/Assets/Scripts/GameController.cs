using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    //ゲームスコア保持変数
    public static string __score;

    //ゲーム分岐用フラグ(0で初期化)
    public static int gameflg = 0;

    // ゲームのステートを定義
    enum State
    {
        MainMenu,
        AddPlayer,
        GameMenu,
        OneShotGame,
        EndressGame
    }
    State state;

    
    private void screen_transition()
    {
        // ゲームのステートごとにイベントを監視
        switch (state)
        {
            case State.MainMenu:
                //メインメニューへ戻る
                SceneManager.LoadScene("MainMenu");
                break;

            case State.GameMenu:
                //ゲームメニューへ遷移
                SceneManager.LoadScene("GameMenu");
                break;

            case State.AddPlayer:
                //プレーヤー追加メニューへ遷移
                SceneManager.LoadScene("AddPlayerMenu");
                break;

            case State.OneShotGame or State.EndressGame:
                //ゲーム開始！
                SceneManager.LoadScene("CountDown");
                break;

        }
    }

    //ひとりでがタップされた時
    public void tap_solo_bottun()
    {
        state = State.GameMenu;
        screen_transition();
    }
    //みんなでがタップされた時
    public void tap_everyone_bottun()
    {
        state = State.AddPlayer;
        screen_transition();
    }
    //一発勝負がタップされた時
    public void tap_one_shot_game_bottun()
    {
        state = State.OneShotGame;
        gameflg = 1;
        screen_transition();
    }
    //エンドレスモードがタップされた時
    public void tap_endless_game_bottun()
    {
        state = State.EndressGame;
        gameflg = 2;
        screen_transition();
    }
    //リスタートボタンが押されたときの処理
    public void tap_restart_bottun()
    {
        if(gameflg != 0)
        {
            SceneManager.LoadScene("CountDown");
        }
    }
    //タイトルへ戻るボダンが押された時
    public void tap_return_title_bottun()
    {
        state = State.MainMenu;
        screen_transition();
    }
}
