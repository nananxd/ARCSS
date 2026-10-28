using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisassemblyCameraManager : MonoBehaviour
{
    [SerializeField] private List<PartCamera> partCameras = new List<PartCamera>();
    [SerializeField] private List<PartMainCamera> mainCameras = new List<PartMainCamera>();
    [SerializeField] private PartCamera currentInUseCamera;
    [SerializeField] private PartMainCamera currentInUseMainCamera;
   
    void Start()
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

        SetMainCam(PCMainCam.NONE);
    }

    public void SetCamera(PCParts id)
    {
        var foundCam =  partCameras.Find(x=> x.partId == id);
        if (foundCam != null)
        {
            //foundCam.SetAsCamera();
            currentInUseMainCamera.ResetAsCamera();
            if (currentInUseCamera != null)
            {
                currentInUseCamera.ResetAsCamera();
            }
           
            currentInUseCamera = foundCam;
            currentInUseCamera.SetAsCamera();
        }
    }

    public void SetMainCam(PCMainCam id)
    {
        var foundMainCam = mainCameras.Find(x => x.partId == id);
        if (foundMainCam != null)
        {
            foundMainCam.SetAsCamera();
            currentInUseMainCamera = foundMainCam;
            if (currentInUseCamera != null)
            {
                currentInUseCamera.ResetAsCamera();
            }
           
        }
    }
}
