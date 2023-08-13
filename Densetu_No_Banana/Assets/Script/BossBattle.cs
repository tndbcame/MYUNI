using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
    Transform __explainBossBattle;
    AnimationStep step;

    //uniTaskキャンセル用トークン生成
    private CancellationTokenSource cts = new CancellationTokenSource();
    private CancellationToken token;

    private int maxHp = 20;
    private float CountDownTime = 5;
    private float timeLimit = 0;
    private double __countdown;
    private int HP;

    private bool GameClearflg;

    //クリックしたゲームオブジェクト
    private GameObject clickedGameObject;


    public enum AnimationStep
    {
        ONE,    
        TWO,  
        THREE,
        GameStart,
    }


    async void Start()
    {
        GameClearflg = true;
        __explainBossBattle = explainBossBattle.transform;
        explainBossBattle.transform.GetChild(0).GetComponent<Text>().text = "<size=300>5</size>びょういないに"
            + "\n<size=300>20</size>かいタップしろ！";
        await GameStart(0.5f);
    }
    async UniTask GameStart(float f)
    {
        step = AnimationStep.ONE;
        await UniTask.Delay(TimeSpan.FromSeconds(f), cancellationToken: token);
        //cts.Cancel();
        bossAppearance.transform.GetChild(0).GetComponent<Text>().enabled = true;
        iTween.ScaleFrom(bossAppearance, getITweenAnimations("Expand", null, 0, 1f));
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
                            this.gameObject.GetComponent<BossBattle>().enabled = false;
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
            bossAppearance.transform.GetChild(0).GetComponent<Text>().enabled = false;
            iTween.MoveTo(explainBossBattle, getITweenAnimations("updateY", __explainBossBattle, 6f));
        }
        else if (step == AnimationStep.THREE && Input.GetMouseButtonDown(0))
        {
            iTween.MoveTo(explainBossBattle, getITweenAnimations("updateY", __explainBossBattle, 8f));

            //大きなバナナ出現
            bigBanana.GetComponent<SpriteRenderer>().enabled = true;
            bigBanana.GetComponent<PolygonCollider2D>().enabled = true;
            iTween.ScaleFrom(bigBanana, getITweenAnimations("Expand", null, 0, 2f));
            //HPバーの設定
            HPbar.GetComponent<Slider>().value = 1;
            HPbar.transform.GetChild(0).GetComponent<Image>().enabled = true;
            HPbar.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().enabled = true;


            scoreText.enabled = false;
            timeText.enabled = true;

            HP = maxHp;
        }
        
    }

    private Hashtable getITweenAnimations(string s, Transform t = null, float f = 0, float f2 = 0)
    {
        Hashtable hash = new Hashtable();

        switch (s)
        {
            case "Expand":
                hash.Add("x", 0.1f);
                hash.Add("y", 0.1f);
                hash.Add("time", f2);
                hash.Add("oncomplete", "OnUpdateValue");
                hash.Add("oncompletetarget", this.gameObject);
                break;

            case "updateY":
                hash.Add("y", t.position.y + f);
                hash.Add("time", 1f);
                hash.Add("oncomplete", "OnUpdateValue");
                hash.Add("oncompletetarget", this.gameObject);
                hash.Add("easeType", "easeOutQuad");
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

            case AnimationStep.TWO:
                step = AnimationStep.THREE;
                break;

            case AnimationStep.THREE:
                step = AnimationStep.GameStart;
                break;
        }
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