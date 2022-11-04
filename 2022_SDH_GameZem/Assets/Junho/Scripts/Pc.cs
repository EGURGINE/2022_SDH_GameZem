using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pc : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject,0.5f);
    }
    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
