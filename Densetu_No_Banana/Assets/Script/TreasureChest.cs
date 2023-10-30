using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour
{
    //選択時の表示
    [SerializeField] private GameObject selectionPoint1;
    [SerializeField] private GameObject selectionPoint2;
    [SerializeField] private GameObject selectionPoint3;
    [SerializeField] private GameObject selectionPoint4;
    [SerializeField] private GameObject selectionPoint5;

    //サルの画像
    [SerializeField] private GameObject Tresure1;
    [SerializeField] private GameObject Tresure2;
    [SerializeField] private GameObject Tresure3;
    [SerializeField] private GameObject Tresure4;
    [SerializeField] private GameObject Tresure5;

    //バックグラウンド、☓ボタン
    [SerializeField] private Image backGround;
    [SerializeField] private Image BatuButton;

    public static int FinishScreenMonkeyImage = 1;


    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("EndlessHiscore", 0) >= 100)
        {
            //まんぷくざる開放
            PlayerPrefs.SetInt("manpukuMonkey", 1);
            Tresure1.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        }
        if (PlayerPrefs.GetInt("EndlessHiscore", 0) >= 200)
        {
            //逃げざる開放
            PlayerPrefs.SetInt("nigeMonkey", 1);
            Tresure2.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (PlayerPrefs.GetInt("DekabananaHiscore", 0) >= 25)
        {
            //へとへとザル開放
            PlayerPrefs.SetInt("hetohetoMonkey", 1);
            Tresure3.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (PlayerPrefs.GetInt("DekabananaHiscore", 0) >= 50)
        {
            //悲しむサル開放
            PlayerPrefs.SetInt("sadMonkey", 1);
            Tresure4.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

    }

    public void OnTresureChest()
    {
        //宝箱部品をenabledにする。
        switch (FinishScreenMonkeyImage)
        {
            case 1:
                selectionPoint1.transform.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 2:
                selectionPoint2.transform.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 3:
                selectionPoint3.transform.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 4:
                selectionPoint4.transform.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 5:
                selectionPoint5.transform.GetComponent<SpriteRenderer>().enabled = true;
                break;
        }
        

        Tresure1.transform.GetComponent<Image>().enabled = true;
        Tresure2.transform.GetComponent<Image>().enabled = true;
        Tresure3.transform.GetComponent<Image>().enabled = true;
        Tresure4.transform.GetComponent<Image>().enabled = true;
        Tresure5.transform.GetComponent<Image>().enabled = true;

        Tresure1.transform.GetChild(0).GetComponent<Text>().enabled = true;
        Tresure2.transform.GetChild(0).GetComponent<Text>().enabled = true;
        Tresure3.transform.GetChild(0).GetComponent<Text>().enabled = true;
        Tresure4.transform.GetChild(0).GetComponent<Text>().enabled = true;
        Tresure5.transform.GetChild(0).GetComponent<Text>().enabled = true;

        Tresure1.transform.GetChild(1).GetComponent<Text>().enabled = true;
        Tresure2.transform.GetChild(1).GetComponent<Text>().enabled = true;
        Tresure3.transform.GetChild(1).GetComponent<Text>().enabled = true;
        Tresure4.transform.GetChild(1).GetComponent<Text>().enabled = true;

        backGround.enabled = true;
        BatuButton.enabled = true;
    }

    public void OffTresureChest()
    {
        //宝箱部品をunenabledにする。
        selectionPoint1.transform.GetComponent<SpriteRenderer>().enabled = false;
        selectionPoint2.transform.GetComponent<SpriteRenderer>().enabled = false;
        selectionPoint3.transform.GetComponent<SpriteRenderer>().enabled = false;
        selectionPoint4.transform.GetComponent<SpriteRenderer>().enabled = false;
        selectionPoint5.transform.GetComponent<SpriteRenderer>().enabled = false;

        Tresure1.transform.GetComponent<Image>().enabled = false;
        Tresure2.transform.GetComponent<Image>().enabled = false;
        Tresure3.transform.GetComponent<Image>().enabled = false;
        Tresure4.transform.GetComponent<Image>().enabled = false;
        Tresure5.transform.GetComponent<Image>().enabled = false;

        Tresure1.transform.GetChild(0).GetComponent<Text>().enabled = false;
        Tresure2.transform.GetChild(0).GetComponent<Text>().enabled = false;
        Tresure3.transform.GetChild(0).GetComponent<Text>().enabled = false;
        Tresure4.transform.GetChild(0).GetComponent<Text>().enabled = false;
        Tresure5.transform.GetChild(0).GetComponent<Text>().enabled = false;

        Tresure1.transform.GetChild(1).GetComponent<Text>().enabled = false;
        Tresure2.transform.GetChild(1).GetComponent<Text>().enabled = false;
        Tresure3.transform.GetChild(1).GetComponent<Text>().enabled = false;
        Tresure4.transform.GetChild(1).GetComponent<Text>().enabled = false;

        backGround.enabled = false;
        BatuButton.enabled = false;
    }

    public void SwitchSelectedButton1()
    {
        selectionPoint1.transform.GetComponent<SpriteRenderer>().enabled = true;
        selectionPoint2.transform.GetComponent<SpriteRenderer>().enabled = false;
        selectionPoint3.transform.GetComponent<SpriteRenderer>().enabled = false;
        selectionPoint4.transform.GetComponent<SpriteRenderer>().enabled = false;
        selectionPoint5.transform.GetComponent<SpriteRenderer>().enabled = false;

        FinishScreenMonkeyImage = 1;
    }
    public void SwitchSelectedButton2()
    {
        if(PlayerPrefs.GetInt("manpukuMonkey") == 1)
        {
            selectionPoint1.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint2.transform.GetComponent<SpriteRenderer>().enabled = true;
            selectionPoint3.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint4.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint5.transform.GetComponent<SpriteRenderer>().enabled = false;

            FinishScreenMonkeyImage = 2;
        }

    }
    public void SwitchSelectedButton3()
    {
        if (PlayerPrefs.GetInt("nigeMonkey") == 1)
        {
            selectionPoint1.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint2.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint3.transform.GetComponent<SpriteRenderer>().enabled = true;
            selectionPoint4.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint5.transform.GetComponent<SpriteRenderer>().enabled = false;

            FinishScreenMonkeyImage = 3;
        }
    }
    public void SwitchSelectedButton4()
    {
         if (PlayerPrefs.GetInt("hetohetoMonkey") == 1)
         {
            selectionPoint1.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint2.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint3.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint4.transform.GetComponent<SpriteRenderer>().enabled = true;
            selectionPoint5.transform.GetComponent<SpriteRenderer>().enabled = false;

            FinishScreenMonkeyImage = 4;
        }
    }
    public void SwitchSelectedButton5()
    {
          if (PlayerPrefs.GetInt("sadMonkey") == 1)
          {
            selectionPoint1.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint2.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint3.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint4.transform.GetComponent<SpriteRenderer>().enabled = false;
            selectionPoint5.transform.GetComponent<SpriteRenderer>().enabled = true;

            FinishScreenMonkeyImage = 5;
        }
    }
}
