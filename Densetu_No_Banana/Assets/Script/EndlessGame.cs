using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndlessGame : MonoBehaviour
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
    //木につかまっているサル
    [SerializeField] private GameObject climbingMonkey;
    //光ったバナナになってからボタンを押すまでの時間
    [SerializeField] private GameObject TimeToTap;
    //何秒に1回バナナを生成する決めるときの変数
    [SerializeField] private int min;
    [SerializeField] private int max;
    //黒いバナナに変化するまでの時間
    [SerializeField] private float chengingTimeForBlackBana;
    //スコア表示用変数
    [SerializeField] private Text Score;
    //初めにバナナが生成されるまでの時間の定数
    [SerializeField] private float __generationSec;
    //バナパワー更新用テキスト
    [SerializeField] private Text banaPowerText;

    //バナナの出現ポジションリスト
    private List<Vector2> bananaAprPosList = new List<Vector2>();
    //バナナリスト
    private Dictionary<int, GameObject> bananasList = new Dictionary<int, GameObject>();
    //光るバナナのポジション操作用
    private Transform __shinybanana;
    //光るバナナポジション保持用変数
    private Vector3 retentionPositionShinybanana;
    //ブラックバナナのポジション操作用
    private Transform __blackBanana;
    //黒バナナポジション初期位置保持用
    private Vector2 retentionPositionBlackbanana;
    //黒バナナポジション保持用変数
    private Vector2 chengingPositionForBlackbanana;
    //ゲットしたサルのポジション操作用
    private Transform __getBananaMonkey;
    //ゲットしたサルの初期ポジション
    private Vector3 retentionPositionGetBananaMonkey;
    //木につかまっているサルのポジション操作用
    private Transform __climbingMonkey;
    //木につかまっているサルの初期ポジション
    private Vector3 retentionPositionClimbingMonkey;
    //時間表示のポジション操作用変数
    private Transform __timeToTap;
    //ボタン押した時の入れ替え用ポジション
    private Vector3 whenTapBottunPotion;

    //ランダムインスタンス生成
    private System.Random random = new System.Random();

    //始めにバナナが生成されるまでの時間を増やす変数(生成時間変数)
    private float generationSec = 0;
    //光るバナナが黒いバナナになるまでの変数定義
    private float ToBlackBananaCount = 0f;
    //クリックしたゲームオブジェクト
    private GameObject clickedGameObject;
    //uniTaskキャンセル用トークン生成
    private CancellationTokenSource cts = new CancellationTokenSource();
    private CancellationToken token;
    //アニメーション定義
    private Animator jampMonkey;
    //光るバナナのインターバルフラグ
    private bool shinyBananaIntervalflg = true;
    //ボタンタップをタップしてからフラグ
    private bool whenTapBottunflg = true;
    //ゲームステータスフラグ
    private int gameStatusFlg;
    //バナパワーのカウント
    private int banaCount;

    //初期設定
    void Start()
    {
        //1 = エンドレスモード
        //2 = ボス戦フラグ
        //3 = ゲームオーバー
        gameStatusFlg = 1;

        //バナパワー初期化
        banaPowerText.text = "0";
        banaCount = 0;

        //スコアを設定
        Score.text = GameController.totalScore.ToString();

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

        //ゲットバナナサルの初期値の設定と保持,Animatorコンポーネントを設定
        __getBananaMonkey = getBananaMonkey.transform;
        retentionPositionGetBananaMonkey = __getBananaMonkey.position;
        jampMonkey = getBananaMonkey.GetComponent<Animator>();
        jampMonkey.SetBool("judgeJamp", false);

        //木につかまっているサルの初期値の設定と保持
        __climbingMonkey = climbingMonkey.transform;
        retentionPositionClimbingMonkey = __climbingMonkey.position;

        //ブラックバナナのを初期値を設定と保持
        __blackBanana = blackBanana.transform;
        retentionPositionBlackbanana = __blackBanana.position;

        //タップしたときの時間表示用の位置変数
        __timeToTap = TimeToTap.transform;

        //キャンセルトークン初期化
        token = cts.Token;
    }


    async void Update()
    {
        //バナナを入れ替える用のポジションが存在しているときにバナナ生成するよ
        if (bananaAprPosList?.Count > 0)
        {
            generationSec += Time.deltaTime;
            Vector3 shinybananaCandidatePos = await BananaLife(min, max);

            //光っているバナナ、ゲットバナナサルとが保持用変数が一緒かどうか,
            //サルがジャンプしていないかどうか、
            //光るバナナのインターバルフラグがtrueかどうか、
            //空のvector2と同じでないかどうか、
            //タップ処理が行われていないかどうか、
            //ボス戦フラグが立っていないかどうか判定
            if (
                __shinybanana.position.Equals(retentionPositionShinybanana)
                && !jampMonkey.GetBool("judgeJamp")
                && shinyBananaIntervalflg
                && !shinybananaCandidatePos.Equals(retentionPositionShinybanana)
                && whenTapBottunflg
                && !(gameStatusFlg == 2)

               )
            {
                Debug.Log("光るバナナを生成しました");
                shinyBananaIntervalflg = false;
                __shinybanana.position = shinybananaCandidatePos;
                whenTapBottunPotion = shinybananaCandidatePos;
                //ブラックバナナが初期位置にいるかどうか
                if (__blackBanana.position.Equals(retentionPositionBlackbanana))
                {
                    //ブラックバナナのポジション設定
                    chengingPositionForBlackbanana = shinybananaCandidatePos;
                }
            }
            //光らず死んだバナナの位置をリストに戻す
            else
            {
                if (!shinybananaCandidatePos.Equals(retentionPositionShinybanana))
                {
                    bananaAprPosList.Add(shinybananaCandidatePos);
                }
            }
        }

        //光っているバナナが存在している時間を計るよ
        if (!__shinybanana.position.Equals(retentionPositionShinybanana) && whenTapBottunflg)
        {
            //光っているバナナが存在している時間を計るよ
            ToBlackBananaCount += Time.deltaTime;
            Debug.Log(ToBlackBananaCount.ToString());
            //光るバナナが出現してからタップするまでの時間
            TimeToTap.transform.GetChild(0).GetComponent<Text>().text = ToBlackBananaCount.ToString("f3");

            //黒いバナナになったらゲーム終了！
            if (ToBlackBananaCount > chengingTimeForBlackBana)
            {
                //光るバナナと黒いバナナと位置を入れ替える
                __blackBanana.position = chengingPositionForBlackbanana;

                //shinybananaを初期位置を入れ替える
                __shinybanana.position = retentionPositionShinybanana;

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);

                //ゲームオーバー
                gameStatusFlg = 3;
                this.gameObject.GetComponent<EndlessGame>().enabled = false;
            }
        }

        //タップしたゲームオブジェクトを取得する
        if (Input.GetMouseButtonDown(0))
        {
            whenTapBottunflg = false;
            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                Debug.Log("光るバナナをタップしました。");
                //光るバナナをタップした時の処理
                clickedGameObject = hit2d.transform.gameObject;
                if(clickedGameObject == shinybanana)
                {
                    //光るバナナのタップした回数を数える
                    GameController.totalScore += 1;

                    if((GameController.totalScore - 19) % 20 == 0)
                    {
                        //bossflg(2)にする
                        gameStatusFlg = 2;
                    }
                    
                    //スコア更新
                    Score.text = GameController.totalScore.ToString();

                    //ゲットバナナサルと光るバナナの位置を入れ替える
                    __getBananaMonkey.position = whenTapBottunPotion;
                    //サルがjumpするアニメーションをtrueにする
                    jampMonkey.SetBool("judgeJamp", true);

                    //タップしたときの時間表示&アニメーション
                    __timeToTap.position = whenTapBottunPotion;
                    iTween.MoveTo(TimeToTap, getITweenAnimations("UP", __timeToTap, "endTimeToTap"));

                    //バナパワー更新
                    banaCount += calculateBanaPower(TimeToTap.transform.GetChild(0).GetComponent<Text>().text);
                    banaPowerText.text = banaCount.ToString();

                    //木につかまっているサルに光るバナナの初期位置にする
                    __climbingMonkey.position = retentionPositionShinybanana;

                    //その位置が入っていないかどうか判定してから光っていたバナナの位置をPosリストに戻す
                    if (!bananaAprPosList.Contains(whenTapBottunPotion))
                    {
                        bananaAprPosList.Add(whenTapBottunPotion);
                    }
                        
                    //shinybananaを初期位置を入れ替える
                    __shinybanana.position = retentionPositionShinybanana;

                    //生成時間変数を0に戻す
                    generationSec =  0;
                    //黒いバナナに変わるまでの時間を0に戻す
                    ToBlackBananaCount = 0;

                    //ゲットバナナサルのジャンプアニメーションの時間待つ
                    await UniTask.Delay(TimeSpan.FromSeconds(0.5), cancellationToken: token);

                    //ゲットバナナサルを初期位置に戻す
                    __getBananaMonkey.position = retentionPositionGetBananaMonkey;
                    //木につかまっているサルを初期位置に戻す
                    __climbingMonkey.position = retentionPositionClimbingMonkey;
                    //サルジャンプアニメーション解除
                    jampMonkey.SetBool("judgeJamp", false);

                    //光るバナナの最低限のインターバル
                    await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
                    whenTapBottunflg = true;
                    shinyBananaIntervalflg = true;

                    //bossflg(2)のとき
                    if (gameStatusFlg == 2)
                    {
                        this.gameObject.GetComponent<EndlessGame>().enabled = false;
                    }
                    
                }
                else
                {
                    gameStatusFlg = 3;
                    this.gameObject.GetComponent<EndlessGame>().enabled = false;
                }
            }
        }
    }

    //バナナの生成〜消滅
    async UniTask<Vector3> BananaLife(int min, int max)
    {
        if (generationSec > __generationSec)
        {
            //ポジションを保持する
            Vector2 retentionPosition = get_replace_bananas();
            //0に戻す
            generationSec = 0;

            //始めに生成するバナナを判定
            bool bananaTransitionCountflg = true;

            //ループしながら次のバナナへ遷移
            foreach (int BananaIndex in decide_bananas_to_use(banana_transition_count()))
            {
                //バナナのClone生成
                GameObject BananaClone = (GameObject)Instantiate(bananasList[BananaIndex]);
                Transform __bananaClone = BananaClone.transform;
                Debug.Log("バナナを生成しました。");

                //ポジションリストからポジションを入れ替える
                __bananaClone.position = retentionPosition;

                //ランダムに遷移時間を決める
                int sec = random.Next(min, max);

                if (bananaTransitionCountflg)
                {
                    iTween.ShakePosition(BananaClone, getITweenAnimations("SHAKE"));
                    //初期生成フラグをfalseにする
                    bananaTransitionCountflg = false;
                }

                try
                {
                    //指定した時間分待つ
                    await UniTask.Delay(TimeSpan.FromSeconds(sec), cancellationToken: token);
                }
                catch (OperationCanceledException)
                {
                    //バナナオブジェクト削除
                    Destroy(BananaClone);
                    break;
                }

                //バナナオブジェクト削除
                Destroy(BananaClone);
            }
            return retentionPosition;
        }
        else
        {
            return retentionPositionShinybanana;
        }
    }

    //何段階か決める処理
    private int banana_transition_count()
    {
        int transitionCount = random.Next(1, bananasList.Count);
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

    //iTweenのアニメーション生成
    private Hashtable getITweenAnimations(string s, Transform t = null, string met = null)
    {
        Hashtable hash = new Hashtable();
        if (s == "UP")
        {
            hash.Add("y", t.position.y + 1f);// 上方向へ移動する
            hash.Add("time", 1f);
            hash.Add("oncomplete", met);// 移動完了した時のコールバック
            hash.Add("oncompletetarget", this.gameObject);
        }
        else if (s == "SHAKE")
        {
            hash.Add("x", 0.05f);//振動
            hash.Add("y", 0.05f);//振動
            hash.Add("time", 0.5f);
            hash.Add("oncompletetarget", this.gameObject);
        }
        return hash;
    }
    // タップしてからの時間表示アニメーションが終了した時の処理
    private void endTimeToTap()
    {
        //光るバナナの初期位置に戻す
        __timeToTap.position = retentionPositionShinybanana;
    }

    //バナパワー計算
    private int calculateBanaPower(string TimeToTap)
    {
        double bp = double.Parse(TimeToTap);
        //0.7以降は1
        if(bp >= 0.7)
        {
            return 1;
        }
        //0.7未満0.5以上は2
        else if (bp < 0.7)
        {
            return 2;
        }
        //0.5未満3
        else if (bp < 0.5)
        {
            return 3;
        }
        return 1;
    }

    //EndlessGameがDisableになったときの処理
    void OnDisable()
    {
        cts.Cancel();
        if(gameStatusFlg == 3)
        {
            GameController.notFirstTimeFlg = false;
        }
        GameController.gameStatus = gameStatusFlg;
    }
}