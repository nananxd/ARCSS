using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleUI : MonoBehaviour,IInitializer
{
    public ArTopics topic;
    [SerializeField] private Button moduleBtn;

    [Header("Content")]
    public ModuleContent content;
    public string moduleName;
    public string moduleDescription;
    public string videoClipName;
    public PhotoTextContentData data;
    public List<Sprite> moduleSprite;
    public List<ModelsName> modelNames;
    public bool isAssesment;
    public bool isDoneReading;

   
    public void Initialize()
    {
        moduleBtn = GetComponent<Button>();
        moduleBtn.onClick.AddListener(OnClickModule);
    }

    public void OnClickModule()
    {
        //GameManager.instance.uiManager.SetContent(this);
        if (!isAssesment)
        {
            GameManager.instance.uiManager.InitializeModuleContent(data);
            GameManager.instance.moduleContentController.SetCurrentActiveModuleContent(moduleName, topic);
        }
       
    }

    public void Setup(Module module)
    {
        data = module.data;
        content = module.content;
        moduleName = module.moduleName;
        moduleDescription = module.moduleDescription;
        videoClipName = module.videoClipName;
        moduleSprite = module.moduleSprite;
        modelNames = module.modelNames;
        isAssesment = module.isAssesment;
        isDoneReading = module.isDoneReading;
       
    }

}
