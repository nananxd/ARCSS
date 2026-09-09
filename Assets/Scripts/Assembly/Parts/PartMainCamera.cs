using Unity.Cinemachine;
using UnityEngine;

public class PartMainCamera : MonoBehaviour
{
    public PCMainCam partId;
    [SerializeField] private CinemachineCamera cinemachineVirtualCamera;
  
    public void Setup()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineCamera>();
    }

    public void SetAsCamera()
    {
        cinemachineVirtualCamera.Priority = 11;
    }

    public void ResetAsCamera()
    {
        cinemachineVirtualCamera.Priority = 10;
    }
}
