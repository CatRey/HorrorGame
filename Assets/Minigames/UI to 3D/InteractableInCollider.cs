using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableInCollider : MonoBehaviour
{
    public List<Collider> interactable = new();
    public bool canInteract;

    public List<GameObject> required = new();
    
    protected void Start()
    {
        MakeUninteractive();
    }


    private void Update()
    {

    }

    public virtual void OnBecameInteractive()
    {
        foreach (var item in required)
        {
            item.SetActive(true);
        }
    }

    public virtual void OnBecameUninteractive()
    {
        foreach (var item in required)
        {
            item.SetActive(false);
        }
    }



    public virtual void MakeInteractable()
    {
        canInteract = true;
        foreach (var item in interactable)
        {
            item.enabled = true;
        }
    }
    public virtual void MakeUninteractable()
    {
        canInteract = false;
        foreach (var item in interactable)
        {
            item.enabled = false;
        }
    }


    public virtual void MakeInteractive()
    {
        OnBecameInteractive();
    }
    public virtual void MakeUninteractive()
    {
        OnBecameUninteractive();
    }
}
