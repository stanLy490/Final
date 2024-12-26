using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景控制器：负责管理场景的切换和加载
/// 使用单例模式确保整个游戏中只有一个场景控制器实例
/// </summary>
public class SceneController : MonoBehaviour
{
    // 静态实例，用于全局访问场景控制器
    public static SceneController instance;

    /// <summary>
    /// Awake 在对象被实例化时立即调用，用于初始化单例
    /// 在 Start 函数之前和脚本实例的生命周期中只调用一次
    /// </summary>
    private void Awake()
    {
        // 检查是否已经存在场景控制器实例
        if (instance == null)
        {
            // 如果不存在实例，将当前对象设置为单例实例
            instance = this;
            // 确保切换场景时不销毁这个游戏对象
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 如果已存在实例，销毁当前对象以维护单例
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 加载下一个场景
    /// 通过当前场景的索引号 +1 来加载下一个场景
    /// 注意：场景需要在 Build Settings 中按顺序添加
    /// </summary>
    public void NextLevel()
    {
        // 获取当前激活场景的索引号并加载索引号+1的场景
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// 通过场景名称加载指定场景
    /// </summary>
    /// <param name="sceneName">要加载的场景名称，必须与 Build Settings 中的场景名称匹配</param>
    public void LoadScene(string sceneName)
    {
        // 异步加载指定名称的场景
        SceneManager.LoadSceneAsync(sceneName);
    }
}
