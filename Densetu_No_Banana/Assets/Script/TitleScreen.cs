using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using KanKikuchi.AudioManager;

public class TitleScreen : MonoBehaviour
{

    //アニメーション定義
    [SerializeField] private Animator Shrink;

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
}
