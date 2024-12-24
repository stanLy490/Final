using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnplayableMarker : MonoBehaviour
{
    public Animator unplayableMarkerAnimator;
    private bool timeLlineMove;


    // Start is called before the first frame update
    public void AnimationCheck()
    {
        timeLlineMove = !timeLlineMove;
        AnimationStatus();
    }

    // Update is called once per frame
    public void AnimationStatus()//Time marker从左到右的动画控制函数
    {
        // 检查脚本中的布尔值是否为false
        if (!timeLlineMove)
        {
            // 如果是false，则将Animator中的布尔值设置为false
            unplayableMarkerAnimator.SetBool("TimeLineMove", false);
            // timeMarkerAnimator.Play("play");
            // Debug.Log($"nononoonononononononon!!");
        }

        else
        {
            // 如果不是false，可以选择将Animator中的布尔值设置回true，或者保持不变
            unplayableMarkerAnimator.SetBool("TimeLineMove", true);
            // timeMarkerAnimator.Play("stop");
        }
    }
}
