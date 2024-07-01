using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    int groundeds;
    public bool isGrounded
    {
        get => groundeds > 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        groundeds--;
    }

    private void OnTriggerStay(Collider other)
    {
        groundeds = 4;
    }

    private void OnTriggerExit(Collider other)
    {
        groundeds--;
    }
}
