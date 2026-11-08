using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
    public RectTransform backgroundRect;
    public RectTransform rect;
    public CanvasGroup canvas;
    public Vector3 initialPos, targetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPos = rect.anchoredPosition;
        //StartCoroutine(DelayAnimate());
    }

    private IEnumerator DelayAnimate()
    {
        yield return new WaitForSeconds(1f);
        SlideIn();
    }
    public void SlideIn()
    {
        backgroundRect.localScale = Vector3.one;
        UITweener.SlideFade(rect,canvas,targetPos);
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }

    public void SlideOut()
    {
        UITweener.SlideFade(rect, canvas, initialPos,0);
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
        backgroundRect.localScale = Vector3.zero;
    }
}
