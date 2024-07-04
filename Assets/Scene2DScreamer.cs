using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2DScreamer : MonoBehaviour
{
    public Transform player;
    public float speed;

    private void Start()
    {
        
    }


    private void Update()
    {
        transform.position += (player.position - transform.position).normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Scene2DController>(out var controller))
        {
            Destroy(gameObject);
            return;
        } 
        controller.OnCollisionEnter2D(new Collision2D());
        Destroy(gameObject);
    }
}
