using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject EndlessGame;
    public static int gameStatus = 0;
    public static int toEndlessGame = 0;
    //public static int __gameStatus = 0;

    public static bool exitEndlessGame = true;
    private bool waitflg = false;

    void Start()
    {
        this.gameObject.GetComponent<GameStart>().enabled= true;
    }

    void LateUpdate()
    {
        switch (gameStatus)
        {
            case 1:
                this.gameObject.GetComponent<EndlessGame>().enabled = true;
                //if (exitEndlessGame)
                //{
                //    exitEndlessGame = false;
                //    GameObject EndlessGameClone = (GameObject)Instantiate(EndlessGame);

                //    //1回目のボス戦以降
                //    if (toEndlessGame == 2)
                //    {
                //        StartCoroutine(StartToWait(5f, EndlessGameClone));
                //    }
                //    //1回目
                //    else if (toEndlessGame == 1)
                //    {
                //        EndlessGameClone.GetComponent<EndlessGame>().enabled = true;
                //    }
                //    //削除
                //    //else if (toEndlessGame == 3)
                //    //{
                //    //    Destroy(EndlessGameClone);
                //    //    gameStatus = __gameStatus;
                //    //}
                //}
                break;

            case 2:
                this.gameObject.GetComponent<BossBattle>().enabled = true;
                break;

            case 3:
                this.gameObject.GetComponent<GameOver>().enabled = true;
                break;
        }

    }
    //IEnumerator StartToWait(float f, GameObject EndlessGameClone)
    //{
    //    yield return new WaitForSeconds(f);
    //    //this.gameObject.GetComponent<EndlessGame>().enabled = true;
    //    EndlessGameClone.GetComponent<EndlessGame>().enabled = true;
    //}
}
