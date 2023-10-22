using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScore : MonoBehaviour
{
    //ハイスコア
    [SerializeField] private Text EndlessHiScoreText;
    [SerializeField] private Text DekabananaHiScoreText;

    // Start is called before the first frame update
    void Start()
    {
        //第二引数：セーブデータが取得されなかった場合に取得される値
        EndlessHiScoreText.text = "えんどれす：" + PlayerPrefs.GetInt("EndlessHiscore", 0);
        DekabananaHiScoreText.text = "でかばなな：" + PlayerPrefs.GetInt("DekabananaHiscore", 0);
    }
}
