using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayer : MonoBehaviour
{
    //canvas
    public GameObject Canvas;

    //bottun
    public GameObject AddPlayerBottun;
    private Transform AddPlayerBottunPos;
    private Vector2 AddPlayerBottunPosY;

    //inputfield
    public GameObject PlayerEntryField;

    private int clickCount = 0;

    public void OnAddPlayerButtonClicked()
    {
        if (clickCount  == 0)
        {
            //AddPlayerBottunPosの初期化
            AddPlayerBottunPos = AddPlayerBottun.transform;
            AddPlayerBottunPosY = AddPlayerBottunPos.position;

            clickCount += 1;
        }


        //PlayerEntryFieldのクローン生成
        GameObject PlayerEntryFieldClone = (GameObject)Instantiate(PlayerEntryField);
        PlayerEntryFieldClone.transform.SetParent(Canvas.transform, false);
        Transform PlayerEntryFieldClonePos = PlayerEntryFieldClone.transform;
        PlayerEntryFieldClonePos.position = AddPlayerBottunPos.position;

        //AddPlayerBottunの位置を下げる
        AddPlayerBottunPosY.y -= 30;
        AddPlayerBottunPos.position = AddPlayerBottunPosY;
    }

}
