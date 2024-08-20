using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTrigger : MonoBehaviour
{
    [SerializeField] public GameObject scaleObject;
    [SerializeField] public bool isLeft = true;

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
            if (hit.collider.CompareTag("LeftTrigger") || hit.collider.CompareTag("RigthTrigger"))
            {
                ScaleController getScaleController = scaleObject.GetComponent<ScaleController>();

                if (hit.collider.CompareTag("LeftTrigger"))
                {
                    if (isLeft)
                    {
                        getScaleController.RemoveLastItemLeft();
                    }
                }
                else if (hit.collider.CompareTag("RigthTrigger"))
                {
                    if (!isLeft)
                    {
                        getScaleController.RemoveLastItemRight();
                    }
                }
            }
        }
    }
}
