using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    //ゲームのスコア表示用変数
    [SerializeField] private Text score;

    //スコア取得
    private void Start()
    {
        score.text = GameController.__score;
    }
}
