using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject Ingame;
    [SerializeField] private GameObject gameOver;

    [SerializeField] private TextMeshProUGUI scoreTxt;

    [SerializeField] private TextMeshProUGUI highScoreTxt;
    public float highScore;

    private float score;
    public float Score 
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
    private void FixedUpdate()
    {
        if (isGameOver == false)
        {
            TimeOver += Time.deltaTime;
        }
    }


    private void Start()
    {
        StartSET();
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

    private bool check1;
    private bool check2;

    public void Checker(EColor _color)
    {
        if (Spawner.Instance.blockList[0].isColor.Length == 2)
        {
            eColors.Add(_color);

            if (eColors.Count == 2)
            {

                foreach (var item in Spawner.Instance.blockList[0].isColor)
                {
                    if (item == eColors[0]) check1 = true;
                    if (item == eColors[1]) check2 = true;

                }
                if (check1 == true && check2 == true)
                {
                    check1 = false;
                    check1 = false;
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

    public void NextBlock()
    {
        Score += 50;
        TimeOver -= 1;
        SetTimeValue();
        Spawner.Instance.Next();
    }


    public void StartSET()
    {
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
        }
        highScoreTxt.text = highScore.ToString();

        Ingame.SetActive(false);
        gameOver.SetActive(true);
        // ø¨√‚
    }
}
