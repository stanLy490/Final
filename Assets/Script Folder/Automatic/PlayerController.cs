using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 角色移动速度
    public float jumpHeight = 50f;
    private Rigidbody2D rb;
    public bool isMoveRight = false;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isMoveRight = true;
    }

    private void Update()
    {
        // 检查玩家是否按下Enter键
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isMoveRight = true;
            Reset();
            Debug.Log($"Key 'Enter' detected");
        }
        MoveRight();
    }

    private void MoveRight()
    {
        if(isMoveRight)
        {
            // Debug.Log($"keep moving");
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        // 使角色朝右边移动
        
    }




/// <summary>
/// 跳跃控制函数
/// </summary>
/// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的物体是否是Jump Trigger
        if (collision.gameObject.tag == "Jump Trigger")
        {
            Jump();
        }
    }

    private void Jump()
    {
        // 使角色向上跳跃
         rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);  
    }



    private void Reset()
    {
        transform.position = new Vector2(-4.5f, 0.25f);
        Debug.Log($"Reset!");
    }


}