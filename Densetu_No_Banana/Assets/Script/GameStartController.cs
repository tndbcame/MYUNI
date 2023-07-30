using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartController : MonoBehaviour
{
    [SerializeField] private Animator expand;
    [SerializeField] private GameObject endlessGame;
    [SerializeField] private GameObject score;
    // Start is called before the first frame update
    void Start()
    {
        expand.SetTrigger("startExpand");
        StartCoroutine(StartToWait(2f));

    }

    //private void StartAnimationDestroy()
    //{
    //    Destroy(right);
    //    Destroy(left);
    //    Destroy(top);
    //    Destroy(bottom);
    //    Destroy(expand);
    //}

    IEnumerator StartToWait(float f)
    {
        yield return new WaitForSeconds(f);
        endlessGame.SetActive(true);
        score.SetActive(true);
    }
}
