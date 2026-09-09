using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AssembleAssesment : MonoBehaviour
{
    [SerializeField] private PCAssesmentData assessmentData;
    [SerializeField] private List<AssemblySlot> slots = new List<AssemblySlot>();
    public int currentStep;

    private void Awake()
    {
        GetAllSlots();
    }
    void Start()
    {
        ResetAssessment();
       
    }

    #region PC slots

    private void GetAllSlots()
    {
        slots = FindObjectsByType<AssemblySlot>(FindObjectsInactive.Include).ToList();
    }

    public AssemblySlot GetSlotById(PCParts id)
    {
        var foundSlot = slots.Find(x => x.partId == id);
        if (foundSlot != null) 
        {
            return foundSlot;
        }

        return null;
    }

    #endregion

    public void ResetAssessment()
    {
        currentStep = 0;
      

        Debug.Log("Assembly assessment started.");
    }

    public void TryPartialAssemble(AssemblyPart part)
    {
        if (part == null)
        {
            return;
        }

        if (!part.CanAssemble(out string reason))
        {
            Debug.Log(reason);

            return;
        }

        part.AssemblePartially();
    }

    public void TryAssemble(AssemblyPart part)
    {
        if (part == null)
            return;

        if (assessmentData == null)
        {
            Debug.LogError("Assessment Data is not assigned.");
            return;
        }

        //if (currentStep >= assessmentData.assemblySteps.Count)
        //{
        //    Debug.Log("Assembly assessment already completed.");
        //    return;
        //}


        // -----------------------------------------
        // CHECK CORRECT PART
        // -----------------------------------------

        //AssemblyStep expectedStep =assessmentData.assemblySteps[currentStep];

        //if (part.partID != expectedStep.partID)
        //{
        //    Debug.Log(
        //        $"WRONG PART! " +
        //        $"Expected: {expectedStep.partID}, " +
        //        $"Selected: {part.partID}"
        //    );

        //    return;
        //}


        // -----------------------------------------
        // CHECK ALIGNMENT
        // -----------------------------------------

        //if (!part.IsAligned())
        //{
        //    Debug.Log($"{part.partID} is not correctly aligned.");
        //    return;
        //}


        // -----------------------------------------
        // CHECK DEPENDENCIES
        // -----------------------------------------

        if (!part.CanAssemble(out string reason))
        {
            Debug.Log(reason);

            return;
        }


        // -----------------------------------------
        // ASSEMBLE
        // -----------------------------------------

        part.Assemble();
        //part.AssemblePartially();
        currentStep++;

        Debug.Log($"Correct! {part.partID} assembled. ");


        //CheckAssessmentComplete();
    }

    private void CheckAssessmentComplete()
    {
        if (currentStep >= assessmentData.assemblySteps.Count)
        {
            Debug.Log($"ASSEMBLY COMPLETE!");
        }
    }

}
