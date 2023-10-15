using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] private Image backgrund;
    [SerializeField] private Text BGMText;
    [SerializeField] private Text KoukaonText;
    [SerializeField] private Text privacyPolicyText;
    [SerializeField] private Image ReturnButton;

    [SerializeField] private GameObject BGMBar;
    [SerializeField] private GameObject KoukaonBar;

    public static float BGMVolume;
    public static float KoukaonVolume;

    void Start()
    {
        BGMVolume = KoukaonBar.GetComponent<Slider>().value / 100;
        KoukaonVolume = BGMBar.GetComponent<Slider>().value / 100;
    }

    public void OnSetting()
    {
        //それぞれenabledする
        backgrund.enabled = true;
        BGMText.enabled = true;
        KoukaonText.enabled = true;
        privacyPolicyText.enabled = true;
        ReturnButton.enabled = true;

        BGMBar.transform.GetChild(0).GetComponent<Image>().enabled = true;
        BGMBar.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().enabled = true;
        BGMBar.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().enabled = true;

        KoukaonBar.transform.GetChild(0).GetComponent<Image>().enabled = true;
        KoukaonBar.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().enabled = true;
        KoukaonBar.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().enabled = true;
    }

    public void OffSetting()
    {
        //それぞれenabledする
        backgrund.enabled = false;
        BGMText.enabled = false;
        KoukaonText.enabled = false;
        privacyPolicyText.enabled = false;
        ReturnButton.enabled = false;

        BGMBar.transform.GetChild(0).GetComponent<Image>().enabled = false;
        BGMBar.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().enabled = false;
        BGMBar.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().enabled = false;

        KoukaonBar.transform.GetChild(0).GetComponent<Image>().enabled = false;
        KoukaonBar.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().enabled = false;
        KoukaonBar.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().enabled = false;
    }

    public void ChengeVolume()
    {
        BGMVolume = BGMBar.GetComponent<Slider>().value / 100;
        KoukaonVolume = KoukaonBar.GetComponent<Slider>().value / 100;
        //BGM全体のボリュームを変更
        BGMManager.Instance.ChangeBaseVolume(BGMVolume);

        //SE全体のボリュームを変更
        SEManager.Instance.ChangeBaseVolume(KoukaonVolume);

        //ログ確認用
        if (KoukaonBar.GetComponent<Slider>().value >= 50 || BGMBar.GetComponent<Slider>().value >= 50)
        {
            Debug.Log("50以上です");
        }
    }
}
