using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTrigger : MonoBehaviour
{
    [SerializeField] public GameObject scaleObject;
    private ScaleController getScaleController = null;
    [SerializeField] public bool isLeft = true;
    private bool isOnDragEndListenerAdded = false;

    private void Awake()
    {
        getScaleController = scaleObject.GetComponent<ScaleController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckMouseClick();
        }
    }

    private void CheckMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("LeftTrigger") || hit.collider.CompareTag("RightTrigger"))
            {
                if (hit.collider.CompareTag("LeftTrigger"))
                {
                    if (isLeft)
                    {
                        getScaleController.RemoveLastItemLeft();
                    }
                }
                else if (hit.collider.CompareTag("RightTrigger"))
                {
                    if (!isLeft)
                    {
                        getScaleController.RemoveLastItemRight();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var parentTransform = other.gameObject.transform.parent;

        if (parentTransform == null || !other.CompareTag("Ingredient"))
        {
            return;
        }

        var draggable = parentTransform.GetComponent<Draggable>();

        if (draggable.isDragging)
        {
            getScaleController.currentDraggable = draggable;

            if (!isOnDragEndListenerAdded)
            {
                getScaleController.currentDraggable.OnDragEndListener += getScaleController.OnDropIngredient;
                isOnDragEndListenerAdded = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {

        var parentTransform = other.gameObject.transform.parent;

        if (parentTransform == null || !other.CompareTag("Ingredient"))
        {
            return;
        }

        var draggable = parentTransform.GetComponent<Draggable>();

        if (draggable == getScaleController.currentDraggable)
        {
            if (isOnDragEndListenerAdded)
            {
                getScaleController.currentDraggable.OnDragEndListener -= getScaleController.OnDropIngredient;
                isOnDragEndListenerAdded = false;
            }
        }
    }
}
