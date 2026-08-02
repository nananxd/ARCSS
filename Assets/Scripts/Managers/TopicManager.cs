using System.Collections.Generic;
using UnityEngine;

public class TopicManager : MonoBehaviour
{
    [SerializeField] private List<TopicData> topics = new ();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public TopicData GetTopic(ArTopics topic)
    {
        var selectedTopic = topics.Find(x => x.topic.ToString().ToLower() == topic.ToString().ToLower());
        if (selectedTopic != null) 
        {
            return selectedTopic;
        }
        return null;
    }
}
