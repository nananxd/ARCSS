using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;


public class NetworkCameraManager : MonoBehaviour
{
    public List<NetworkCamera> cameras;
  

    public void SwitchCam(CameraType cameraType)
    {
        SetCamera(cameraType);
    }

    private void SetCamera(CameraType cameraType)
    {
        foreach (var cam in cameras)
        {
            if (cam.cameraId == cameraType)
            {
                cam.SetAsMainCam();
            }
            else
            {
                cam.ResetAsMainCam();
            }
        }
    }
}

public enum Mode
{
    NONE,
    STRAIGHTTHROUGH,
    CROSSOVER
}
public enum CameraType
{
    NONE,
    MAINCAM,
    RJ45ONECAM,
    RJ45TWOCAM,
    MENU
}

public enum ConnectorType
{
    NONE,
    RJ45_1,
    RJ45_2
}
