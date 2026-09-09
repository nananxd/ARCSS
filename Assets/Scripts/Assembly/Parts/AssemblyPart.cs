using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
            transform.position = correctSlot.snapPoint.position;
            transform.rotation = correctSlot.snapPoint.rotation;
        }

        IsAssembled = true;

        Debug.Log($"{partID} assembled.");
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
