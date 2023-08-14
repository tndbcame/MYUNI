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

    [SerializeField] private Text banaPowerText;
    [SerializeField] private Image bana;

    // Start is called before the first frame update
    void Start()
    {
        if (MainMenu.GameMode == 0)
        {
            if (!GameController.notFirstTimeFlg)
            {
                //ゲーム開始時のアニメーション
                expand.SetTrigger("startExpand");
                StartCoroutine(StartToEndless(2f));
            }
            else
            {
                Left.GetComponent<SpriteRenderer>().enabled = false;
                Right.GetComponent<SpriteRenderer>().enabled = false;
                Top.GetComponent<SpriteRenderer>().enabled = false;
                Bottom.GetComponent<SpriteRenderer>().enabled = false;
                KirinukiBanana.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(StartToEndless(0.5f));
            }
        }
        if (MainMenu.GameMode == 1)
        {
            if (!GameController.notFirstTimeFlg)
            {
                //ゲーム開始時のアニメーション
                expand.SetTrigger("startExpand");
                StartCoroutine(StartToBigBanana(2f));
            }
            else
            {
                StartCoroutine(StartToBigBanana(0.5f));
            }
        }





    }

    IEnumerator StartToEndless(float f)
    {
        yield return new WaitForSeconds(f);
        //エンドレスモードスタート！
        GameController.gameStatus = 1;
        scoreText.enabled = true;
        banaPowerText.enabled = true;
        bana.enabled = true;
    }

    IEnumerator StartToBigBanana(float f)
    {
        yield return new WaitForSeconds(f);
        //エンドレスモードスタート！
        GameController.gameStatus = 2;
        scoreText.enabled = true;
    }
}
