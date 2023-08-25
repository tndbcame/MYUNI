using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Animator GameOverAnime;
    public static bool StartFinishAnimeflg = true;

    void Start()
    {
        if (StartFinishAnimeflg)
        {
            GameOverAnime.SetTrigger("startGameOverAnime");
            StartCoroutine(ToFinishScreen(2f));
        }
        else
        {
            StartCoroutine(StartToWait(2f));
        }

    }

    IEnumerator ToFinishScreen(float f)
    {
        yield return new WaitForSeconds(f);
        SceneManager.LoadScene("FinishGameScreen");
    }

    IEnumerator StartToWait(float f)
    {
        yield return new WaitForSeconds(f);
    }
}
