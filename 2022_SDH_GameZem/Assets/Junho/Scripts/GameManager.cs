using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] ParticleSystem particle;

    public GameObject TitleObj;
    [SerializeField] private GameObject Ingame;
    public GameObject settingWnd;

    public GameObject gameOver;

    [SerializeField] private GameObject darumaObj;

    public TextMeshProUGUI scoreTxt;

    [SerializeField] private TextMeshProUGUI highScoreTxt;
    public int highScore;

    private int score;
    public int Score 
    { 
        get 
        { 
            return score; 
        }
        set 
        { 
            score = value; 
            scoreTxt.text = score.ToString();
        }
    }

    [SerializeField] Slider timeOverSlider;

    [SerializeField] private float maxTime;
    private float timeOver;
    public float TimeOver
    {
        get 
        { 
            return timeOver; 
        }
        set 
        { 
            timeOver = value;
            if (timeOver < 0) timeOver = 0;

            if (timeOver >= maxTime) GameOver();

            timeOverSlider.value = value / maxTime;
        }
    }
    public bool isGameOver;

    private void Start()
    {
        isGameOver = true;
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }else  highScore = 0;
    }
    private void FixedUpdate()
    {
        if (isGameOver == false)
        {
            TimeOver += Time.deltaTime;
        }
    }

    private int timeNum;
    public void SetTimeValue()
    {
        if (maxTime <= 2) return;

        timeNum++;
        if (timeNum == 2)
        {
            timeNum = 0;
            maxTime -= 0.1f;
        }
    }

    public List<EColor> eColors = new List<EColor>();

    private bool check;

    public void Checker(EColor _color)
    {
        if (Spawner.Instance.blockList[0].isColor.Length == 2)
        {
            eColors.Add(_color);

            if (eColors.Count == 2)
            {

                foreach (var item in Spawner.Instance.blockList[0].isColor)
                {
                    if (Spawner.Instance.blockList[0].isColor[0] == eColors[0] 
                        && Spawner.Instance.blockList[0].isColor[1] == eColors[1]
                        || Spawner.Instance.blockList[0].isColor[0] == eColors[1]
                        && Spawner.Instance.blockList[0].isColor[1] == eColors[0]
                        ) check = true;

                }
                if (check == true)
                {
                    check = false;
                    eColors.Clear();
                    NextBlock();
                    ParticleSystem pc = Instantiate(particle);
                    pc.transform.position = Vector3.zero;
                    pc.Play();
                }
                else GameOver();
            }
        }
        else
        {

            if (Spawner.Instance.blockList[0].isColor[0] == _color)
            {
                NextBlock();
                ParticleSystem pc = Instantiate(particle);
                pc.transform.position = Vector3.zero;
                pc.Play();
            }
            else
            {
                GameOver();
            }
        }

    }
    private bool isSetting;
    public void SettingWnd()
    {
        if (isSetting == false)
        {
            Time.timeScale = 0;
            isSetting = true;
            settingWnd.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isSetting = false;
            settingWnd.SetActive(false);
        }
    }

    public bool LevelDesign() 
    {
        if (score > 4000)
        {
            int ranNUm = Random.Range(0, 3);
            if (ranNUm == 0)
            {
                return false;
            }
            else return true;
        }
        else if (score > 2000)
        {
            int ranNUm = Random.Range(0, 8);
            if (ranNUm == 0)
            {
                return false;
            }
            else return true;
        }
        else
        {
            return true;
        }
    }
    public void NextBlock()
    {
        SoundManager.Instance.PlaySound(ESoundSources.Hammer);
        Score += 50;
        TimeOver -= 1;
        SetTimeValue();
        Spawner.Instance.Next();
    }


    public void StartSET()
    {
        darumaObj.SetActive(true);

        isGameOver = false;
        Ingame.SetActive(true);
        Score = 0;
        timeNum = 0;
        maxTime = 3f;
        TimeOver = 0;
        gameOver.SetActive(false);
        Spawner.Instance.StartGame();
    }
    public void GameOver()
    {
        SoundManager.Instance.PlaySound(ESoundSources.GameOver);


        darumaObj.SetActive(false);
        isGameOver = true;
        eColors.Clear();
        Spawner.Instance.darumaObj.SetActive(false);
        foreach (var item in Spawner.Instance.blockList)
        {
            Spawner.Instance.Push(item);
        }
        Spawner.Instance.blockList.Clear();


        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        highScoreTxt.text = highScore.ToString();

        Ingame.SetActive(false);
        gameOver.SetActive(true);
        // ????
        gameOver.transform.position = new Vector3(0, 3.8f, -5.9f);
        gameOver.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.Linear);
        score = 0;
    }
}
