using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class AssemblyPart : MonoBehaviour
{
    [Header("Virtual CameraId")]
    [Space(3)]
    public PCParts cameraId;
    [Header("Part Id")]
    [Space(3)]
    public PCParts partID;

    [SerializeField] private AssemblySlot correctSlot;
    [Header("Alignment")]
    [SerializeField] private float positionTolerance = 0.05f;
    [SerializeField] private float rotationTolerance = 5f;

    [SerializeField] private List<AssemblyPart> requiredPartsToAssemble = new List<AssemblyPart>();
    [SerializeField] private List<AssemblyPart> requiredPartsToDisassemble = new List<AssemblyPart>();
    public bool IsAssembled;
    public AssemblySlot CorrectSlot { get { return correctSlot; } }

    private void Start()
    {
        //correctSlot = AssemblyManager.Instance.assembleAssesment.GetSlotById(partID);
    }

    public void SetupSlot()
    {
        correctSlot = AssemblyManager.Instance.assembleAssesment.GetSlotById(partID);
    }

    public bool IsAligned()
    {
        if (correctSlot == null)
        {
            Debug.LogWarning($"{name} has no AssemblySlot assigned.");
            return false;
        }

        float positionDifference = Vector3.Distance(
            transform.position,
            correctSlot.snapPoint.position
        );

        float rotationDifference = Quaternion.Angle(
            transform.rotation,
            correctSlot.snapPoint.rotation
        );

        return positionDifference <= positionTolerance &&
               rotationDifference <= rotationTolerance;
    }

    public bool CanAssemble(out string reason)
    {
        foreach (AssemblyPart requiredPart in requiredPartsToAssemble)
        {
            if (requiredPart == null)
                continue;

            if (!requiredPart.IsAssembled)
            {
                reason =$"Cannot assemble {partID}." +$"{requiredPart.partID} must be assembled first.";
                AssemblyManager.Instance.assemblyUIManager.errorFeedbackUI.AnimateFeedback();
                return false;
            }
        }

        reason = "";
        return true;
    }

    public bool CanDisassemble(out string reason)
    {
        foreach (AssemblyPart requiredPart in requiredPartsToDisassemble)
        {
            if (requiredPart == null)
                continue;

            if (requiredPart.IsAssembled)
            {
                
                reason =
                    $"Cannot disassemble {partID}. " +
                    $"{requiredPart.partID} must be disassembled first.";

                return false;
            }
        }

        reason = "";
        return true;
    }
    public void Assemble()
    {
        if (IsAssembled)
            return;

        if (correctSlot != null)
        {
            // animate movement here
            AnimateMove();
            //StartCoroutine( AnimateMove());
            //transform.position = correctSlot.snapPoint.position;
            //transform.rotation = correctSlot.snapPoint.rotation;
            //AssemblyManager.Instance.assemblyUIManager.Fade(true);
        }
       
        IsAssembled = true;
       // AssemblyManager.Instance.assemblyUIManager.Fade(false);
        Debug.Log($"{partID} assembled.");
    }

    private void AnimateMove()
    {
        TweenHelper.MoveTo(transform, correctSlot.snapPoint, 1f,Ease.Linear).OnComplete(() =>
        {
            
            DOVirtual.DelayedCall(1f,() => 
            {
                AssemblyManager.Instance.assemblyUIManager.Fade(true);
            });
            DOVirtual.DelayedCall(2f, () =>
            {
                
                transform.position = correctSlot.snapPoint.position;
                transform.rotation = correctSlot.snapPoint.rotation;
                AssemblyManager.Instance.assemblyCameraManager.SetMainCamera(AssemblyManager.Instance.currentAssesment.mainCam);
                AssemblyManager.Instance.ActivateCorrectPart(AssemblyManager.Instance.currentSelectedPart.partID);
                AssemblyManager.Instance.cinematicUI.ResetCinematic();

                AssemblyManager.Instance.CheckIfAllAssesmentsFinish();
                AssemblyManager.Instance.SetNextCurrentAssesment();
            });
           
        });

        Debug.Log("Animate");
       // yield return new WaitForSeconds(1f);
       
        //yield return new WaitForSeconds(1f);
        //AssemblyManager.Instance.assemblyUIManager.Fade(true);
    }

    public void AssemblePartially()
    {
        var pos = new Vector3(correctSlot.snapPoint.position.x,correctSlot.snapPoint.position.y + 1f,correctSlot.snapPoint.position.z);
        transform.position = pos;
    }

    public void Disassemble()
    {
        if (!IsAssembled)
            return;

        IsAssembled = false;

        Debug.Log($"{partID} disassembled.");
    }
}
