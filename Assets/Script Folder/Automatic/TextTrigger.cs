using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MyUI
{
    [SerializeField] private CanvasGroup colliderCanvasGroup;//出现后过一会会消失
    [SerializeField] private CanvasGroup colliderCanvasGroup2;
    [SerializeField] private CanvasGroup uiCanvasGroup;//检测到玩家触发后显示的UI
    [SerializeField] private CanvasGroup startTextCanvasGroup;//游戏开始就出现的UI
    [SerializeField] private CanvasGroup secondTextCanvasGroup;//需要二次碰撞触发的UI，永久出现
    public bool isStart = false;
    public bool isUi = false;
    public bool secondHit = false;

    void Start()
    {
        colliderCanvasGroup.alpha = 0;
        colliderCanvasGroup2.alpha = 0;
        startTextCanvasGroup.alpha = 0;
        uiCanvasGroup.alpha = 0;
        secondTextCanvasGroup.alpha = 0;

        if(isStart)//如果没有一开始就出现，那么下面方法不会被执行
        {
            FadeIn(startTextCanvasGroup,1,1);//游戏开始后1秒淡入
            FadeOut(startTextCanvasGroup,1,3);//游戏开始3秒后淡出
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//内置函数，不用主动调用
    {
        if (collision.gameObject.tag == "Player")//检测到玩家触发，再显示的物体
        {
            if(isUi)
            {
               FadeIn(uiCanvasGroup,1);//这些出现了不会再消失 
            }

            
            FadeIn(colliderCanvasGroup,1);//这些会出现再消失的物体
            FadeOut(colliderCanvasGroup,1,2);
            FadeIn(colliderCanvasGroup2,1);
            FadeOut(colliderCanvasGroup2,1,2);

            if(secondHit)
            {
                FadeIn(secondTextCanvasGroup,1);
            }
            secondHit = true;
        }
    }

}
