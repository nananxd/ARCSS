using UnityEngine;
using Unity.Cinemachine;

public class PartCamera : MonoBehaviour
{
    public PCParts partId;
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
