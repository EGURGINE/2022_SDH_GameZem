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

    public EColor color;

    public void SwitchColor(EColor color)
    {
        this.GetComponent < MeshRenderer >().material = colorsM[((int)color)];
    }
}
