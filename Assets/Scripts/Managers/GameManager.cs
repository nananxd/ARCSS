using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera modelCamera;

    [Header("Managers")]
    public TopicManager topicManager;
    public UIManager uiManager;
    private void Awake()
    {
        instance = this;
    }
}
