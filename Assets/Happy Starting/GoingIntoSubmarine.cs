using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingIntoSubmarine : MonoBehaviour
{
    public float speed, limit;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        limit -= speed * Time.deltaTime;
        transform.position += Vector3.down * speed;
    }
}
