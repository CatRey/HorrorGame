using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public IsGrounded isGrounded;
    public new Rigidbody rigidbody;
    public float speed;
    public float JumpPower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        velocity.y = 0;
        velocity.Normalize();
        velocity *= speed;
        if (isGrounded.isGrounded && rigidbody.velocity.y <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = JumpPower;
        }
        else
        {
            velocity.y = rigidbody.velocity.y;
        }
        rigidbody.velocity = velocity;
    }

    private void OnDisable()
    {
        if (rigidbody) rigidbody.AddForceAtPosition(transform.right, rigidbody.transform.TransformPoint(Vector3.up * 0.5f), ForceMode.VelocityChange);
    }
}
