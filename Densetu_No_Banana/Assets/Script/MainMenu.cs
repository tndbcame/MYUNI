using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //アニメーション定義
    [SerializeField] private Animator Shrink;

    public void onStartEndlessGameAsync()
    {
        StartCoroutine(StartEndlessGame());
    }

    IEnumerator StartEndlessGame()
    {
        Shrink.SetTrigger("Shrink");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameScreen");
    }
}
