using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : Singleton<GameManager>,FeverObserver
{
    [SerializeField] ParticleSystem particle;

    public GameObject TitleObj;
    [SerializeField] private GameObject Ingame;
    public GameObject settingWnd;

    public GameObject gameOver;

    [SerializeField] private GameObject darumaObj;

    public ButtonManager buttonManager;

    public TextMeshProUGUI scoreTxt;

    private List<IObserver> observers = new List<IObserver>();

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

    [SerializeField] Image feverSlider;
    [SerializeField] GameObject feverSliderPC;
    [SerializeField] private float maxFever;
    private float curFever;
    public bool isFeverTime;
    [SerializeField] private ParticleSystem FeverPc;
    [SerializeField] private ParticleSystem FeverBGPc;


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

            float value = (isFeverTime == true) ? 20 : 1;
            FeverValueSET(-Time.deltaTime * value);
        }
    }
    private void Update()
    {
        if(isGameOver == false) InputKey();
    }

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Checker(EColor.RED);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Checker(EColor.ORANGE);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Checker(EColor.YELLOW);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Checker(EColor.GREEN);
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

    public void ColorsBtn(int InputColor)
    {
        SoundManager.Instance.PlaySound(ESoundSources.Btn);

        Checker((EColor)InputColor);
    }
    public void Checker(EColor _color)
    {
        if(isFeverTime == true || Spawner.Instance.blockList[0].isFever == true)
        {
            NextBlock();
            particle.Play();
        }
        else
        {
            if (Spawner.Instance.blockList[0].isSingle == false)
            {
                eColors.Add(_color);

                if (eColors.Count == 2)
                {

                    foreach (var color in Spawner.Instance.blockList[0].isColor)
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

                        particle.Play();
                    }
                    else GameOver();
                }
            }
            else
            {

                if (Spawner.Instance.blockList[0].isColor[0] == _color)
                {
                    NextBlock();
                    particle.Play();
                }
                else
                {
                    GameOver();
                }
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

        if(isFeverTime == false)
        FeverValueSET(5);
    }

    private void FeverTime()
    {
        curFever = maxFever;

        isFeverTime = true;
        feverSliderPC.SetActive(true);
        NotifyObserver();
        FeverPc.Play();
        FeverBGPc.Play();
    }
    private void FeverValueSET(float value)
    {
        curFever += value;

        // Fever
        if (curFever >= maxFever) FeverTime();


        if (curFever <= 0)
        {
            if (isFeverTime == true)
            {
                isFeverTime = false;
                FeverPc.Stop();
                FeverBGPc.Stop();
                feverSliderPC.SetActive(false);
            }
            curFever = 0;
        }

        feverSlider.fillAmount = curFever / maxFever;

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
        curFever = 0;
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
        // ø¨√‚
        gameOver.transform.position = new Vector3(0, 3.8f, -5.9f);
        gameOver.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.Linear);
        score = 0;
    }

    public void ResisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObserver()
    {
        foreach (var observer in observers)
        {
            observer.ColorChange();
        }
    }
}
