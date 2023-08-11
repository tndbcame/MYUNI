using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Animator expand;
    [SerializeField] private Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //ゲーム開始時のアニメーション
        expand.SetTrigger("startExpand");
        StartCoroutine(StartToWait(2f));

    }

    IEnumerator StartToWait(float f)
    {
        yield return new WaitForSeconds(f);
        //エンドレスモードスタート！
        //this.gameObject.GetComponent<EndlessGame>().enabled = true;
        //GameController.toEndlessGame = 1;
        GameController.gameStatus = 1;
        scoreText.enabled = true;
    }
}
