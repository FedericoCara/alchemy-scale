using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private bool interactable = true;
    public bool Interactable { get { return interactable; } set { interactable = value; } }

    [SerializeField] 
    private ClampRect clampRect;
    
    [SerializeField] 
    private Rigidbody rigidbody;
    
    [SerializeField]
    private Camera targetCamera;
    public virtual Camera TargetCamera { get { return targetCamera; } set { targetCamera = value; } }

    [SerializeField]
    private bool useLocalPosition = true;

    private Vector3 startingOffset;

    void Start() {
        if (targetCamera == null) {
            targetCamera = Camera.main;
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData) {
        if (!interactable) {
            return;
        }

        Vector3 screenToWorldPoint = GetWorldPositionOnPlane(clampRect.Clamp(eventData.position), transform.position.z);
        if (useLocalPosition) {
            startingOffset = transform.localPosition - screenToWorldPoint;
        } else {
            startingOffset = transform.position - screenToWorldPoint;
        }

        rigidbody.isKinematic = true;
    }

    public virtual void OnDrag(PointerEventData eventData) {
        if (!interactable) {
            return;
        }

        Vector3 screenToWorldPoint = GetWorldPositionOnPlane(clampRect.Clamp(eventData.position), transform.position.z);
        if (useLocalPosition) {
            rigidbody.MovePosition(screenToWorldPoint + startingOffset);
        } else {
            rigidbody.MovePosition(screenToWorldPoint + startingOffset);
        }
        Debug.Log("Dragging startingOffset-"+startingOffset+", mousePosition-"+eventData.position+", screenToWorldPoint-"+screenToWorldPoint, gameObject);
        List<RaycastResult> resultAppendList = new List<RaycastResult>();
        targetCamera.GetComponent<PhysicsRaycaster>().Raycast(eventData, resultAppendList);
        OnDraggedOver(eventData, resultAppendList);
    }
    
    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
        Ray ray = TargetCamera.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public virtual void OnDraggedOver(PointerEventData eventData, List<RaycastResult> objectsDraggedOver) {

    }

    public virtual void OnEndDrag(PointerEventData eventData) {
        rigidbody.isKinematic = false;
    }
}