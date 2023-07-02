using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OneShotGame : MonoBehaviour
{
    List<string> playerNameList = AddPlayer.playerNameLists;
    public GameObject tamagokakegohan;
    public GameObject siroigohan;

    [SerializeField] private double minSecondsForChengetexture;
    [SerializeField] private double maxSecondsForChengetexture;
    [SerializeField] private Text _timerText;

    private Vector2 BeforePosTama;
    private Vector2 BeforePosSiro;

    private bool _isStart = false;
    private bool isTimer = false;
    private float TimerCount = 0;

    // Start is called before the first frame update
    void Start()
    {

        Invoke(nameof(Second),GetRndSeconds());

    }

    void Second()
    {
        _isStart = true;
        Transform tamagokake = tamagokakegohan.transform;
        Transform sirogohan = siroigohan.transform;
        BeforePosTama = tamagokake.position;
        BeforePosSiro = sirogohan.position;
        sirogohan.position = BeforePosTama;
        tamagokake.position = BeforePosSiro;
        if (BeforePosSiro.Equals(tamagokakegohan.transform.position))
        {
            isTimer = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (_isStart)
        {

            if (isTimer)
            {
                TimerCount += Time.deltaTime;
            }
            if (Input.GetMouseButtonDown(0))
            {
                isTimer = false;
                GameController.__score = get_score(TimerCount);
                _timerText.text = TimerCount.ToString("f3");
                SceneManager.LoadScene("FinishGameScreen");
            }
        }

    }

    //IEnumerator waitSecond(float f)
    //{
    //    yield return new WaitForSeconds(f);
    //}

    private float GetRndSeconds()
    {
        System.Random rnd = new System.Random();
        double rndSeco = rnd.NextDouble() * (maxSecondsForChengetexture - minSecondsForChengetexture) + minSecondsForChengetexture;

        return (float)rndSeco;
    }
    //private void FixedUpdate()
    //{
    //    if (isTimer)
    //    {
    //        TimerCount += Time.deltaTime;
    //    }
    //    _timerText.text = TimerCount.ToString("f3");
    //}

    //スコア取得
    private string get_score(float time)
    {
        string score = (1000000 / (int)time).ToString();
        return score;
    }


}
