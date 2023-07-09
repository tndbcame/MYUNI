using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //列挙されたposition
    [SerializeField] private Transform positions;
    //緑〜黄緑色のバナナ
    [SerializeField] private GameObject bananas;
    //光るバナナ
    [SerializeField] private GameObject shinybanana;
    //黒バナナ
    [SerializeField] private GameObject blackBanana;

    //バナナをゲットしたときのサル
    [SerializeField] private GameObject getBananaMonkey;

    //何秒に1回バナナを生成する決めるときの変数
    [SerializeField] private int min;
    [SerializeField] private int max;


    //バナナの出現ポジションリスト
    List<Vector2> bananaAprPosList = new List<Vector2>();
    //バナナリスト
    Dictionary<int, GameObject> bananasList = new Dictionary<int, GameObject>();
    //光るバナナのポジション操作用
    private Transform __shinybanana;
    //光るバナナポジション保持用変数
    private Vector2 retentionPositionShinybanana;

    //ゲットしたサルのポジション操作用
    private Transform __getBananaMonkey;
    //ゲットしたサルの初期ポジション
    private Vector2 retentionPositionGetBananaMonkey;

    //ランダムインスタンス生成
    System.Random random = new System.Random();
    //光ったバナナをタップしたときのカウント変数
    private int shinybananaCount = 0;
    //始めにバナナが生成されるまでの時間を増やす変数(生成時間変数)
    private int generationSec = 0;

    //クリックしたゲームオブジェクト
    private GameObject clickedGameObject;

    //uniTaskキャンセル用トークン生成
    private CancellationTokenSource cts = new CancellationTokenSource();
    private CancellationToken token;


    //初期設定
    void Start()
    {
        // bananasの子オブジェクトの数を取得
        int childCount = bananas.transform.childCount;

        //positionsの子オブジェクトを全て取得する
        foreach (Transform pos in positions)
        {
            //取得した出現ポジションリストに格納する
            bananaAprPosList.Add(pos.position);
        }

        //bananasの子オブジェクトを全て取得する
        for (int i = 0; i < childCount; i++)
        {
            Transform banana = bananas.transform.GetChild(i);
            bananasList.Add(i,banana.gameObject);
        }

        //光るバナナのポジションの初期値を設定と保持
        __shinybanana = shinybanana.transform;
        retentionPositionShinybanana = __shinybanana.position;

        //ゲットバナナサルの初期位置取得
        __getBananaMonkey = getBananaMonkey.transform;
        retentionPositionGetBananaMonkey = __getBananaMonkey.position;

        //キャンセルトークン初期化
        token = cts.Token;
    }


    async void Update()
    {
        //バナナを入れ替える用のポジションが存在しているときにバナナ生成するよ
        if (bananaAprPosList?.Count > 0)
        {
            Vector2 shinybananaCandidatePos = await BananaLife(min, max);

            //光っているバナナと保持用変数が一緒かどうか判定
            if (__shinybanana.position.Equals(retentionPositionShinybanana))
            {
                __shinybanana.position = shinybananaCandidatePos;
            }
            //光らず死んだバナナの位置をリストに戻す
            else
            {
                bananaAprPosList.Add(shinybananaCandidatePos);
            }
        }
        

        //タップしたゲームオブジェクトを取得する
        if (Input.GetMouseButtonDown(0))
        {
            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                //光るバナナをタップした時の処理
                clickedGameObject = hit2d.transform.gameObject;
                if(clickedGameObject == shinybanana)
                {
                    //光るバナナのタップした回数を数える
                    shinybananaCount += 1;

                    //ゲットバナナサルと位置を入れ替える
                    __getBananaMonkey.position = __shinybanana.position;

                    //光っていたバナナの位置をPosリストに戻す
                    bananaAprPosList.Add(__shinybanana.position);

                    //shinybananaを初期位置を入れ替える
                    __shinybanana.position = retentionPositionShinybanana;

                    //生成時間変数を0に戻す
                    generationSec =  0;

                    await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);

                    //ゲットバナナサルを初期位置に戻す
                    __getBananaMonkey.position = retentionPositionGetBananaMonkey;


                }
                else
                {
                    cts.Cancel();
                    SceneManager.LoadScene("FinishGameScreen");
                }
            }

        }
    }

    //バナナの生成〜消滅
    async UniTask<Vector2> BananaLife(int min, int max)
    {
        //ポジションを保持する
        Vector2 retentionPosition = get_replace_bananas();

        //始めに生成するバナナを判定
        bool bananaTransitionCountflg = true;

        generationSec += 1;

        //ループしながら次のバナナへ遷移
        foreach (int BananaIndex in decide_bananas_to_use(banana_transition_count()))
        {
            if (bananaTransitionCountflg)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(generationSec), cancellationToken: token);

                //初期生成フラグをfalseにする
                bananaTransitionCountflg = false; 
            }

            //バナナのClone生成
            GameObject BananaClone = (GameObject)Instantiate(bananasList[BananaIndex]);
            Transform __bananaClone = BananaClone.transform;
            Debug.Log("バナナを生成しました。");

            //ポジションリストからポジションを入れ替える
            __bananaClone.position = retentionPosition;

            //ランダムに遷移時間を決める
            int sec = random.Next(min, max);

            //指定した時間分待つ
            await UniTask.Delay(TimeSpan.FromSeconds(sec), cancellationToken: token);

            //バナナオブジェクト削除
            Destroy(BananaClone);
        }
        return retentionPosition;
    }

    //何段階か決める処理
    private int banana_transition_count()
    {
        int transitionCount = random.Next(0, bananasList.Count);
        return transitionCount;
    }

    //段階が決まったあとになんのバナナをつかうか決める
    private List<int> decide_bananas_to_use(int transitionCount)
    {
        //バナナindex定義
        var BananaIndex = new List<int>();

        //最初のバナナのインデックスを決める
        int __transitionCount = bananasList.Count - transitionCount;
        int min = 0;

        //段階の応じてランダムで生成されるバナナを決める
        for (int i = 0; i < __transitionCount; i++)
        {
            min = random.Next(min, bananasList.Count + 1 + i - __transitionCount);
            BananaIndex.Add(min);
        }
        return BananaIndex;
    }

    //posListの中からランダムにバナナの入れ替える用位置を取得
    private Vector2 get_replace_bananas()
    {
        int rnd = random.Next(bananaAprPosList.Count);

        //bananaAprPosListから取得して削除
        Vector2 pos = bananaAprPosList[rnd];
        bananaAprPosList.RemoveAt(rnd);

        return pos;
    }
}
