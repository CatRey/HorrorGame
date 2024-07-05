using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCursor : MonoBehaviour
{
    public Animator animator;
    public string pressAnimation;
    public float speed;
    public Vector3 offset, drawingFrom;
    public float distanceLimit;
    public LayerMask walls;
    public float drawTime;
    float timeDrawing;
    bool drawn;

    public ButtonWithCollider wasPressing;

    private void Start()
    {
        
    }


    private void Update()
    {
        Vector3 target = transform.position;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hit, distanceLimit, walls))
        {
            target = hit.point + transform.TransformVector(offset);
        }
        else
        {
            target = (ray.origin + ray.direction * distanceLimit) + transform.TransformVector(offset);
        }

        transform.position = Vector3.Lerp(transform.parent.position - transform.TransformVector(drawingFrom), target, (timeDrawing+=Time.deltaTime) / drawTime);

        if (!drawn)
        {
            drawn = Vector3.Distance(transform.position, target) <= 0.1f;
            return;
        }

        if (hit.collider)
        {

            if (hit.collider.TryGetComponent(typeof(ButtonWithCollider), out var component))
            {
                var button = component as ButtonWithCollider;

                if (button.interactable)
                {
                    if (Input.GetMouseButtonDown(0) || (wasPressing != button && Input.GetMouseButton(0)))
                    {
                        button.onPressed.Invoke();
                        button.Push();
                        button.holdAtInside = true;
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        button.holdAtInside = false;
                        if (!button.moving)
                        {
                            button.Move(button.isPressed ? ButtonWithCollider.MoveState.movingToPressed : ButtonWithCollider.MoveState.movingToOutside);
                        }
                    }
                }
            }
            if (wasPressing && (component as ButtonWithCollider) != wasPressing)
            {
                wasPressing.holdAtInside = false;
                if (!wasPressing.moving)
                {
                    wasPressing.Move(wasPressing.isPressed ? ButtonWithCollider.MoveState.movingToPressed : ButtonWithCollider.MoveState.movingToOutside);
                }
            }

            wasPressing = component as ButtonWithCollider;

        }
        else
        {
            if (wasPressing)
            {
                wasPressing.holdAtInside = false;
                if (!wasPressing.moving)
                {
                    wasPressing.Move(wasPressing.isPressed ? ButtonWithCollider.MoveState.movingToPressed : ButtonWithCollider.MoveState.movingToOutside);
                }
                wasPressing = null;
            }
        }

        transform.position = target;
    }

    private void OnEnable()
    {
        timeDrawing = 0;
        transform.position -= Vector3.up * 100000;
    }

    private void OnDisable()
    {
        drawn = false;
    }
}
