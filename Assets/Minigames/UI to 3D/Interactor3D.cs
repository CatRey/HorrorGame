using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor3D : MonoBehaviour
{
    public float maxDistance;
    public LayerMask notPlayer;
    InteractableInCollider wasInteracting;

    private void Start()
    {
        
    }


    private void Update()
    {
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out var hit, maxDistance, notPlayer))
        {
            var interactable = hit.collider.GetComponent<InteractableInCollider>();
            if (interactable && !interactable.enabled) interactable = null;
            if (wasInteracting && wasInteracting != interactable)
            {
                wasInteracting.MakeUninteractive();
            }
            if (interactable)
            {
                interactable.MakeInteractive();

            }

            wasInteracting = interactable;

        }
        else if (wasInteracting)
        {
            wasInteracting.MakeUninteractive();
            wasInteracting = null;
        }
    }
}
