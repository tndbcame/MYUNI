using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

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
    //何秒に1回バナナを生成する決めるときの変数
    [SerializeField] private double sec;

    //バナナの出現ポジションリスト
    List<Vector2> bananaAprPosList = new List<Vector2>();
    //バナナリスト
    Dictionary<int, GameObject> bananasList = new Dictionary<int, GameObject>();

    //ランダムインスタンス生成
    System.Random random = new System.Random();


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
    }

    async void Update()
    {
        await BananaLife(sec);
    }

    //バナナの人生
    async UniTask BananaLife(double sec)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(sec));

        //バナナのClone生成
        GameObject bananasClone = (GameObject)Instantiate(bananas);
        Debug.Log("バナナを生成しました。");




    }

    //バナナをタップされた回数を数える処理

    //posListの中からランダムにバナナの位置を入れ替える処理
    private void replace_bananas()
    {
        int rnd = random.Next(bananaAprPosList.Count);

        //bananaAprPosListから取得して削除
        Vector2 pos = bananaAprPosList[rnd];
        bananaAprPosList.RemoveAt(rnd);
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
        int __transitionCount = bananaAprPosList.Count - transitionCount;
        int min = 0;

        //段階の応じてランダムで生成されるバナナを決める
        for (int i = 0; i < __transitionCount; i++)
        {
            min = random.Next(min, bananasList.Count + 1 + i - __transitionCount);
            BananaIndex.Add(min);
        }
        return BananaIndex;
    }
}
