using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisassemblyManager : MonoBehaviour
{
    public static DisassemblyManager instance;
    [Header("Managers")]
    public DisassemblyUIManager uIManager;
    public DisassemblyAssesment assement;
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
            foundPart.gameObject.transform.localScale = Vector3.zero;
        }
    }


    public bool CheckIfAllDisassemble()
    {
        var isAllDisassemble = assemblyParts.All(x => x.IsAssembled == false);
        return isAllDisassemble;
    }

    

    
}
