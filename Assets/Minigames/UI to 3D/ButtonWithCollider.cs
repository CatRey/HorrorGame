using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonWithCollider : MonoBehaviour
{
    public UnityEvent onPressed;
    public bool interactable;
    public bool isPressed;
    public bool canInteract
    {
        get => !moving && interactable;
    }
    public float moveTime;
    public Vector3 outsidePosition, pressedPosition, insidePosition;
    Vector3 wasInPosition;
    float timeMoving;
    public bool moving;
    public MoveState moveState;
    public bool holdAtInside;
    public float moveDirection = 1;
    
    private void Start()
    {
        
    }


    private void Update()
    {

        if (moving)
        {
            timeMoving += Time.deltaTime;

            switch (moveState)
            {
                case MoveState.movingToInside:
                    transform.localPosition = Vector3.Lerp(wasInPosition, insidePosition, timeMoving / moveTime);
                    break;
                case MoveState.movingToOutside:
                    transform.localPosition = Vector3.Lerp(wasInPosition, outsidePosition, timeMoving / moveTime);
                    break;
                case MoveState.movingToPressed:
                    transform.localPosition = Vector3.Lerp(wasInPosition, pressedPosition, timeMoving / moveTime);
                    break;
                default:
                    break;
            }

            if (timeMoving >= moveTime)
            {
                if (!holdAtInside && moveState == MoveState.movingToInside)
                {
                    moveState = isPressed ? MoveState.movingToPressed : MoveState.movingToOutside;
                    wasInPosition = transform.position;
                }
                else
                {
                    moving = false;
                }
            }
        }
    }

    public void Push()
    {
        isPressed = !isPressed;
        moveState = MoveState.movingToInside;
        moving = true;
        wasInPosition = transform.localPosition;
        moveDirection *= -1;
        timeMoving = 0;
    }

    public void Move(MoveState moveState)
    {
        this.moveState = moveState;
        moving = true;
        wasInPosition = transform.localPosition;
        moveDirection *= -1;
        timeMoving = 0;
    }

    public void Unmove()
    {
        moving = true;
        isPressed = false;
        moveState = MoveState.movingToOutside;
        wasInPosition = transform.localPosition;
        moveDirection = -1;
    }

    [System.Serializable]
    public enum MoveState
    {
        movingToInside,
        movingToOutside,
        movingToPressed
    }
}
