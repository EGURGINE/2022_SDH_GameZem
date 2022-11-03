using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
public enum Btntype
{
    Start,
    Setting,
    Back,
    Sound,
    Title,
    GameEnd,
}

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject fakeDarumaObj;
    public Btntype currentType;
    public Transform SettingPanner;
    [SerializeField] private TextMeshProUGUI text;

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
            case Btntype.Back:
                Back();
                break;
            case Btntype.Title:
                GoTitle();
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
    public void Setting()
    {
        Vector3 pos = new Vector3(723, 1476, 0);
        SettingPanner.DOMove(pos,1).SetEase(Ease.Unset);
    }
    public void GoTitle()
    {
        fakeDarumaObj.SetActive(true);
        GameManager.Instance.gameOver.gameObject.SetActive(false);
        GameManager.Instance.TitleObj.SetActive(true);
        GameManager.Instance.scoreTxt.gameObject.SetActive(false);
        Camera.main.transform.DOMove(new Vector3(0f, 7f, -7.5f), 1f);
        Camera.main.transform.DORotate(new Vector3(35f, 0f, 0f), 1f);
    }
    public void Back()
    {
        Vector3 pos = new Vector3(2717, 1476, 0);
        SettingPanner.DOMove(pos, 1).SetEase(Ease.Unset);
    }
}
