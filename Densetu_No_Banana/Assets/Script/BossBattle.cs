using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private GameObject bigBanana;
    [SerializeField] private GameObject explainBossBattle;
    [SerializeField] private GameObject bossAppearance;
    //ParticleSystem
    [SerializeField] private GameObject startEffect;

    [SerializeField] private GameObject HPbar;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;

    [SerializeField] private Text banaPowerText;
    [SerializeField] private Image bana;


    Transform __explainBossBattle;
    AnimationStep step;

    private int maxHp = 50;
    private float CountDownTime;
    private float timeLimit = 0;
    private int HP;
    private bool GameClearflg;
    private Vector2 explainBossBattleInitialPosition;

    //クリックしたゲームオブジェクト
    private GameObject clickedGameObject;


    public enum AnimationStep
    {
        ONE,    
        TWO,  
        THREE,
        FOUR,
        GameStart,
        TWO_Wait,
        THREE_Wait,
        FOUR_Wait,
    }


    void Start()
    {
        
        __explainBossBattle = explainBossBattle.transform;
        //ムーブするから最初のポジションを保持する
        explainBossBattleInitialPosition = __explainBossBattle.position;
        StartCoroutine(GameStart(0.5f));
    }
    IEnumerator GameStart(float f)
    {
        //ステップ1
        step = AnimationStep.ONE;
        //デカバナナを倒す条件
        explainBossBattle.transform.GetChild(0).GetComponent<Text>().text = "<size=300>5</size>びょういないに"
    + "\n<size=300>20</size>かいタップしろ！";
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
            if (Input.GetMouseButtonDown(0))
            {
                clickedGameObject = null;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
                if (hit2d)
                {
                    clickedGameObject = hit2d.transform.gameObject;
                    if (clickedGameObject == bigBanana)
                    {
                        //タップしたら1引く
                        if (!(HP == 0))
                        {
                            HP = HP - 1;
                        }

                        //HPバーに反映。
                        HPbar.GetComponent<Slider>().value = (float)HP / (float)maxHp; ;
                        Debug.Log(HPbar.GetComponent<Slider>().value);
                        if (HPbar.GetComponent<Slider>().value == 0)
                        {
                            if(MainMenu.GameMode == 1)
                            {

                                GameController.totalScore　++;
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
                    }


                }
            }

            if (!(HP <= 0) || GameClearflg)
            {
                CountDownTime -= Time.deltaTime;
                // カウントダウンタイムを整形して表示
                timeText.text = CountDownTime.ToString("F");
                
                Debug.Log(CountDownTime);
                if (CountDownTime <= timeLimit)
                {
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
            iTween.MoveTo(explainBossBattle, getITweenAnimations("updateY", __explainBossBattle, 6f, 0, "OnUpdateValue"));
        }
        else if (step == AnimationStep.THREE && Input.GetMouseButtonDown(0))
        {
            //一回のループで一回まで押せるようにする
            step = AnimationStep.THREE_Wait;
            iTween.MoveTo(explainBossBattle, getITweenAnimations("updateY", __explainBossBattle, 8f, 0, "OnUpdatePosition"));
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

            HP = maxHp;
            CountDownTime = 5;
        }
        else if (step == AnimationStep.FOUR)
        {
            //アニメーションを終わるまで待つ
            step = AnimationStep.FOUR_Wait;

            if(int.Parse(banaPowerText.text) == 0)
            {
                banaPowerText.enabled = false;
                bana.enabled = false;
                step = AnimationStep.GameStart;
            }
            else
            {
                iTween.ShakePosition(bigBanana, getITweenAnimations("SHAKE", null, 0, 0, "DecreaseHP"));
            }

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
                hash.Add("time", 1f);
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
                    step = AnimationStep.FOUR;
                }
                else
                {
                    step = AnimationStep.GameStart;
                }
                break;
        }
    }

    void OnUpdatePosition()
    {
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

    void OnDisable()
    {

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