using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject fakeDarumaObj;
    [SerializeField] private TextMeshProUGUI text;
    public Sprite[] boolImage; 
    [SerializeField] private Image boolImageObj; 
    private void Start()
    {
        text.DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo);
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
            boolImageObj.sprite = boolImage[0];
            SoundManager.Instance.PlaySound(ESoundSources.Bgm);
        }
        else
        {
            SoundManager.Instance.soundOn = false;
            boolImageObj.sprite = boolImage[1];
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
