using UnityEngine;
using UnityEngine.EventSystems;

public class DisassemblySelection : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private AssemblyPart part;

    void Start()
    {
        part = GetComponent<AssemblyPart>();
    }


    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(part.partID.ToString());
        DisassemblyManager.instance.uIManager.SetPartNameUI(part.partID.ToString());
        DisassemblyManager.instance.uIManager.OpenAction();
        DisassemblyManager.instance.currentSelected = part;
    }

  
}
