using UnityEngine;

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


    // 引用两个物体的Transform组件
    [SerializeField] private Transform objectA;
    [SerializeField] private Transform leftBar;
    [SerializeField] private Transform rightBar;
    public float maxDistance;
    public float gameTime;//游戏的总时长

    private void Start()//这一行函数是为了计算整个时间轴的总长度
    {
        maxDistance = Mathf.Abs(rightBar.position.x - leftBar.position.x);
        Debug.Log(maxDistance);
    }

    // 计算两个物体的x轴距离
    // public float CalculateXDistance()
    // {
    //     if (objectA == null || leftBar == null)
    //     {
    //         Debug.LogError("One or both of the objects are not assigned.");
    //         return 0f;
    //     }

    //     float xDistance = Mathf.Abs(objectA.position.x - leftBar.position.x);
    //     Debug.Log(xDistance);
    //     return xDistance;
    // }
}