using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DisassemblyUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI partNameTxt;
    [SerializeField] private Button closeActionBtn;
    [SerializeField] private Button disassembleBtn;
    public RectTransform uiAction;

    [Header("Game Finished UI")]
    public RectTransform gameEndRectranform;
    [SerializeField] private TextMeshProUGUI gameEndText;
    [SerializeField] private Button mainMenuBtn;

    [Header("Error Feedback UI")]
    public ErrorFeedbackUI errorFeedback;
    public TextMeshProUGUI errorText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeActionBtn.onClick.AddListener(CloseAction);
        disassembleBtn.onClick.AddListener(OnClickDisAssemble);
        mainMenuBtn.onClick.AddListener(GoToMainMenu);
    }

    
    public void SetPartNameUI(string partName)
    {
        partNameTxt.text = partName.ToUpper();
    }

    public void OnClickDisAssemble()
    {
        DisassemblyManager.instance.assement.TryDisassemble(DisassemblyManager.instance.currentSelected);
        //DisassemblyManager.instance.DisablePart(DisassemblyManager.instance.currentSelected.partID);
    }
    public void OpenAction()
    {
        Debug.Log("Open");
        uiAction.localScale = Vector3.one;
    }

    public void CloseAction() 
    {
        Debug.Log("Close");
        uiAction.localScale = Vector3.zero;
        DisassemblyManager.instance.currentSelected = null;
        DisassemblyManager.instance.cameraManager.SetMainCam(PCMainCam.NONE);
    }

    public void OpenGameEndUI()
    {
        gameEndRectranform.localScale = Vector3.one;
    }

    public void CloseGameEndUI() 
    {
        gameEndRectranform.localScale = Vector3.zero;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetErrorTextFeedback()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(errorText.DOFade(1f, .5f).SetEase(Ease.InSine));
        sequence.Append(errorText.DOFade(0, 2f).SetEase(Ease.InSine));
    }
}
