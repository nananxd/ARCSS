using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextContentData", menuName = "Scriptable Objects/TextContentData")]
public class TextContentData : ScriptableObject
{
    public List<TextContent> data;
}

[System.Serializable]
public class TextContent
{
    public TextName contentName;
    public string description;
}

public enum TextName
{
    none,
    testName
}
