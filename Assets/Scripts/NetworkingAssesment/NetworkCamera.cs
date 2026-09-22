using Unity.Cinemachine;
using UnityEngine;

public class NetworkCamera : MonoBehaviour
{
    public CameraType cameraId;
    public CinemachineCamera virtualCam;

    private void Start()
    {
        virtualCam = GetComponent<CinemachineCamera>();
    }

    public void SetAsMainCam()
    {
        virtualCam.Priority = 11;
    }

    public void ResetAsMainCam()
    {
        virtualCam.Priority = 10;
    }
}
