// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;

// public class Block {
    // private List<Key> keyList = new List<Key>();

    

//     public void AddNewKey()
//     {
        
//         // GameObject newKeyObject 
//         // newKeyObject = GameObject.Instantiate() // 通过Assest目录下建一个Resources文件夹，把prefab放在里面，就可以通过名字来生成
//         Key key;
//         // KeyComponent  = newKeyObject.GetComponent<Key>()
//         // KeyComponent.Initiate(this); //this是指正在使用AddNewKey方法的“这个”对象

//         // keyList.Add(KeyComponent);
//         // list应该做一下根据每个key的time的排序
//     }

// }

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Block : MonoBehaviour
{


    public List<Key> keyList = new List<Key>();
    public GameObject keyPrefab; // 钥匙预制体
    public TimelineDrag timelineDrag;







    public void AddNewKey() 
    {
        Transform parent = GameObject.Find("TimeLine").transform;
        GameObject newKeyObject = Instantiate(keyPrefab,Vector3.zero,Quaternion.identity,parent);
        // newKeyObject.transform.localPosition = GameObject.Find("Time Marker").transform.localPosition;
        Transform timeMarkerTransform = GameObject.Find("Time Marker").transform;
        // 设置newKeyObject的位置，使其在Time Marker的x和z轴相同位置，y轴下方一点
        newKeyObject.transform.localPosition = new Vector3(timeMarkerTransform.localPosition.x, timeMarkerTransform.localPosition.y - 2.0f, timeMarkerTransform.localPosition.z);


        Key KeyComponent;
        KeyComponent  = newKeyObject.GetComponent<Key>();
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
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // TimeLine.Instance.CalculateXDistance();//这是外部计算两个物体之间x轴距离的函数
            AddNewKey();//触发创建列表 
            // TimelineDrag.Instance.CalculateMouseXDistance();
        }
    }

       
}
// // 假设的Key类
// public class Key : MonoBehaviour {
//     public float Time { get; set; } // 假设每个Key都有一个Time属性

//     public void Initiate(Block block) {
//         // 初始化Key对象，可能包括设置时间、关联Block等
//     }

