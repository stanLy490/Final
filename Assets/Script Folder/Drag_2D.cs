using UnityEngine;

public class Drag_2D : MonoBehaviour
{
    [SerializeField] private bool isSelected;
    private Vector2 initialMousePosition;
    private Vector2 initialPositionOffset;
    
    // 静态变量，用于控制所有实例的状态
    public static bool isGamePlaying = false;
    
    public bool moveInXAxis = true;  // true: 只在X轴移动, false: 只在Y轴移动
    public float minX = -10f;        // X轴最小值
    public float maxX = 10f;         // X轴最大值
    public float minY = -5f;         // Y轴最小值
    public float maxY = 5f;          // Y轴最大值

    private void Start()
    {
        isGamePlaying = false;
        Debug.Log($"isGamePlaying: {isGamePlaying}");
        // 在启动时注册到BlockManager
        if (BlockManager.instance != null)
        {
            BlockManager.instance.RegisterDragObject(this);
        }
    }

    private void Update()
    {
        if (isSelected && !isGamePlaying)
        {
            Vector3 screenPos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            worldPos.z = transform.position.z;

            // 计算新位置
            Vector2 newPosition = (Vector2)worldPos - initialPositionOffset;

            // 根据moveInXAxis决定移动方式
            if (moveInXAxis)
            {
                // 只在X轴移动，保持Y轴不变
                newPosition.y = transform.position.y;
                // 限制X轴移动范围
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            }
            else
            {
                // 只在Y轴移动，保持X轴不变
                newPosition.x = transform.position.x;
                // 限制Y轴移动范围
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            }

            transform.position = newPosition;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSelected = true;
            initialMousePosition = Input.mousePosition;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(initialMousePosition);
            initialPositionOffset = new Vector2(worldPoint.x - transform.position.x, worldPoint.y - transform.position.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
        }
    }

    private void OnMouseEnter()
    {
        if(!isGamePlaying)
        {
            transform.localScale += Vector3.one * 1f;
        }
    }

    private void OnMouseExit()
    {
        if(!isGamePlaying)
        {
            transform.localScale -= Vector3.one * 1f;
        }
    }

    // 可选：在Scene视图中显示移动范围
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (moveInXAxis)
        {
            // 显示X轴移动范围
            Gizmos.DrawLine(
                new Vector3(minX, transform.position.y, 0),
                new Vector3(maxX, transform.position.y, 0)
            );
        }
        else
        {
            // 显示Y轴移动范围
            Gizmos.DrawLine(
                new Vector3(transform.position.x, minY, 0),
                new Vector3(transform.position.x, maxY, 0)
            );
        }
    }

    /// <summary>
    /// 开始游戏，禁用所有拖拽功能
    /// 此方法现在由BlockManager调用，不再需要直接绑定到按钮
    /// </summary>
    // public void GamePlay()
    // {
    //     isGamePlaying = true;
    //     Debug.Log($"游戏开始，所有拖拽功能已禁用");
    // }

    // /// <summary>
    // /// 重置游戏，启用所有拖拽功能
    // /// 此方法现在由BlockManager调用，不再需要直接绑定到按钮
    // /// </summary>
    // public void ResetGamePlay()
    // {
    //     isGamePlaying = false;
    //     Debug.Log($"游戏重置，所有拖拽功能已启用");
    // }
}
