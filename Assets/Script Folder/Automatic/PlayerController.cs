using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 50f;
    private Rigidbody2D rb;
    public bool isMoveRight = false;
    public bool isFreezed = false;

    private void Start()
    {
        isFreezed = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveRight();
    }

    private void MoveRight()
    {
        if(isMoveRight && !isFreezed)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    public void Play()//和play按钮相连接
    {
        if (!isFreezed)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isFreezed = true;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            isFreezed = false;
        }
        isMoveRight = true;
    }

    public void ResetCharacterPos()
    {
        transform.position = new Vector2(-4.5f, 1.0f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isFreezed = false;
        isMoveRight = false;
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

    // public void StopMoving()
    // {
    //     isMoveRight = !isMoveRight;
    //     Debug.Log(isMoveRight);
    // }
}