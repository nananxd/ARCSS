using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public GameObject currentModel;
    [SerializeField] private List<GameObject> models;

    
    public void GetModel(ModelsName modelName)
    {
        EnableDisableModel();
        //var foundModel = models.Find(x => x.GetComponent<ModelObject>().topic.ToString() == selectedTopic.ToString() &&
        //x.GetComponent<ModelObject>().modelName.ToString() == modelName.ToString());

        var foundModel = models.Find(x => x.GetComponent<ModelObject>().modelName.ToString() == modelName.ToString());

        if (foundModel != null) 
        {
           foundModel.SetActive(true);
        }

       
    }
    
    public void EnableDisableModel(bool isEnable = false)
    {
        foreach (var item in models)
        {
            item.SetActive(isEnable);
        }
    }
}
