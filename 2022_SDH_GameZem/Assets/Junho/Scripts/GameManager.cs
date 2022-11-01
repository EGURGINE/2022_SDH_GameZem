using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] TextMeshProUGUI scoreTxt;

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

            if (timeOverSlider.value > maxTime) GameOver();

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
        maxTime = 15f;
        Score = 0;
        TimeOver = 0;
    }
    public void GameOver()
    {
        isGameOver = true;

        if (score > highScore)
        {
            highScore = score;
        }
        // ø¨√‚
    }
}
