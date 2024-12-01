///////////////////////////////////////////////下拉功能实现

// using UnityEngine;

// public class EscapeKeyTogglePull : MonoBehaviour
// {
//     public Animator animator; // 指向Animator组件的引用

//     void Start()
//     {
//         // 确保在Inspector中设置了animator变量
//         if (animator == null)
//         {
//             Debug.LogError("Animator is not assigned!");
//         }
//     }

//     void Update()
//     {
//         // 检查是否按下了ESC键
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             // 切换Pull布尔值
//             animator.SetBool("Pull", !animator.GetBool("Pull"));
//         }
//     }
// }





using UnityEngine;

public class EscapeKeyTogglePull : MonoBehaviour
{
    public Animator animator; // 指向Animator组件的引用

    void Start()
    {
        // 确保在Inspector中设置了animator变量
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned!");
        }
    }

    void Update()
    {
        // 检查是否按下了ESC键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 切换Pull布尔值
            animator.SetBool("Pull", !animator.GetBool("Pull"));
        }
    }
}