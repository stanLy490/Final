using UnityEngine;
public class Drag_2D : MonoBehaviour

{
    [SerializeField] private bool isSelected;
    private Vector2 initialMousePosition;
    private Vector2 initialPositionOffset;
    // public Rect movementArea; // 定义矩形区域。限制玩家可操作范围
    


    private void Update()
    {
        if (isSelected)
        {
            Vector3 screenPos = Input.mousePosition;
            // Debug.Log(Input.mousePosition.x + "-" +  Input.mousePosition.y);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            // 确保z坐标与物体当前的z坐标相同
            worldPos.z = transform.position.z;
            // 计算鼠标移动后的新位置
            Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(screenPos) - initialPositionOffset;
            // newPosition.x = Mathf.Clamp(newPosition.x, movementArea.xMin, movementArea.xMax);//限制范围
            // newPosition.y = Mathf.Clamp(newPosition.y, movementArea.yMin, movementArea.yMax);
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
}


        // 使用Gizmos在Scene视图中绘制movementArea
    // void OnDrawGizmos()
    // {
    //     // Gizmos.color = Color.red;
    //     // Gizmos.DrawWireCube(transform.position, new Vector3(movementArea.width, movementArea.height, 0));

    //     // Draw a yellow sphere at the transform's position
    //     // Gizmos.color = Color.yellow;//测试画图功能是否被激活
    //     // Gizmos.DrawSphere(transform.position, 1);
    
    // }

    

