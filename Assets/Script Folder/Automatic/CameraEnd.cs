using UnityEngine;
using Cinemachine;

/// <summary>
/// 相机结束跟随触发器：当触发器检测到Player标签的物体时，停止指定虚拟相机的跟随
/// </summary>
public class CameraEnd : MonoBehaviour
{
    // 需要控制的虚拟相机，可在Inspector中指定
    public CinemachineVirtualCamera targetCamera;
    private CameraControl cameraControl;

    private void Start()
    {
        // 检查是否已指定目标相机
        if (targetCamera == null)
        {
            Debug.LogError("请在Inspector中指定要控制的虚拟相机！");
            return;
        }

        // 获取CameraControl组件
        cameraControl = targetCamera.GetComponent<CameraControl>();
        if (cameraControl == null)
        {
            Debug.LogError("目标相机缺少CameraControl组件！");
        }
    }

    /// <summary>
    /// 当其他碰撞体进入触发器时调用
    /// 只有当检测到Player标签的物体时才会触发
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞物体是否为玩家
        if (collision.gameObject.CompareTag("Player") && targetCamera != null && cameraControl != null)
        {
            // 通过CameraControl脚本停止相机跟随
            cameraControl.StopFollowing();
        }
    }
}
