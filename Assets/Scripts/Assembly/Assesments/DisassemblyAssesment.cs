using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DisassemblyAssesment : MonoBehaviour
{
    [SerializeField] private PCAssesmentData assessmentData;
    public int currentStep;
   
    void Start()
    {
        ResetAssessment();
    }

    public void ResetAssessment()
    {
        currentStep = 0;
        Debug.Log("Disassembly assessment started.");
    }

    public void TryDisassemble(AssemblyPart part)
    {
        if (part == null)
            return;

        if (assessmentData == null)
        {
            Debug.LogError("Assessment Data is not assigned.");
            return;
        }

        if (currentStep >= assessmentData.disassemblySteps.Count)
        {
            Debug.Log("Disassembly assessment already completed.");
            return;
        }


        // -----------------------------------------
        // CHECK CORRECT PART
        // -----------------------------------------

        AssemblyStep expectedStep = assessmentData.disassemblySteps[currentStep];

        if (part.partID != expectedStep.partID)
        {
            Debug.Log(
                $"WRONG PART! " +
                $"Expected: {expectedStep.partID}, " +
                $"Selected: {part.partID}"
            );

            return;
        }


        // -----------------------------------------
        // CHECK IF ASSEMBLED
        // -----------------------------------------

        if (!part.IsAssembled)
        {
            Debug.Log(
                $"{part.partID} is already disassembled."
            );

            return;
        }


        // -----------------------------------------
        // CHECK DEPENDENCIES
        // -----------------------------------------

        if (!part.CanDisassemble(out string reason))
        {
            Debug.Log(reason);

            return;
        }


        // -----------------------------------------
        // DISASSEMBLE
        // -----------------------------------------

        part.Disassemble();

       
        currentStep++;

        Debug.Log( $"Correct! {part.partID} disassembled.");


        CheckAssessmentComplete();
    }

    private void CheckAssessmentComplete()
    {
        if (currentStep >= assessmentData.disassemblySteps.Count)
        {
            Debug.Log( $"DISASSEMBLY COMPLETE!");
        }
    }

}
