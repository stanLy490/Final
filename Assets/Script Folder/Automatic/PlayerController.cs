using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 50f;
    private Rigidbody2D rb;
    public bool isMoveRight = false;
    private bool isFreezed;  // 新增：记录是否被冻结

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isFreezed = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isMoveRight = true;
            ResetCharacterPos();
            Debug.Log($"Key 'Enter' detected");
        }
        MoveRight();
    }

    private void MoveRight()
    {
        if(isMoveRight && !isFreezed)  // 只有在未被冻结时才移动
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    // UI Play按钮调用此方法
    public void Play()
    {
        if (!isFreezed)
        {
            // 如果当前未冻结，则冻结
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isFreezed = true;
        }
        else
        {
            // 如果当前已冻结，则解除冻结（只允许旋转被冻结）
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            isFreezed = false;
        }
        isMoveRight = true;  // 设置移动标志
    }

    public void ResetCharacterPos()
    {
        transform.position = new Vector2(-4.5f, 1.0f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;  // 重置时解除位置冻结
        isFreezed = false;
        isMoveRight = false;  // 重置移动状态
        Debug.Log($"Reset!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jump Trigger")
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    public void StopMoving()
    {
        isMoveRight = !isMoveRight;
        Debug.Log(isMoveRight);
    }
}