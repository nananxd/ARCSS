using UnityEngine;
using DG.Tweening;

public class PointerUI : MonoBehaviour
{
    [SerializeField] private Ease easeType;
    [SerializeField] private float duration;
    [SerializeField] private Vector3 targetScale;
    [SerializeField] private Vector3 defaultScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultScale = Vector3.one;
        transform.localScale = Vector3.zero;

        //AnimatePointer();
    }

    
    public void AnimatePointer()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(Vector3.zero,.1f).SetEase(easeType));
        sequence.Append(transform.DOScale(targetScale, duration).SetEase(easeType));
        sequence.Join(transform.DOScale(defaultScale, duration).SetEase(easeType));
    }

    public void Reset()
    {
        transform.localScale = Vector3.zero;
    }
}
