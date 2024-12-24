//这传代码还缺一个功能，就是为了防止玩家将时间轴拖到了范围外，如果拖到了范围外，将强行重置到两个边界

//打点的函数
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineDrag : MonoBehaviour
{

    private static TimelineDrag _instance;
    public static TimelineDrag Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TimelineDrag>();
            }
            return _instance;
        }
    }



  
    [SerializeField] private bool isSelected;
    private Vector2 initialMousePosition;


    // 引用名为"Cam"的父物体
    [SerializeField] private Transform camParent;


    public Transform leftTargetObject;//获取左边边界的位置
    [SerializeField] private Transform rightTargetObject;

    public float temporaryDistance;//这是用来记录每次按下“K”打点的位子

    public float maxDistance;//在一开始就计算左右最大距离
    public float percentage;
    public float xLeftDistance;
    
    public bool DistanceJudge { get; private set; } = false;//这串代码是用来判断鼠标是否超过了UI上可操作范围
                                                            //{ get; private set; }：这部分定义了属性的访问器（accessors）。get访问器是公共的，这意味着您可以从类的外部获取DistanceJudge的值。private set访问器意味着这个属性只能在这个类的内部被设置。它不能从类的外部被修改。
    public Animator timeMarkerAnimator;
    private bool timeLlineMove;


    public bool play;//游戏中的开始模式




    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        Debug.LogWarning("这传代码还缺一个功能，就是为了防止玩家将时间轴拖到了范围外，如果拖到了范围外，将强行重置到两个边界");
        maxDistance = Mathf.Abs(rightTargetObject.position.x - leftTargetObject.position.x);//计算左右时间轴最大距离
        Debug.Log("请注意，这个时间轴的总长度是"+maxDistance);
        timeLlineMove = false;
        transform.position = leftTargetObject.position;//一开始，把“小眼睛”的位置设置到时间轴最左边
        // play = false;
    }
    
    private void Update()
    {
        CalculateMouseXDistance();//持续的检测鼠标和边界的距离
       // Reset();//这个函数，是为了防止玩家将时间轴拖到了范围外，如果拖到了范围外，将强行重置到两个边界
        if (isSelected && DistanceJudge && !timeLlineMove)//如果鼠标超出了边界，那么不再进行可拖拽互动
        {
            Debug.Log($"drag!");
            // 获取当前鼠标位置
            Vector3 screenPos = Input.mousePosition;
            // Debug.Log(Input.mousePosition);+
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            // 确保z坐标与物体当前的z坐标相同
            worldPos.z = transform.position.z;

            // 计算鼠标移动后的新位置，但只考虑x轴的变化
            Vector2 newPosition = new Vector2(worldPos.x, transform.position.y);
            transform.position = newPosition;
        }
        // 检查是否按下了“K”键
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSelected = true;
            // 记录鼠标点击时的屏幕位置
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
        }
    }

/// <summary>
/// 这些代码，是去计算鼠标和左侧的边界的距离到底是多少，并做出一个bool判断
/// </summary>
    private void CalculateMouseXDistance()    // 计算鼠标和左边边界的距离
    {
        // 获取鼠标的屏幕位置
        Vector3 mouseScreenPosition = Input.mousePosition;
        // 将鼠标的屏幕坐标转换为世界坐标
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0; // 因为我们在2D中工作，所以将z坐标设置为0
        // 计算鼠标的x坐标与leftTargetObject的x坐标之间的差值
        xLeftDistance = mouseWorldPosition.x - leftTargetObject.position.x;
        float xRightDistance = rightTargetObject.position.x - mouseWorldPosition.x;

        // 根据xDistance的值设置DistanceJudge
        DistanceJudge = xLeftDistance > 0 && xRightDistance > 0;//如果为正数，则将 DistanceJudge 设置为 true，否则设置为 false
        // Debug.Log(xLeftDistance);
        // 输出到控制台，或者根据需要进行其他操作
        Debug.Log("The x-axis distance between the mouse and the target object is: " + (xLeftDistance / 26.5f * 15f) );
    }

    public void AnimationCheck()//Play / stop按钮会触发这个功能
    {
        timeLlineMove = !timeLlineMove;
        AnimationStatus();
    }

    public void AnimationStatus()//Time marker从左到右的动画控制函数
    {
        // 检查脚本中的布尔值是否为false
        if (!timeLlineMove)
        {
            // 如果是false，则将Animator中的布尔值设置为false
            timeMarkerAnimator.SetBool("TimeLineMove", false);
            // timeMarkerAnimator.Play("play");
            // Debug.Log($"nononoonononononononon!!");ss
        }
        else
        {
            // 如果不是false，可以选择将Animator中的布尔值设置回true，或者保持不变
            timeMarkerAnimator.SetBool("TimeLineMove", true);
            // timeMarkerAnimator.Play("stop");
        }
    }

    // public void PlayMode()
    // {
    //     play = true;
    // }
    //Vector3(-13.138483,-6.90999985,10)
}