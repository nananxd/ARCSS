using UnityEngine;

public class ModuleContentController : MonoBehaviour
{
    public string currentModuleName;
    public ArTopics currentTopic;
    public bool isNotEmpty;


    public void SetCurrentActiveModuleContent(string moduleName,ArTopics topic)
    {
        currentModuleName = moduleName;
        currentTopic = topic;
    }


    public void OnFinishedReadModuleContent(Vector2 position)
    {
        
        if (position.y <= 0.01f)
        {
            Debug.Log("Content Reached Button");
            GameManager.instance.uiManager.currentModule.CompleteModuleReading(currentModuleName,currentTopic);
        }
    }
   
}
