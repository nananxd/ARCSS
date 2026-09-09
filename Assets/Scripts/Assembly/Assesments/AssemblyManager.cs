using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssemblyManager : MonoBehaviour
{
    public static AssemblyManager Instance;
    public Transform currentSelectedTransform;
    public AssemblyPart currentSelectedPart;
    public AssembleAssesment assembleAssesment;
    public AssemblyUIManager assemblyUIManager;
    public AssemblyCameraManager assemblyCameraManager;

    [SerializeField] private List<CorrectPositionPart> correctPositionParts = new List<CorrectPositionPart>();
    [SerializeField] private List<AssemblySlotTrigger> slotTriggers = new List<AssemblySlotTrigger>();
    [SerializeField] private List<AssemblyPart> parts = new List<AssemblyPart>();


    [Header("Assesment")]
    public int currentAssesmentCounter;
    public int assesmentCounter;
    public CurrentAssesment currentAssesment;
    public List<CurrentAssesment> assesments = new List<CurrentAssesment>();



    private void Awake()
    {
        Instance = this;
        correctPositionParts = FindObjectsByType<CorrectPositionPart>(FindObjectsInactive.Include).ToList();
        slotTriggers = FindObjectsByType<AssemblySlotTrigger>().ToList();
        parts = FindObjectsByType<AssemblyPart>(FindObjectsInactive.Include).ToList();

        
    }

    private void Start()
    {
        SetCurrentAssesment();
    }

    #region Assesment
    public void SetCurrentAssesment()
    {
        currentAssesment = assesments[assesmentCounter];
        assemblyCameraManager.SetMainCamera(currentAssesment.mainCam);
    }

    public void NextAssesment()
    {
        if (assesmentCounter < assesments.Count -1)
        {
            assesmentCounter++;
            currentAssesment = assesments[assesmentCounter];
        }
    }

    public void CheckIfAllAssesmentsFinish()
    {
        if (assesmentCounter  >= assesments.Count - 1)
        {
            Debug.Log("Finished Assemble Assesment");
            // Assesment finished proceed to disassembly
        }
    }

    // Current assesment

    public bool CheckIfCurrentAssesmentFinished()
    {
        var isAllDone = currentAssesment.parts.All( x=> x.isDone);
        return isAllDone;
    }

    public void SetPartAssesmentFinished(PCParts partId)
    {
       var selectedPart = currentAssesment.parts.Find( x => x.part == partId);
        if (selectedPart != null) 
        {
            selectedPart.isDone = true;
        }
    }

    public void SetNextCurrentAssesment()
    {
        if (CheckIfCurrentAssesmentFinished())
        {
            NextAssesment();
            assemblyCameraManager.SetMainCamera(currentAssesment.mainCam);
            //Camera change and other ui

        }
    }
    #endregion

    #region Slot Triggers
    public void EnableSlotTriggerVisual()
    {
        foreach (var item in slotTriggers)
        {
            item.ActivateVisual();
        }
    }

    public void DisableSlotTriggerVisual() 
    {
        foreach (var item in slotTriggers)
        {
            item.DeactivateVisual();
        }
    }
    #endregion

    #region CorrectParts
    public void ActivateCorrectPart(PCParts partId)
    {
        var correctPart = correctPositionParts.Find( x=>x.partId == partId);

        if (correctPart != null) 
        {
            correctPart.transform.localScale = Vector3.one;
            DeactivatePart(partId);
        }

    }
    #endregion

    #region Draggable Parts

    public void DeactivatePart(PCParts partId)
    {
        var correctPart = parts.Find(x => x.partID == partId);
        if (correctPart != null)
        {
            correctPart.transform.localScale = Vector3.zero;
        }
    }

    #endregion
}


[System.Serializable]
public class CurrentAssesment
{
    public PCMainCam mainCam;
    public List<PartAssesment> parts = new List<PartAssesment>();

}

[System.Serializable]
public class PartAssesment
{
    public PCParts part;
    public bool isDone;
}
