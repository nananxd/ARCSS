using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public ArTopics currentSelectedTopic;
    [SerializeField] public ModuleInitializer currentModule;
    [SerializeField] private List<ModuleInitializer> modules;

    [Header("Module Content")]
    [SerializeField] private RectTransform currentContentActive;
    [SerializeField] private RectTransform textContentRect;
    [SerializeField] private RectTransform threeDContentRect;
    [SerializeField] private RectTransform photoContentRect;
    [SerializeField] private RectTransform videoContentRect;
    [SerializeField] private RectTransform contentParent;
    [Header("Module Lecture Container")]
    [SerializeField] private RectTransform moduleLectureContainerRect;
    [SerializeField] private ScrollRect moduleContentScrollRect;
    [SerializeField] private Button contentBackBtn;
    [Header("Content")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private TextMeshProUGUI textModuleContent;
    [Header("Prefab")]
    [SerializeField] private GameObject photoPrefab;
    [SerializeField] private GameObject contentUIPrefab;

    [Header("Video Clips")]
    [SerializeField] private List<VideoClip> clips;

    [Header("3D Models UI Button")]
    [SerializeField] private Button close3dViewBtn;
    [SerializeField] private GameObject modelUIBtn;
    [SerializeField] private RectTransform modelUIParent;
    [SerializeField] private List<GameObject> moduleUIBtnSpawned;

    [Header("Spawned ContentUI")]
    [SerializeField] private List<GameObject> contentUISpawned = new List<GameObject>();

    [Header("Login Dropdown")]
    public TMP_Dropdown loginNameDropdown;

    [Header("ARCSS")]

    public Button loginButton;
    public Button createButton;
    public Button profileButton;
    public Button exitButton;

    public Button closeProfileButton;
    public Button saveCreateButton;


    [Header("Accounts Related Panel")]
    public GameObject createAccountPanel;
    public GameObject profilePanel;
    public GameObject loginPanel;

   

    
    

    void Start()
    {
        contentBackBtn.onClick.AddListener(ModuleContentBackUI);
        close3dViewBtn.onClick.AddListener(Close3DView);
        InitializeArcss();


    }

    public void SetupModuleUI(ArTopics currentModule)
    {
        var selectedModule = modules.Find(x => x.topics.ToString().ToLower() == currentModule.ToString().ToLower());
        selectedModule.Setup();
        this.currentModule = selectedModule;
        currentSelectedTopic = currentModule;
    }

    #region ARCSS 

    public void InitializeArcss()
    {
        loginButton.onClick.AddListener(OnLoginClick);
        createButton.onClick.AddListener(OnCreateAccountClick);
        profileButton.onClick.AddListener(OnProfileClick);
        exitButton.onClick.AddListener(OnExitClick);
    }
    public void OnLoginClick()
    {

    }

    public void OnCreateAccountClick()
    {

    }

    public void OnProfileClick()
    {

    }

    public void OnExitClick()
    {

    }

    #endregion

    #region Content

    public void InitializeModuleContent(PhotoTextContentData data)
    {
        for (int i = 0; i < data.data.Count; i++)
        {
            var currentData = data.data[i];
            GameObject contentGo = Instantiate(contentUIPrefab);
            contentGo.transform.SetParent(contentParent,false);
            contentGo.SetActive(true);

            var contentData = contentGo.GetComponent<ContentUI>();
            if (contentData != null)
            {
                contentData.Initialize();
                contentData.Setup(currentData);
                
            }

            contentUISpawned.Add(contentGo);
        }

        contentBackBtn.transform.localScale = Vector3.one;
        moduleLectureContainerRect.transform.localScale = Vector3.one;
        moduleContentScrollRect.verticalNormalizedPosition = 1f;
        EnableDisableCurrentModuleUI(false);
        StartCoroutine(InitializeModuleContentEvent());
       
       
    }

    private IEnumerator InitializeModuleContentEvent()
    {
        yield return new WaitForSeconds(1f);
        moduleContentScrollRect.onValueChanged.AddListener(GameManager.instance.moduleContentController.OnFinishedReadModuleContent);
    }

    public void SetContent(ModuleUI moduleUI)
    {
        switch (moduleUI.content)
        {
            case ModuleContent.textContent:
                SetTextContentUI(moduleUI);
                break;
            case ModuleContent.photoContent:
                SetPhotoContentUI(moduleUI);
                break;
            case ModuleContent.threeDContent:
                SetModelContentUI(moduleUI);
                break;
            case ModuleContent.videoContent:
                SetVideoContentUI(moduleUI);
                break;

        }
    }

    public void SetTextContentUI(ModuleUI moduleUI)
    {
        textContentRect.transform.localScale = Vector3.one;
        threeDContentRect.transform.localScale = Vector3.zero;
        photoContentRect.transform.localScale = Vector3.zero;
        videoContentRect.transform.localScale = Vector3.zero;

        contentBackBtn.transform.localScale = Vector3.one;
        EnableDisableCurrentModuleUI(false);

        currentContentActive = textContentRect;

        textModuleContent.text = moduleUI.moduleDescription;
    }

    public void SetPhotoContentUI(ModuleUI moduleUI)
    {
        textContentRect.transform.localScale = Vector3.zero;
        threeDContentRect.transform.localScale = Vector3.zero;
        photoContentRect.transform.localScale = Vector3.one;
        videoContentRect.transform.localScale = Vector3.zero;

        contentBackBtn.transform.localScale = Vector3.one;
        EnableDisableCurrentModuleUI(false);

        currentContentActive = photoContentRect;

        for (int i = 0; i < moduleUI.moduleSprite.Count; i++)
        {
            var photoContent = moduleUI.moduleSprite[i];
            var go = Instantiate(photoPrefab);
            var img = go.GetComponent<Image>();
            img.sprite = photoContent;
            go.transform.parent = photoContentRect;
        }
    }

    public void SetModelContentUI(ModuleUI moduleUI) // 3D
    {
        textContentRect.transform.localScale = Vector3.zero;
        threeDContentRect.transform.localScale = Vector3.one;
        photoContentRect.transform.localScale = Vector3.zero;
        videoContentRect.transform.localScale = Vector3.zero;

        contentBackBtn.transform.localScale = Vector3.one;
        EnableDisableCurrentModuleUI(false);
        currentContentActive = threeDContentRect;

        for (int i = 0; i < moduleUI.modelNames.Count; i++)
        {
            Debug.Log($"Count{moduleUI.modelNames.Count}");
            var gO = Instantiate(modelUIBtn);
            gO.SetActive(true);
            var ui = gO.GetComponent<ModelObjectUI>();
            gO.transform.parent = modelUIParent.transform;
            ui.Initialize();
            ui.Setup(moduleUI.modelNames[i]);
            moduleUIBtnSpawned.Add(gO);
        }
    }

    public void RemoveModelButtonUI()
    {
        foreach (var item in moduleUIBtnSpawned)
        {
            Destroy(item);
        }

        moduleUIBtnSpawned.Clear();
    }


    public void RemoveSpawnContentUI()
    {
        foreach (var item in contentUISpawned)
        {
            Destroy(item);
        }

        contentUISpawned.Clear();
        moduleContentScrollRect.onValueChanged.RemoveAllListeners();


    }

    public void SetVideoContentUI(ModuleUI moduleUI) 
    {
        textContentRect.transform.localScale = Vector3.zero;
        threeDContentRect.transform.localScale = Vector3.zero;
        photoContentRect.transform.localScale = Vector3.zero;
        videoContentRect.transform.localScale = Vector3.one;

        contentBackBtn.transform.localScale = Vector3.one;
        EnableDisableCurrentModuleUI(false);
        currentContentActive = videoContentRect;

        var foundClip = clips.Find(x => x.name == moduleUI.videoClipName);
    }

  

    public void EnableModuleUI(ArTopics currentTopics)
    {
        foreach (var item in modules)
        {
            if (item.topics == currentTopics)
            {
                item.transform.localEulerAngles = Vector3.one;
            }
            else
            {
                item.transform.localScale = Vector3.zero;
            }
        }
    }

    public void EnableDisableCurrentModuleUI(bool isEnable)
    {
        currentModule.transform.localScale = isEnable ? Vector3.one : Vector3.zero;
        //moduleLectureContainerRect.transform.localScale = isEnable ? Vector3.one : Vector3.zero;
    }
    public void ModuleContentBackUI()
    {
        moduleLectureContainerRect.transform.localScale = Vector3.zero;
        contentBackBtn.transform.localScale = Vector3.zero;
        //currentModule.transform.localScale = Vector3.one;
        RemoveSpawnContentUI();
        EnableDisableCurrentModuleUI(true);
    }

    
    public void Open3DView()
    {
        Debug.Log("Opening3dView");
        threeDContentRect.transform.localScale = Vector3.one;
    }

    public void Close3DView()
    {
        threeDContentRect.transform.localScale = Vector3.zero;
    }


    #endregion

}
