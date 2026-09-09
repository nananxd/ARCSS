using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssemblyCameraManager : MonoBehaviour
{
    [SerializeField] private List<PartCamera> partCameras = new List<PartCamera>();
    [SerializeField] private List<PartMainCamera> mainCameras = new List<PartMainCamera>();
    [SerializeField] private PartCamera currentInUseCamera;
    [SerializeField] private PartMainCamera currentInUseMainCamera;
    private void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        partCameras = FindObjectsByType<PartCamera>().ToList();
        mainCameras = FindObjectsByType<PartMainCamera>().ToList();

        foreach (var partCamera in partCameras)
        {
            partCamera.Setup();
        }

        foreach (var mainCam in mainCameras)
        {
            mainCam.Setup();
        }
    }

    public void SetCamera(PCParts partId)
    {
        var selectedCamera = partCameras.Find(x => x.partId == partId);
        currentInUseCamera = selectedCamera;
        currentInUseCamera.SetAsCamera();
        currentInUseMainCamera.ResetAsCamera();
    }

    public void SetMainCamera(PCMainCam camId)
    {     
        //var selectedCamera = mainCameras.Find(x => x.partId == camId);
        //currentInUseMainCamera = selectedCamera;
        //currentInUseMainCamera.SetAsCamera();
        //currentInUseCamera.ResetAsCamera();

        foreach (var mainCam in mainCameras)
        {
            if (mainCam.partId == camId)
            {
                mainCam.SetAsCamera();
                currentInUseMainCamera = mainCam;
            }
            else
            {
                mainCam.ResetAsCamera();

            }
        }
    }

    public void ResetMainCamera(PCMainCam camId)
    {
        var selectedCamera = mainCameras.Find(x => x.partId == camId);
        selectedCamera.ResetAsCamera();
    }
}
