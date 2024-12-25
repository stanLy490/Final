using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Block : MonoBehaviour
{
    private float currentTime = 0f; // 当前计时
    private int currentKeyIndex = 0; // 当前key的索引
    public List<Key> keyList = new List<Key>();
    public GameObject keyPrefab; // 钥匙预制体
    public TimelineDrag timelineDrag;
 
    public bool play;//游戏中的开始模式

    public void Start()
    {
        play = false;
        // 在Start中注册当前Block到BlockManager
        if (BlockManager.instance != null)
        {
            BlockManager.instance.RegisterBlock(this);
        }
    }

    public void AddNewKey() 
    {
        Transform parent = GameObject.Find("TimeLine").transform;
        GameObject newKeyObject = Instantiate(keyPrefab,Vector3.zero,Quaternion.identity,parent);
        // newKeyObject.transform.localPosition = GameObject.Find("Time Marker").transform.localPosition;
        Transform timeMarkerTransform = GameObject.Find("Time Marker").transform;
        // 设置newKeyObject的位置，使其在Time Marker的x和z轴相同位置，y轴下方一点
        newKeyObject.transform.localPosition = new Vector3(timeMarkerTransform.localPosition.x, timeMarkerTransform.localPosition.y - 2.0f, timeMarkerTransform.localPosition.z);


        Key KeyComponent;
        KeyComponent = newKeyObject.GetComponent<Key>();
            if (KeyComponent != null) 
            {
                KeyComponent.Initiate(this); // 假设Key类有一个Initiate方法接受Block类型的参数
                keyList.Add(KeyComponent);
                // 根据每个key的时间属性对keyList进行排序
                // keyList.Sort((x, y) => x.Time.CompareTo(y.Time));
                Debug.Log($"AddNewwKey detected");
            } 
            else 
            {
                Debug.LogError("Key component not found on the prefab.");
            }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !BlockManager.instance.isPlaying)
        {
            if (BlockManager.instance.currentActivateBlock == this)
            {
                AddNewKey();
            }
        }
        if(play)
        {
            currentTime += Time.deltaTime;
            Debug.Log(currentTime);
            UpdateObjectPosition();
        }
    }

    public void StartPlayMode()
    {
        keyList.Sort((x, y) => x.keyTime.CompareTo(y.keyTime));
        play = true;
        currentTime = 0f;
        currentKeyIndex = 0;
        // transform.position = new Vector2(13.6400003f, 0.189999998f);
    }

    public void StopPlayMode()
    {
        play = false;
        currentTime = 0f;
        currentKeyIndex = 0;
    }

    private void UpdateObjectPosition()
    {
        if (currentKeyIndex < keyList.Count)
        {
            Key currentKey = keyList[currentKeyIndex];
            if (currentTime >= currentKey.keyTime)
            {
                // 移动物体到blockPos的位置
                MoveObjectToPosition(currentKey.blockPos);
                // 移动到下一个key
                currentKeyIndex++;
                currentTime = 0f; // 重置计时器
            }
            else
            {
                // 平滑移动物体
                MoveObjectSmoothly(currentKey);
            }
        }
    }

    private void MoveObjectToPosition(Vector3 targetPosition)
    {
        // 直接移动自己
        transform.position = targetPosition;
    }

    private void MoveObjectSmoothly(Key currentKey)
    {
        // 直接对自己进行插值移动
        Vector3 lerpTarget = Vector3.Lerp(transform.position, currentKey.blockPos, (currentTime / currentKey.keyTime));
        transform.position = lerpTarget;
    }

    public void RemoveKey(Key keyToRemove)//和key脚本连接，按下delete键会删除列表里的这个元素
    {
        if (keyList.Contains(keyToRemove))
        {
            keyList.Remove(keyToRemove);
        }
    }

    private void OnMouseDown()//这个本和BlockManager相关联，用来管理哪个Block会被激活
    {
        if (BlockManager.instance != null)
        {
            BlockManager.instance.ActivateBlock(this);
        }
    }
}
