using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] private Block blockObj;
    [SerializeField] private GameObject blocks;
    private Stack<Block> blockStack = new Stack<Block>();
    public List<Block> blockList = new List<Block>();

    private void Start()
    {
        CreateObj();
        StartGame();
    }

        int i;
    public void StartGame()
    {
        for (int i = 0; i < 4; i++)
        {
            Pop();
        }
    }


    private void CreateObj()
    {
        for (int i = 0; i < 10; i++)
        {
            Block obj = Instantiate(blockObj,blocks.transform);
            obj.transform.position = Vector3.zero;
            blockStack.Push(obj);
            obj.gameObject.SetActive(false);
        }

    }

    public void Push(Block _this)
    {
        _this.transform.position = Vector3.zero;
        _this.transform.parent = blocks.transform;
        blockStack.Push(_this);
        _this.gameObject.SetActive(false);
    }
    public void Pop()
    {
        EColor ranColor = (EColor)Random.Range(0, 4);

        Block obj = blockStack.Pop();
        blockList.Add(obj);
        obj.transform.position = Vector3.zero;
        obj.transform.parent = null;
        obj.SwitchColor(ranColor);
        obj.color = ranColor;
        obj.gameObject.SetActive(true);
        foreach (var item in blockList)
        {
            item.transform.position = new Vector2(0,i);
            i++;
        }
        i = 0;
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

        // ÀÌµ¿ 
    }
}
