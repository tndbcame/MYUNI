using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class GameTransitionAnime : MonoBehaviour
{

    public void ExpandSE()
    {
        SEManager.Instance.Play(SEPath.EXPAND);
    }
    public void ShrinkSE()
    {
        SEManager.Instance.Play(SEPath.SHRINK);
    }
}
