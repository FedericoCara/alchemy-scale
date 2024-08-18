using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Components
{
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private bool interactable = true;
        public bool Interactable { get { return interactable; } set { interactable = value; } }

        [SerializeField] 
        private ClampRect clampRect;
    
        [SerializeField] 
        private Rigidbody targetRigidbody;
    
        [SerializeField]
        private Camera targetCamera;
        public virtual Camera TargetCamera { get { return targetCamera; } set { targetCamera = value; } }
        [SerializeField] private int followInputSpeed = 5;


        [SerializeField]
        private bool useLocalPosition = true;

        private Vector3 startingOffset;
        private bool _dragging;
        private PointerEventData _lastDragEventData;


        void Start() {
            if (targetCamera == null) {
                targetCamera = Camera.main;
            }
        }

        private void FixedUpdate()
        {
            if(!_dragging || !interactable)
                return;
            DragObject();
        }

        private void DragObject()
        {
            Vector3 screenToWorldPoint = GetWorldPositionOnPlane(clampRect.Clamp(_lastDragEventData.position), transform.position.z);
            MoveTowards(screenToWorldPoint + startingOffset);
            Debug.Log("Dragging startingOffset-"+startingOffset+", mousePosition-"+_lastDragEventData.position+", screenToWorldPoint-"+screenToWorldPoint, gameObject);
            List<RaycastResult> resultAppendList = new List<RaycastResult>();
            targetCamera.GetComponent<PhysicsRaycaster>().Raycast(_lastDragEventData, resultAppendList);
            OnDraggedOver(_lastDragEventData, resultAppendList);
        }

        private void MoveTowards(Vector3 targetPosition)
        {
            targetRigidbody.velocity = (targetPosition-targetRigidbody.position) * followInputSpeed;
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

            targetRigidbody.useGravity = false;
            _dragging = true;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            _lastDragEventData = eventData;
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

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            targetRigidbody.useGravity = true;
            _dragging = false;
        }
    }
}