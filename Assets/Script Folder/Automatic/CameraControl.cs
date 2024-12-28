using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public bool isPlay = true; // 控制相机模式
    public float scrollSpeed = 10f; // 滚动速度
    public float minX = -10f; // X轴最小值
    public float maxX = 10f;  // X轴最大值
    
    private CinemachineVirtualCamera virtualCamera;
    private Transform originalFollow;
    private Transform originalLookAt;
    public bool shouldFollow = false; // 是否应该跟随目标

    private void Start()
    {
        // 获取虚拟相机组件
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera == null)
        {
            Debug.LogError("CameraControl脚本需要CinemachineVirtualCamera组件！");
            return;
        }

        // 保存原始的Follow和LookAt目标
        originalFollow = virtualCamera.Follow;
        originalLookAt = virtualCamera.LookAt;
    }

    private void Update()
    {
        if (isPlay && shouldFollow)
        {
            IsPlayCamera();
        }
        else
        {
            // 清除Follow和LookAt
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;

            // 只有在非跟随模式时才允许滚轮控制
            if (!isPlay && !shouldFollow)
            {
                // 获取鼠标滚轮输入
                float scrollInput = Input.GetAxis("Mouse ScrollWheel");
                if (scrollInput != 0)
                {
                    // 计算新位置
                    Vector3 newPosition = transform.position;
                    newPosition.x += scrollInput * scrollSpeed;
                    
                    // 限制在范围内
                    newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                    
                    // 应用新位置
                    transform.position = newPosition;
                }
            }
        }
    }

    private void IsPlayCamera()
    {
        // 恢复Follow和LookAt
        virtualCamera.Follow = originalFollow;
        virtualCamera.LookAt = originalLookAt;
    }

    /// <summary>
    /// 停止相机跟随
    /// </summary>
    public void StopFollowing()//这个功能和CameraEnd相互关联
    {
        shouldFollow = false;
        virtualCamera.Follow = null;
        virtualCamera.LookAt = null;
    }

    /// <summary>
    /// 恢复相机跟随
    /// </summary>
    public void ResumeFollowing()
    {
        shouldFollow = true;
    }

    public void Play()
    {
        isPlay = true;
        shouldFollow = true;
    }

    public void Reset()
    {
        isPlay = false;
        shouldFollow = false;
    }

    // 在Scene视图中显示移动范围
    private void OnDrawGizmos()
    {
        // 绘制移动范围线
        Gizmos.color = Color.yellow;
        Vector3 currentPos = transform.position;
        
        // 绘制左边界
        Vector3 leftBound = new Vector3(minX, currentPos.y, currentPos.z);
        Gizmos.DrawWireSphere(leftBound, 0.5f);
        
        // 绘制右边界
        Vector3 rightBound = new Vector3(maxX, currentPos.y, currentPos.z);
        Gizmos.DrawWireSphere(rightBound, 0.5f);
        
        // 绘制连接线
        Gizmos.DrawLine(leftBound, rightBound);
    }
}
