using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float intensity, shakingTime;
    public UniversalLook look;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        shakingTime -= Time.deltaTime;

        if (shakingTime <= 0) return;

        transform.Rotate(0, Random.value, 0);

        look._rotationX = -Random.value * intensity;


        look._rotationX = Mathf.Clamp(look._rotationX, look.mvert, look.vert);

        float rotationY = transform.localEulerAngles.y;

        transform.localEulerAngles = new Vector3(look._rotationX, rotationY, 0);
    }
}
