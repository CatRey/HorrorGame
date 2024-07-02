using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MinigameRotating;
using static System.MathF;

public class Scene2DController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float speed;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        rigidbody.velocity = new Vector2(-Sin(rotation), Cos(rotation)) * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // BREAK
    }
}
