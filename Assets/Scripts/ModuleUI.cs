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
        else
        {
           
            AssesmentSetup();
        }

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

       
    }

}
