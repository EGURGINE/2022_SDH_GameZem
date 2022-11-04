using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public enum Btntype
{
    Start,
    Setting,
    Back,
    Sound,
    Title,
    VolumeON,
    GameEnd,
}

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject fakeDarumaObj;
    public Btntype currentType;
    [SerializeField] private TextMeshProUGUI text;
    public Sprite[] boolImage; 
    private void Start()
    {
        if (currentType == Btntype.Start)
        {

        text.DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo);
        }
    }
    public void Title()
    {
        SoundManager.Instance.PlaySound(ESoundSources.Btn);

        switch (currentType)
        {
            case Btntype.Start:
                StartGame();
                break;
            case Btntype.Setting:
                Setting();
                break;
            case Btntype.Title:
                GoTitle();
                break;
            case Btntype.VolumeON:
                Volum();
                break;
            case Btntype.GameEnd:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
                break;
        }
    }
    public void StartGame()
    {
        GameManager.Instance.TitleObj.SetActive(false);
        GameManager.Instance.scoreTxt.gameObject.SetActive(true);
        fakeDarumaObj.SetActive(false);
        Camera.main.transform.DOMove(new Vector3(0f ,4.5f,-7.5f), 1f);
        Camera.main.transform.DORotate(new Vector3(20f, 0f, 0f), 1f).OnComplete(() =>
        {
            GameManager.Instance.StartSET();

        });
    }

    public void Volum() 
    {
        if (SoundManager.Instance.soundOn == false)
        {
            SoundManager.Instance.soundOn = true;
            this.GetComponent<Image>().sprite = boolImage[0];
            SoundManager.Instance.PlaySound(ESoundSources.Bgm);
        }
        else
        {
            SoundManager.Instance.soundOn = false;
            this.GetComponent<Image>().sprite = boolImage[1];
            Destroy(SoundManager.Instance.bgm);
        }
    }
    public void Setting()
    {
        GameManager.Instance.SettingWnd();
    }
    public void GoTitle()
    {
        GameManager.Instance.Score = 0;
        fakeDarumaObj.SetActive(true);
        GameManager.Instance.gameOver.gameObject.SetActive(false);
        GameManager.Instance.TitleObj.SetActive(true);
        GameManager.Instance.scoreTxt.gameObject.SetActive(false);
        Camera.main.transform.DOMove(new Vector3(0f, 7f, -7.5f), 1f);
        Camera.main.transform.DORotate(new Vector3(35f, 0f, 0f), 1f);
    }
}
