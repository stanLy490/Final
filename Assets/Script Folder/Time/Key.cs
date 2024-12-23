using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    private Vector3 originalScale; // 原始大小
    private float scaleFactor = 2f; // 放大的比例
    private bool canBeDeleted = false; // 是否可以被删除的标志

    private Block myBlock;
    public Vector2 blockPos;
    public float keyTime;//计算当前的时间



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        // anim = GetComponent<Animator>();
        originalScale = transform.localScale; // 保存原始大小
    }


/// <summary>
/// 
/// </summary>
/// <param name="blockFromOutSide"></param>
    public void Initiate(Block blockFromOutSide)//我们从Block脚本中的AddNewKey方法中的key.Initiate(this);传进来的
    {
        myBlock  = blockFromOutSide;//我们把Block这个对象传过来了 
        SetBlockPosition();
        SetTime();
    }



    public void SetBlockPosition()
    {
        blockPos = myBlock.transform.position;
    }



    public void SetTime()
    {
        keyTime = TimelineDrag.Instance.xLeftDistance / TimeLine.Instance.maxDistance * TimeLine.Instance.gameTime; //计算打点所对应的时间
        Debug.Log("当前打的关键帧的时间是：" + keyTime);
    }



    void OnMouseEnter()//Mouse有关的代码，是为了删除标记点的一些列方法
    {
        // 鼠标悬停时放大物体
        transform.localScale = originalScale * scaleFactor;
        canBeDeleted = true; // 设置物体可以被删除
    }

    void OnMouseExit()
    {
        // 鼠标离开时恢复原始大小
        transform.localScale = originalScale;
        canBeDeleted = false;
    }

    void Update()
    {
        // Debug.Log(canBeDeleted);
        // 检测Delete键是否被按下
        if (canBeDeleted && Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(gameObject); // 删除物体
        }
    }

}
