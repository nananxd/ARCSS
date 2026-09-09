using System.Collections.Generic;
using UnityEngine;

public class ContentDataManager : MonoBehaviour
{
    [Header("Videos")]
    public List<VideoContentData> videoContents;
    [Header("Text")]
    public List<TextContentData> textContents;
    [Header ("PhotoText")]
    public List<PhotoTextContentData> photoTextContents;
}
