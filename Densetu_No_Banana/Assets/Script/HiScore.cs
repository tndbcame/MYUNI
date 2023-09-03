using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScore : MonoBehaviour
{
    //ハイスコア
    [SerializeField] private Text HiScoreText;

    // Start is called before the first frame update
    void Start()
    {
        //第二引数：セーブデータが取得されなかった場合に取得される値
        HiScoreText.text = "ハイスコア：" + PlayerPrefs.GetInt("HiScore", 0);
    }
}
