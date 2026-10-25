using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CinematicUI : MonoBehaviour
{
    [SerializeField] private RectTransform topUIRect, bottomUIRect;
    [SerializeField] private Vector3 topPos;
    [SerializeField] private Vector3 bottomPos;
    [SerializeField] private Vector3 topEndPos;
    [SerializeField] private Vector3 bottomEndPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        topPos = topUIRect.anchoredPosition;
        bottomPos = bottomUIRect.anchoredPosition;

        //StartCinematic();
        //ResetCinematic();
       // StartCoroutine(DelayInit());
    }
    private IEnumerator DelayInit()
    {
        StartCinematic();
        yield return new WaitForSeconds(1f);
        ResetCinematic();
    }
    public void StartCinematic()
    {
        TweenHelper.Move(topUIRect,topEndPos,.1f,Ease.Linear);
        TweenHelper.Move(bottomUIRect,bottomEndPos,.1f,Ease.Linear);
    }

    public void ResetCinematic()
    {
        TweenHelper.Move(topUIRect, topPos, .05f, Ease.Linear);
        TweenHelper.Move(bottomUIRect, bottomPos, .05f, Ease.Linear);
    }
}
