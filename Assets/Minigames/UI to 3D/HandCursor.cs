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
    public Overlayable3D wasOverlaying;
    public Rotator3D wasRotating;
    bool rotating;
    public Vector3 relativePosition;

    private void Start()
    {
        
    }


    private void Update()
    {
        Vector3 target = transform.position;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var interactableHit, distanceLimit);
        if (Physics.Raycast(ray, out var hit, distanceLimit, walls))
        {
            target = hit.point + transform.TransformVector(offset);
        }
        else
        {
            target = (ray.origin + ray.direction * distanceLimit) + transform.TransformVector(offset);
        }

        if (!rotating) transform.position = Vector3.Lerp(transform.parent.position - transform.TransformVector(drawingFrom), target, (timeDrawing+=Time.deltaTime) / drawTime);

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



            if (hit.collider.TryGetComponent(typeof(Overlayable3D), out component))
            {
                var overlay = component as Overlayable3D;

                overlay.overlayed = true;
            }

            if (wasOverlaying && (component as Overlayable3D) != wasOverlaying)
            {
                wasOverlaying.overlayed = false;
            }
            wasOverlaying = component as Overlayable3D;



            if (interactableHit.collider && interactableHit.collider.TryGetComponent(typeof(Rotator3D), out component))
            {
                var rotate = component as Rotator3D;

                bool canRotate = rotating;

                if (!canRotate)
                {
                    foreach (var item in rotate.grabables)
                    {
                        if (item.overlayed)
                        {
                            canRotate = true;
                            break;
                        }
                    }
                }

                if (canRotate && Input.GetMouseButtonDown(0))
                {
                    Vector3 newProjectedPosition = interactableHit.point;
                    var vector = newProjectedPosition - rotate.projectedPosition;
                    rotate.wasVector = vector;
                    rotating = true;

                    relativePosition = rotate.rotatableObject.InverseTransformPoint(target);
                }
                if (rotating && Input.GetMouseButton(0))
                {
                    Vector3 newProjectedPosition = interactableHit.point;
                    var vector = newProjectedPosition - rotate.projectedPosition;
                    float angle = rotate.angleSign * Vector3.SignedAngle(rotate.wasVector, vector, Vector3.forward);

                    rotate.rotatableObject.eulerAngles += Vector3.forward * angle;
                    rotate.wasVector = vector;

                    target = rotate.rotatableObject.TransformPoint(relativePosition);
                }
            }

            if (wasRotating && (component as Rotator3D) != wasRotating)
            {
                rotating = false;
            }
            wasRotating = component as Rotator3D;

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

            if (wasOverlaying)
            {
                wasOverlaying.overlayed = false;
                wasOverlaying = null;
            }


            if (wasRotating)
            {
                rotating = false;
                wasRotating = null;
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
        rotating = false;
    }
}
