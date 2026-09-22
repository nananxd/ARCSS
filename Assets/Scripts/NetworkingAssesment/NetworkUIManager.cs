using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class NetworkUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI modeTxt;
    [Header("Start Screen")]
    [SerializeField] private RectTransform startScreenUI;
    [SerializeField] private Button straightThroughBtn, crossOverBtn;
    [Header("Zoom-in pace Screen")]
    [SerializeField] private RectTransform zoomInScreen;
    [SerializeField] private Button zoomInBackBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        straightThroughBtn.onClick.AddListener(OnChooseStraightThrough);
        crossOverBtn.onClick.AddListener(OnChooseCrossOver);
        zoomInBackBtn.onClick.AddListener(HideZoomInUI);
        SetStartScreenUI(true);
    }

  
    public void OnChooseStraightThrough()
    {
        var currentMode = NetworkAssesmentManager.Instance.mode = Mode.STRAIGHTTHROUGH;
        SetModeUI(currentMode);
        SetStartScreenUI(false);
        NetworkAssesmentManager.Instance.networkCamera.SwitchCam(CameraType.MAINCAM);
    }

    public void OnChooseCrossOver()
    {
        var currentMode = NetworkAssesmentManager.Instance.mode = Mode.CROSSOVER;
        SetModeUI(currentMode);
        SetStartScreenUI(false);
        NetworkAssesmentManager.Instance.networkCamera.SwitchCam(CameraType.MAINCAM);
    }
    #region Zoom In Phase 
    public void ShowZoomInUI()
    {
        ShowOrHideZoomInScreen(true);
        
    }

    public void HideZoomInUI()
    {
        ShowOrHideZoomInScreen(false);
        NetworkAssesmentManager.Instance.networkCamera.SwitchCam(CameraType.MAINCAM);
    }

    private void ShowOrHideZoomInScreen(bool isShown)
    {
        zoomInScreen.localScale = isShown ? Vector3.one : Vector3.zero;
    }
    #endregion

    public void SetModeUI(Mode mode)
    {
        modeTxt.text = mode.ToString();
    }

    public void SetStartScreenUI(bool isActive)
    {
        startScreenUI.localScale = isActive ? Vector3.one : Vector3.zero;
    }
}
