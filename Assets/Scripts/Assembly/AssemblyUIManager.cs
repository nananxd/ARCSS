using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class AssemblyUIManager : MonoBehaviour
{
    [SerializeField] private PointerUI pointerGameobject;
    [SerializeField] private GameObject actionUI;
  

    [Header("Action UI Buttons")]
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button rotateBtn;

    [Header("End Screen UI")]
    [SerializeField] private GameObject endScreenUI;

    public ErrorFeedbackUI errorFeedbackUI;

    private void Awake()
    {
        InitializeActionUI();
    }

    public void PositionPointerUI(Transform location)
    {
        var pos = Camera.main.WorldToScreenPoint(location.position);
        pointerGameobject.gameObject.transform.position = pos;
        pointerGameobject.AnimatePointer();

       
    }

    #region Action UI

    public void InitializeActionUI()
    {
        closeBtn.onClick.AddListener(CloseActionUI);
        confirmButton.onClick.AddListener(Confirm);
        rotateBtn.onClick.AddListener(RotateSelectedPart);
    }
    public void PositionActionUI(Transform location)
    {
        //var pos = Camera.main.WorldToScreenPoint(location.position);
        //actionUI.transform.position = pos;
        actionUI.transform.localScale = Vector3.one;
    }

    public void CloseActionUI()
    {
        actionUI.transform.localScale = Vector3.zero;
        AssemblyManager.Instance.DisableSlotTriggerVisual();
    }

    public void RotateSelectedPart()
    {

    }

    public void Confirm()
    {
        AssemblyManager.Instance.assembleAssesment.TryAssemble(AssemblyManager.Instance.currentSelectedPart);
        AssemblyManager.Instance.assemblyCameraManager.SetMainCamera(AssemblyManager.Instance.currentAssesment.mainCam);
        AssemblyManager.Instance.SetPartAssesmentFinished(AssemblyManager.Instance.currentSelectedPart.partID);
        AssemblyManager.Instance.ActivateCorrectPart(AssemblyManager.Instance.currentSelectedPart.partID);
        AssemblyManager.Instance.CheckIfAllAssesmentsFinish();
        AssemblyManager.Instance.SetNextCurrentAssesment();
        
    }
    #endregion
}
