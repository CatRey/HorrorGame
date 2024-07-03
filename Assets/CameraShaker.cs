using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float intensity, shakingTime;
    public Vector3 direction;
    public UniversalLook look;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        shakingTime -= Time.deltaTime;

        if (shakingTime <= 0) return;

        transform.Rotate(0, (Random.value*2-1 + Vector3.Dot(transform.right, direction)) * intensity * Time.deltaTime, 0);

        look._rotationX -= (Random.value*2- 1 + direction.y) * intensity * Time.deltaTime;


        look._rotationX = Mathf.Clamp(look._rotationX, look.mvert, look.vert);

        float rotationY = transform.localEulerAngles.y;

        transform.localEulerAngles = new Vector3(look._rotationX, rotationY, 0);
    }
}
