using UnityEngine;

/// <summary>
/// 视差背景控制器：用于创建2D游戏中的视差滚动效果
/// 通过调整多层背景的移动速度，创造出远近层次感
/// </summary>
public class ParallaxController : MonoBehaviour
{
    // 主相机的Transform组件
    private Transform cam;
    // 相机的初始位置
    private Vector3 camStartPos;
    // 相机从起始位置移动的距离
    private float distance;
    
    // 背景层级对象数组
    private GameObject[] backgrounds;
    // 背景材质数组
    private Material[] mat;
    // 每层背景的移动速度数组
    private float[] backSpeed;
    // 最远背景层与相机的距离
    private float farthestBack;
    
    // 视差效果的基础速度，可在Inspector中调节
    [Range(0.01f, 0.05f)]
    public float parallaxSpeed = 0.02f;

    /// <summary>
    /// 初始化所有必要的组件和变量
    /// </summary>
    private void Start()
    {
        // 获取主相机的Transform组件
        cam = Camera.main.transform;
        camStartPos = cam.position;

        // 获取子对象数量（背景层数）
        int backCount = transform.childCount;
        
        // 初始化数组
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        // 获取所有背景层的游戏对象和材质
        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        // 计算每层背景的移动速度
        BackSpeedCalculate(backCount);
    }

    /// <summary>
    /// 计算每层背景的移动速度
    /// 根据背景与相机的距离计算相对移动速度
    /// </summary>
    /// <param name="backCount">背景层数量</param>
    private void BackSpeedCalculate(int backCount)
    {
        // 找出最远的背景层
        for (int i = 0; i < backCount; i++)
        {
            float currentDistance = backgrounds[i].transform.position.z - cam.position.z;
            if (currentDistance > farthestBack)
            {
                farthestBack = currentDistance;
            }
        }

        // 根据距离设置每层背景的移动速度
        // 距离越远，移动速度越慢，创造视差效果
        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    /// <summary>
    /// 在每帧渲染后更新背景位置
    /// 使用LateUpdate确保在相机移动后再更新背景
    /// </summary>
    private void LateUpdate()
    {
        // 计算相机移动的距离
        distance = cam.position.x - camStartPos.x;
        
        // 更新背景容器的位置，跟随相机移动
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        // 更新每层背景的纹理偏移，创造视差效果
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
