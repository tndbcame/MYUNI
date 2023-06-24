using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddPlayer : MonoBehaviour
{
    //canvas
    public GameObject Content;

    //bottun
    public GameObject AddPlayerBottun;

    //inputfield
    public GameObject playerEntryFieldkara;
    public GameObject playerEntryField1;
    public GameObject playerEntryField2;

    private int playerNum;
    public static List<string> playerNameLists;

    public string GetPlayerEntryField(Transform playerEntryField)
    {
        playerNum += 1; 
        GameObject playerEntryFieldText = playerEntryField.Find("Text").gameObject;
        Text inputFieldText = playerEntryFieldText.GetComponent<Text>();
        if(inputFieldText.text == "")
        {
            string defaultPlayerName = "プレーヤー " + playerNum;
            return defaultPlayerName;
        }

        return inputFieldText.text;
    }

    public void OnAddPlayerButtonClicked()
    { 

        GameObject PlayerEntryFieldClone = (GameObject)Instantiate(playerEntryFieldkara);
        PlayerEntryFieldClone.transform.SetParent(Content.transform, false);
        AddPlayerBottun.transform.SetAsLastSibling();
    }

    public void OnNextButtonClicked()
    {
        playerNum = 0;
        List<string> playerNameList = new List<string>();
        foreach (Transform child in Content.transform)
        {
            // 子オブジェクトに対する処理をここに書く
            playerNameList.Add(GetPlayerEntryField(child));
        }
        playerNameList.RemoveAt(playerNameList.Count - 1);

        playerNameLists = playerNameList;
        SceneManager.LoadScene("MultiMenu");
    }

    internal class player
    {
    }
}
