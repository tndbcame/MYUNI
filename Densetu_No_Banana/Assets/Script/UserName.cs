using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserName : MonoBehaviour
{
    [SerializeField] private GameObject UserNameCanvas;
    [SerializeField] private GameObject inputfield;
    [SerializeField] private Image background1;
    [SerializeField] private Image background2;
    [SerializeField] private Text text1;
    [SerializeField] private Text text2;
    [SerializeField] private Text text3;//書き換えられる変数
    [SerializeField] private Text placePlaceholder;

    [SerializeField] private GameObject button;

    [SerializeField] private Text usernameMain;

    private string username = "";
    private InputField _username;

    void Start()
    {
        if (PlayerPrefs.GetString("UserName","") == "")
        {
            //すべてONにする
            _username = inputfield.transform.GetComponent<InputField>();
            inputfield.transform.GetComponent<Image>().enabled = true;
            inputfield.transform.GetComponent<InputField>().enabled = true;
            background1.enabled = true;
            background2.enabled = true;
            text1.enabled = true;
            text2.enabled = true;
            text3.enabled = true;
            placePlaceholder.enabled = true;
            button.transform.GetComponent<Image>().enabled = true;
            button.transform.GetChild(0).transform.GetComponent<Text>().enabled = true;

            Hashtable hash = new Hashtable();
            hash.Add("x", 2f);
            hash.Add("y", 2f);
            hash.Add("time", 0.8f);
            iTween.ScaleFrom(UserNameCanvas, hash);
        }
    }

    public void OnInputUserName()
    {
        if(_username.text.Length > 0 && _username.text.Length < 9)
        {
            //UserNameを取得
            username = _username.text;
        }
        else
        {
            _username.text = "";
        }

    }

    public void OnSaveUserName()
    {
        //usernameが初期化されている場合
        if(username != "")
        {
            PlayerPrefs.SetString("UserName", username);
            //すべてOFFにする
            _username.text = "";
            inputfield.transform.GetComponent<Image>().enabled = false;
            inputfield.transform.GetComponent<InputField>().enabled = false;
            background1.enabled = false;
            background2.enabled = false;
            text1.enabled = false;
            text2.enabled = false;
            text3.enabled = false;
            placePlaceholder.enabled = false;
            button.transform.GetComponent<Image>().enabled = false;
            button.transform.GetChild(0).transform.GetComponent<Text>().enabled = false;
        }
    }

    public void ChengeUserName()
    {
        //すべてONにする
        _username = inputfield.transform.GetComponent<InputField>();
        inputfield.transform.GetComponent<Image>().enabled = true;
        inputfield.transform.GetComponent<InputField>().enabled = true;
        background1.enabled = true;
        background2.enabled = true;
        text1.enabled = true;
        text2.enabled = true;
        text3.enabled = true;
        placePlaceholder.enabled = true;
        button.transform.GetComponent<Image>().enabled = true;
        button.transform.GetChild(0).transform.GetComponent<Text>().enabled = true;

        Hashtable hash = new Hashtable();
        hash.Add("x", 2f);
        hash.Add("y", 2f);
        hash.Add("time", 0.8f);
        iTween.ScaleFrom(UserNameCanvas, hash);
    }

    public void ChengeUserNameMain()
    {
        usernameMain.text = PlayerPrefs.GetString("UserName", "");
    }
}
