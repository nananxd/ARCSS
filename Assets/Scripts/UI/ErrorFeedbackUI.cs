using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ErrorFeedbackUI : MonoBehaviour
{
    [SerializeField] private Ease easeType;
    [SerializeField] private float duration;
    [SerializeField] private Image feedbackImage;
   

    public void AnimateFeedback()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(feedbackImage.DOFade(.4f,duration).SetEase(easeType));
        sequence.Append(feedbackImage.DOFade(0,duration).SetEase(easeType));
    }

    
}
