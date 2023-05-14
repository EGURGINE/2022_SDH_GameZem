using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public enum EColor
{
    RED,
    ORANGE,
    YELLOW,
    GREEN,
    Rainbow
}
public class Block : MonoBehaviour,IObserver
{
    [SerializeField] Material[] colorsM;
    public bool isSingle;
    public bool isFever;
    public EColor[] isColor;

    [SerializeField] private MeshRenderer[] myMaterials;

    private List<EColor> eColors = new List<EColor>();

    public void SwitchColor(EColor color)
    {
        if(GameManager.Instance.isFeverTime == true)
        {
            isFever = true;
            ColorChange();
            return;
        }else isFever = false;



        if (isSingle)
        {
            // �Ѱ� ���϶� �� �ٲٱ�
            myMaterials[0].material = colorsM[((int)color)];
            isColor[0] = color;
        }
        else
        {
            // �ΰ� ���϶� �� �ٲٱ�
            for (int i = 0; i < 4; i++)
            {
                eColors.Add((EColor)i);
            }
            eColors.Remove(color);
            EColor ranColor = eColors[Random.Range(0, 3)];
            isColor[0] = color;
            isColor[1] = ranColor;

            myMaterials[0].material = colorsM[((int)color)];
            myMaterials[1].material = colorsM[((int)ranColor)];
            eColors.Clear();
        }
    }

    public void ColorChange()
    {
        if (isSingle)
        {
            myMaterials[0].material = colorsM[(int)(EColor.Rainbow)];
        }
        else
        {
            myMaterials[0].material = colorsM[(int)(EColor.Rainbow)];
            myMaterials[1].material = colorsM[(int)(EColor.Rainbow)];
        }
    }
}
