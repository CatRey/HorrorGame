using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boyancy : MonoBehaviour
{
    public float heightRange;
    float wentHeight, goDirection = 1;
    public float moveSpeed;
    public Vector3 angleRange;
    Vector3 wentAngles, toGoAngles, rotateDirection = Vector3.one;
    public float rotateSpeed;

    private void Start()
    {
        
    }


    private void Update()
    {
        wentHeight -= Time.deltaTime * moveSpeed;
        transform.position += Vector3.up * goDirection * moveSpeed * Time.deltaTime;
        if (wentHeight <= 0)
        {
            wentHeight = Mathf.Abs(transform.position.y) + Random.Range(0, heightRange);
            goDirection *= -1;
        }


        wentAngles -= rotateSpeed * Time.deltaTime * Vector3.one;
        transform.eulerAngles += rotateSpeed * Time.deltaTime * rotateDirection;
        if (wentAngles.x <= 0)
        {
            wentAngles.x = toGoAngles.x;
            toGoAngles.x = Random.Range(0, angleRange.x);
            wentAngles.x += toGoAngles.x;
            rotateDirection.x *= -1;
        }
        if (wentAngles.y <= 0)
        {
            wentAngles.y = toGoAngles.y;
            toGoAngles.y = Random.Range(0, angleRange.y);
            wentAngles.y += toGoAngles.y;
            rotateDirection.y *= -1;
        }
        if (wentAngles.z <= 0)
        {
            wentAngles.z = toGoAngles.z;
            toGoAngles.z = Random.Range(0, angleRange.z);
            wentAngles.z += toGoAngles.z;
            rotateDirection.z *= -1;
        }

    }
}
