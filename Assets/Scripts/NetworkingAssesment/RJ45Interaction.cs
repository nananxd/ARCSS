using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class RJ45Interaction : MonoBehaviour,IPointerDownHandler
{
    public CameraType cameraType;
    public ConnectorType connectorType;
    public Collider rj45Collider;

    public List<Collider> lanColliders;

    public void EnableCollider(bool isEnable)
    {
        foreach (var collider in lanColliders)
        {
            collider.enabled = isEnable;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(connectorType.ToString());
        NetworkAssesmentManager.Instance.networkCamera.SwitchCam(cameraType);
        NetworkAssesmentManager.Instance.uiManager.ShowZoomInUI();
        NetworkAssesmentManager.Instance.uiManager.SetRJConnectorUI(connectorType);
        rj45Collider.enabled = false;
        EnableCollider(true);
    }
}
