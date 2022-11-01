using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBtn : MonoBehaviour
{
    [SerializeField] EColor btnColor;
    public void ColorsBtn()
    {
        if (Spawner.Instance.blockList[0].color == btnColor)
        {
            GameManager.Instance.Score += 50;
            GameManager.Instance.TimeOver -= 1;
            Spawner.Instance.Next();
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }
}
