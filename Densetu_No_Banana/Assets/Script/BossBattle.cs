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

    [SerializeField] private Text banaPowerText;
    [SerializeField] private Image bana;

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

    void Update()
    {
        if(step == AnimationStep.GameStart)
        {
            //ゲームスタートテキスト非アクティブにする
            gameStartText.transform.GetChild(0).GetComponent<Text>().enabled = false;

            if (!(HP <= 0) || GameClearflg)
            {
                CountDownTime -= Time.deltaTime;
                // カウントダウンタイムを整形して表示
                timeText.text = CountDownTime.ToString("F");
                
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
        else if (step == AnimationStep.THREE && Input.GetMouseButtonDown(0))
        {
            //一回のループで一回まで押せるようにする
            step = AnimationStep.THREE_Wait;
            iTween.MoveTo(explainBossBattle, getITweenAnimations("updateY", __explainBossBattle, 8f, 1f, "OnUpdatePosition"));
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

            //ここでmaxHPと秒数を設定
            ChengeDifficultyLevel();
            HP = maxHp;
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
                hash.Add("x", 0.05f);//振動
                hash.Add("y", 0.05f);//振動
                hash.Add("time", 0.1f);
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
        int sec = random.Next(1, 6);
        if (MainMenu.GameMode == 0)
        {
            maxHp = 50;
            CountDownTime = 5;
        }
        else
        {
            switch (sec)
            {
                case 1:
                    maxHp = 10;
                    CountDownTime = 1;
                    break;

                case 2:
                    maxHp = 20;
                    CountDownTime = 2;
                    break;

                case 3:
                    maxHp = 30;
                    CountDownTime = 3;
                    break;

                case 4:
                    maxHp = 40;
                    CountDownTime = 4;
                    break;

                case 5:
                    maxHp = 50;
                    CountDownTime = 5;
                    break;

            }
        }
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
            HPbar.GetComponent<Slider>().value = (float)HP / (float)maxHp; ;
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
        GameController.totalScore++;

        if (MainMenu.GameMode == 1)
        {

            
            //全部戻してもう一度
            bigBanana.GetComponent<SpriteRenderer>().enabled = false;
            bigBanana.GetComponent<PolygonCollider2D>().enabled = false;
            HPbar.transform.GetChild(0).GetComponent<Image>().enabled = false;
            timeText.enabled = false;
            scoreText.enabled = true;
            scoreText.text = GameController.totalScore.ToString();
            //もう一度
            StartCoroutine(GameStart(1f));
        }
        else
        {
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