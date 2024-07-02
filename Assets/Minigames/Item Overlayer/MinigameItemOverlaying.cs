using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public class MinigameItemOverlaying : Minigame
{
    public Image toOverlay;
    public RectTransform overlayBox;
    bool dragging;
    private void Start()
    {
        
    }


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (toOverlay.rectTransform.rect.Contains(toOverlay.transform.InverseTransformPoint(Input.mousePosition)))
            {
                dragging = true;
            }


        }

        if (Input.GetMouseButton(0) && dragging)
        {

            toOverlay.transform.position = Input.mousePosition;

            if (overlayBox.rect.Contains(overlayBox.InverseTransformPoint(Input.mousePosition)))
            {
                invoker.MinigameStopped();
            }


        }
        else
        {
            dragging = false;
        }
    }
}