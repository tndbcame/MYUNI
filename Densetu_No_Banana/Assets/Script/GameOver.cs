using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Animator GameOverAnime;
    public static bool StartFinishAnimeflg;

    void Start()
    {
        if (StartFinishAnimeflg)
        {
            GameOverAnime.SetTrigger("startGameOverAnime");
            StartCoroutine(ToFinishScreen(2f));
        }
        else
        {
            GameController.gameStatus = 0;
            StartCoroutine(ToFinishScreen(2f));
        }

    }

    IEnumerator ToFinishScreen(float f)
    {
        yield return new WaitForSeconds(f);
        SceneManager.LoadScene("FinishGameScreen");
    }
}
