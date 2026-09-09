using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModuleInitializer : MonoBehaviour
{
    public ArTopics topics;
    [SerializeField] private TopicData currentTopic;
    [SerializeField] private Button closeButton;

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

    public void InitializeModules()
    {
        transform.localScale = Vector3.one;
        currentTopic = GameManager.instance.topicManager.GetTopic(topics);
        GameManager.instance.uiManager.SetupModuleUI(topics);
        GameManager.instance.uiManager.EnableModuleUI(topics);
        closeButton.onClick.AddListener(CloseTopic);
        scrollRect.verticalNormalizedPosition = 1f;
    }

    public void Setup()
    {

        if (!isModuleInitialize)
        {
            foreach (var item in currentTopic.modules)
            {
                GameObject moduleGO = Instantiate(modulePrefab);
                moduleGO.transform.parent = parent;
                spawnedModules.Add(moduleGO);

                InitializeModule(item,currentTopic.topic);
                ModuleUI ui = moduleGO.GetComponent<ModuleUI>();
               
                ui.topic = currentTopic.topic;
                if (item.isAssesment)
                {
                    var text = moduleGO.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = "ASSESMENT";
                   
                }
                else
                {
                   
                    ui.Initialize();
                    ui.Setup(item);
                }

                modulesUI.Add(ui);



            }

            isModuleInitialize = true;
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
