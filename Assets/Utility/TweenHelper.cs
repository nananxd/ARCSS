using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public  static class TweenHelper 
{
    // =========================================================
    // POSITION
    // =========================================================

    public static Tween Move( Transform target,Vector3 position,float duration = 0.5f,Ease ease = Ease.OutQuad)
    {
        if (target == null)
            return null;

        return target
            .DOMove(position, duration)
            .SetEase(ease);
    }

    public static Tween MoveLocal(Transform target,Vector3 localPosition, float duration = 0.5f,Ease ease = Ease.OutQuad)
    {
        if (target == null)
            return null;

        return target
            .DOLocalMove(localPosition, duration)
            .SetEase(ease);
    }

    public static Tween MoveTo(Transform target,Transform destination,float duration = 0.5f,Ease ease = Ease.OutQuad)
    {
        if (target == null || destination == null)
            return null;

        return target
            .DOMove(destination.position, duration)
            .SetEase(ease);
    }


    // =========================================================
    // ROTATION
    // =========================================================

    public static Tween Rotate( Transform target, Vector3 rotation,float duration = 0.5f, Ease ease = Ease.OutQuad)
    {
        if (target == null)
            return null;

        return target
            .DORotate(rotation, duration)
            .SetEase(ease);
    }

    public static Tween RotateTo(Transform target, Transform destination,float duration = 0.5f,Ease ease = Ease.OutQuad)
    {
        if (target == null || destination == null)
            return null;

        return target
            .DORotate(
                destination.eulerAngles,
                duration
            )
            .SetEase(ease);
    }

    public static Tween RotateLocal(Transform target,Vector3 rotation, float duration = 0.5f,Ease ease = Ease.OutQuad)
    {
        if (target == null)
            return null;

        return target
            .DOLocalRotate(rotation, duration)
            .SetEase(ease);
    }


    // =========================================================
    // SCALE
    // =========================================================

    public static Tween Scale(Transform target,Vector3 scale,float duration = 0.5f,Ease ease = Ease.OutBack)
    {
        if (target == null)
            return null;

        return target
            .DOScale(scale, duration)
            .SetEase(ease);
    }

    public static Tween ScaleTo( Transform target,Transform destination,float duration = 0.5f, Ease ease = Ease.OutBack)
    {
        if (target == null || destination == null)
            return null;

        return target
            .DOScale(destination.localScale, duration)
            .SetEase(ease);
    }


    // =========================================================
    // MOVE + ROTATE
    // =========================================================

    public static Sequence MoveAndRotateTo(Transform target,Transform destination, float duration = 0.5f,  Ease ease = Ease.OutQuad)
    {
        if (target == null || destination == null)
            return null;

        Sequence sequence = DOTween.Sequence();

        sequence.Join(
            target.DOMove(
                destination.position,
                duration
            )
        );

        sequence.Join(
            target.DORotate(
                destination.eulerAngles,
                duration
            )
        );

        sequence.SetEase(ease);

        return sequence;
    }


    // =========================================================
    // MOVE + ROTATE + SCALE
    // =========================================================

    public static Sequence MoveRotateScaleTo(Transform target,Transform destination,float duration = 0.5f,Ease ease = Ease.OutQuad)
    {
        if (target == null || destination == null)
            return null;

        Sequence sequence = DOTween.Sequence();

        sequence.Join(
            target.DOMove(
                destination.position,
                duration
            )
        );

        sequence.Join(
            target.DORotate(
                destination.eulerAngles,
                duration
            )
        );

        sequence.Join(
            target.DOScale(
                destination.localScale,
                duration
            )
        );

        sequence.SetEase(ease);

        return sequence;
    }


    // =========================================================
    // FADE CANVAS GROUP
    // =========================================================

    public static Tween Fade( CanvasGroup canvasGroup,float alpha, float duration = 0.5f, Ease ease = Ease.OutQuad)
    {
        if (canvasGroup == null)
            return null;

        return canvasGroup
            .DOFade(alpha, duration)
            .SetEase(ease);
    }


    // =========================================================
    // CALLBACK
    // =========================================================

    public static Tween MoveTo(Transform target, Transform destination,float duration,System.Action onComplete)
    {
        if (target == null || destination == null)
            return null;

        return target
            .DOMove(destination.position, duration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => onComplete?.Invoke());
    }


    // =========================================================
    // KILL
    // =========================================================

    public static void Kill(Transform target)
    {
        if (target == null)
            return;

        target.DOKill();
    }
}
