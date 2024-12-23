using UnityEngine;

public class Drag_2D : MonoBehaviour
{
    [SerializeField] private bool isSelected;
    private Vector2 initialMousePosition;
    private Vector2 initialPositionOffset;
    
    // [SerializeField] private Transform leftBar;

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