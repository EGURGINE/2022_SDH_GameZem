using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ESoundSources
{
    Hammer,
    GameOver,
    Btn,
    Bgm
}
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private List<AudioClip> audioSources = new List<AudioClip>();
    [SerializeField] private Image btn;


    public float soundVolum;
    public bool soundOn;

    public AudioSource bgm;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("SoundOn"))
        {
            if (PlayerPrefs.GetInt("SoundOn") == 0)
            {
                soundOn = false;
                btn.sprite = GameManager.Instance.buttonManager.boolImage[1];
            }
            else
            {
                soundOn = true;
                btn.sprite = GameManager.Instance.buttonManager.boolImage[0];
            }
        }
        else soundOn = true;


        PlaySound(ESoundSources.Bgm);
    }

    public void PlaySound(ESoundSources source)
    {
        if (soundOn == false) return;

        GameObject go = new GameObject(source + "Sound");

        AudioSource audio = go.AddComponent<AudioSource>();
        audio.clip = audioSources[((int)source)];

        if (source == ESoundSources.Bgm)
        {
            bgm = audio;
            audio.loop = true;
        }
        audio.Play();

        if (source != ESoundSources.Bgm)
            Destroy(go, audio.clip.length);
    }

    private void OnApplicationQuit()
    {
        if (soundOn) PlayerPrefs.SetInt("SoundOn", 1);
        else PlayerPrefs.SetInt("SoundOn", 0);
    }
}
