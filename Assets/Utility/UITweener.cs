using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public static class UITweener 
{
    public static void SlideFade(RectTransform target,CanvasGroup canvas, Vector3 position, float alpha = 1f,float duration = 0.5f, Ease ease = Ease.OutCubic)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Join(target.DOAnchorPos(position,duration).SetEase(ease));
        sequence.Join(canvas.DOFade(alpha, duration).SetEase(ease).SetDelay(.15f));
    }
}
