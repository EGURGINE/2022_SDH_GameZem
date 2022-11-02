using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum Btntype
{
    Start,
    Setting,
    Back,
    Sound,
    GameEnd,
}

public class Button : MonoBehaviour
{
    public Btntype currentType;
    public Transform SettingPanner;
    public void Title()
    {
        switch (currentType)
        {
            case Btntype.Start:
                print("start");
                break;
            case Btntype.Setting:
                Setting();
                break;
            case Btntype.Back:
                Back();
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
    public void Setting()
    {
        Vector3 pos = new Vector3(723, 1476, 0);
        SettingPanner.DOMove(pos,1).SetEase(Ease.Unset);
    }

    public void Back()
    {
        Vector3 pos = new Vector3(2717, 1476, 0);
        SettingPanner.DOMove(pos, 1).SetEase(Ease.Unset);
    }
}
