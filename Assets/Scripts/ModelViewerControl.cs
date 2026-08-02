using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModelViewerControl : MonoBehaviour,IDragHandler
{
    public Transform targetModel;
    public float rotationSpeed = 0.3f;
    public float zoomSpeed;
    public float minDistance = -5f;
    public float maxDistance = -1f;
    float currentDistance;
    private bool isPinching;

    private void Update()
    {
        //isPinching = Input.touchCount >= 2;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                targetModel.Rotate(Vector3.up, -touch.deltaPosition.x * rotationSpeed, Space.World);
                targetModel.Rotate(Vector3.right, touch.deltaPosition.y * rotationSpeed, Space.World);
            }
        }

        else if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 prev0 = t0.position - t0.deltaPosition;
            Vector2 prev1 = t1.position - t1.deltaPosition;

            float prevDist = Vector2.Distance(prev0, prev1);
            float currentDist = Vector2.Distance(t0.position, t1.position);

            float delta = currentDist - prevDist;
            currentDistance += delta * zoomSpeed;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

            Vector3 pos = GameManager.instance.modelCamera.transform.localPosition;
            pos.z = currentDistance;

            #region
            //float scale = Mathf.Clamp(
            //    targetModel.localScale.x + delta * zoomSpeed,
            //    minScale,
            //    maxScale);

            //targetModel.localScale = Vector3.one * scale;
            #endregion
            GameManager.instance.modelCamera.transform.localPosition = pos;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        //if (isPinching)
        //{
        //    return;
        //}
        //targetModel.Rotate(Vector3.up, -eventData.delta.x * rotationSpeed, Space.World);
        //targetModel.Rotate(Vector3.right, eventData.delta.y * rotationSpeed, Space.World);
    }

   
}
