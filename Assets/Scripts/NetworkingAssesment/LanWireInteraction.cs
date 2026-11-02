using UnityEngine;
using UnityEngine.EventSystems;

public class LanWireInteraction : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler
{
    public Transform location;
    private Camera mainCamera;

    private Plane dragPlane;

    private float distanceFromCamera;
    private Vector3 offset;
    private Vector3 originalPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;

        //dragPlane = new Plane(
        //    Vector3.up,
        //    new Vector3(0, transform.position.y, 0)
        //);

        //Ray ray = mainCamera.ScreenPointToRay(eventData.position);

        //if (dragPlane.Raycast(ray, out float distance))
        //{
        //    Vector3 hitPoint = ray.GetPoint(distance);
        //    offset = transform.position - hitPoint;
        //}



        distanceFromCamera =
           mainCamera.WorldToScreenPoint(transform.position).z;

        Vector3 mousePosition = new Vector3(
            eventData.position.x,
            eventData.position.y,
            distanceFromCamera
        );

        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(mousePosition);

        offset = transform.position - worldPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(
            eventData.position.x,
            eventData.position.y,
            distanceFromCamera
        );

        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(mousePosition);

        transform.position = worldPosition + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Cast a ray from the mouse position
        Ray ray = mainCamera.ScreenPointToRay(eventData.position);

        RaycastHit[] hits = Physics.RaycastAll(ray);

        LanWireInteraction target = null;
        float closestDistance = Mathf.Infinity;

        // Find another draggable object under the mouse
        foreach (RaycastHit hit in hits)
        {
            LanWireInteraction other =
                hit.collider.GetComponentInParent<LanWireInteraction>();

            if (other == null || other == this)
                continue;

            if (hit.distance < closestDistance)
            {
                closestDistance = hit.distance;
                target = other;
            }
        }

        // Swap positions if another object was found
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;

            target.transform.position = originalPosition;

            transform.position = targetPosition;

            Debug.Log("Objects swapped!");
        }
    }
}

   

