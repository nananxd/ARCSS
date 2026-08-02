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
    public string moduleDescription;
    public string videoClipName;
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
