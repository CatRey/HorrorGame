using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameRotating : Minigame
{
    public RectTransform rotating;
    public bool sendValues;
    public static float rotation;
    Vector3 previousVector;

    private void Start()
    {
        
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousVector = Input.mousePosition - rotating.position;
        }


        if (Input.GetMouseButton(0))
        {
            var nowVector = Input.mousePosition - rotating.position;

            rotating.eulerAngles += Vector3.forward * Vector3.SignedAngle(previousVector, nowVector, Vector3.forward);

            if (sendValues)
            {
                rotation = rotating.eulerAngles.z * Mathf.Deg2Rad;
            }

            previousVector = nowVector;
        }
        else
        {

        }

        base.Update();
    }
}
