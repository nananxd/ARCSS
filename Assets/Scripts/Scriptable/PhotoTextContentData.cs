using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhotoTextContentData", menuName = "Scriptable Objects/PhotoTextContentData")]
public class PhotoTextContentData : ScriptableObject
{
    public List<PhotoTextContent> data;
}


[System.Serializable]
public class PhotoTextContent
{
    public PhotoTextName contentName;
    public ModelsName modelName;
    public List<Sprite> display;
    [TextArea(5, 1)]
    public string titleName;
    [TextArea(20,5)]
    public string description;
    public bool has3dView;
}

public enum PhotoTextName
{
    none,
    testName
}
