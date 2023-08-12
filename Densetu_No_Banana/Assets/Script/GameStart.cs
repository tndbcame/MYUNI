using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Animator expand;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject Left;
    [SerializeField] private GameObject Right;
    [SerializeField] private GameObject Top;
    [SerializeField] private GameObject Bottom;
    [SerializeField] private GameObject KirinukiBanana;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameController.toEndless)
        {
            //ゲーム開始時のアニメーション
            expand.SetTrigger("startExpand");
            StartCoroutine(StartToWait(2f));
        }
        else
        {
            Left.GetComponent<SpriteRenderer>().enabled = false;
            Right.GetComponent<SpriteRenderer>().enabled = false;
            Top.GetComponent<SpriteRenderer>().enabled = false;
            Bottom.GetComponent<SpriteRenderer>().enabled = false;
            KirinukiBanana.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(StartToWait(0.5f));
        }
        

    }

    IEnumerator StartToWait(float f)
    {
        yield return new WaitForSeconds(f);
        //エンドレスモードスタート！
        GameController.gameStatus = 1;
        scoreText.enabled = true;
    }
}
