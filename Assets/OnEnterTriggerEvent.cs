using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnterTriggerEvent : MonoBehaviour
{
    public bool interactable = true, oneTime;
    public UnityEvent onTriggerEnter;
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!interactable) return;
        onTriggerEnter.Invoke();
        if (oneTime) interactable = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!interactable) return;
        onTriggerEnter.Invoke();
        if (oneTime) interactable = false;
    }
}
