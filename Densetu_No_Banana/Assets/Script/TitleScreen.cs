using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using KanKikuchi.AudioManager;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{

    //アニメーション定義
    [SerializeField] private Animator Shrink;
    [SerializeField] private Text username;
    public void TransitionToMainMenu()
    {
        SEManager.Instance.Play(SEPath.KOUKAON1);
        StartCoroutine(StartEndlessGame());
    }

    IEnumerator StartEndlessGame()
    {
        Shrink.SetTrigger("Shrink");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        string _usename = PlayerPrefs.GetString("UserName", "");
        if (_usename != "")
        {
            //usernameを表示
            username.text = _usename;
            username.enabled = true;
        }
    }
}
