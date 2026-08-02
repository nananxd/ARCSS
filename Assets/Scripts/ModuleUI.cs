using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleUI : MonoBehaviour,IInitializer
{
    [SerializeField] private Button moduleBtn;

    [Header("Content")]
    public ModuleContent content;
    public string moduleName;
    public string moduleDescription;
    public string videoClipName;
    public List<Sprite> moduleSprite;
    public bool isAssesment;
    public bool isDoneReading;

   
    public void Initialize()
    {
        moduleBtn = GetComponent<Button>();
        moduleBtn.onClick.AddListener(OnClickModule);
    }

    public void OnClickModule()
    {
        GameManager.instance.uiManager.SetContent(this);
    }

    public void Setup(Module module)
    {
        content = module.content;
        moduleName = module.moduleName;
        moduleDescription = module.moduleDescription;
        videoClipName = module.videoClipName;
        moduleSprite = module.moduleSprite;
        isAssesment = module.isAssesment;
        isDoneReading = module.isDoneReading;
    }

}
