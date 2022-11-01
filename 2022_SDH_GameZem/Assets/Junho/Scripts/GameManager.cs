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

            if (score > 500)
            {
                maxTime = 5f;
            }
            else if ( score > 300)
            {
                maxTime = 8f;
            }
            else if(score > 100)
            {
                maxTime = 10f;
            }
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

    public void StartSET()
    {
        isGameOver = false;
        Ingame.SetActive(true);
        gameOver.SetActive(false);

        maxTime = 15f;
        Score = 0;
        TimeOver = 0;

        Spawner.Instance.blockList.Clear();
        Spawner.Instance.StartGame();
    }
    public void GameOver()
    {
        isGameOver = true;

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
