using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreSave : MonoBehaviour
{
    //canvas
    public GameObject Content;

    //bottun
    //public GameObject AddPlayerBottun;

    //inputfield
    public GameObject playerEntryFieldkara;
    public GameObject playerEntryField2;

    private int playerNum;
    public static List<string> playerNameLists;

    private void Start()
    {
        OnAddPlayerButtonClicked();
    }

    public void OnAddPlayerButtonClicked()
    { 

        GameObject PlayerEntryFieldClone = (GameObject)Instantiate(playerEntryFieldkara);
        PlayerEntryFieldClone.transform.SetParent(Content.transform, false);
        //AddPlayerBottun.transform.SetAsLastSibling();
    }

}
