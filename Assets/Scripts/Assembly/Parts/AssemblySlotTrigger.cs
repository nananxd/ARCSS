using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AssemblySlotTrigger : MonoBehaviour,IPointerClickHandler
{
    public List<PCParts> partIds;
    [SerializeField] private MeshRenderer m_Renderer;

    private void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Renderer.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (AssemblyManager.Instance.currentSelectedPart == null) return;
        
        if (partIds.Contains(AssemblyManager.Instance.currentSelectedPart.partID) && AssemblyManager.Instance.currentSelectedPart.CanAssemble(out string reason))
        {
            Debug.Log(reason);
            AssemblyManager.Instance.assembleAssesment.TryPartialAssemble(AssemblyManager.Instance.currentSelectedPart);
            AssemblyManager.Instance.DisableSlotTriggerVisual();
            AssemblyManager.Instance.assemblyCameraManager.SetCamera(AssemblyManager.Instance.currentSelectedPart.cameraId);
        }
        else
        {
            AssemblyManager.Instance.assemblyUIManager.errorFeedbackUI.AnimateFeedback();
        }
    }

    public void ActivateVisual()
    {
        m_Renderer.enabled = true;
    }

    public void DeactivateVisual() 
    {
        m_Renderer.enabled = false;
    }
}
