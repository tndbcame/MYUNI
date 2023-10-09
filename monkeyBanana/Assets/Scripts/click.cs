using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class click : MonoBehaviour
{
    public GameObject tamagokakegohan;
    public GameObject siroigohan;

    [SerializeField] private double minSecondsForChengetexture;
    [SerializeField] private double maxSecondsForChengetexture;
    [SerializeField] private Text _timerText;

    private Vector2 BeforePosTama;
    private Vector2 BeforePosSiro;

    private bool isTimer = false;
    private float TimerCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSecond(GetRndSeconds()));
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
        if (isTimer)
        {
            TimerCount += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isTimer = false;
            _timerText.text = TimerCount.ToString("f3");
        }
    }

    IEnumerator waitSecond(float f)
    {
        yield return new WaitForSeconds(f);
    }

    private float GetRndSeconds()
    {
        System.Random rnd = new System.Random();
        //�ݒ肵���ő�A�ŏ��̂Ȃ��Ń����_���ŕb��������
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
}
