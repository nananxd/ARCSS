using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "VideoContentData", menuName = "Scriptable Objects/VideoContentData")]
public class VideoContentData : ScriptableObject
{
    public List<VideoContent> data;
}

[System.Serializable]
public class VideoContent
{
    public VideoName videoName;
    public VideoClip clip;
}

public enum VideoName
{
    none,
    testName
}

