using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private GameObject bigBanana;
    [SerializeField] private GameObject explainBossBattle;
    [SerializeField] private GameObject BanaPowerBonus;
    [SerializeField] private GameObject bossAppearance;
    [SerializeField] private GameObject gameStartText;
    //ParticleSystem
    [SerializeField] private GameObject startEffect;

    [SerializeField] private GameObject HPbar;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text HPtext;

    [SerializeField] private Text banaPowerText;
    [SerializeField] private Image bana;
    [SerializeField] private GameObject climbingMonkey;

    //ランダムインスタンス生成
    private System.Random random = new System.Random();

    Transform __explainBossBattle;
    Transform __BanaPowerBonus;
    AnimationStep step;

    private int maxHp;
    private float CountDownTime;
    private float timeLimit = 0;
    private int HP;
    private bool GameClearflg;
    private Vector2 explainBossBattleInitialPosition;
    private Vector2 BanaPowerBonusInitialPosition;
    private float MonitoringBananaHP;
    //バナナのHPの最初の一回を判定するフラグ
    private bool MonitoringBananaHPflg = false;
    private bool GameStartAfterSecondTimesflg = false;

    private SpriteRenderer __bigBanana;
    private Sprite bigbanana1;
    [SerializeField] private Sprite bigbanana2;
    [SerializeField] private Sprite bigbanana3;
    [SerializeField] private Sprite bigbanana4;



    public enum AnimationStep
    {
        ONE,    
        TWO,  
        THREE,
        FOUR,
        FIVE,
        BanaPower,
        BanaPower_Wait,
        GameStart,
        TWO_Wait,
        THREE_Wait,
        FOUR_Wait,
        FIVE_Wait,
        GameFinish,
        THREE_SECONDS_TIME,
    }

    void Start()
    {
        //デカバナナ変更用変数
        __bigBanana = bigBanana.GetComponent<SpriteRenderer>();
        //一番最初のバナナ
        bigbanana1 = __bigBanana.sprite;
        //説明
        __explainBossBattle = explainBossBattle.transform;
        //バナパワーのテキスト表示用
        __BanaPowerBonus = BanaPowerBonus.transform;
        //ムーブするから最初のポジションを保持する
        explainBossBattleInitialPosition = __explainBossBattle.position;
        BanaPowerBonusInitialPosition = __BanaPowerBonus.position;
        StartCoroutine(GameStart(0.5f));
    }

    IEnumerator GameStart(float f)
    {
        //ステップ1
        step = AnimationStep.ONE;
        //木につかまっているサルがある時はこれをtrueにするよ
        GameOver.StartFinishAnimeflg = true;
        //ここでスプライトを初期化するよ
        __bigBanana.sprite = bigbanana1;
        //デカバナナを倒す条件
        explainBossBattle.transform.GetChild(0).GetComponent<Text>().text = "<size=250>れんだ</size>して"
    + "\n<size=250>でかばなな</size>をたべろ！";
        //ゲームクリアフラグ
        GameClearflg = true;
        yield return new WaitForSeconds(f);

        //デカバナナはっけんenable
        bossAppearance.transform.GetChild(0).GetComponent<Text>().enabled = true;
        //最初のアニメーション
        iTween.ScaleFrom(bossAppearance, getITweenAnimations("Expand", null, 0.1f, 1f, "OnUpdateValue"));
        Instantiate(startEffect, bossAppearance.transform);
    }

    IEnumerator GameStartAfterSecondTimes(float f)
    {
        //木につかまっているサルtrueにする
        GameOver.StartFinishAnimeflg = true;
        //でかばななスプライトを初期化
        __bigBanana.sprite = bigbanana1;

        yield return new WaitForSeconds(f);
        //ゲーム２週目以降に建てるフラグだよ
        GameStartAfterSecondTimesflg = true;

        step = AnimationStep.THREE_SECONDS_TIME;
    }


    void Update()
    {
        if(step == AnimationStep.GameStart)
        {
            //ゲームスタートテキスト非アクティブにする
            gameStartText.transform.GetChild(0).GetComponent<Text>().enabled = false;

            if (!(HP <= 0) || GameClearflg)
            {
                //カウントダウンタイムを更新
                CountDownTime -= Time.deltaTime;
                timeText.text = CountDownTime.ToString("F");

                //HPtextの値を更新
                HPtext.text = HP + "/" + maxHp;

                //Debug.Log(CountDownTime);
                if (CountDownTime <= timeLimit)
                {
                    step = AnimationStep.GameFinish;
                    GameClearflg = false;
                    timeText.text = "0.00";
                    this.gameObject.GetComponent<BossBattle>().enabled = false;
                }
            }

        }
        else if(step == AnimationStep.TWO && Input.GetMouseButtonDown(0))
        {
            //一回のループで一回まで押せるようにする
            step = AnimationStep.TWO_Wait;
            bossAppearance.transform.GetChild(0).GetComponent<Text>().enabled = false;
            iTween.MoveTo(explainBossBattle, getITweenAnimations("updateY", __explainBossBattle, 6f, 1f, "OnUpdateValue"));
        }
        else if ((step == AnimationStep.THREE && Input.GetMouseButtonDown(0))|| step == AnimationStep.THREE_SECONDS_TIME)
        {

            //一回のループで一回まで押せるようにする
            step = AnimationStep.THREE_Wait;
            if (!GameStartAfterSecondTimesflg)
            {
                iTween.MoveTo(explainBossBattle, getITweenAnimations("updateY", __explainBossBattle, 8f, 1f, "OnUpdatePosition"));
            }

            //maxHPと秒数を設定
            ChengeDifficultyLevel();

            //カウントダウンタイムの初期値を設定する
            timeText.text = CountDownTime.ToString("F");

            //HPtextの値の初期値を設定する
            HPtext.text = HP + "/" + maxHp;

            //大きなバナナ出現
            bigBanana.GetComponent<SpriteRenderer>().enabled = true;
            bigBanana.GetComponent<PolygonCollider2D>().enabled = true;
            iTween.ScaleFrom(bigBanana, getITweenAnimations("Expand", null, 0.1f, 2f, "OnUpdateValue"));
            //HPバーの設定
            HPbar.GetComponent<Slider>().value = 1;
            HPbar.transform.GetChild(0).GetComponent<Image>().enabled = true;
            HPbar.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().enabled = true;
            scoreText.enabled = false;
            timeText.enabled = true;
            HPtext.enabled = true;

        }
        else if (step == AnimationStep.FOUR)
        {
            //アニメーションを終わるまで待つ
            step = AnimationStep.FOUR_Wait;

            if(int.Parse(banaPowerText.text) == 0)
            {
                banaPowerText.enabled = false;
                bana.enabled = false;
                step = AnimationStep.FIVE;
            }
            else
            {
                iTween.ShakePosition(bigBanana, getITweenAnimations("SHAKE", null, 0, 0, "DecreaseHP"));
            }

        }
        else if (step == AnimationStep.FIVE)
        {
            step = AnimationStep.FIVE_Wait;
            gameStartText.transform.GetChild(0).GetComponent<Text>().enabled = true;
            iTween.ScaleFrom(gameStartText, getITweenAnimations("Expand", null, 0.1f, 2f, "OnUpdateValue"));
        }
        else if(step == AnimationStep.BanaPower)
        {
            step = AnimationStep.BanaPower_Wait;
            iTween.MoveTo(BanaPowerBonus, getITweenAnimations("updateY", __BanaPowerBonus, 16f, 3f, "OnUpdateValue"));
        }


    }

    private Hashtable getITweenAnimations(string s, Transform t = null, float f = 0, float f2 = 0, string met = null)
    {
        Hashtable hash = new Hashtable();

        switch (s)
        {
            case "Expand":
                hash.Add("x", f);
                hash.Add("y", f);
                hash.Add("time", f2);
                hash.Add("oncomplete", met);
                hash.Add("oncompletetarget", this.gameObject);
                break;

            case "updateY":
                hash.Add("y", t.position.y + f);
                hash.Add("time", f2);
                hash.Add("oncomplete", met);
                hash.Add("oncompletetarget", this.gameObject);
                hash.Add("easeType", "easeOutQuad");
                break;

            case "SHAKE":
                hash.Add("x", 0.02f);//振動
                hash.Add("y", 0.02f);//振動
                hash.Add("time", 0.04f);
                hash.Add("oncomplete", met);
                hash.Add("oncompletetarget", this.gameObject);
                break;
        }

        return hash;
    }

    void OnUpdateValue()
    {
        switch (step)
        {
            case AnimationStep.ONE:
                step = AnimationStep.TWO;
                break;

            case AnimationStep.TWO_Wait:
                step = AnimationStep.THREE;
                break;

            case AnimationStep.THREE_Wait:
                if(MainMenu.GameMode == 0)
                {
                    step = AnimationStep.BanaPower;
                }
                else
                {
                    step = AnimationStep.FIVE;
                }
                break;

            case AnimationStep.BanaPower_Wait:
                //バナパワーボーナス初期位置に戻す
                __BanaPowerBonus.position = BanaPowerBonusInitialPosition;
                step = AnimationStep.FOUR;
                break;

            case AnimationStep.FIVE_Wait:
                step = AnimationStep.GameStart;
                break;

        }
    }

    void OnUpdatePosition()
    {
        //タップの説明ポジション初期位置に戻す
        __explainBossBattle.position = explainBossBattleInitialPosition;
    }

    void DecreaseHP()
    {
        HP = HP - 1;
        HPbar.GetComponent<Slider>().value = (float)HP / (float)maxHp;
        int i = int.Parse(banaPowerText.text);
        i--;
        banaPowerText.text = i.ToString();
        step = AnimationStep.FOUR;
    }
    private void ChengeDifficultyLevel()
    {
        float reductionNumber = 0f;
        int sec = random.Next(6, 11);

        //カウントが上がる毎にカウントダウンの時間小さくなるように設定する
        if (MainMenu.GameMode == 1 && GameController.dekabananaTotalScore > 0)
        {
            //前回のカウントダウンタイムに応じて
            reductionNumber = 0.3f * GameController.dekabananaTotalScore * CountDownTime;
        }
        if (MainMenu.GameMode == 0)
        {
            //HPを設定
            maxHp = 90;

            //カウントダウンを設定
            CountDownTime = 5;

            //エンドレスモード二回目以降のでかばなな処理
            if (GameController.endlessTotalScore > 200)
            {
                maxHp += (int)System.Math.Round(0.3 * GameController.endlessTotalScore);
                CountDownTime += 0.05f * (int)System.Math.Round(GameController.endlessTotalScore / 25.0);
            }
            else if (GameController.endlessTotalScore > 100)
            {
                maxHp += (int)System.Math.Round(0.6 * GameController.endlessTotalScore);
                CountDownTime += 0.4f * (int)System.Math.Round(GameController.endlessTotalScore / 25.0);
            }
            else if (GameController.endlessTotalScore > 25)
            {
                maxHp += (int)System.Math.Round(0.4 * GameController.endlessTotalScore);
                CountDownTime += 0.4f * (int)System.Math.Round(GameController.endlessTotalScore / 25.0);
            }



            //バナパワーボーナス
            if (int.Parse(banaPowerText.text) >= 50)
            {
                CountDownTime += 1;
            }
        }
        else
        {
            //秒数を決める
            CountDownTime = sec;


            //カウントダウンタイムを削る
            CountDownTime -= reductionNumber;

            //タップする回数を決める
            switch (sec)
            {
                case 6:
                    maxHp = 55;
                    break;

                case 7:
                    maxHp = 65;
                    break;

                case 8:
                    maxHp = 75;
                    break;

                case 9:
                    maxHp = 85;
                    break;

                case 10:
                    maxHp = 95;
                    break;

            }
        }
        //HPを設定
        HP = maxHp;
    }

    public void ClickBigBanana()
    {
        if (step == AnimationStep.GameStart)
        {
            
            if (!(HP == 0))
            {
                HP = HP - 1;
            }

            //HPバーに反映。
            HPbar.GetComponent<Slider>().value = (float)HP / (float)maxHp;
            HPtext.text = HP + "/" + maxHp;
            if (!MonitoringBananaHPflg)
            {
                MonitoringBananaHPflg = true;
                //ここでバナナ監視変数初期化
                MonitoringBananaHP = HPbar.GetComponent<Slider>().value;
            }

            if (HPbar.GetComponent<Slider>().value < (MonitoringBananaHP * 1 / 3))
            {
                __bigBanana.sprite = bigbanana3;
                
            }
            else if(HPbar.GetComponent<Slider>().value < (MonitoringBananaHP * 2 / 3))
            {
                __bigBanana.sprite = bigbanana2;
                climbingMonkey.GetComponent<SpriteRenderer>().enabled = false;
                GameOver.StartFinishAnimeflg = false;
            }
            Debug.Log(HPbar.GetComponent<Slider>().value);

            //ボスバナナを倒した時の処理
            if (HPbar.GetComponent<Slider>().value == 0)
            {
                __bigBanana.sprite = bigbanana4;
                step = AnimationStep.GameFinish;
                StartCoroutine(WaitWhenGameEnd(1f));
            }
        }
    }

    IEnumerator WaitWhenGameEnd(float f)
    {
        yield return new WaitForSeconds(f);
        climbingMonkey.GetComponent<SpriteRenderer>().enabled = true;

        if (MainMenu.GameMode == 1)
        {
            //でかばななモードのときのみプラスする
            GameController.dekabananaTotalScore++;

            //全部戻してもう一度
            bigBanana.GetComponent<SpriteRenderer>().enabled = false;
            bigBanana.GetComponent<PolygonCollider2D>().enabled = false;
            HPbar.transform.GetChild(0).GetComponent<Image>().enabled = false;
            timeText.enabled = false;
            scoreText.enabled = true;
            scoreText.text = GameController.dekabananaTotalScore.ToString();
            HPtext.enabled = false;
            //もう一度
            StartCoroutine(GameStartAfterSecondTimes(1f));
        }
        else
        {
            GameController.endlessTotalScore++;
            this.gameObject.GetComponent<BossBattle>().enabled = false;
        }
    }

    void OnDisable()
    {
        //木につかまっているサルがある時はこれをtrueにするよ
        GameOver.StartFinishAnimeflg = true;

        if (!GameClearflg)
        {
            GameController.notFirstTimeFlg = false;
            GameController.gameStatus = 3;
        }
        else
        {
            bigBanana.GetComponent<SpriteRenderer>().enabled = false;
            bigBanana.GetComponent<PolygonCollider2D>().enabled = false;
            HPbar.transform.GetChild(0).GetComponent<Image>().enabled = false;
            timeText.enabled = false;
            GameController.notFirstTimeFlg = true;
            GameController.gameStatus = 4;
        }
        

    }
}