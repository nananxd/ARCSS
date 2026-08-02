using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<ModuleInitializer> modules;

    [Header("Module Content")]
    [SerializeField] private RectTransform textContentRect;
    [SerializeField] private RectTransform threeDContentRect;
    [SerializeField] private RectTransform photoContentRect;
    [SerializeField] private RectTransform videoContentRect;
    [Header("Content")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private TextMeshProUGUI textModuleContent;
    [Header("Prefab")]
    [SerializeField] private GameObject photoPrefab;

    [Header("Video Clips")]
    [SerializeField] private List<VideoClip> clips;
    
    

    void Start()
    {
        
    }

    public void SetupModuleUI(ArTopics currentModule)
    {
        var selectedModule = modules.Find(x => x.topics.ToString().ToLower() == currentModule.ToString().ToLower());
        selectedModule.Setup();
    }

    #region Content

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
        textModuleContent.text = moduleUI.moduleDescription;
    }

    public void SetPhotoContentUI(ModuleUI moduleUI)
    {
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

    }

    public void SetVideoContentUI(ModuleUI moduleUI) 
    {
        var foundClip = clips.Find(x => x.name == moduleUI.videoClipName);
    }
    #endregion

}
