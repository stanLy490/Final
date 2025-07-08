using UnityEngine;
using UnityEngine.UI; // 用于按钮组件
using UnityEngine.SceneManagement; // 用于场景管理

public class Cheater : MonoBehaviour
{
    private Button cheaterButton;
    
    void Start()
    {
        // 获取按钮组件
        cheaterButton = GetComponent<Button>();
        
        // 添加按钮点击事件监听
        if (cheaterButton != null)
        {
            cheaterButton.onClick.AddListener(SkipToScene5);
        }
    }

    public void SkipToScene5()
    {
        // 加载第五个场景（索引为4，因为场景索引从0开始）
        SceneManager.LoadScene(5);
    }
}
