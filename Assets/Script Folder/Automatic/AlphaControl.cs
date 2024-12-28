using System.Collections;
using UnityEngine;

public class AlphaControl : MonoBehaviour
{
    public SpriteRenderer targetRenderer; // 目标物体的精灵渲染器组件
    public float fadeSpeed = 1f; // 渐变速度（1秒完成一次循环）
    public float duration = 5f; // 透明度变化持续时间，可在Inspector中调整

    private void Start()
    {
        // 检查是否已指定目标渲染器
        if (targetRenderer == null)
        {
            Debug.LogError("请在Inspector中指定要控制的目标物体的SpriteRenderer！");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && targetRenderer != null)
        {
            StartCoroutine(FadeLoop());
            Debug.Log("Start FadeLoop");
        }
    }

    /// <summary>
    /// 透明度循环渐变的协程
    /// </summary>
    private IEnumerator FadeLoop()
    {
        float elapsedTime = 0f; // 已经过的时间

        while (elapsedTime < duration)
        {
            // 从透明到不透明
            for (float alpha = 0; alpha <= 0.6f && elapsedTime < duration; alpha += Time.deltaTime * fadeSpeed)
            {
                Color color = targetRenderer.color;
                color.a = alpha;
                targetRenderer.color = color;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 从不透明到透明
            for (float alpha = 0.6f; alpha >= 0 && elapsedTime < duration; alpha -= Time.deltaTime * fadeSpeed)
            {
                Color color = targetRenderer.color;
                color.a = alpha;
                targetRenderer.color = color;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        // 时间结束后，设置为完全透明
        Color finalColor = targetRenderer.color;
        finalColor.a = 0f;
        targetRenderer.color = finalColor;
    }
}
