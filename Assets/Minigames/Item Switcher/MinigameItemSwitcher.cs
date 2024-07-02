using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameItemSwitcher : Minigame
{
    public Image toOverlay, toRemove;
    public RectTransform overlayBox, removeOutOfBox;
    bool draggable;
    bool dragging1, dragging2;

    private void Start()
    {
        
    }


    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            if (draggable && toOverlay.rectTransform.rect.Contains(toOverlay.transform.InverseTransformPoint(Input.mousePosition)))
            {
                dragging1 = true;
            }
            if (!draggable && toRemove.rectTransform.rect.Contains(toRemove.transform.InverseTransformPoint(Input.mousePosition)))
            {
                dragging2 = true;
            }


        }

        if (Input.GetMouseButton(0) && dragging1)
        {

            toOverlay.transform.position = Input.mousePosition;

            if (overlayBox.rect.Contains(overlayBox.InverseTransformPoint(Input.mousePosition)))
            {
                invoker.MinigameStopped(true);
            }


        }
        else
        {
            dragging1 = false;
        }

        if (Input.GetMouseButton(0) && dragging2)
        {

            toRemove.transform.position = Input.mousePosition;

            if (!removeOutOfBox.rect.Contains(removeOutOfBox.InverseTransformPoint(Input.mousePosition)))
            {
                draggable = true;
                dragging2 = false;
            }


        }
        else
        {
            dragging2 = false;
        }


        base.Update();
    }
}
