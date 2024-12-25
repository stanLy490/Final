using UnityEngine;
using TMPro; // 如果需要显示时间，添加这个引用

public class TimeLine : MonoBehaviour
{
    private static TimeLine _instance;
    public static TimeLine Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TimeLine>();
            }
            return _instance;
        }
    }

    // 计时器相关变量
    private float currentTime = 0f;
    private bool isPlaying = false;
    public TextMeshProUGUI timeText; // 可选：用于显示时间的UI文本组件

    // 引用两个物体的Transform组件
    [SerializeField] private Transform objectA;
    [SerializeField] private Transform leftBar;
    [SerializeField] private Transform rightBar;
    public float maxDistance;
    public float gameTime;

    private void Start()
    {
        maxDistance = Mathf.Abs(rightBar.position.x - leftBar.position.x);
        ResetTimer();
    }

    private void Update()
    {
        if (isPlaying)
        {
            currentTime += Time.deltaTime;
            UpdateTimeDisplay();
            Debug.Log(currentTime);
        }
    }

    // 开始/暂停计时
    public void TogglePlay()
    {
        isPlaying = !isPlaying;
    }

    // 重置计时器
    public void ResetTimer()
    {
        isPlaying = false;
        currentTime = 0f;
        UpdateTimeDisplay();
    }

    // 获取当前时间
    public float GetCurrentTime()
    {
        return currentTime;
    }

    // 更新时间显示（可选）
    private void UpdateTimeDisplay()
    {
        if (timeText != null)
        {
            float minutes = Mathf.Floor(currentTime / 60);
            float seconds = Mathf.Floor(currentTime % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}