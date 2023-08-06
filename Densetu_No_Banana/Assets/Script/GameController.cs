using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //エンドレスモードフラグ
    public static bool EndlessGameflg = false;
    //boss戦フラグ
    public static bool bossFlg = false;
    //game over フラグ
    public static bool gameOverFlg = false;

    void Start()
    {
        this.gameObject.GetComponent<GameStart>().enabled= true;
    }

    void LateUpdate()
    {
        if(EndlessGameflg && gameOverFlg)
        {
            this.gameObject.GetComponent<GameOver>().enabled = true;
        }
    }
}
