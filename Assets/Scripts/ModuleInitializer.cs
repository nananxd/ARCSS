using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class ModuleInitializer : MonoBehaviour
{
    public ArTopics topics;
    [SerializeField] private TopicData currentTopic;
    [SerializeField] private Button closeButton;
    [SerializeField] private bool isArcss;

    [Header("UI")]
    [SerializeField] private RectTransform parent;
    [SerializeField] private GameObject modulePrefab;
    [SerializeField] private GameObject assesmentPrefab;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private List<ModuleUI> modulesUI = new List<ModuleUI>();
    [SerializeField] private List<ModuleData> modules = new List<ModuleData>();
    [SerializeField] private List<GameObject> spawnedModules = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnedAssesment = new List<GameObject>();
    

    [SerializeField] private bool isModuleInitialize;


    private void Start()
    {
        //InitializeModules();
    }

    public void InitializeModules() //
    {
        transform.localScale = Vector3.one;
        closeButton.onClick.AddListener(CloseTopic);
        if (isArcss)
        {
            GameManager.instance.accountManager.SetHeaderProfile();
        }
        else
        {
           
            currentTopic = GameManager.instance.topicManager.GetTopic(topics);
            GameManager.instance.uiManager.SetupModuleUI(topics);
            GameManager.instance.uiManager.EnableModuleUI(topics);
           
            scrollRect.verticalNormalizedPosition = 1f;
        }
       
    }

    public void Setup()
    {

        if (!isModuleInitialize)
        {
            foreach (var item in currentTopic.modules)
            {
                GameObject moduleGO = Instantiate(modulePrefab);
                moduleGO.transform.SetParent(parent,false);
                spawnedModules.Add(moduleGO);

                InitializeModule(item,currentTopic.topic);
                ModuleUI ui = moduleGO.GetComponent<ModuleUI>();
               
                ui.topic = currentTopic.topic;
                if (item.isAssesment)
                {
                    var text = moduleGO.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = "ASSESMENT";
                   
                }
                //else
                //{

                //    ui.Initialize();
                //    ui.Setup(item);
                //}

                ui.Setup(item);
                ui.Initialize();               
                modulesUI.Add(ui);



            }

            isModuleInitialize = true;
            CheckIfModuleDoneReading();
        }
        else
        {
            Debug.Log("Module Already Initialize");
        }


    }

    public void CloseTopic()
    {
        transform.localScale = Vector3.zero;
        //ClearModules();
    }

    private void ClearModules()
    {
        foreach (var item in spawnedModules)
        {
            Destroy(item);
        }
        spawnedModules.Clear();

        foreach (var assesment in spawnedAssesment)
        {
            Destroy(assesment);
        }

        spawnedAssesment.Clear();
    }


    private void CheckIfModuleDoneReading()
    {
        var currentPlayerLogin = GameManager.instance.currentSelectedPlayer;
        foreach (var item in modules)
        {
            var foundModule = currentPlayerLogin.moduleDatas.Find(x => x.moduleName == item.moduleName);
            if (foundModule != null)
            {
                item.isDoneReading = foundModule.isDoneReading;
               
            }

            GameManager.instance.uiManager.InitializeBadge(item);
        }

        LoadAssesmentState();
    }

    private void InitializeModule(Module module,ArTopics topic)
    {
        ModuleData currentModule = new ModuleData();
        currentModule.topic = topic.ToString();
        currentModule.moduleName = module.moduleName;
        currentModule.isDoneReading = module.isDoneReading;
        currentModule.isAssesment = module.isAssesment; 
       

        modules.Add(currentModule);
    }

    public void CompleteModuleReading(string moduleName ,ArTopics topics)
    {
       var foundModule = modules.Find(x=> x.moduleName == moduleName && x.topic == topics.ToString());
        if (foundModule != null) 
        {
            foundModule.isDoneReading = true;
            // save

            ModuleData progress = GameManager.instance.currentSelectedPlayer.moduleDatas.Find(x => x.moduleName == moduleName);
            if (progress == null)
            {
                progress = new ModuleData
                {
                    topic = topics.ToString(),
                    moduleName = moduleName,
                    isDoneReading = true
                };

                GameManager.instance.currentSelectedPlayer.moduleDatas.Add(progress);
            }
            else
            {
                progress.isDoneReading = true;
            }

            GameManager.instance.Save();
        }
    }


    public bool IsModuleCompleted(string topicName, string moduleID)
    {
        var topic = GameManager.instance.currentSelectedPlayer.moduleDatas.Find(x => x.topic == topicName && x.moduleName == moduleID);
        //var topic = modules.Find(x => x.topic == topicName && x.moduleName == moduleID);

        if (topic == null)
            return false;

        //ModuleData module = topic.modules.Find(x => x.moduleID == moduleID);

        //if (module == null)
        //    return false;

        return topic.isDoneReading;
        //return module.isDoneReading;
    }

    #region Old Code Checking for assesment

    //public bool CanTakeAssessment(string topicName,List<Module> modules,int assessmentIndex)
    //{
    //    if (assessmentIndex < 0 || assessmentIndex >= modules.Count)
    //        return false;

    //    Module targetAssessment = modules[assessmentIndex];

    //    if (!targetAssessment.isAssesment)
    //        return false;

    //    // Check all modules before this assessment,
    //    // stopping at the previous assessment.
    //    for (int i = assessmentIndex - 1; i >= 0; i--)
    //    {
    //        Module module = modules[i];

    //        if (module.isAssesment)
    //            break;

    //        if (!IsModuleCompleted(topicName, module.moduleID))
    //            return false;
    //    }

    //    return true;
    //}

    //public bool CanTakeAssessment(
    //string topicName,
    //List<Module> modules)
    //{
    //    foreach (Module module in modules)
    //    {
    //        if (module.isAssesment)
    //            break;

    //        if (!IsModuleCompleted(topicName, module.moduleID))
    //            return false;
    //    }

    //    return true;
    //}

    #endregion

    private void LoadAssesmentState()
    {
        foreach (var button in modulesUI)
        {
            if (!button.isAssesment)
                continue;

            // Check if all required modules are completed.
            bool canUnlock = true;

            foreach (string requiredID in button.moduleReference)
            {
                if (!IsModuleCompleted(topics.ToString(), requiredID))
                {
                    canUnlock = false;
                    break;
                }
            }

            //Check whether this assessment was already completed.
            bool isCompleted = IsAssessmentCompleted(topics.ToString(), button.quizId.ToString());

            //Enable only if unlocked and not already completed.
            button.SetInteractable(canUnlock && isCompleted);
        }
    }

    public bool IsAssessmentCompleted( string topicName,string quizId)
    {
        // Replace this with your actual saved assessment data.
        var foundAssesment = GameManager.instance.currentSelectedPlayer.assesment.Find(x => x.topic == topicName && x.assesmentName == quizId);
        if (foundAssesment == null) return false;
        return foundAssesment.isCompleted;
    }

    public void CheckAssessments(string completedModuleID)
    {
        foreach (var button in modulesUI)
        {
            if (!button.isAssesment)
                continue;

            // Only recheck assessments that reference
            // the module that was just completed.
            if (!button.moduleReference.Contains(completedModuleID))
                continue;

            bool canUnlock = true;

            foreach (string requiredID in button.moduleReference)
            {
                if (!IsModuleCompleted(topics.ToString(), requiredID))
                {
                    canUnlock = false;
                    break;
                }
            }

            button.SetInteractable(canUnlock);
        }
    }


}

[System.Serializable]
public class SaveData
{
    public List<PlayerSaveData> players = new List<PlayerSaveData>(); 
}

[System.Serializable]
public class PlayerSaveData
{
    public string playerId;
    public string playerName;
    public string profilePhotoName;

    public List<ModuleData> moduleDatas = new List<ModuleData>();
    public List<AssesmentProgressData> assesment = new List<AssesmentProgressData>();
}

[System.Serializable]
public class ModuleData
{
    public string topic;
    public string moduleName;
    public bool isDoneReading;
    public bool isAssesment;
}

[System.Serializable]
public class AssesmentProgressData
{
    public string topic;
    public string assesmentName;
    public bool isCompleted;
    public int score;
}
