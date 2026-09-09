using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TopicData", menuName = "Scriptable Objects/TopicData")]
public class TopicData : ScriptableObject
{
    public ArTopics topic;
    public List<Module> modules = new List<Module>();
}

[System.Serializable]
public class Module
{
    public ModuleContent content;
    public string moduleName;
    [TextArea(3,1)]
    public string moduleDescription;
    public string videoClipName;
    public PhotoTextContentData data;
    public List<ModelsName> modelNames; // refactor to enum later
    public List<Sprite> moduleSprite;
    public bool isAssesment;
    public bool isDoneReading;
    
    
}

public enum ModuleContent
{
    textContent,
    photoContent,
    threeDContent,
    videoContent
}
public enum ArTopics
{
    None,
    ComputerSystem,
    ComputerNetwork,
    ComputerServer,
    ArCSS,
    ComputerMaintenance,
    BuisnessAndCareer
}

public enum ModelsName
{
    cube,
    testCube,
    ram,
    cpu,
    motherboard,
    fan,
    gpu
}
