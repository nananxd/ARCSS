using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
public class NetworkUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI modeTxt;
    [Header("Start Screen")]
    [SerializeField] private RectTransform startScreenUI;
    [SerializeField] private Button straightThroughBtn, crossOverBtn;
    [Header("Zoom-in pace Screen")]
    [SerializeField] private RectTransform zoomInScreen;
    [SerializeField] private Button zoomInBackBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private TextMeshProUGUI selectedRj45Txt, currentModeTxt;
    [Header("End Screen")]
    [SerializeField] private RectTransform endScreen;
    [SerializeField] private Button endScreenBtn;
    [SerializeField] private TextMeshProUGUI endScreenTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        straightThroughBtn.onClick.AddListener(OnChooseStraightThrough);
        crossOverBtn.onClick.AddListener(OnChooseCrossOver);
        zoomInBackBtn.onClick.AddListener(HideZoomInUI);
        SetStartScreenUI(true);
        nextBtn.onClick.AddListener(NetworkAssesmentManager.Instance.networkAssesment.CheckSequence);
        endScreenBtn.onClick.AddListener(OnMainMenu);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnGameFinished(bool isGameOver)
    {
        endScreenTxt.text = isGameOver ? "Assesment Failed" : "Assesment Completed";
        endScreen.localScale = Vector3.one;
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
        NetworkAssesmentManager.Instance.networkAssesment.EnableRj45Collider(true);
    }

    private void ShowOrHideZoomInScreen(bool isShown)
    {
        zoomInScreen.localScale = isShown ? Vector3.one : Vector3.zero;
    }
    #endregion

    public void SetModeUI(Mode mode)
    {
        modeTxt.text = mode.ToString();
        SetInGameModeText(mode);
    }

    private void SetInGameModeText(Mode mode)
    {
        switch (mode)
        {
            case Mode.NONE:
                break;
            case Mode.STRAIGHTTHROUGH:
                currentModeTxt.text = "Straight-Through";
                break;
            case Mode.CROSSOVER:
                currentModeTxt.text = "Crossover";
                break;
            default:
                break;
        }
    }

    public void SetRJConnectorUI(ConnectorType connectorType)
    {
        switch (connectorType)
        {
            case ConnectorType.NONE:
                break;
            case ConnectorType.RJ45_1:
                selectedRj45Txt.text = "RJ45 Connector 1";
                break;
            case ConnectorType.RJ45_2:
                selectedRj45Txt.text = "RJ45 Connector 2";
                break;
           
        }
    }
    public void SetStartScreenUI(bool isActive)
    {
        startScreenUI.localScale = isActive ? Vector3.one : Vector3.zero;
    }
}
