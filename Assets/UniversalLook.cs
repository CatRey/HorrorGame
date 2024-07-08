using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalLook : MonoBehaviour
{
    public float mvert = -45.0f;
    public float vert = 45.0f;
    public float mhor = -Mathf.Infinity;
    public float hor = Mathf.Infinity;
    public float _rotationX;
    public string YMouse;
    public string XMouse;
    public float senitivityHor = 9f;
    public float senitivityVert = 9f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //поворот головы

        transform.Rotate(0, Input.GetAxis(XMouse) * senitivityHor, 0);

        _rotationX -= Input.GetAxis(YMouse) * senitivityVert;


        _rotationX = Mathf.Clamp(_rotationX, mvert, vert);

        float rotationY = transform.localEulerAngles.y;

        rotationY = ClampAngle(rotationY, mhor, hor);

        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        
    }

    public static float ClampAngle(float angle, float minAngle, float maxAngle)
    {
        if (angle > maxAngle && angle < minAngle && angle < 180)
        {
            return maxAngle;
        }
        else if (angle < minAngle && angle > maxAngle && angle > 180)
        {
            return minAngle;
        }
        else
        {
            return angle;
        }
    }

    private void OnDisable()
    {

        Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    private void OnEnable()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
