using UnityEngine;
using UnityEngine.EventSystems;
public class RJ45Interaction : MonoBehaviour,IPointerDownHandler
{
    public CameraType cameraType;
    public ConnectorType connectorType;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(connectorType.ToString());
        NetworkAssesmentManager.Instance.networkCamera.SwitchCam(cameraType);
        NetworkAssesmentManager.Instance.uiManager.ShowZoomInUI();
    }
}
