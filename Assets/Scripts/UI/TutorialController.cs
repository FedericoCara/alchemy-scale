using System;
using System.Collections.Generic;
using Components;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private CameraAnimator cameraAnimator;
    [SerializeField]
    private List<Graphic> graphics;
    [SerializeField]
    private Color normalColor = Color.white;
    [SerializeField]
    private Color hiddenColor = new Color(1,1,1,0);
    [SerializeField]
    private float graphicsChangeDuration = 0.5f;
    [SerializeField]
    private float camMovementDuration = 3;

    [SerializeField]
    private GameObject closeButton;

    private void Awake()
    {
        foreach (Graphic graphic in graphics)
        {
            graphic.color = hiddenColor;
        }
    }

    private void HideGraphics()
    {
        Tween lastTween = null;
        foreach (Graphic graphic in graphics)
        {
            lastTween = graphic.DOColor(hiddenColor, graphicsChangeDuration);
        }
        lastTween?.OnComplete(() => closeButton.SetActive(false));
    }

    private void ShowGraphics()
    {
        Tween lastTween = null;
        foreach (Graphic graphic in graphics)
        {
            lastTween = graphic.DOColor(normalColor, graphicsChangeDuration);
        }  
        lastTween?.OnComplete(() => closeButton.SetActive(true));
    }

    public void ShowTutorial()
    {
        cameraAnimator.MoveToTutorialPosition();
        Invoke(nameof(ShowGraphics),camMovementDuration*0.9f);
    }

    public void HideTutorial()
    {
        HideGraphics();
        cameraAnimator.ReturnToMainPosition();
    }
}
