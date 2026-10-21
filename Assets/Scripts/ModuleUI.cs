using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("Assesment Details")]
    public bool isQuizAssesment;
    public QuizId quizId;
    public AssesmentSceneName sceneName;
    public List<string> moduleReference = new List<string>();

    [Header("UI")]
    public Color neutralColor;
    public Color readColor;
    public Image moduleUiImage;
    public Image doneReadingIcon; // badge ui

    [Header("For Badge UI")]
    public Color notFinishColor;
    public Color doneReadingColor;
    public Sprite moduleBadge, assesmentBadge;


    public void Initialize()
    {
        moduleBtn = GetComponent<Button>();
        moduleBtn.onClick.AddListener(OnClickModule);
        moduleUiImage.color = isDoneReading? readColor: neutralColor;
        // doneReadingIcon.gameObject.SetActive(isDoneReading? true:false);
        doneReadingIcon.sprite = isAssesment ? assesmentBadge : moduleBadge;
        doneReadingIcon.color = isDoneReading ? doneReadingColor : notFinishColor;

        if (isAssesment)
        {
            SetInteractable(false);
        }
       
        
    }

    public void SetInteractable(bool isInteractable)
    {
        moduleBtn.interactable = isInteractable;    
    }

    public void OnClickModule()
    {
        //GameManager.instance.uiManager.SetContent(this);
        if (!isAssesment)
        {
            GameManager.instance.uiManager.InitializeModuleContent(data);
            GameManager.instance.moduleContentController.SetCurrentActiveModuleContent(moduleName, topic);
        }
        else
        {
           
            AssesmentSetup();
        }

        AnimatePress();
    }

    private void AssesmentSetup()
    {
        if (isQuizAssesment)
        {
            GameManager.instance.multipleChoiceManager.GetQuizById(quizId);
            GameManager.instance.multipleChoiceManager.InitializeQuestions();
            GameManager.instance.uiManager.ShowAssesmentScreen();
        }
        else
        {
            SceneManager.LoadScene(sceneName.ToString().Replace(" ",""));
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

        isQuizAssesment = module.assesmentDetails.isQuizAssesment;
        sceneName = module.assesmentDetails.sceneName;
        quizId = module.assesmentDetails.quizId;
        moduleReference = module.assesmentDetails.moduleReference;
       
    }

    public void AnimatePress()
    {
        TweenHelper.ScaleYoyo(transform,new Vector3(.9f,.9f,.9f),.1f);
    }

}
