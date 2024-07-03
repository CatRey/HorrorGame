using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavingArm : MonoBehaviour
{
    public Vector2 up, right;
    Vector2 nowUp;
    public int direction;
    public Vector2 speedRange;
    public float speed;
    
    private void Start()
    {
        speed = Random.Range(speedRange.x, speedRange.y);
        nowUp = transform.up;
    }


    private void Update()
    {
        nowUp = Vector3.Lerp(nowUp, direction == 1 ? up : right, speed * Time.deltaTime);
        if (Vector3.Distance(nowUp, direction == 1 ? up : right) <= 0.01f)
        {
            direction *= -1;
        }
        transform.up = nowUp;
    }
}
