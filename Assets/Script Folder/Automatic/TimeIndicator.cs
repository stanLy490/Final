using UnityEngine;

public class TimeIndicator : MonoBehaviour
{
    [SerializeField] private Transform startPoint;  // 起始点
    [SerializeField] private Transform endPoint;    // 终点
    private float maxDistance;    // 从 Timeline 获取的最大距离
    private float gameTime;       // 从 Timeline 获取的总时间
    public float currentTime;    // 当前计时
    private bool isMoving;        // 是否正在移动
    private Vector3 lastPosition; // 记录上次停止时的位置

    private void Start()
    {
        // 从 Timeline 获取参数
        TimeLine timeline = FindObjectOfType<TimeLine>();
        if (timeline != null)
        {
            maxDistance = timeline.maxDistance;
            gameTime = timeline.gameTime;
        }

        // 初始化位置和状态
        if (startPoint != null)
        {
            transform.position = new Vector3(startPoint.position.x, transform.position.y, transform.position.z);
            lastPosition = transform.position;
        }
        isMoving = false;
        currentTime = 0f;
    }

    private void Update()
    {
        if (isMoving)
        {
            // 更新计时器
            currentTime += Time.deltaTime;
            
            // 计算移动进度
            float progress = currentTime / gameTime;
            
            // 计算新位置
            float newX = Mathf.Lerp(startPoint.position.x, endPoint.position.x, progress);//运动的功能的方法，progress应该是从0~1的。1就是到达的最终位置
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

            // 检查是否到达终点
            if (currentTime >= gameTime)
            {
                isMoving = false;
                currentTime = gameTime;
            }
        }
    }

    // 由 UI 按钮调用的切换方法
    public void ToggleMovement()
    {
        if (!isMoving)
        {
            // 开始移动
            isMoving = true;
            lastPosition = transform.position;
        }
        else
        {
            // 停止移动
            isMoving = false;
            lastPosition = transform.position;
        }
    }

    // 重置位置和时间（可选）
    public void Reset()
    {
        Debug.Log($"Reset detected");
        isMoving = false;
        currentTime = 0f;
        if (startPoint != null)
        {
            transform.position = new Vector3(startPoint.position.x, transform.position.y, transform.position.z);
            lastPosition = transform.position;
        }
    }
}