using UnityEngine;

public class NetworkAssesmentManager : MonoBehaviour
{
    public static NetworkAssesmentManager Instance;
    public Mode mode;

    [Header("Managers")]
    public NetworkCameraManager networkCamera;
    public NetworkUIManager uiManager;
    

    private void Awake()
    {
        Instance = this;
    }
   
}
