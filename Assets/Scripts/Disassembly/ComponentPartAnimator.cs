using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ComponentPartAnimator : MonoBehaviour
{
    public Transform target, location;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayInit());
    }


    private IEnumerator DelayInit()
    {
        yield return new WaitForSeconds(1f);
        MoveTarget();
    }
   
    public void MoveTarget()
    {
        target.DOMove(location.transform.position, 1f).SetEase(Ease.InSine); 
    }
}
