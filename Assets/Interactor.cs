using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float maxDistance;
    Interactable wasInteracting;
    void Start()
    {
        
    }


    void Update()
    {
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out var hit, maxDistance))
        {
            var interactable = hit.collider.GetComponent<Interactable>();
            if (!interactable.enabled) interactable = null;
            if (wasInteracting && wasInteracting != interactable)
            {
                wasInteracting.OnBecameUninteractable();
            }
            if (interactable)
            {
                interactable.OnBecameInteractable();
                if (Input.GetKeyDown(interactable.toInteract))
                {
                    interactable.Interact();
                }

            }
            wasInteracting = interactable;
        }
        else if (wasInteracting)
        {
            wasInteracting.OnBecameUninteractable();
            wasInteracting = null;
        }
    }
}
