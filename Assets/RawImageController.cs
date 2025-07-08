using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RawImageController : MonoBehaviour
{
    private RawImage rawImage;
    private RectTransform rectTransform;
    
    [SerializeField]
    private float moveDistance = 1000f;
    [SerializeField]
    private float moveDuration = 1f;
    
    void Start()
    {
        rawImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
        
        Vector2 startPos = rectTransform.anchoredPosition;
        startPos.y += moveDistance;
        rectTransform.anchoredPosition = startPos;
        
        StartCoroutine(MoveImageAfterDelay());
    }

    IEnumerator MoveImageAfterDelay()
    {
        yield return new WaitForSeconds(30f);
        
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 targetPosition = startPosition - new Vector2(0, moveDistance);
        
        float elapsedTime = 0f;
        
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        
        rectTransform.anchoredPosition = targetPosition;
    }

    void Update()
    {
        
    }
}
