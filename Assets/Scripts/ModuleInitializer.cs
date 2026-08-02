using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleInitializer : MonoBehaviour
{
    public ArTopics topics;
    [SerializeField] private TopicData currentTopic;

    [Header("UI")]
    [SerializeField] private RectTransform parent;
    [SerializeField] private GameObject modulePrefab;

    


    public void InitializeModules()
    {
        currentTopic = GameManager.instance.topicManager.GetTopic(topics);
        GameManager.instance.uiManager.SetupModuleUI(topics);
    }

    public void Setup()
    {
        foreach (var item in currentTopic.modules)
        {
            GameObject moduleGO = Instantiate(modulePrefab);
            moduleGO.transform.parent = parent;
            ModuleUI ui = moduleGO.GetComponent<ModuleUI>();
            ui.Initialize();
            ui.Setup(item);
            
        }
    }
}
