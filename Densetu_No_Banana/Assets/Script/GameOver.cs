using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Animator GameOverAnime;

    void Start()
    {
        GameOverAnime.SetTrigger("startGameOverAnime");
        StartCoroutine(StartToWait(2f));
    }

    IEnumerator StartToWait(float f)
    {
        yield return new WaitForSeconds(f);
        SceneManager.LoadScene("FinishGameScreen");
    }
}
