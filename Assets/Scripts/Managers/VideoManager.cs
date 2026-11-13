using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


[System.Serializable]
public class VideoClipData
{
    public VideoClipName videoName;
    public VideoClip vidClip;
}

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

   // [SerializeField] private List<VideoClip> videoClips = new List<VideoClip>();
    [SerializeField] private List<VideoClipData> videoClips = new List<VideoClipData>();
   
    public void SetVideoClip(VideoClipName clipName)
    {
        var foundClip = videoClips.Find(x => x.videoName == clipName);
        if (foundClip != null) 
        {
            videoPlayer.clip = foundClip.vidClip;
        }
    }

    public void PlayVideoClip()
    {
        videoPlayer.Play();
    }

    public void PauseVideoClip()
    {
        videoPlayer.Stop();
    }
}

