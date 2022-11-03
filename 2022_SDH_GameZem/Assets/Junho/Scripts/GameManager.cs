using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class GameManager : Singleton<GameManager>
{
    public GameObject TitleObj;
    [SerializeField] private GameObject Ingame;
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
                }
                else GameOver();
            }
        }
        else
        {

            if (Spawner.Instance.blockList[0].isColor[0] == _color)
            {
                NextBlock();

            }
            else
            {
                GameOver();
            }
        }

    }
    private void LevelDesign() 
    {
        if (score > 2000)
        {
            //두개 확률 업
        }
        else if (score > 2000)
        {
            //가끔 두개
        }
        else
        {
            //단색
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
        gameOver.SetActive(false);

        timeNum = 0;
        maxTime = 3f;
        Score = 0;
        TimeOver = 0;

        Spawner.Instance.blockList.Clear();
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


        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        highScoreTxt.text = highScore.ToString();

        Ingame.SetActive(false);
        gameOver.SetActive(true);
        // 연출
    }
}
