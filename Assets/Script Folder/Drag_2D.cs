using UnityEngine;
public class Drag_2D : MonoBehaviour

{
    [SerializeField] private bool isSelected;
    private Vector2 initialMousePosition;
    private Vector2 initialPositionOffset;
    // public Rect movementArea; // 定义矩形区域。限制玩家可操作范围
    


    public bool moveInXAxis = true;  // true: 只在X轴移动, false: 只在Y轴移动
    public float minX = -10f;        // X轴最小值
    public float maxX = 10f;         // X轴最大值
    public float minY = -5f;         // Y轴最小值
    public float maxY = 5f;          // Y轴最大值



    private void Update()
    {
        if (isSelected)
        {
            Vector3 screenPos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            // 确保z坐标与物体当前的z坐标相同
            worldPos.z = transform.position.z;
            // 计算鼠标移动后的新位置
            Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(screenPos) - initialPositionOffset;

            if (moveInXAxis)//这段代码，分别实现了对于物体在X轴和Y轴的移动限制
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
            // 记录鼠标点击时的屏幕位置
            initialMousePosition = Input.mousePosition;
            // 计算物体中心到鼠标点击点的偏移量
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
        transform.localScale += Vector3.one * 1f;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * 1f;
    }


    private void OnDrawGizmos()//这一部分函数并没有实际功能，它只会在Scene场景里面显示一个区域  
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
}

    

