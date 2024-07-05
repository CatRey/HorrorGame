using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOverlayer3D : InteractableInCollider
{
    public Overlayable3D overlayable;

    private void Start()
    {
        
    }


    private void Update()
    {
        if (overlayable.overlayed)
        {
            MakeUninteractable();
        }
    }

    public override void MakeUninteractable()
    {
        overlayable.overlayed = false;
        base.MakeUninteractable();
    }

    public override void MakeInteractable()
    {
        overlayable.gameObject.SetActive(true);
        base.MakeInteractable();
    }
}
