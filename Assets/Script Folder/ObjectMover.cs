using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 物体移动速度
    public Vector3 moveDirection = Vector3.forward; // 物体移动方向
    public float moveDuration = 2.0f; // 物体移动持续时间

    private float elapsedTime = 0.0f; // 已经过去的时间
    private bool isMoving = false; // 是否正在移动

    // void Update()
    // {
    //     // 检查玩家是否按下了空格键
    //     if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
    //     {
    //         isMoving = true;
    //         elapsedTime = 0.0f; // 重置已经过去的时间
    //     }

    //     // 如果正在移动
    //     if (isMoving)
    //     {
    //         // 计算移动距离
    //         float distanceThisFrame = moveSpeed * Time.deltaTime;
    //         Vector3 moveVector = moveDirection * distanceThisFrame;

    //         // 更新物体位置
    //         transform.position += moveVector;

    //         // 更新已经过去的时间
    //         elapsedTime += Time.deltaTime;

    //         // 如果移动时间结束，停止移动
    //         if (elapsedTime >= moveDuration)
    //         {
    //             isMoving = false;
    //         }
    //     }
    // }
}