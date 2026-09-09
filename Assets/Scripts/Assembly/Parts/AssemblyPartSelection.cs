using UnityEngine;

public class AssemblyPartSelection : MonoBehaviour
{
   
    [SerializeField] private AssemblyPart assemblyPart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assemblyPart = GetComponent<AssemblyPart>();
    }

   


    // change it to ipointerclick
    private void OnMouseDown() 
    {
        if (assemblyPart == null)
            return;

        //show choices
        // actionUI.SelectPart(assemblyPart);
        AssemblyManager.Instance.assemblyUIManager.PositionPointerUI(assemblyPart.CorrectSlot.gameObject.transform);
        AssemblyManager.Instance.assemblyUIManager.PositionActionUI(assemblyPart.gameObject.transform);
        AssemblyManager.Instance.EnableSlotTriggerVisual();
        AssemblyManager.Instance.currentSelectedPart = assemblyPart;
    }
}
