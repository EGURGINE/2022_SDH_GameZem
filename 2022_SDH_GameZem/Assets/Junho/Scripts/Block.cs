using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor
{
    RED,
    ORANGE,
    YELLOW,
    GREEN
}
public class Block : MonoBehaviour
{
    [SerializeField] Material[] colorsM;
    public bool isSingle;
    public EColor[] isColor;

    private List<EColor> eColors = new List<EColor>();
    public void SwitchColor(EColor color)
    {
        if (isSingle)
        {
            this.GetComponent <MeshRenderer>().material = colorsM[((int)color)];
            isColor[0] = color;
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                eColors.Add((EColor)i);
            }
            eColors.Remove(color);
            EColor ranColor = eColors[Random.Range(0, 3)];
            isColor[0] = color;
            isColor[1] = ranColor;

            transform.GetChild(0).GetComponent<MeshRenderer>().material = colorsM[((int)color)];
            transform.GetChild(1).GetComponent<MeshRenderer>().material = colorsM[((int)ranColor)];
            eColors.Clear();
        }
    }
}
