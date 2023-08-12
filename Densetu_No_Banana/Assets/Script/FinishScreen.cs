using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private Text YourScore;

    void Start()
    {
        YourScore.text = string.Format("あなたのスコア\n{0}", GameController.shinybananaCount);
        GameController.shinybananaCount = 0;
    }

    public void onReStart()
    {
        SceneManager.LoadScene("GameScreen");
    }

    public void onToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
