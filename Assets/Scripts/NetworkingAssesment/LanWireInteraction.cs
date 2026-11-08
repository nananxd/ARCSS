
using UnityEngine;
using UnityEngine.EventSystems;

public class LanWireInteraction : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler,IDropHandler,IPointerDownHandler
{
    public int lanId;
    public Transform location;
    public Collider collider;
    private Camera mainCamera;

    private Plane dragPlane;

    [SerializeField] private Outline modelOutline;
    [SerializeField] private LanWireInteraction target;
    [SerializeField] private Vector3 originalPosition;
    private float distanceFromCamera;
    private Vector3 offset;
   

    private void Awake()
    {
        mainCamera = Camera.main;
        modelOutline = GetComponent<Outline>();
        collider = GetComponent<Collider>();
        originalPosition = transform.localPosition;
    }

    private void Start()
    {
        modelOutline.enabled = false;
    }

    public void SetLanOutline(bool isEnable)
    {
        modelOutline.enabled = isEnable;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //originalPosition = transform.position;
        //collider.enabled = false;

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



        //distanceFromCamera =
        //   mainCamera.WorldToScreenPoint(transform.position).z + 1f;

        //Vector3 mousePosition = new Vector3(
        //    eventData.position.x,
        //    eventData.position.y,
        //    distanceFromCamera
        //);

        //Vector3 worldPosition =
        //    mainCamera.ScreenToWorldPoint(mousePosition);

        //offset = transform.position - worldPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Vector3 mousePosition = new Vector3(
        //    eventData.position.x,
        //    eventData.position.y,
        //    distanceFromCamera
        //);

        //Vector3 worldPosition =
        //    mainCamera.ScreenToWorldPoint(mousePosition);

        //transform.position = worldPosition + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //collider.enabled = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
       

        //// Get the object being dragged
        //GameObject draggedObject = eventData.pointerDrag;

        //Debug.Log($"Draggable:{draggedObject.name}");
        //Debug.Log($"Droppable:{gameObject.name}");

        ////var hit = CastRay(draggedObject.transform.position,transform.position);

        //if (draggedObject == null)
        //    return;

        ////if (hit.collider.CompareTag("Wire"))
        ////{
            
        ////}

        //LanWireInteraction draggedItem = draggedObject.GetComponent<LanWireInteraction>();

        //if (draggedItem == null)
        //    return;

        //// Save the target's current position
        //Vector3 targetPosition = transform.localPosition;

        //// Get the dragged object's original position
        //Vector3 draggedOriginalPosition = draggedItem.transform.localPosition;

        //// Swap positions
        //draggedItem.transform.localPosition = targetPosition;
        //gameObject.transform.localPosition = draggedOriginalPosition;

       

        Debug.Log("Objects successfully swapped!");
    }


    public void SwapPosition(Vector3 targetPosition)
    {
       gameObject.transform.position = targetPosition;
    }

    public Vector3 GetOriginalPosition()
    {
        return originalPosition;
    }

    private RaycastHit CastRay(Vector3 origin,Vector3 direction)
    {
        Vector3 screenMousePosFar = new Vector3( Input.mousePosition.x,Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3( Input.mousePosition.x,Input.mousePosition.y,Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(origin, direction , out hit);

        return hit;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        NetworkAssesmentManager.Instance.networkAssesment.SetLanwire(this);
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



