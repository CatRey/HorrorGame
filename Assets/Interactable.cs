using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public KeyCode toInteract;
    public static Text interactIcon;
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public virtual void Interact()
    {

    }

    public void OnBecameInteractable()
    {
        interactIcon.text = toInteract.ToString();
    }
    
    public void OnBecameUninteractable()
    {
        interactIcon.text = "";
    }
}
