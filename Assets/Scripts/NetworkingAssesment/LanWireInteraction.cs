
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Splines;

public class LanWireInteraction : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler,IDropHandler
{
    public Transform location;
    public Collider collider;
    private Camera mainCamera;

    private Plane dragPlane;

    [SerializeField] private LanWireInteraction target;
    [SerializeField] private Vector3 originalPosition;
    private float distanceFromCamera;
    private Vector3 offset;
   

    private void Awake()
    {
        mainCamera = Camera.main;
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        collider.enabled = false;

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
           mainCamera.WorldToScreenPoint(transform.position).z + 1f;

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
        collider.enabled = true;
    }

    public void OnDrop(PointerEventData eventData)
    {


        // Get the object being dragged
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject == null)
            return;

        LanWireInteraction draggedItem =
            draggedObject.GetComponent<LanWireInteraction>();

        if (draggedItem == null)
            return;

        // Save the target's current position
        Vector3 targetPosition = transform.position;

        // Get the dragged object's original position
        Vector3 draggedOriginalPosition =
            draggedItem.GetOriginalPosition();

        // Swap positions
        transform.position = draggedOriginalPosition;

        draggedItem.SwapPosition(targetPosition);

        Debug.Log("Objects successfully swapped!");
    }


    public void SwapPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }

    public Vector3 GetOriginalPosition()
    {
        return originalPosition;
    }

    #region
    //// Cast a ray from the mouse position
    //Ray ray = mainCamera.ScreenPointToRay(eventData.position);

    //    RaycastHit[] hits = Physics.RaycastAll(ray);

    //    target = null;
    //    float closestDistance = Mathf.Infinity;

    //    // Find another draggable object under the mouse
    //    foreach (RaycastHit hit in hits)
    //    {
    //        LanWireInteraction other =
    //            hit.collider.GetComponent<LanWireInteraction>();

    //        if (other == null || other == this)
    //            continue;

    //        if (hit.distance < closestDistance)
    //        {
    //            closestDistance = hit.distance;
    //            target = other;
    //        }
    //    }

    //    // Swap positions if another object was found
    //    if (target != null)
    //    {
    //        Vector3 targetPosition = target.gameObject.transform.position;

    //        target.gameObject.transform.position = originalPosition;

    //        transform.position = targetPosition;

    //        Debug.Log("Objects swapped!");
    //    }
    //}
    #endregion
}



