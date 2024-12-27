using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUI : MonoBehaviour//这是黄奕坚的UI脚本，
{
    static public float KeepDecimal(float input, int n)//���ڱ���nλС��
    {
        string fn = "F" + n.ToString();
        float a = float.Parse(input.ToString(fn));
        return a;
    }
    public void FadeIn(CanvasGroup canvasGroup,float fadeTime = 1,float waitTime = 0)//fadetime为淡入时间，waitTime为等待时间
    {
        StartCoroutine(HandleFadeIn(canvasGroup, fadeTime, waitTime));
    }
    public void FadeOut(CanvasGroup canvasGroup,float fadeTime = 1,float waitTime = 0)//这些功能必须用于UI，不能用于其他物体
    {
        StartCoroutine(HandleFadeOut(canvasGroup, fadeTime, waitTime));
    }
    IEnumerator HandleFadeIn(CanvasGroup canvasGroup, float fadeTime, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Start FadeIn" + canvasGroup);
        float initialAlpha = canvasGroup.alpha;
        for (float i = 0; i < fadeTime; i += Time.fixedDeltaTime)
        {
            canvasGroup.alpha =initialAlpha + (i / fadeTime);
            yield return new WaitForFixedUpdate();
        }
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    IEnumerator HandleFadeOut(CanvasGroup canvasGroup, float fadeTime, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Start FadeOut" + canvasGroup);
        float initialAlpha = canvasGroup.alpha;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        for (float i = 0; i < fadeTime; i += Time.fixedDeltaTime)
        {
            canvasGroup.alpha = (1 - i / fadeTime)*initialAlpha;
            yield return new WaitForFixedUpdate();
        }
        canvasGroup.alpha = 0;
    }
}
