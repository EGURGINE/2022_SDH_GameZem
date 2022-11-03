using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorBtn : MonoBehaviour
{
    [SerializeField] EColor btnColor;
    Button button;
    private void Update()
    {
        switch (btnColor)
        {
            case EColor.RED:
                if (Input.GetKeyDown(KeyCode.D))
                {
                    ColorsBtn();
                }
                break;
            case EColor.ORANGE:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ColorsBtn();
                }
                break;
            case EColor.YELLOW:
                if (Input.GetKeyDown(KeyCode.J))
                {
                    ColorsBtn();
                }
                break;
            case EColor.GREEN:
                if (Input.GetKeyDown(KeyCode.K))
                {
                    ColorsBtn();
                }
                break;
        }

    }
    public void ColorsBtn()
    {
        SoundManager.Instance.PlaySound(ESoundSources.Btn);

        GameManager.Instance.Checker(btnColor);
    }
}
