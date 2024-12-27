using UnityEngine;

/// <summary>
/// 相机控制器：控制相机的自动移动
/// </summary>
public class CameraMove : MonoBehaviour
{
    // 相机移动速度，可在Inspector中调整
    public float moveSpeed = 5f;
    
    // 是否正在移动
    public bool isMoving = true;

    /// <summary>
    /// 每帧更新相机位置
    /// </summary>
    private void Update()
    {
        if (isMoving)
        {
            // 获取当前位置
            Vector3 currentPosition = transform.position;
            
            // 计算新位置（只在X轴上移动）
            currentPosition.x += moveSpeed * Time.deltaTime;
            
            // 更新相机位置
            transform.position = currentPosition;
        }
    }

    /// <summary>
    /// 开始移动相机
    /// </summary>
    public void StartMoving()
    {
        isMoving = true;
    }

    /// <summary>
    /// 停止移动相机
    /// </summary>
    public void StopMoving()
    {
        isMoving = false;
    }
}
