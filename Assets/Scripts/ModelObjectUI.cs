using UnityEngine;
using UnityEngine.UI;

public class ModelObjectUI : MonoBehaviour,IInitializer
{
    [SerializeField] private Button btn;
    [SerializeField] private ModelsName modelName;
   
    public void Initialize()
    {
        btn.onClick.AddListener(OnModelUIClick);
    }

    public void Setup(ModelsName modName)
    {
        modelName = modName;
    }

    public void OnModelUIClick()
    {
        //var currentTopic = GameManager.instance.uiManager.currentSelectedTopic;
        //GameManager.instance.modelController.GetModel(currentTopic,modelName);
    }
}
