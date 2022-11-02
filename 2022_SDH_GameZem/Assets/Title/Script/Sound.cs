using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Sound : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Slider AudioSlider;
    public TextMeshProUGUI valuetext;

    public void Start()
    {
        valuetext = GetComponent<TextMeshProUGUI>();
    }

    public void masterAudioControl()
    {
        float sound = AudioSlider.value;

        if (sound == 0f) audiomixer.SetFloat("Master", -80);
        else audiomixer.SetFloat("Master", sound);
    }
    public void BackgroundAudioControl()
    {
        float sound = AudioSlider.value;

        if (sound == 0f) audiomixer.SetFloat("BackGround", -80);
        else audiomixer.SetFloat("BackGround", sound);
    }
    public void SFXAudioContrxol()
    {
        float sound = AudioSlider.value;

        if (sound == 0f) audiomixer.SetFloat("SFX", -80);
        else audiomixer.SetFloat("SFX", sound);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
    public void AudioCheck(float value)
    {
        valuetext.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
