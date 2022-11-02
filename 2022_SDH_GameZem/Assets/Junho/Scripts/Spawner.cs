using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : Singleton<Spawner>
{
    public GameObject darumaObj;

    [SerializeField] private Block[] blockObj;
    [SerializeField] private GameObject blocks;
    private Stack<Block> blockStack = new Stack<Block>();
    public List<Block> blockList = new List<Block>();

    [SerializeField] private GameObject hammer;

    private int posY;

    private int isRight;

    private void Start()
    {
        CreateObj();
        isRight = -1;
    }

    public void StartGame()
    {
        darumaObj.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            Pop();
        }
    }


    private void CreateObj()
    {
        for (int i = 0; i < 10; i++)
        {
            Block obj = Instantiate(blockObj[Random.Range(0,2)],blocks.transform);
            obj.transform.position = Vector3.zero;
            blockStack.Push(obj);
            obj.gameObject.SetActive(false);
        }

    }

    public void Push(Block _this)
    {
        _this.GetComponent<Transform>().DORestart(true);

        _this.transform.parent = blocks.transform;
        blockStack.Push(_this);
        _this.gameObject.SetActive(false);
    }
    public void Pop()
    {

        Block obj = blockStack.Pop();
        blockList.Add(obj);
        obj.transform.position = Vector3.zero;
        obj.transform.parent = null;

        EColor ranColor = (EColor)Random.Range(0, 4);
        obj.SwitchColor(ranColor);
        obj.gameObject.SetActive(true);
        foreach (var item in blockList)
        {
            item.transform.Rotate(0, Random.Range(10, -10), 0);
            item.transform.position = new Vector2(0, posY * 0.6f);
            posY++;
        }
        Camera.main.transform.DOShakePosition(1,0.1f); 
        hammer.transform.DORotate(new Vector3(0, 85 * isRight, 0), 0.1f);

        if (isRight > 0)
        {
            isRight = -1;
        }
        else isRight = 1;
        posY = 0;
    }
    public void Next()
    {
        blockStack.Push(blockList[0]);
        blockList[0].transform.parent = blocks.transform;
        blockList[0].gameObject.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
            {
                blockList.Remove(blockList[i]);
                Pop();
                break;
            }else
            blockList[i] = blockList[i + 1];
        }

        // �̵� 
    }
}
