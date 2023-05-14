using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraSetting : Singleton<CameraSetting>
{

    private Vector3[] titleTransform = { new Vector3(0f, 7f, -7.5f), new Vector3(35f, 0f, 0f) };
    private Vector3[] ingameTransform = { new Vector3(0f, 4.5f, -7.5f), new Vector3(20f, 0f, 0f) };

    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }
    public void IngameMove()
    {
        Camera.main.transform.DOMove(ingameTransform[0], 1f);
        Camera.main.transform.DORotate(ingameTransform[1], 1f).OnComplete(() =>
        {
            GameManager.Instance.StartSET();
        });
    }
    public void TitleMove()
    {
        Camera.main.transform.DOMove(titleTransform[0], 1f);
        Camera.main.transform.DORotate(titleTransform[1], 1f);
    }
    public void CameraShake()
    {
        StartCoroutine(Shake());
    }


    private IEnumerator Shake()
    {
        float t = 0;

        // 유지 시간
        while (t < 0.5f) 
        {
            yield return null;
            t += Time.deltaTime;

            Camera.main.transform.localPosition = Random.insideUnitSphere * 0.075f + ingameTransform[0];
        }
        Camera.main.transform.localPosition = ingameTransform[0];
    }
}
