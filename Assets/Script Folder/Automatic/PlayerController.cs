using UnityEngine;
using System.Collections;

/// <summary>
/// 角色控制器：负责处理角色的移动、跳跃、死亡等行为
/// </summary>
public class PlayerController : MonoBehaviour
{
    // 角色移动速度
    private float moveSpeed = 5f;
    // 跳跃高度
    public float jumpHeight = 15f;
    // 死亡时向上跳跃的力度
    private float deathJumpForce = 8f;
    // 刚体组件引用
    private Rigidbody2D rb;
    // 胶囊碰撞体组件引用
    private CapsuleCollider2D capsuleCollider;
    // 跳跃音效组件引用
    [SerializeField] private AudioSource audioSourceJump;
    // 死亡音效组件引用
    [SerializeField] private AudioSource audioSourceDeath;
    // 控制角色是否向右移动
    public bool isMoveRight = false;
    // 控制角色是否被冻结（不能移动）
    public bool isFreezed;
    // 控制角色是否存活
    public bool isAlive;
    // 记录角色的原始大小
    private Vector3 originalScale;

    /// <summary>
    /// 初始化：获取必要组件并设置初始状态
    /// </summary>
    private void Start()
    {
        // 获取刚体组件
        rb = GetComponent<Rigidbody2D>();
        // 获取胶囊碰撞体组件
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        // 记录角色的原始大小
        originalScale = transform.localScale;
        // 初始状态设为冻结
        isFreezed = true;
        isAlive = true;
    }

    /// <summary>
    /// 每帧更新：检测输入并控制移动
    /// </summary>
    private void Update()
    {
        // 检测回车键输入
        // if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     isMoveRight = true;
        //     ResetCharacterPos();
        //     Debug.Log($"Key 'Enter' detected");
        // }
        // 控制角色移动
        MoveRight();
    }

    /// <summary>
    /// 控制角色向右移动
    /// 只有在未被冻结且存活的状态下才能移动
    /// </summary>
    private void MoveRight()
    {
        if(isMoveRight && !isFreezed && isAlive)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    /// <summary>
    /// 游戏开始/暂停控制
    /// 切换角色的冻结状态，重置碰撞体
    /// </summary>
    public void Play()
    {
        if(isAlive)
        {
            // 切换冻结状态
            if (!isFreezed)
            {
                // 完全冻结刚体
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                isFreezed = true;
            }
            else
            {
                // 只冻结旋转
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                isFreezed = false;
            }
            isMoveRight = true;
            
            // 启用碰撞体
            if (capsuleCollider != null)
            {
                capsuleCollider.enabled = true;
            }

            // 恢复原始体积和旋转
            // transform.localScale = originalScale;
            transform.rotation = Quaternion.identity;
        }
    }

    /// <summary>
    /// 重置角色位置和状态
    /// </summary>
    public void ResetCharacterPos()
    {
        // 重置位置到起点
        transform.position = new Vector2(-4.5f, 4.0f);
        // 重置旋转
        transform.rotation = Quaternion.identity;
        // 只冻结旋转
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isFreezed = true;
        isMoveRight = false;
        isAlive = true;
        
        // 重新启用碰撞体
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = true;
        }
        
        // 恢复原始体积
        transform.localScale = originalScale;
        
        Debug.Log($"Reset!");
    }

    /// <summary>
    /// 触发器碰撞检测
    /// 处理跳跃触发器和敌人碰撞
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测跳跃触发器
        if (collision.gameObject.tag == "Jump Trigger")
        {
            Jump();
        }
        // 检测敌人碰撞
        else if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    /// <summary>
    /// 角色死亡处理
    /// 旋转90度，向上跳跃，禁用碰撞体，改变体积，3秒后传送
    /// </summary>
    private void Die()
    {
        isAlive = false;
        
        // 播放死亡音效
        if (audioSourceDeath != null && audioSourceDeath.clip != null)
        {
            audioSourceDeath.Play();
        }

        // 立即旋转90度
        transform.Rotate(0, 0, 90f);
        
        // 改变体积（Y轴拉伸为原始大小的2倍）
        Vector3 deathScale = originalScale;
        deathScale.y *= 1.5f;//改变角色死亡后的身体长度
        transform.localScale = deathScale;
        
        // 清除当前速度并施加向上的力
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * deathJumpForce, ForceMode2D.Impulse);
        
        // 禁用碰撞体
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = false;
        }

        // 启动倒计时协程
        StartCoroutine(TeleportAfterDelay());
    }

    /// <summary>
    /// 倒计时3秒后传送的协程
    /// </summary>
    private IEnumerator TeleportAfterDelay()//这是角色死亡后的重生复活点倒计时
    {
        // 等待0.5秒
        yield return new WaitForSeconds(1f);
        
        // 停止所有移动
        rb.velocity = Vector2.zero;
        
        // 传送到指定位置
        transform.position = new Vector2(-4.5f, 4.0f);
        
        // 重新启用碰撞体
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = true;
        }
    }

    /// <summary>
    /// 角色跳跃
    /// </summary>
    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        // 播放跳跃音效
        if (audioSourceJump != null && audioSourceJump.clip != null)
        {
            audioSourceJump.Play();
        }
    }

    /// <summary>
    /// 停止/开始移动
    /// 切换移动状态
    /// </summary>
    public void StopMoving()
    {
        isMoveRight = !isMoveRight;
        Debug.Log(isMoveRight);
    }
}
