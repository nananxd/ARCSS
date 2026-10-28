using UnityEngine;

public class PcPartsAnimation : MonoBehaviour
{
    [SerializeField] private Transform target, location;
    
    public void AnimateParts()
    {
        TweenHelper.MoveTo(target,location); ;
    }
}
