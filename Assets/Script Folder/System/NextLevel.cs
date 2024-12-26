using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)//这个是触发器调用的，需要在游戏场景角色激活
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneController.instance.NextLevel();//与SceneController.cs中的NextLevel()方法对应
        }
    }

    public void ClickNextLevel()//这个是UI按钮调用的
    {
        SceneController.instance.NextLevel();
    }
}
