using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnterTriggerEvent : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter.Invoke();
    }
}
