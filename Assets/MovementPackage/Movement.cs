using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public IsGrounded isGrounded;
    public IsGrounded isInWater, headInWater;
    public GameObject headUnderwater;
    public new Rigidbody rigidbody;
    public float speed, inWaterSpeed;
    public float JumpPower, inWaterJumpAcceleration, inWaterDamp;

    public List<AudioClip> stepSounds = new();
    public AnimationCurve cameraLiftPerStep;
    public float stepPeiod;
    float timeStepping;
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

        if (isInWater.isGrounded)
        {
            rigidbody.drag = inWaterDamp;

            velocity *= inWaterSpeed;

            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = rigidbody.velocity.y;
                velocity.y += inWaterJumpAcceleration * Time.deltaTime;
            }
            else
            {
                velocity.y = rigidbody.velocity.y;
            }
        }
        else
        {
            rigidbody.drag = 0;

            velocity *= speed;

            if (isGrounded.isGrounded && rigidbody.velocity.y <= 0 && Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = JumpPower;
            }
            else
            {
                velocity.y = rigidbody.velocity.y;
            }
        }

        rigidbody.velocity = velocity;

        headUnderwater.SetActive(headInWater.isGrounded);
    }

    private void OnDisable()
    {
        if (rigidbody) rigidbody.AddForceAtPosition(transform.right, rigidbody.transform.TransformPoint(Vector3.up * 0.5f), ForceMode.VelocityChange);
    }
}
