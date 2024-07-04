using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonWithCollider : MonoBehaviour
{
    public UnityEvent onPressed;
    public bool interactable;
    public bool canInteract
    {
        get => !moving && interactValue > 0;
    }
    public int interactValue;
    public float moveWay, moveTime;
    float timeMoving;
    public bool moving;
    public float moveDirection = 1;
    
    private void Start()
    {
        
    }


    private void Update()
    {
        interactValue--;

        if (moving)
        {
            transform.position += (moveWay / moveTime) * moveDirection * Time.deltaTime * transform.up;
            timeMoving += Time.deltaTime;
            if (timeMoving >= moveTime)
            {
                moving = false;
            }
        }
    }

    public void Move()
    {
        moving = true;
        moveDirection *= -1;
        timeMoving = 0;
    }

    public void Unmove()
    {
        if (moveDirection == 1) return;
        else if (moving)
        {
            moveDirection *= -1;
            timeMoving = moveTime - timeMoving;

        }
        else
        {
            timeMoving = 0;
        }
        moving = true;
        moveDirection *= -1;
    }
}
