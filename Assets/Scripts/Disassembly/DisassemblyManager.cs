using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisassemblyManager : MonoBehaviour
{
    public static DisassemblyManager instance;
    [Header("Managers")]
    public DisassemblyUIManager uIManager;
    public DisassemblyAssesment assement;
    public DisassemblyCameraManager cameraManager;
    public List<AssemblyPart> assemblyParts = new List<AssemblyPart>();

    public AssemblyPart currentSelected;

    private void Awake()
    {
        instance = this;
        assemblyParts = FindObjectsByType<AssemblyPart>(FindObjectsInactive.Include).ToList();
    }

    public void DisablePart(PCParts part)
    {
       var foundPart = assemblyParts.Find(x =>x.partID ==part);
        if (foundPart != null)
        {
            PcPartsAnimation partAnimation = foundPart.gameObject.GetComponent<PcPartsAnimation>();
            if (partAnimation != null)
            {
                partAnimation.AnimateParts();
            }
            else
            {
                foundPart.gameObject.transform.localScale = Vector3.zero;
            }

            CheckIfFinished();
        }
    }


    private void CheckIfFinished()
    {
        if (CheckIfAllDisassemble())
        {
            Debug.Log("All components disable");
            uIManager.OpenGameEndUI();
        }
    }

    public bool CheckIfAllDisassemble()
    {
        var isAllDisassemble = assemblyParts.All(x => x.IsAssembled == false);
        return isAllDisassemble;
    }

    

    
}
