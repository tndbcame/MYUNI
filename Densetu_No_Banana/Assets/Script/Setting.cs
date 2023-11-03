using Hypertext;
using KanKikuchi.AudioManager;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private Image backgrund;
    [SerializeField] private Text BGMText;
    [SerializeField] private Text KoukaonText;
    [SerializeField] private Text privacyPolicyText;
    [SerializeField] private Image ReturnButton;
    [SerializeField] private RegexHypertext privacyPolicy;

    [SerializeField] private GameObject BGMBar;
    [SerializeField] private GameObject KoukaonBar;
    [SerializeField] private GameObject username;

    public static float BGMVolume;
    public static float KoukaonVolume;

    const string RegexURL = "http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?";

    void Start()
    {
        BGMVolume = KoukaonBar.GetComponent<Slider>().value / 100;
        KoukaonVolume = BGMBar.GetComponent<Slider>().value / 100;
        
    }

    public void OnSetting()
    {
        SEManager.Instance.Play(SEPath.KOUKAON1);

        //それぞれenabledする
        backgrund.enabled = true;
        BGMText.enabled = true;
        KoukaonText.enabled = true;
        privacyPolicyText.enabled = true;
        ReturnButton.enabled = true;
        privacyPolicy.enabled = true;

        BGMBar.transform.GetChild(0).GetComponent<Image>().enabled = true;
        BGMBar.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().enabled = true;
        BGMBar.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().enabled = true;

        KoukaonBar.transform.GetChild(0).GetComponent<Image>().enabled = true;
        KoukaonBar.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().enabled = true;
        KoukaonBar.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().enabled = true;

        username.transform.GetComponent<Image>().enabled = true;
        username.transform.GetChild(0).transform.GetComponent<Text>().enabled = true;

        //urlを表示
        ShowURL();
    }

    public void OffSetting()
    {
        SEManager.Instance.Play(SEPath.KOUKAON1);

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

        username.transform.GetComponent<Image>().enabled = false;
        username.transform.GetChild(0).transform.GetComponent<Text>().enabled = false;

        privacyPolicy.text = "";
        privacyPolicy.enabled = false;
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

    public void ShowURL()
    {
        privacyPolicy.text = "https://bpro3413privacy.hatenablog.com/";
        privacyPolicy.OnClick(RegexURL, new Color32(156, 203, 194, 255), url => OpenBrowser(url));
    }

    public void OpenBrowser(string url)
    {
        Application.OpenURL(url);
    }
}
