using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    private Vector3 originalScale; // 原始大小
    private float scaleFactor = 2f; // 放大的比例
    private bool canBeDeleted = false; // 是否可以被删除的标志

    [SerializeField] // 添加这个特性使其在Inspector中可见
    private Block _parentBlock; // 私有字段
    public Block parentBlock  // 属性
    {
        get => _parentBlock;
        private set => _parentBlock = value;
    }
    
    public Vector2 blockPos;
    public float keyTime;
    private TimelineDrag timelineDrag;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // anim = GetComponent<Animator>();
        originalScale = transform.localScale; // 保存原始大小
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        // 获取TimelineDrag和TimeLine的实例
        timelineDrag = TimelineDrag.Instance;
    }
/// <summary>
/// 
/// </summary>
/// <param name="blockFromOutSide"></param>
    public void Initiate(Block blockFromOutSide)//我们从Block脚本中的AddNewKey方法中的key.Initiate(this);传进来的
    {
        parentBlock = blockFromOutSide;  // 记录这个 Key 是由哪个 Block 创建的
        SetBlockPosition();
        SetTime();
    }

    public void SetBlockPosition()
    {
        blockPos = parentBlock.transform.position;  // 使用新的变量名
    }

    public void SetTime()                                                                     //需要的数据1
    {
        keyTime = (transform.position.x - timelineDrag.leftTargetObject.position.x) / TimeLine.Instance.maxDistance * TimeLine.Instance.gameTime; //计算打点所对应的时间
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

    void OnMouseDown()
    {
        // 检查是否是当前激活的Block的Key
        if (BlockManager.instance != null && BlockManager.instance.currentActivateBlock == parentBlock)
        {
            // 更新Block的位置到这个Key记录的位置
            parentBlock.transform.position = blockPos;
        }
    }

    void Update()
    {
        // 检查是否是当前激活Block的Key
        if (BlockManager.instance != null)
        {
            bool shouldShow = BlockManager.instance.currentActivateBlock == parentBlock;
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = shouldShow;
            }
        }

        // Debug.Log(canBeDeleted);
        // 检测Delete键是否被按下
        if (canBeDeleted && Input.GetKeyDown(KeyCode.Delete))
        {
            parentBlock.RemoveKey(this);  // 使用新的变量名
            Destroy(gameObject);
        }
    }
}
