using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HandCursor : MonoBehaviour
{
    public Animator animator;
    public float pressTime, unpressTime, animationSpeed, canPressTime;
    float nowTime;
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
    public Vector3 relativeRotatingPosition;
    public RepeatingRotator3D wasRepeatingRotating;
    bool repeatingRotating;
    public Vector3 relativeRepeatingRotatingPosition;

    private void Start()
    {
        
    }


    private void Update()
    {
        bool canGrab = false;

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
                    canGrab = true;

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
                canGrab = true;

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

                    relativeRotatingPosition = rotate.rotatableObject.InverseTransformPoint(target);
                }
                if (rotating && Input.GetMouseButton(0))
                {
                    Vector3 newProjectedPosition = interactableHit.point;
                    var vector = newProjectedPosition - rotate.projectedPosition;
                    float angle = rotate.angleSign * Vector3.SignedAngle(rotate.wasVector, vector, Vector3.forward);

                    rotate.rotatableObject.eulerAngles += Vector3.forward * angle;
                    rotate.wasVector = vector;

                    target = rotate.rotatableObject.TransformPoint(relativeRotatingPosition);
                }
            }

            if (wasRotating && (component as Rotator3D) != wasRotating)
            {
                rotating = false;
            }
            wasRotating = component as Rotator3D;
            

            if (interactableHit.collider && interactableHit.collider.TryGetComponent(typeof(RepeatingRotator3D), out component))
            {
                canGrab = true;

                var rotate = component as RepeatingRotator3D;

                bool canRotate = repeatingRotating;

                if (!canRotate &&rotate.isSelected.overlayed)
                {
                    canRotate = true;
                }

                if (canRotate && Input.GetMouseButtonDown(0))
                {
                    Vector3 newProjectedPosition = interactableHit.point;
                    var vector = newProjectedPosition - rotate.projectedPosition;
                    rotate.wasVector = vector;
                    repeatingRotating = true;

                    relativeRepeatingRotatingPosition = rotate.rotating.InverseTransformPoint(target);
                }
                if (repeatingRotating && Input.GetMouseButton(0))
                {
                    Vector3 newProjectedPosition = interactableHit.point;
                    var vector = newProjectedPosition - rotate.projectedPosition;
                    float angle = rotate.angleSign * Vector3.SignedAngle(rotate.wasVector, vector, Vector3.forward);

                    rotate.rotating.eulerAngles += Vector3.forward * angle;
                    if (Mathf.Sign(angle) == rotate.angleDirection)
                    {
                        rotate.driving.eulerAngles += Vector3.forward * angle;
                        rotate.angleRotated += angle;
                        if (rotate.angleRotated * rotate.angleDirection >= rotate.angleToRotate)
                        {
                            rotate.MakeUninteractable();
                        }
                    }
                    rotate.wasVector = vector;

                    target = rotate.rotating.TransformPoint(relativeRepeatingRotatingPosition);
                }
            }

            if (wasRepeatingRotating && (component as RepeatingRotator3D) != wasRepeatingRotating)
            {
                repeatingRotating = false;
            }
            wasRepeatingRotating = component as RepeatingRotator3D;



            if (hit.collider.transform.parent && hit.collider.transform.parent.TryGetComponent(typeof(Eatable3D), out component))
            {
                canGrab = true;
            }
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


            if (wasRepeatingRotating)
            {
                repeatingRotating = false;
                wasRepeatingRotating = null;
            }
        }

        if (animator)
        {

            float targetValue = (Input.GetMouseButton(0) ? pressTime : (canGrab ? canPressTime : unpressTime));
            nowTime += Time.deltaTime * animationSpeed * Mathf.Sign(targetValue - nowTime);
            nowTime = Mathf.Clamp(nowTime, 0, pressTime);
            animator.SetFloat("play time", nowTime);
        }

        transform.position = target;
    }

    private void OnEnable()
    {
        timeDrawing = 0;
        transform.position -= Vector3.up * 100000;

        if (animator)
        {
            if (Input.GetMouseButton(0))
            {
                nowTime = pressTime;
                animator.SetFloat("play time", nowTime);
            }
        }
    }

    private void OnDisable()
    {
        drawn = false;
        rotating = false;
    }
}
