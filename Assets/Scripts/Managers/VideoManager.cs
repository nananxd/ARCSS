using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    [SerializeField] private List<VideoClip> videoClips = new List<VideoClip>();
   
    public void SetVideoClip(string clipName)
    {
        var foundClip = videoClips.Find(x => x.name == clipName);
        if (foundClip != null) 
        {
            videoPlayer.clip = foundClip;
        }
    }

    public void PlayVideoClip()
    {
        videoPlayer.Play();
    }

    public void PauseVideoClip()
    {
        videoPlayer.Pause();
    }
}

