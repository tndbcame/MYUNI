using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using NCMB;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowRanking : MonoBehaviour
{
    [SerializeField] private GameObject Trofy;

    [SerializeField] private GameObject Content;

    //ランキング表示用レコード
    [SerializeField] private GameObject rankingRecord;

    [SerializeField] private Text gameModeLabel;

    private int LabelFlg = 1;

    
    private void Start()
    {
        ShowRecord();
    }

    public void ShowRecord()
    {
        int rank = 1;
        string GameMode = "EndlessScore";
        string RankingClass = "endlessRankingClass";


        if (LabelFlg == 1)
        {
            GameMode = "EndlessScore";
            RankingClass = "endlessRankingClass";

        }
        else if (LabelFlg == 2)
        {
            GameMode = "DekabananaScore";
            RankingClass = "dekabananaRankingClass";
        }

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(RankingClass);
        query.Limit = 100;
        //ゲームモード順に並び替える
        query.OrderByDescending(GameMode);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogWarning("error: " + e.ErrorMessage);
            }
            else
            {
                //1位をここで表示する
                rankingRecord.transform.GetComponent<Text>().text =
                "[rank" + rank + "]: " + (objList[0]["UserName"]) + " " + (objList[0][GameMode]);

                //2位以降はクローンで表示させる
                for (int i = 1; i < objList.Count; i++)
                {
                    rank = i + 1;

                    //ここでクローン生成
                    GameObject rankingRecordClone = (GameObject)Instantiate(rankingRecord);

                    //タグを付与
                    rankingRecordClone.tag = "clone";

                    //クローンしたテキストに代入する
                    rankingRecordClone.transform.GetComponent<Text>().text =
                    "[rank" + rank + "]: " + (objList[i]["UserName"]) + " " + (objList[i][GameMode]);

                    //fitさせる
                    rankingRecordClone.transform.SetParent(Content.transform, false);
                }
            }
        });
    }

    public void chengeGameMode()
    {
        if(LabelFlg == 1)
        {
            SEManager.Instance.Play(SEPath.KOUKAON1);
            GameObject[] clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (GameObject clone in clones)
            {
                Destroy(clone);
            }
            LabelFlg = 2;
            gameModeLabel.text = "でかばなな";
            ShowRecord();
        }
        else if(LabelFlg == 2)
        {
            SEManager.Instance.Play(SEPath.KOUKAON1);
            GameObject[] clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (GameObject clone in clones)
            {
                Destroy(clone);
            }
            LabelFlg = 1;
            gameModeLabel.text = "えんどれす";
            ShowRecord();
        } 
    }

    public void onRankingScreen()
    {
        SEManager.Instance.Play(SEPath.KOUKAON1);
        Trofy.SetActive(true);
    }

    public void offRankingScreen()
    {
        SEManager.Instance.Play(SEPath.KOUKAON1);
        Trofy.SetActive(false);
    }
}
