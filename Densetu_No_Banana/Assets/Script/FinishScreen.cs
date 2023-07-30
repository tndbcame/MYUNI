using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScreen : MonoBehaviour
{

    public void onReStart()
    {
        SceneManager.LoadScene("GameScreen");
    }

    public void onToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
